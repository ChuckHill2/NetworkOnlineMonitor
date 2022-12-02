using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChuckHill2.Forms;
using NetworkOnlineMonitor.Properties;

// Note: Identifiers for all controls on all forms consist of m_[2-3 letter control type][camel-case field name] 
// This makes it much easier to to identify UI controls distinct from other methods and objects. This helps tremendously when using intellisense.

namespace NetworkOnlineMonitor
{
    public partial class FormMain : Form
    {
        private Settings settings;        //Current properties saved in XML. config.
        private CaptionBarCloseButton CloseButtonHandler; //Reassign the close button to minimize app to the system tray.
        private Task MonitorTask;         //Handle to wait for task exit
        private Target[] Targets;         //List of ping targets where index 0 is the LAN gateway address.
        private FileLogging Log;         //Log the monitored events.
        private bool CancelWork = false;  //We use 2 forms of cancellation: a bool flag and cancel token, maybe overkill?
        private CancellationTokenSource CancelTokenSource;
        private CancellationToken CancelToken;
        private Thread MonitorThread;     //Keep thread for MonitorStop() just in case Monitor() refuses to stop. Monitor() set's this.
        private long LogFaultStart = 0;   //needs to be global to support tray tooltip. Set and used in Monitor() and used in m_TrayIcon.MouseMove.

        private readonly List<long> DownTimeList = new List<long>();

        public FormMain()
        {
            InitializeComponent();

            // Interesting replacement to Control.Invoke() ??
            // private WindowsFormsSynchronizationContext Context = new WindowsFormsSynchronizationContext();
            // Context.Send((f) => this.Text = "HelloWorld", null);

            settings = Settings.Deserialize();

            if (settings.MainFormLocation == Point.Empty) this.StartPosition = FormStartPosition.CenterScreen;
            else
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = settings.MainFormLocation;
            }

            if (settings.StartMinimized)
            {
                Hide();
                this.WindowState = FormWindowState.Minimized;
            }

            CloseButtonHandler = new CaptionBarCloseButton(this, () => 
            {
                this.Hide();
                this.WindowState = FormWindowState.Minimized;
            }, "Minimize to\nSystem Tray.");
            CloseButtonHandler.UpdateSysMenuCloseToMinimize();

            this.FormClosing += (s, e) =>
            {
                LogWriteFailureSummary();
                StopMonitor();
            };
            this.FormClosed += (s, e) => 
            { 
                m_ctxTray.Visible = false;
                CloseButtonHandler.Dispose();
                Application.Exit();
            };
            m_TrayIcon.MouseMove += (s, e) =>
            {
                if (!m_grpPingTests.Enabled) m_TrayIcon.Text = "Network Monitoring\r\nStopped";
                else if (LogFaultStart == 0) m_TrayIcon.Text = "Network Monitor\r\nDuration " + TimeSpanFormat(StaticTools.UtcTimerTicks - MonitorDurationStartTicks);
                else m_TrayIcon.Text = "Network Down\r\nDuration " + TimeSpanFormat(StaticTools.UtcTimerTicks - LogFaultStart);
            };
        }

        protected override void OnShown(EventArgs e)
        {
            if (settings.StartMinimized)
            {
                Hide();
                this.WindowState = FormWindowState.Minimized;
            }
            base.OnShown(e);

            if (settings.LogFileOption == LogFileOption.None) Log = new FileLogging(); //empty logger / no-op
            else if (settings.LogFileOption == LogFileOption.CreateNew) Log = new FileLogging(settings.LogFilePath);
            else if (settings.LogFileOption == LogFileOption.Append) Log = new FileLogging(settings.LogFilePath, true);
            LogWriteSettingsInfo();

            StartMonitor();
            m_txtMonitorStarted.Text = DateTime.Now.ToString("G");
        }

        private void m_tsExitMenuItem_Click(object sender, EventArgs e) => this.Close();

