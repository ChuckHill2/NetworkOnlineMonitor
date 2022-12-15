using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ChuckHill2.Forms;

namespace NetworkOnlineMonitor
{
    /// <summary>
    /// Perform Event logging. May be used in a multi-threaded environment.
    /// </summary>
    [Serializable]
    public class FileLogging: MarshalByRefObject, IDisposable
    {
        private readonly object StreamLock = new object();
        private const int MaxLogSize = 1024 * 1024 * 100; //100MB
        private bool WroteSoftDivider = true; //flag to disallow multiple SoftDividers to be written consecutively.
        private StreamWriter stream = null;
        private int HeaderLength = 0;
        private FormLogEditor EditDialog = null;

        public readonly string OutputFile;
        public readonly bool Append;

        /// <summary>
        /// Creates a free-form diary-like plain text log file.
        /// </summary>
        /// <param name="outputFile">name of log file to write to or null to create a dummy log that writes to the bit-bucket</param>
        /// <param name="append">True to append to an existing file. False (the default) to open a new file overwriting any existing file.</param>
        public FileLogging(string outputFile=null, bool append=false)
        {
            OutputFile = outputFile;
            Append = append;

            if (string.IsNullOrWhiteSpace(outputFile)) return;

            //If file is too big, rename the old one and start a new one.
            var fi = new FileInfo(outputFile);
            if (fi.Exists && fi.Length > MaxLogSize)
            {
                var dt = fi.CreationTime;
                var fn = string.Concat(Path.GetDirectoryName(outputFile), "\\", Path.GetFileNameWithoutExtension(outputFile)," ", fi.CreationTime.ToString("yyyy-MM-dd HH.mm.ss"), Path.GetExtension(outputFile));
                if (File.Exists(fn)) File.Delete(fn);
                File.Move(outputFile, fn);
            }

            //Use FileShare.ReadWrite instead of just FileShare.Read for external editing support.
            FileStream fs;
            if (append) fs = new FileStream(outputFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite, 4096, FileOptions.SequentialScan);
            else fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite, 4096, FileOptions.SequentialScan);
            //LeaveOpen==false, We want StreamWriter to close our base stream as well.
            stream = new StreamWriter(fs, System.Text.Encoding.UTF8, 1024, leaveOpen:false);

            //Write the starting hard divider with centered text
            var msg = $"= {Path.GetFileNameWithoutExtension(StaticTools.ExecutableName)} Log {DateTime.Now.ToString("G")} =";
            int pad = 78 - msg.Length;
            if (pad < 0) pad = 0;
            int leftpad = pad / 2;
            int rightpad = leftpad + (pad % 2);
            msg = new string('=', leftpad) + msg + new string('=', rightpad);
            HeaderLength = msg.Length;
            if (fs.Length > 0) msg = Environment.NewLine + msg;
            stream.WriteLine(msg);
            stream.Flush();
            stream.BaseStream.FlushAsync();

            //Handle if something bad happens.
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DisposeLog;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_DisposeLog;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_DisposeLog;
        }

        private void CurrentDomain_DisposeLog(object sender, EventArgs e) => Dispose();

        /// <summary>
        /// Flush and close the log file. If not used, it will be safely closed upon exit.
        /// </summary>
        public void Dispose()
        {
            if (stream == null) return;
            lock (StreamLock)
            {
                //Remove disposing events
                AppDomain.CurrentDomain.DomainUnload -= CurrentDomain_DisposeLog;
                AppDomain.CurrentDomain.ProcessExit -= CurrentDomain_DisposeLog;
                AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_DisposeLog;

                //Close the log editor if it is open
                EditDialog?.Close();
                EditDialog = null;

                //Write the ending hard divider (with same length a header) and close the log.
                var msg = $"==== End of Log {DateTime.Now.ToString("G")} =";
                msg = msg + new string('=', HeaderLength - msg.Length);
                stream.WriteLine(msg);
                stream.Close();
                stream = null;
            }
        }

        /// <summary>
        /// Determine if this instance has already been disposed.
        /// </summary>
        public bool IsDisposed => stream == null;

        /// <summary>
        /// Write a standardized soft divider line while disallowing multiple divider lines to be written consecutively.
        /// </summary>
        public void WriteSoftDivider()
        {
            if (WroteSoftDivider) return;
            WriteLine(@"---------------------------------------");
            WroteSoftDivider = true;
        }

        /// <summary>
        /// Write a message to the log file.
        /// </summary>
        /// <param name="msg">the message string to write (using string interpolation?)</param>
        public void WriteLine(string msg)
        {
            if (msg == null) return;
            if (stream == null) return;
            lock (StreamLock)
            {
                if (stream == null) return;
                WroteSoftDivider = false;
                if (EditDialog != null && !EditDialog.IsDisposed) EditDialog.AppendLine(msg);
                stream.BaseStream.Seek(0, SeekOrigin.End); //Incase someone edited the file, externally (not FormLogEditor).
                stream.WriteLine(msg);
                //Always flush to disk so external editing is always current. It is up to the external editor to update itself to any new changes.
                stream.Flush();
                stream.BaseStream.FlushAsync();
            }
        }