        private void m_tsSettingsMenuItem_Click(object sender, EventArgs e)
        {
            var newSettings = FormSettings.Show(this, settings);
            if (newSettings != null)
            {
                bool doLogWriteNewSettings = !settings.Targets.SequenceEqual(newSettings.Targets)
                    || settings.TestInterval != newSettings.TestInterval
                    || settings.OfflineTrigger != newSettings.OfflineTrigger
                    || settings.PingTimeout != newSettings.PingTimeout;

                bool doRestartLog = settings.LogFileOption != newSettings.LogFileOption
                    || !settings.LogFileFolder.Equals(newSettings.LogFileFolder, StringComparison.CurrentCultureIgnoreCase);

                bool doRestartMonitor = doLogWriteNewSettings || doRestartLog
                    || settings.PopUpOnFailure != newSettings.PopUpOnFailure; //because we need to update this on the "Settings" groupbox.

                settings = newSettings; //monitor uses these settings, so set before starting monitor.
                settings.Serialize();

                if (doRestartMonitor)
                {
                    StopMonitor();
                    if (doRestartLog)
                    {
                        //changing logging.
                        LogWriteFailureSummary();
                        Log.Dispose();
                        if (settings.LogFileOption == LogFileOption.None) Log = new FileLogging();
                        else if (settings.LogFileOption == LogFileOption.CreateNew) Log = new FileLogging(settings.LogFilePath);
                        else if (settings.LogFileOption == LogFileOption.Append) Log = new FileLogging(settings.LogFilePath, true);
                        doLogWriteNewSettings = true;
                    }

                    StartMonitor();
                }

                if (doLogWriteNewSettings) LogWriteSettingsInfo();
            }
        }

        private void m_tsEditLogMenuItem_Click(object sender, EventArgs e)
        {
            if (settings.LogFileOption==LogFileOption.None)
            {
                MiniMessageBox.ShowDialog(this, "Logging has been disabled. See Settings to re-enable.", "Logging History", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var logFile = Log?.OutputFile;
            if (string.IsNullOrEmpty(logFile))  //This should never happen!
            {
                Debug.WriteLine($"ERROR: m_tsHistoryMenuItem_Click: {nameof(FileLogging)}.{nameof(FileLogging.OutputFile)} is NULL! This should never happen!");
                return;
            }

            Log.LogEdit();

            //// Hack to use my favorite editor instead of ugly windows notepad.exe
            //var viewer = @"C:\Program Files\Notepad++\notepad++.exe";
            //if (!File.Exists(viewer)) viewer = @"C:\Program Files (x86)\Notepad++\notepad++.exe";
            //if (!File.Exists(viewer)) viewer = null; //use the users default text editor.

            //if (viewer == null) Process.Start(logFile);
            //else Process.Start(new ProcessStartInfo(viewer, $" -nosession -n2147483647 \"{logFile}\""));
        }

        private void m_tsAboutMenuItem_Click(object sender, EventArgs e) => FormAbout.Show(this);

        private void m_ctxTrayMenuOpen_Click(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }
        private void m_ctxTrayMenuItemExit_Click(object sender, EventArgs e) => this.Close();

        private void m_TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Show();
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                Hide();
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void m_TrayIcon_Click(object sender, EventArgs e)
        {
            //https://stackoverflow.com/questions/2208690/invoke-notifyicons-context-menu
            var mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(m_TrayIcon, null);
        }

        private void StopMonitor()
        {
            Debug.WriteLine($"StopMonitor() Started");
            CancelWork = true;
            CancelTokenSource?.Cancel();

            if (MonitorTask != null && !MonitorTask.IsCompleted)
            {
                var ok = MonitorTask.Wait(settings.TestInterval*1000);
                if (!ok) 
                {
                    Debug.WriteLine($"StopMonitor via CancelWork failed after {settings.TestInterval} seconds"); 
                    MonitorThread?.Abort(); //We corrupt the Task with this so the only thing we can do is Dispose it.
                    ok = MonitorThread.Join(5000);  //However we can still use Thread API.
                }
                if (!ok) Debug.WriteLine("StopMonitor Thread Abort failed after 5 seconds");
            }
            MonitorTask?.Dispose();
            MonitorTask = null;
            CancelTokenSource?.Dispose();
            CancelTokenSource = null;
            UpdateUI(UIState.Stop);
            Debug.WriteLine($"StopMonitor() Ended");
        }

        private void StartMonitor()
        {
            Debug.WriteLine($"StartMonitor() Started");
            CancelWork = false;
            CancelTokenSource = new CancellationTokenSource();
            CancelToken = CancelTokenSource.Token;
            Targets = BuildTargets();

            UpdateUI(UIState.Start);

            Task.Run(() => MonitorDuration(), CancelToken);
            MonitorTask = Task.Run((Action)Monitor, CancelToken);
            Debug.WriteLine($"StartMonitor() Ended");
        }

        private void Monitor()
        {
            Debug.WriteLine($"Monitor Started");
            MonitorThread = Thread.CurrentThread; //save this thread in case StopMonitor() needs to forceably abort this thread.
            bool faulted = false;
            long loopStart = 0;
            Target[] wanTargets = Targets.Skip(1).ToArray(); //skip the Lan target. All following targets are the WAN.
            int testInterval = settings.TestInterval * 1000; //pre-convert to ms
            long offlineTrigger = settings.OfflineTrigger * TimeSpan.TicksPerSecond; //compute to ticks

            LogFaultStart = 0;
            var ping = new PingTest(settings.PingTimeout);
            int targetIndex = 0;
            while (!CancelWork)
            {
                if (loopStart != 0)
                {
                    var pingDuration = (int)((StaticTools.UtcTimerTicks - loopStart) / TimeSpan.TicksPerMillisecond); //this works because our Ping is synchronous.
                    var timeout = ping.Success ? testInterval - pingDuration : 500+settings.PingTimeout - pingDuration; //test more quickly if ping failed.
                    if (timeout < 0) timeout = 0;
                    if (timeout > testInterval) timeout = testInterval;
                    Debug.WriteLine($"Monitor Pinging {wanTargets[targetIndex]} after {timeout}ms: Previous Duration={pingDuration}ms, TestInterval={testInterval}ms");
                    SpinWait.SpinUntil(() => CancelWork, timeout);
                    if (CancelWork) break;
                }
                else Debug.WriteLine($"Monitor Pinging {wanTargets[targetIndex]}: Starting ping immediately");

                loopStart = StaticTools.UtcTimerTicks;

                var t = wanTargets[targetIndex++ % wanTargets.Length];
                if (targetIndex == wanTargets.Length) targetIndex = 0;

                CallForm(() => t.Icon = Resources.SphereYellow24); //Blink yellow to show that we are in progress starting a ping.
                SpinWait.SpinUntil(() => CancelWork, 100);

                if (ping.Send(t.Address))
                {
                    if (faulted) //We recovered from offline condition 
                    {
                        LogNetworkDown(LogFaultStart, Targets[0].Response); //Now that we know how long the network was down, we write out outstanding network down message.
                        UpdateUI(UIState.Online);
                        faulted = false;
                    }
                    LogFaultStart = 0;  //ping successful. stop the fault countdown.
                    CallForm(() => //set success green ball after yellow
                    {
                        t.Icon = Resources.SphereGreen24;
                        t.Response = ping.ResponseTime;
                    });
                }
                else
                {
                    CallForm(() => //set failure red ball after yellow
                    {
                        t.Icon = Resources.SphereRed24;
                        t.Response = ping.ResponseTime;
                    });
                    if (LogFaultStart == 0) //ping failed, start counting.
                    {
                        LogFaultStart = StaticTools.UtcTimerTicks;
                    }

                    long faultDuration = StaticTools.UtcTimerTicks - LogFaultStart;
                    if (faultDuration < offlineTrigger) continue; //we wait until we *really* want to notify the user that we are down. Want to ignore downtime transients.
                    if (!faulted) 
                    {
                        faulted = true;
                        UpdateUI(UIState.Offline, ping); //need to ping LAN to verify if it is also down and adjust the LAN icons accordingly,
                    }
                }
            }

            if (LogFaultStart != 0)
            {
                //flush any outstanding network down messages.
                LogNetworkDown(LogFaultStart, Targets[0].Response);
                UpdateUILastFault();
            }
            ping.Dispose();
            Debug.WriteLine($"Monitor Stopped");
        }

        private enum UIState { Stop, Start , Online, Offline }
        private void UpdateUI(UIState uiState, PingTest ping=null)
        {
            Debug.WriteLine($"UpdateUI: {uiState}");
            if (uiState == UIState.Start)
            {
                CallForm(() =>
                {
                    m_grpPingTests.Enabled = true;
                    m_grpResults.Enabled = true;
                    m_grpSettings.Enabled = true;
                    m_grpStatus.Enabled = true;

                    m_txtCurrentFailDuration.Text = "00:00:00";
                    m_lblCurrentFailDuration.Visible = false;
                    m_txtCurrentFailDuration.Visible = false;

                    m_pbWanStatus.Image = Resources.WanUp64;
                    m_txtWanStatus.Text = Resources.WanUpText;
                    m_pbLanStatus.Image = Resources.LanUp64;
                    m_txtLanStatus.Text = Resources.LanUpText;

                    m_txtPopupOnFailure.Text = settings.PopUpOnFailure ? "Yes" : "No";
                    m_txtLogging.Text = settings.LogFileOption != LogFileOption.None ? "Yes" : "No";
                    m_txtPingInterval.Text = settings.TestInterval.ToString() + " seconds";
                    m_txtOfflineTrigger.Text = settings.OfflineTrigger.ToString() + " seconds";
                    m_txtPingTimeout.Text = settings.PingTimeout.ToString() + " ms";
                });
                return;
            }

            if (uiState == UIState.Stop)
            {
                StopCurrentFailUICounter();
                CallForm(() =>
                {
                    m_grpPingTests.Enabled = false;
                    m_grpResults.Enabled = false;
                    m_grpSettings.Enabled = false;
                    m_grpStatus.Enabled = false;

                    m_txtCurrentFailDuration.Text = "00:00:00";
                    m_lblCurrentFailDuration.Visible = false;
                    m_txtCurrentFailDuration.Visible = false;

                    m_pbWanStatus.Image = Resources.WanDisabled64;
                    m_txtWanStatus.Text = Resources.WanUpText;
                    m_pbLanStatus.Image = Resources.LanDisabled64;
                    m_txtLanStatus.Text = Resources.LanUpText;

                    m_pbTestTarget1.Image = global::NetworkOnlineMonitor.Properties.Resources.SphereGray24;
                    m_pbTestTarget2.Image = global::NetworkOnlineMonitor.Properties.Resources.SphereGray24;
                    m_pbTestTarget3.Image = global::NetworkOnlineMonitor.Properties.Resources.SphereGray24;

                    this.BackColor = SystemColors.Control;
                    this.m_grpPingTests.BorderColor = Color.LightGray;
                    this.m_grpResults.BorderColor = Color.LightGray;
                    this.m_grpSettings.BorderColor = Color.LightGray;
                    this.m_grpStatus.BorderColor = Color.LightGray;
                    this.m_MenuStrip.BackColor = Color.Gainsboro;

                    m_TrayIcon.Icon = Resources.favicon;
                });
                return;
            }

            if (uiState == UIState.Online)
            {
                StopCurrentFailUICounter();
                CallForm(() =>
                {
                    m_grpPingTests.Enabled = true;
                    m_grpResults.Enabled = true;
                    m_grpSettings.Enabled = true;
                    m_grpStatus.Enabled = true;

                    PlayReconnectSound();

                    m_pbWanStatus.Image = Resources.WanUp64;
                    m_txtWanStatus.Text = Resources.WanUpText;
                    m_pbLanStatus.Image = Resources.LanUp64;
                    m_txtLanStatus.Text = Resources.LanUpText;

                    this.BackColor = SystemColors.Control;
                    this.m_grpPingTests.BorderColor = Color.LightGray;
                    this.m_grpResults.BorderColor = Color.LightGray;
                    this.m_grpSettings.BorderColor = Color.LightGray;
                    this.m_grpStatus.BorderColor = Color.LightGray;
                    this.m_MenuStrip.BackColor = Color.Gainsboro;

                    m_TrayIcon.Icon = Resources.favicon;

                    UpdateUILastFault();
                });
                return;
            }

            if (uiState == UIState.Offline)
            {
                CallForm(() =>
                {
                    if (settings.PopUpOnFailure)
                    {
                        Show();
                        this.WindowState = FormWindowState.Normal;
                    }
                    m_grpPingTests.Enabled = true;
                    m_grpResults.Enabled = true;
                    m_grpSettings.Enabled = true;
                    m_grpStatus.Enabled = true;

                    PlayAlertSound();

                    m_pbWanStatus.Image = Resources.WanDown64;
                    m_txtWanStatus.Text = Resources.WanDownText;

                    this.BackColor = Color.MistyRose;
                    var highlight = Color.LightPink;
                    this.m_grpPingTests.BorderColor = highlight;
                    this.m_grpResults.BorderColor = highlight;
                    this.m_grpSettings.BorderColor = highlight;
                    this.m_grpStatus.BorderColor = highlight;
                    this.m_MenuStrip.BackColor = Color.FromArgb(255, 255, 209, 204);

                    m_TrayIcon.Icon = Resources.favicon_Red;
                });

                if (!ping.Send(Targets[0].Address, 100))
                {
                    CallForm(() =>
                    {
                        m_pbLanStatus.Image = Resources.LanDown64;
                        m_txtLanStatus.Text = Resources.LanDownText;
                    });
                }
                Targets[0].Response = ping.ResponseTime;

                Debug.WriteLine($"UpdateUI: {uiState} LAN Ping {(ping.Success ? "Success" : "Failed")}" + (ping.Success ? $" Response={ping.ResponseTime}ms" : ""));

                Task.Run(() => StartCurrentFailUICounter(), CancelToken);
                return;
            }
        }

        private void UpdateUILastFault()
        {
            if (LogFaultStart <= 0) return;
            CallForm(() =>
            {
                m_txtLastFailureStart.Text = new DateTime(LogFaultStart, DateTimeKind.Utc).ToLocalTime().ToString("G");
                DownTimeList.Add(StaticTools.UtcTimerTicks - LogFaultStart); //Add to tick downtime duration array
                m_txtLastFailDuration.Text = TimeSpanFormat(DownTimeList[DownTimeList.Count - 1]);
                m_txtFailureCount.Text = DownTimeList.Count.ToString();
            });
        }

        /// <summary>
        /// Invoke an action in the context of form's message pump thread.
        /// Action does not get executed if the the window is minimized unless it is forced.
        /// </summary>
        /// <param name="command">Action to perform.</param>
        /// <param name="force">True to force the action in spite that this may be a minimized window.</param>
        private void CallForm(Action command, bool force = true) //Invoke takes a delegate. Pre-cast to Action type
        {
            if (force || this.WindowState != FormWindowState.Minimized)
            {
                if (this.InvokeRequired)
                {
                    //Hide ThreadAbortException and ObjectDisposedException. Occurs when application exiting.
                    try { this.Invoke(command); } catch { }
                }
                else
                {
                    command();
                }
            }
        }

        private void PlayAlertSound()
        {
            if (string.IsNullOrEmpty(settings?.AlertSoundClip?.FileName) || !File.Exists(settings.AlertSoundClip.FileName)) return;
            Sound.Play(settings.AlertSoundClip.FileName, settings.AlertSoundClip.Volume);
        }
        private void PlayReconnectSound()
        {
            if (string.IsNullOrEmpty(settings?.ReconnectSoundClip?.FileName) || !File.Exists(settings.ReconnectSoundClip.FileName)) return;
            Sound.Play(settings.ReconnectSoundClip.FileName, settings.ReconnectSoundClip.Volume);
        }

        private bool _CancelCurrentFailUICounter = false;
        private void StopCurrentFailUICounter() => _CancelCurrentFailUICounter = true;
        private void StartCurrentFailUICounter()
        {
            _CancelCurrentFailUICounter = false;
            Debug.WriteLine("CurrentFailDuration Started");

            var started = StaticTools.UtcTimerTicks;
            if (!CancelWork && !_CancelCurrentFailUICounter)
            {
                CallForm(() =>
                {
                    m_lblCurrentFailDuration.Visible = true;
                    m_txtCurrentFailDuration.Visible = true;
                });
            }

            while (true)
            {
                if (CancelWork || _CancelCurrentFailUICounter)
                {
                    CallForm(() =>
                    {
                        m_lblCurrentFailDuration.Visible = false;
                        m_txtCurrentFailDuration.Visible = false;
                        m_txtCurrentFailDuration.Text = "00:00:00";
                    });
                    Debug.WriteLine("CurrentFailDuration Ended");
                    return;
                }
                CallForm(() => m_txtCurrentFailDuration.Text = TimeSpanFormat(StaticTools.UtcTimerTicks - started), false);
                SpinWait.SpinUntil(() => CancelWork, 1000);
            }
        }

        private long MonitorDurationStartTicks = -1;
        private void MonitorDuration()
        {
            Debug.WriteLine("MonitorDuration Started");

            long started;

            //Exclude time when monitoring was stopped.
            if (MonitorDurationStartTicks == -1)
            {
                started = StaticTools.UtcTimerTicks;
                MonitorDurationStartTicks = started;
            }
            else
            {
                started = StaticTools.UtcTimerTicks - MonitorDurationStartTicks;
                MonitorDurationStartTicks = started;
            }

            while (!CancelWork)
            {
                CallForm(() => m_txtMonitorDuration.Text = TimeSpanFormat(StaticTools.UtcTimerTicks - started), false);
                SpinWait.SpinUntil(() => CancelWork, 1000);
            }
            Debug.WriteLine("MonitorDuration Ended");
        }

        //This is written at the beginning of the log and every time one of these Settings properties has changed.
        private void LogWriteSettingsInfo()
        {
            int i = 0;
            Log.WriteSoftDivider();
            var sb = new StringBuilder();
            Array.ForEach(settings.Targets, t => sb.AppendLine($"Ping Target {++i}: {t.AddressStr} - {t.Name}"));
            sb.Append("Wait for Ping (milliseconds): "); sb.AppendLine(settings.PingTimeout.ToString());
            sb.Append("Test Interval (seconds): "); sb.AppendLine(settings.TestInterval.ToString());
            sb.Append("Log Failure Longer Than (seconds): "); sb.Append(settings.OfflineTrigger.ToString());
            Log.WriteLine(sb.ToString());
            Log.WriteSoftDivider();
        }

        //This is written when the network is back up after a failure. Done this way in order to get the failure duration.
        private void LogNetworkDown(long faultStartTimeTicks, int lanResponseTimeMs)
        {
            if (faultStartTimeTicks <= 0) throw new ArgumentOutOfRangeException(nameof(faultStartTimeTicks), faultStartTimeTicks, $"{nameof(faultStartTimeTicks)} is <= 0");
            string duration = TimeSpanFormat(TimeSpan.FromTicks(StaticTools.UtcTimerTicks - faultStartTimeTicks));
            DateTime startDate = new DateTime(faultStartTimeTicks,DateTimeKind.Utc).ToLocalTime();

            if (lanResponseTimeMs != PingTest.ResponseTimeout)
                Log.WriteLine($"WAN Failed - {startDate:G}, Duration={duration}, LAN responded in {lanResponseTimeMs} ms");
            else
                Log.WriteLine($"LAN Failed - {startDate:G}, Duration={duration}");
        }

        //This is the last item in the Log before it is closed.
        private void LogWriteFailureSummary()
        {
            DateTime now = DateTime.Now;
            var szMonitoredDuration = m_txtMonitorDuration.Text;
            TimeSpan monitoredDuration = TimeSpanParse(szMonitoredDuration);
            Log.WriteSoftDivider();
            var sb = new StringBuilder();
            sb.AppendLine($"Log End {now:G}");
            sb.AppendLine($"Monitor Duration {szMonitoredDuration}");

            if (DownTimeList.Count > 0)
            {
                var downtimeList = DownTimeList.OrderBy(m=>m).ToList();

                sb.AppendLine($"Failure Summary:");
                sb.AppendLine($"  Count          {downtimeList.Count}");
                sb.AppendLine($"  Total Downtime {TimeSpanFormat(downtimeList.Sum())}");
                sb.AppendLine($"  % Downtime     {(downtimeList.Sum() / (double)monitoredDuration.Ticks).ToString("0.00%")}");
                sb.AppendLine($"  Minimum Length {TimeSpanFormat(downtimeList.Min())}");
                sb.AppendLine($"  Maximum Length {TimeSpanFormat(downtimeList.Max())}");
                sb.AppendLine($"  Average Length {TimeSpanFormat((long)downtimeList.Average())}");
                var median = downtimeList.Count % 2 == 0 ? (downtimeList[downtimeList.Count / 2] + downtimeList[downtimeList.Count / 2 - 1]) / 2 : downtimeList[downtimeList.Count / 2];
                sb.Append($"  Median Length  {TimeSpanFormat(median)}");
            }
            else
                sb.Append($"(No Failures)");

            Log.WriteLine(sb.ToString());
            Log.WriteSoftDivider();
        }

        private static string TimeSpanFormat(TimeSpan tsp) => $"{(int)tsp.TotalHours:#0}:{tsp.Minutes:00}:{tsp.Seconds:00}";
        private static string TimeSpanFormat(long ticks) => TimeSpanFormat(new TimeSpan(ticks));
        private static TimeSpan TimeSpanParse(string s)
        {
            //TimeSpan.TryParse() does not work because it throws exceptions if 'hours' field exceeds 23 hrs.
            //This does not support days or milliseconds - good enough for us.
            var hms = s.Split(':').Select(m => int.TryParse(m, out var i) ? i : 0).ToList();
            if (hms.Count < 1 || hms.Count > 3) return TimeSpan.Zero;
            return new TimeSpan(0, hms[0], hms.Count>1 ? hms[1]: 0, hms.Count > 2 ? hms[2] : 0);
        }

        private Target[] BuildTargets()
        {
            var lanInfo = StaticTools.GetLanInfo();
            return new Target[]
            {
                //By our definition, the first target element is always the LAN.
                new Target("Gateway",IPAddress.Parse(lanInfo.GatewayAddess),null,null,null,100),
                new Target(settings.Targets[0].Name, settings.Targets[0].Address, m_pbTestTarget1, m_lblTestTarget1, m_lblTestTarget1Resp, settings.PingTimeout),
                new Target(settings.Targets[1].Name, settings.Targets[1].Address, m_pbTestTarget2, m_lblTestTarget2, m_lblTestTarget2Resp, settings.PingTimeout),
                new Target(settings.Targets[2].Name, settings.Targets[2].Address, m_pbTestTarget3, m_lblTestTarget3, m_lblTestTarget3Resp, settings.PingTimeout),
            };
        }

        private class Target
        {
            public IPAddress Address { get; private set; }
            public string Name
            {
                get => _Name;
                private set 
                {
                    _Name = value ?? string.Empty;
                    if (NameControl != null)
                    {
                        NameControl.Text = _Name;
                        NameControl.Invalidate();
                    }
                }
            }
            public Image Icon
            {
                get => _Icon;
                set 
                {
                    _Icon = value;
                    if (IconControl != null)
                    {
                        IconControl.Image = value;
                        IconControl.Invalidate();
                    }
                }
            }
            public int Response
            {
                get => _Response;
                set 
                {
                    _Response = value;
                    if (ResponseControl != null)
                    {
                        ResponseControl.Text = value == PingTest.ResponseTimeout ? $">{Timeout}" : value.ToString();
                        ResponseControl.Invalidate();
                    }
                }
            }

            private readonly int Timeout = 200; //to provide context for ResponseControl.Text
            private string _Name = string.Empty;
            private Image _Icon = null;
            private int _Response = PingTest.ResponseTimeout;

            private readonly Label NameControl;
            private readonly PictureBox IconControl;
            private readonly Label ResponseControl;

            private readonly string toStringValue = string.Empty;

            private Target() { } //must provide parameters
            public Target(string name, IPAddress address, PictureBox iconCtrl, Label nameCtrl, Label responseCtrl, int timeout)
            {
                NameControl = nameCtrl;
                IconControl = iconCtrl;
                ResponseControl = responseCtrl;
                Timeout = timeout;
                Icon = global::NetworkOnlineMonitor.Properties.Resources.SphereGray24;
                Response = 0;
                Address = address ?? IPAddress.Loopback;

                var addr = Address.ToString();
                name = name ?? "NONAME";
                Name = $"{name}\n{addr}";
                toStringValue = $"{name} ({addr})"; //pre-computed.
            }

            public override string ToString() => toStringValue;
        }

        private class PingTest : IDisposable
        {
            public const int ResponseTimeout = -1;

            /// <summary>The default timeout to use in the ping.</summary>
            public readonly int Timeout;
            /// <summary>The last IP address pinged.</summary>
            public IPAddress Address;
            /// <summary>The last response time or 'ResponseTimeout' upon error.</summary>
            public int ResponseTime;
            /// <summary>The last response status.</summary>
            public bool Success;

            private Ping Pinger = new Ping();
            private PingOptions pingOptions = new PingOptions() { DontFragment = true, Ttl = 128 };
            private readonly byte[] PingBuf = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

            public PingTest(int timeout = 200)
            {
                if (timeout <= 0) timeout = 200;
                Timeout = timeout;
            }

            public bool IsDisposed => Pinger == null;
            public void Dispose()
            {
                Pinger?.Dispose();
                Pinger = null;
            }

            /// <summary>
            /// Perform the ping.
            /// </summary>
            /// <param name="addr">IP address to ping. Also sets the 'Address' property.</param>
            /// <param name="timeout">Override the default timeout.</param>
            /// <returns>True if ping successful. Also sets the 'Success' property.</returns>
            public bool Send(IPAddress addr, int timeout=0)
            {
                try
                {
                    if (timeout <= 0) timeout = Timeout;
                    Address = addr;
                    PingReply pingReply = Pinger.Send(addr, timeout, PingBuf, pingOptions);
                    Success = pingReply.Status == IPStatus.Success;
                    ResponseTime = Success ? checked((int)pingReply.RoundtripTime) : PingTest.ResponseTimeout;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{nameof(PingTest)}.{nameof(Ping)}: {ex.GetType().Name}:{ex.Message}");
                    Success = false;
                    ResponseTime = PingTest.ResponseTimeout;
                }
                return Success;
            }
        }
    }
}