        /// <summary>
        /// Open log editor. It is kept in sync with live log.
        /// </summary>
        public void LogEdit()
        {
            if (string.IsNullOrEmpty(OutputFile)) return;

            if (EditDialog != null && !EditDialog.IsDisposed)
            {
                EditDialog.Focus();
                return;
            }

            //Show editor dialog as modalless (i.e. returns immediately) and owned by the form that called this method (e.g. FormMain)
            //It is closed by the editor dialog close button (e.g. the user) or automatically when the log file is closed.

            EditDialog = new FormLogEditor(OutputFile);
            EditDialog.Show(Application.OpenForms[Application.OpenForms.Count - 1]);
        }

        /// <summary>
        /// Generate an accurately protrayed fake log file to test:
        /// (1) large file rename and new log.
        /// (2) LogEditor performance.
        /// </summary>
        /// <param name="outputFile">Name of log to create</param>
        public static void GenerateFakeLog(string outputFile)
        {
            Func<TimeSpan, string> TimeSpanFormat = tsp => $"{(int)tsp.TotalHours:#0}:{tsp.Minutes:00}:{tsp.Seconds:00}";
            Func<int, string> TimeSpanFormatI = seconds => TimeSpanFormat(new TimeSpan(0, 0, 0, seconds));

            if (string.IsNullOrEmpty(outputFile)) outputFile = "NetworkOnLineMonitor.txt";

            var dt = DateTime.Now;
            Random rand = new Random();

            using (FileStream fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite, 4096, FileOptions.SequentialScan))
            using (var stream = new StreamWriter(fs))
            {
                var downtimeList = new List<int>(255);
                var msg = string.Empty;
                int fileLen = 0;
                int recordCount = 0;
                while (fileLen < MaxLogSize)
                {
                    var startDate = dt;

                    msg = $@"
================= NetworkOnlineMonitor Log {dt:yyyy-MM-dd HH:mm:ss} =================
Ping Target 1: 8.8.8.8 - Google
Ping Target 2: 4.2.2.2 - Level3
Ping Target 3: 1.1.1.1 - Cloudflare
Wait for Ping (milliseconds): 200
Test Interval (seconds): 5
Log Failure Longer Than (seconds): 10
---------------------------------------";
                    stream.WriteLine(msg);
                    if ((fileLen += msg.Length) > MaxLogSize) break;
                    recordCount++;

                    int kount = rand.Next(1, 255);
                    for (int i = 0; i < kount; i++)
                    {
                        int duration = rand.Next(0, 3600);
                        dt = dt.AddMinutes(duration / 60.0 + rand.Next(1, 10));
                        downtimeList.Add(duration);

                        if (rand.Next(0, 100) == 0)
                            msg = $"LAN Failed - {dt:yyyy-MM-dd HH:mm:ss}, Duration={TimeSpanFormatI(duration)}";
                        else
                            msg = $"WAN Failed - {dt:yyyy-MM-dd HH:mm:ss}, Duration={TimeSpanFormatI(duration)}, LAN responded in 1 ms";
                        if ((fileLen += msg.Length) > MaxLogSize) break;
                        stream.WriteLine(msg);
                        recordCount++;

                        if (rand.Next(0, 255) == 0)
                        {
                            dt = dt.AddMinutes(rand.Next(60, 3600) / 60.0);
                            msg = $"{dt:yyyy-MM-dd HH:mm:ss},\"Something happened here\r\nSecond Line\r\nThird Line\"";
                            fileLen += msg.Length;
                            stream.WriteLine(msg);
                            recordCount++;
                        }
                    }

                    downtimeList.Sort();
                    var median = downtimeList.Count % 2 == 0 ? (downtimeList[downtimeList.Count / 2] + downtimeList[downtimeList.Count / 2 - 1]) / 2 : downtimeList[downtimeList.Count / 2];

                    msg = $@"---------------------------------------
Log End {dt:G}
Monitor Duration {TimeSpanFormat(dt-startDate)}
Failure Summary:
  Count          {kount}
  Total Downtime {TimeSpanFormatI(downtimeList.Sum())}
  % Downtime     {(downtimeList.Sum()/(dt - startDate).TotalSeconds).ToString("0.00%")}%
  Minimum Length {TimeSpanFormatI(downtimeList.Min())}
  Maximum Length {TimeSpanFormatI(downtimeList.Max())}
  Average Length {TimeSpanFormatI((int)downtimeList.Average())}
  Median Length  {TimeSpanFormatI(median)}
==== End of Log ==============================================================";
                    downtimeList.Clear();
                    fileLen += msg.Length;
                    stream.WriteLine(msg);
                    recordCount++;
                }
            }
        }
    }
}
