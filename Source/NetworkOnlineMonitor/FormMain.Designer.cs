
namespace NetworkOnlineMonitor
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.m_ctxTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_ctxTrayMenuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ctxTrayMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.m_ctxTrayMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuStrip = new System.Windows.Forms.MenuStrip();
            this.m_tsExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_tsSettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_tsHistoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_tsAboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.m_lblLogging = new System.Windows.Forms.Label();
            this.m_lblPopupOnFailure = new System.Windows.Forms.Label();
            this.m_lblPingTimeout = new System.Windows.Forms.Label();
            this.m_lblOfflineTrigger = new System.Windows.Forms.Label();
            this.m_lblPingInterval = new System.Windows.Forms.Label();
            this.m_lblMonitorStarted = new System.Windows.Forms.Label();
            this.m_lblFailureCount = new System.Windows.Forms.Label();
            this.m_lblLastFailDuration = new System.Windows.Forms.Label();
            this.m_lblLastFailureStart = new System.Windows.Forms.Label();
            this.m_lblMonitorDuration = new System.Windows.Forms.Label();
            this.m_grpSettings = new NetworkOnlineMonitor.MyGroupBox();
            this.m_txtPingTimeout = new System.Windows.Forms.Label();
            this.m_txtOfflineTrigger = new System.Windows.Forms.Label();
            this.m_txtPingInterval = new System.Windows.Forms.Label();
            this.m_txtLogging = new System.Windows.Forms.Label();
            this.m_txtPopupOnFailure = new System.Windows.Forms.Label();
            this.m_grpResults = new NetworkOnlineMonitor.MyGroupBox();
            this.m_txtMonitorStarted = new System.Windows.Forms.Label();
            this.m_txtFailureCount = new System.Windows.Forms.Label();
            this.m_txtLastFailDuration = new System.Windows.Forms.Label();
            this.m_txtLastFailureStart = new System.Windows.Forms.Label();
            this.m_txtMonitorDuration = new System.Windows.Forms.Label();
            this.m_grpPingTests = new NetworkOnlineMonitor.MyGroupBox();
            this.m_lblTestTarget3Resp = new System.Windows.Forms.Label();
            this.m_lblTestTarget3 = new System.Windows.Forms.Label();
            this.m_pbTestTarget3 = new System.Windows.Forms.PictureBox();
            this.m_lblTestTarget2Resp = new System.Windows.Forms.Label();
            this.m_lblTestTarget2 = new System.Windows.Forms.Label();
            this.m_pbTestTarget2 = new System.Windows.Forms.PictureBox();
            this.m_lblTestTarget1Resp = new System.Windows.Forms.Label();
            this.m_lblTestTarget1 = new System.Windows.Forms.Label();
            this.m_lblResponseTimeTitle = new System.Windows.Forms.Label();
            this.m_lblTestTargetTitle = new System.Windows.Forms.Label();
            this.m_pbTestTarget1 = new System.Windows.Forms.PictureBox();
            this.m_grpStatus = new NetworkOnlineMonitor.MyGroupBox();
            this.m_txtCurrentFailDuration = new System.Windows.Forms.Label();
            this.m_txtLanStatus = new System.Windows.Forms.Label();
            this.m_txtWanStatus = new System.Windows.Forms.Label();
            this.m_pbLanStatus = new System.Windows.Forms.PictureBox();
            this.m_pbWanStatus = new System.Windows.Forms.PictureBox();
            this.m_lblCurrentFailDuration = new System.Windows.Forms.Label();
            this.m_ctxTray.SuspendLayout();
            this.m_MenuStrip.SuspendLayout();
            this.m_grpSettings.SuspendLayout();
            this.m_grpResults.SuspendLayout();
            this.m_grpPingTests.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbTestTarget3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbTestTarget2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbTestTarget1)).BeginInit();
            this.m_grpStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbLanStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbWanStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // m_TrayIcon
            // 
            this.m_TrayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.m_TrayIcon.BalloonTipText = "Up for 10 minutes";
            this.m_TrayIcon.BalloonTipTitle = "Up Time";
            this.m_TrayIcon.ContextMenuStrip = this.m_ctxTray;
            this.m_TrayIcon.Icon = global::NetworkOnlineMonitor.Properties.Resources.favicon;
            this.m_TrayIcon.Text = "Up Time";
            this.m_TrayIcon.Visible = true;
            this.m_TrayIcon.Click += new System.EventHandler(this.m_TrayIcon_Click);
            this.m_TrayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_TrayIcon_MouseDoubleClick);
            // 
            // m_ctxTray
            // 
            this.m_ctxTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ctxTrayMenuOpen,
            this.m_ctxTrayMenuSeparator,
            this.m_ctxTrayMenuItemExit});
            this.m_ctxTray.Name = "m_ctxNotify";
            this.m_ctxTray.Size = new System.Drawing.Size(104, 54);
            // 
            // m_ctxTrayMenuOpen
            // 
            this.m_ctxTrayMenuOpen.Image = global::NetworkOnlineMonitor.Properties.Resources.Open16;
            this.m_ctxTrayMenuOpen.Name = "m_ctxTrayMenuOpen";
            this.m_ctxTrayMenuOpen.Size = new System.Drawing.Size(103, 22);
            this.m_ctxTrayMenuOpen.Text = "Open";
            this.m_ctxTrayMenuOpen.ToolTipText = "Open this application.";
            this.m_ctxTrayMenuOpen.Click += new System.EventHandler(this.m_ctxTrayMenuOpen_Click);
            // 
            // m_ctxTrayMenuSeparator
            // 
            this.m_ctxTrayMenuSeparator.Name = "m_ctxTrayMenuSeparator";
            this.m_ctxTrayMenuSeparator.Size = new System.Drawing.Size(100, 6);
            // 
            // m_ctxTrayMenuItemExit
            // 
            this.m_ctxTrayMenuItemExit.Image = global::NetworkOnlineMonitor.Properties.Resources.Close16;
            this.m_ctxTrayMenuItemExit.Name = "m_ctxTrayMenuItemExit";
            this.m_ctxTrayMenuItemExit.Size = new System.Drawing.Size(103, 22);
            this.m_ctxTrayMenuItemExit.Text = "Exit";
            this.m_ctxTrayMenuItemExit.ToolTipText = "Exit this application.";
            this.m_ctxTrayMenuItemExit.Click += new System.EventHandler(this.m_ctxTrayMenuItemExit_Click);
            // 
            // m_MenuStrip
            // 
            this.m_MenuStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_MenuStrip.AutoSize = false;
            this.m_MenuStrip.BackColor = System.Drawing.Color.Gainsboro;
            this.m_MenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.m_MenuStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tsExitMenuItem,
            this.m_tsSettingsMenuItem,
            this.m_tsHistoryMenuItem,
            this.m_tsAboutMenuItem});
            this.m_MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.m_MenuStrip.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.m_MenuStrip.Name = "m_MenuStrip";
            this.m_MenuStrip.Padding = new System.Windows.Forms.Padding(0);
            this.m_MenuStrip.ShowItemToolTips = true;
            this.m_MenuStrip.Size = new System.Drawing.Size(478, 24);
            this.m_MenuStrip.TabIndex = 0;
            this.m_MenuStrip.Text = "Menu Strip";
            // 
            // m_tsExitMenuItem
            // 
            this.m_tsExitMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_tsExitMenuItem.ForeColor = System.Drawing.Color.Crimson;
            this.m_tsExitMenuItem.Name = "m_tsExitMenuItem";
            this.m_tsExitMenuItem.Size = new System.Drawing.Size(42, 24);
            this.m_tsExitMenuItem.Text = "Exit";
            this.m_tsExitMenuItem.ToolTipText = "Shutdown and \r\nexit this app.";
            this.m_tsExitMenuItem.Click += new System.EventHandler(this.m_tsExitMenuItem_Click);
            // 
            // m_tsSettingsMenuItem
            // 
            this.m_tsSettingsMenuItem.Name = "m_tsSettingsMenuItem";
            this.m_tsSettingsMenuItem.Size = new System.Drawing.Size(64, 24);
            this.m_tsSettingsMenuItem.Text = "Settings";
            this.m_tsSettingsMenuItem.ToolTipText = "Modify the \r\napp settings.";
            this.m_tsSettingsMenuItem.Click += new System.EventHandler(this.m_tsSettingsMenuItem_Click);
            // 
            // m_tsHistoryMenuItem
            // 
            this.m_tsHistoryMenuItem.Name = "m_tsHistoryMenuItem";
            this.m_tsHistoryMenuItem.Size = new System.Drawing.Size(56, 24);
            this.m_tsHistoryMenuItem.Text = "History";
            this.m_tsHistoryMenuItem.ToolTipText = "Pops up the current log \r\nin the default text editor.";
            this.m_tsHistoryMenuItem.Click += new System.EventHandler(this.m_tsHistoryMenuItem_Click);
            // 
            // m_tsAboutMenuItem
            // 
            this.m_tsAboutMenuItem.Name = "m_tsAboutMenuItem";
            this.m_tsAboutMenuItem.Size = new System.Drawing.Size(53, 24);
            this.m_tsAboutMenuItem.Text = "About";
            this.m_tsAboutMenuItem.ToolTipText = "Description and\r\nhelp for this app.";
            this.m_tsAboutMenuItem.Click += new System.EventHandler(this.m_tsAboutMenuItem_Click);
            // 
            // m_lblLogging
            // 
            this.m_lblLogging.AutoSize = true;
            this.m_lblLogging.Location = new System.Drawing.Point(10, 52);
            this.m_lblLogging.Name = "m_lblLogging";
            this.m_lblLogging.Size = new System.Drawing.Size(50, 14);
            this.m_lblLogging.TabIndex = 27;
            this.m_lblLogging.Text = "Logging";
            this.m_ToolTips.SetToolTip(this.m_lblLogging, "Are the network down \r\nevents being logged?");
            // 
            // m_lblPopupOnFailure
            // 
            this.m_lblPopupOnFailure.AutoSize = true;
            this.m_lblPopupOnFailure.Location = new System.Drawing.Point(10, 17);
            this.m_lblPopupOnFailure.Name = "m_lblPopupOnFailure";
            this.m_lblPopupOnFailure.Size = new System.Drawing.Size(74, 28);
            this.m_lblPopupOnFailure.TabIndex = 26;
            this.m_lblPopupOnFailure.Text = "Popup upon\r\nfailure";
            this.m_ToolTips.SetToolTip(this.m_lblPopupOnFailure, "Popup this app if the\r\nnetwork goes down.");
            // 
            // m_lblPingTimeout
            // 
            this.m_lblPingTimeout.AutoSize = true;
            this.m_lblPingTimeout.Location = new System.Drawing.Point(10, 129);
            this.m_lblPingTimeout.Name = "m_lblPingTimeout";
            this.m_lblPingTimeout.Size = new System.Drawing.Size(57, 28);
            this.m_lblPingTimeout.TabIndex = 25;
            this.m_lblPingTimeout.Text = "Ping wait\r\nresponse";
            this.m_ToolTips.SetToolTip(this.m_lblPingTimeout, "The ping response timeout.");
            // 
            // m_lblOfflineTrigger
            // 
            this.m_lblOfflineTrigger.AutoSize = true;
            this.m_lblOfflineTrigger.Location = new System.Drawing.Point(10, 94);
            this.m_lblOfflineTrigger.Name = "m_lblOfflineTrigger";
            this.m_lblOfflineTrigger.Size = new System.Drawing.Size(83, 28);
            this.m_lblOfflineTrigger.TabIndex = 24;
            this.m_lblOfflineTrigger.Text = "Trigger offline\r\nevent after…";
            this.m_ToolTips.SetToolTip(this.m_lblOfflineTrigger, "Notify and log the network down event if \r\nthe down time exceeds this amount of t" +
        "ime.");
            // 
            // m_lblPingInterval
            // 
            this.m_lblPingInterval.AutoSize = true;
            this.m_lblPingInterval.Location = new System.Drawing.Point(10, 73);
            this.m_lblPingInterval.Name = "m_lblPingInterval";
            this.m_lblPingInterval.Size = new System.Drawing.Size(75, 14);
            this.m_lblPingInterval.TabIndex = 23;
            this.m_lblPingInterval.Text = "Ping Interval";
            this.m_ToolTips.SetToolTip(this.m_lblPingInterval, "The amount of time\r\nbetween pings.");
            // 
            // m_lblMonitorStarted
            // 
            this.m_lblMonitorStarted.AutoSize = true;
            this.m_lblMonitorStarted.Location = new System.Drawing.Point(12, 24);
            this.m_lblMonitorStarted.Name = "m_lblMonitorStarted";
            this.m_lblMonitorStarted.Size = new System.Drawing.Size(93, 14);
            this.m_lblMonitorStarted.TabIndex = 11;
            this.m_lblMonitorStarted.Text = "Monitor Started";
            this.m_ToolTips.SetToolTip(this.m_lblMonitorStarted, "The date/time that this app started \r\nmonitering the network.");
            // 
            // m_lblFailureCount
            // 
            this.m_lblFailureCount.AutoSize = true;
            this.m_lblFailureCount.Location = new System.Drawing.Point(12, 140);
            this.m_lblFailureCount.Name = "m_lblFailureCount";
            this.m_lblFailureCount.Size = new System.Drawing.Size(78, 14);
            this.m_lblFailureCount.TabIndex = 5;
            this.m_lblFailureCount.Text = "Failure Count";
            this.m_ToolTips.SetToolTip(this.m_lblFailureCount, "Network failure count\r\nsince monitor started.");
            // 
            // m_lblLastFailDuration
            // 
            this.m_lblLastFailDuration.AutoSize = true;
            this.m_lblLastFailDuration.Location = new System.Drawing.Point(12, 111);
            this.m_lblLastFailDuration.Name = "m_lblLastFailDuration";
            this.m_lblLastFailDuration.Size = new System.Drawing.Size(99, 14);
            this.m_lblLastFailDuration.TabIndex = 4;
            this.m_lblLastFailDuration.Text = "Last Fail Duration";
            this.m_ToolTips.SetToolTip(this.m_lblLastFailDuration, "Duration of the last\r\nnetwork down event\r\nsince monitor started.");
            // 
            // m_lblLastFailureStart
            // 
            this.m_lblLastFailureStart.AutoSize = true;
            this.m_lblLastFailureStart.Location = new System.Drawing.Point(12, 82);
            this.m_lblLastFailureStart.Name = "m_lblLastFailureStart";
            this.m_lblLastFailureStart.Size = new System.Drawing.Size(98, 14);
            this.m_lblLastFailureStart.TabIndex = 3;
            this.m_lblLastFailureStart.Text = "Last Failure Start";
            this.m_ToolTips.SetToolTip(this.m_lblLastFailureStart, "Last time a network\r\nfailure ocured since \r\nmonitor started.");
            // 
            // m_lblMonitorDuration
            // 
            this.m_lblMonitorDuration.AutoSize = true;
            this.m_lblMonitorDuration.Location = new System.Drawing.Point(12, 53);
            this.m_lblMonitorDuration.Name = "m_lblMonitorDuration";
            this.m_lblMonitorDuration.Size = new System.Drawing.Size(98, 14);
            this.m_lblMonitorDuration.TabIndex = 2;
            this.m_lblMonitorDuration.Text = "Monitor Duration";
            this.m_ToolTips.SetToolTip(this.m_lblMonitorDuration, "The amount of time this app has \r\nbeen monitoring the network.");
            // 
            // m_grpSettings
            // 
            this.m_grpSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_grpSettings.BorderColor = System.Drawing.Color.LightGray;
            this.m_grpSettings.Controls.Add(this.m_txtPingTimeout);
            this.m_grpSettings.Controls.Add(this.m_txtOfflineTrigger);
            this.m_grpSettings.Controls.Add(this.m_txtPingInterval);
            this.m_grpSettings.Controls.Add(this.m_txtLogging);
            this.m_grpSettings.Controls.Add(this.m_txtPopupOnFailure);
            this.m_grpSettings.Controls.Add(this.m_lblLogging);
            this.m_grpSettings.Controls.Add(this.m_lblPopupOnFailure);
            this.m_grpSettings.Controls.Add(this.m_lblPingTimeout);
            this.m_grpSettings.Controls.Add(this.m_lblOfflineTrigger);
            this.m_grpSettings.Controls.Add(this.m_lblPingInterval);
            this.m_grpSettings.Location = new System.Drawing.Point(283, 220);
            this.m_grpSettings.Margin = new System.Windows.Forms.Padding(8, 1, 8, 8);
            this.m_grpSettings.Name = "m_grpSettings";
            this.m_grpSettings.Padding = new System.Windows.Forms.Padding(0);
            this.m_grpSettings.Size = new System.Drawing.Size(177, 170);
            this.m_grpSettings.TabIndex = 23;
            this.m_grpSettings.TabStop = false;
            this.m_grpSettings.Text = "Settings";
            this.m_grpSettings.TitleFont = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // m_txtPingTimeout
            // 
            this.m_txtPingTimeout.AutoSize = true;
            this.m_txtPingTimeout.Location = new System.Drawing.Point(99, 136);
            this.m_txtPingTimeout.Name = "m_txtPingTimeout";
            this.m_txtPingTimeout.Size = new System.Drawing.Size(47, 14);
            this.m_txtPingTimeout.TabIndex = 32;
            this.m_txtPingTimeout.Text = "200 ms";
            // 
            // m_txtOfflineTrigger
            // 
            this.m_txtOfflineTrigger.AutoSize = true;
            this.m_txtOfflineTrigger.Location = new System.Drawing.Point(99, 101);
            this.m_txtOfflineTrigger.Name = "m_txtOfflineTrigger";
            this.m_txtOfflineTrigger.Size = new System.Drawing.Size(62, 14);
            this.m_txtOfflineTrigger.TabIndex = 31;
            this.m_txtOfflineTrigger.Text = "5 seconds";
            // 
            // m_txtPingInterval
            // 
            this.m_txtPingInterval.AutoSize = true;
            this.m_txtPingInterval.Location = new System.Drawing.Point(99, 73);
            this.m_txtPingInterval.Name = "m_txtPingInterval";
            this.m_txtPingInterval.Size = new System.Drawing.Size(62, 14);
            this.m_txtPingInterval.TabIndex = 30;
            this.m_txtPingInterval.Text = "5 seconds";
            // 
            // m_txtLogging
            // 
            this.m_txtLogging.AutoSize = true;
            this.m_txtLogging.Location = new System.Drawing.Point(99, 52);
            this.m_txtLogging.Name = "m_txtLogging";
            this.m_txtLogging.Size = new System.Drawing.Size(27, 14);
            this.m_txtLogging.TabIndex = 29;
            this.m_txtLogging.Text = "Yes";
            // 
            // m_txtPopupOnFailure
            // 
            this.m_txtPopupOnFailure.AutoSize = true;
            this.m_txtPopupOnFailure.Location = new System.Drawing.Point(99, 24);
            this.m_txtPopupOnFailure.Name = "m_txtPopupOnFailure";
            this.m_txtPopupOnFailure.Size = new System.Drawing.Size(22, 14);
            this.m_txtPopupOnFailure.TabIndex = 28;
            this.m_txtPopupOnFailure.Text = "No";
            // 
            // m_grpResults
            // 
            this.m_grpResults.BorderColor = System.Drawing.Color.LightGray;
            this.m_grpResults.Controls.Add(this.m_txtMonitorStarted);
            this.m_grpResults.Controls.Add(this.m_lblMonitorStarted);
            this.m_grpResults.Controls.Add(this.m_txtFailureCount);
            this.m_grpResults.Controls.Add(this.m_txtLastFailDuration);
            this.m_grpResults.Controls.Add(this.m_txtLastFailureStart);
            this.m_grpResults.Controls.Add(this.m_txtMonitorDuration);
            this.m_grpResults.Controls.Add(this.m_lblFailureCount);
            this.m_grpResults.Controls.Add(this.m_lblLastFailDuration);
            this.m_grpResults.Controls.Add(this.m_lblLastFailureStart);
            this.m_grpResults.Controls.Add(this.m_lblMonitorDuration);
            this.m_grpResults.Location = new System.Drawing.Point(16, 220);
            this.m_grpResults.Margin = new System.Windows.Forms.Padding(8, 1, 8, 8);
            this.m_grpResults.Name = "m_grpResults";
            this.m_grpResults.Padding = new System.Windows.Forms.Padding(0);
            this.m_grpResults.Size = new System.Drawing.Size(252, 170);
            this.m_grpResults.TabIndex = 3;
            this.m_grpResults.TabStop = false;
            this.m_grpResults.Text = "Results";
            this.m_grpResults.TitleFont = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // m_txtMonitorStarted
            // 
            this.m_txtMonitorStarted.AutoSize = true;
            this.m_txtMonitorStarted.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtMonitorStarted.Location = new System.Drawing.Point(115, 24);
            this.m_txtMonitorStarted.Name = "m_txtMonitorStarted";
            this.m_txtMonitorStarted.Size = new System.Drawing.Size(129, 14);
            this.m_txtMonitorStarted.TabIndex = 12;
            this.m_txtMonitorStarted.Text = "00/00/0000 00:00:00 PM";
            // 
            // m_txtFailureCount
            // 
            this.m_txtFailureCount.AutoSize = true;
            this.m_txtFailureCount.Location = new System.Drawing.Point(115, 140);
            this.m_txtFailureCount.Name = "m_txtFailureCount";
            this.m_txtFailureCount.Size = new System.Drawing.Size(14, 14);
            this.m_txtFailureCount.TabIndex = 10;
            this.m_txtFailureCount.Text = "0";
            // 
            // m_txtLastFailDuration
            // 
            this.m_txtLastFailDuration.AutoSize = true;
            this.m_txtLastFailDuration.Location = new System.Drawing.Point(115, 111);
            this.m_txtLastFailDuration.Name = "m_txtLastFailDuration";
            this.m_txtLastFailDuration.Size = new System.Drawing.Size(21, 14);
            this.m_txtLastFailDuration.TabIndex = 9;
            this.m_txtLastFailDuration.Text = "‒‒";
            // 
            // m_txtLastFailureStart
            // 
            this.m_txtLastFailureStart.AutoSize = true;
            this.m_txtLastFailureStart.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtLastFailureStart.Location = new System.Drawing.Point(115, 82);
            this.m_txtLastFailureStart.Name = "m_txtLastFailureStart";
            this.m_txtLastFailureStart.Size = new System.Drawing.Size(19, 14);
            this.m_txtLastFailureStart.TabIndex = 7;
            this.m_txtLastFailureStart.Text = "‒‒";
            // 
            // m_txtMonitorDuration
            // 
            this.m_txtMonitorDuration.AutoSize = true;
            this.m_txtMonitorDuration.Location = new System.Drawing.Point(115, 53);
            this.m_txtMonitorDuration.Name = "m_txtMonitorDuration";
            this.m_txtMonitorDuration.Size = new System.Drawing.Size(57, 14);
            this.m_txtMonitorDuration.TabIndex = 6;
            this.m_txtMonitorDuration.Text = "00:00:00";
            // 
            // m_grpPingTests
            // 
            this.m_grpPingTests.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_grpPingTests.BorderColor = System.Drawing.Color.LightGray;
            this.m_grpPingTests.Controls.Add(this.m_lblTestTarget3Resp);
            this.m_grpPingTests.Controls.Add(this.m_lblTestTarget3);
            this.m_grpPingTests.Controls.Add(this.m_pbTestTarget3);
            this.m_grpPingTests.Controls.Add(this.m_lblTestTarget2Resp);
            this.m_grpPingTests.Controls.Add(this.m_lblTestTarget2);
            this.m_grpPingTests.Controls.Add(this.m_pbTestTarget2);
            this.m_grpPingTests.Controls.Add(this.m_lblTestTarget1Resp);
            this.m_grpPingTests.Controls.Add(this.m_lblTestTarget1);
            this.m_grpPingTests.Controls.Add(this.m_lblResponseTimeTitle);
            this.m_grpPingTests.Controls.Add(this.m_lblTestTargetTitle);
            this.m_grpPingTests.Controls.Add(this.m_pbTestTarget1);
            this.m_grpPingTests.Location = new System.Drawing.Point(283, 33);
            this.m_grpPingTests.Margin = new System.Windows.Forms.Padding(8, 1, 8, 8);
            this.m_grpPingTests.Name = "m_grpPingTests";
            this.m_grpPingTests.Padding = new System.Windows.Forms.Padding(0);
            this.m_grpPingTests.Size = new System.Drawing.Size(177, 178);
            this.m_grpPingTests.TabIndex = 2;
            this.m_grpPingTests.TabStop = false;
            this.m_grpPingTests.Text = "Ping Tests";
            this.m_grpPingTests.TitleFont = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // m_lblTestTarget3Resp
            // 
            this.m_lblTestTarget3Resp.AutoSize = true;
            this.m_lblTestTarget3Resp.Location = new System.Drawing.Point(108, 145);
            this.m_lblTestTarget3Resp.Name = "m_lblTestTarget3Resp";
            this.m_lblTestTarget3Resp.Size = new System.Drawing.Size(28, 14);
            this.m_lblTestTarget3Resp.TabIndex = 10;
            this.m_lblTestTarget3Resp.Text = "200";
            // 
            // m_lblTestTarget3
            // 
            this.m_lblTestTarget3.AutoSize = true;
            this.m_lblTestTarget3.Location = new System.Drawing.Point(44, 137);
            this.m_lblTestTarget3.Name = "m_lblTestTarget3";
            this.m_lblTestTarget3.Size = new System.Drawing.Size(47, 28);
            this.m_lblTestTarget3.TabIndex = 9;
            this.m_lblTestTarget3.Text = "Google\r\n8.8.8.8";
            // 
            // m_pbTestTarget3
            // 
            this.m_pbTestTarget3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.m_pbTestTarget3.Image = global::NetworkOnlineMonitor.Properties.Resources.SphereRed24;
            this.m_pbTestTarget3.Location = new System.Drawing.Point(17, 140);
            this.m_pbTestTarget3.Name = "m_pbTestTarget3";
            this.m_pbTestTarget3.Size = new System.Drawing.Size(24, 24);
            this.m_pbTestTarget3.TabIndex = 8;
            this.m_pbTestTarget3.TabStop = false;
            // 
            // m_lblTestTarget2Resp
            // 
            this.m_lblTestTarget2Resp.AutoSize = true;
            this.m_lblTestTarget2Resp.Location = new System.Drawing.Point(110, 100);
            this.m_lblTestTarget2Resp.Name = "m_lblTestTarget2Resp";
            this.m_lblTestTarget2Resp.Size = new System.Drawing.Size(28, 14);
            this.m_lblTestTarget2Resp.TabIndex = 7;
            this.m_lblTestTarget2Resp.Text = "200";
            // 
            // m_lblTestTarget2
            // 
            this.m_lblTestTarget2.AutoSize = true;
            this.m_lblTestTarget2.Location = new System.Drawing.Point(44, 92);
            this.m_lblTestTarget2.Name = "m_lblTestTarget2";
            this.m_lblTestTarget2.Size = new System.Drawing.Size(47, 28);
            this.m_lblTestTarget2.TabIndex = 6;
            this.m_lblTestTarget2.Text = "Google\r\n8.8.8.8";
            // 
            // m_pbTestTarget2
            // 
            this.m_pbTestTarget2.Image = global::NetworkOnlineMonitor.Properties.Resources.SphereYellow24;
            this.m_pbTestTarget2.Location = new System.Drawing.Point(17, 94);
            this.m_pbTestTarget2.Name = "m_pbTestTarget2";
            this.m_pbTestTarget2.Size = new System.Drawing.Size(24, 24);
            this.m_pbTestTarget2.TabIndex = 5;
            this.m_pbTestTarget2.TabStop = false;
            // 
            // m_lblTestTarget1Resp
            // 
            this.m_lblTestTarget1Resp.AutoSize = true;
            this.m_lblTestTarget1Resp.Location = new System.Drawing.Point(110, 55);
            this.m_lblTestTarget1Resp.Name = "m_lblTestTarget1Resp";
            this.m_lblTestTarget1Resp.Size = new System.Drawing.Size(28, 14);
            this.m_lblTestTarget1Resp.TabIndex = 4;
            this.m_lblTestTarget1Resp.Text = "200";
            // 
            // m_lblTestTarget1
            // 
            this.m_lblTestTarget1.AutoSize = true;
            this.m_lblTestTarget1.Location = new System.Drawing.Point(44, 47);
            this.m_lblTestTarget1.Name = "m_lblTestTarget1";
            this.m_lblTestTarget1.Size = new System.Drawing.Size(47, 28);
            this.m_lblTestTarget1.TabIndex = 3;
            this.m_lblTestTarget1.Text = "Google\r\n8.8.8.8";
            // 
            // m_lblResponseTimeTitle
            // 
            this.m_lblResponseTimeTitle.AutoSize = true;
            this.m_lblResponseTimeTitle.Location = new System.Drawing.Point(97, 16);
            this.m_lblResponseTimeTitle.Name = "m_lblResponseTimeTitle";
            this.m_lblResponseTimeTitle.Size = new System.Drawing.Size(63, 28);
            this.m_lblResponseTimeTitle.TabIndex = 2;
            this.m_lblResponseTimeTitle.Text = "Response\r\nTime (ms)";
            // 
            // m_lblTestTargetTitle
            // 
            this.m_lblTestTargetTitle.AutoSize = true;
            this.m_lblTestTargetTitle.Location = new System.Drawing.Point(16, 24);
            this.m_lblTestTargetTitle.Name = "m_lblTestTargetTitle";
            this.m_lblTestTargetTitle.Size = new System.Drawing.Size(73, 14);
            this.m_lblTestTargetTitle.TabIndex = 1;
            this.m_lblTestTargetTitle.Text = "Test Target";
            // 
            // m_pbTestTarget1
            // 
            this.m_pbTestTarget1.Image = global::NetworkOnlineMonitor.Properties.Resources.SphereGreen24;
            this.m_pbTestTarget1.Location = new System.Drawing.Point(17, 50);
            this.m_pbTestTarget1.Name = "m_pbTestTarget1";
            this.m_pbTestTarget1.Size = new System.Drawing.Size(24, 24);
            this.m_pbTestTarget1.TabIndex = 0;
            this.m_pbTestTarget1.TabStop = false;
            // 
            // m_grpStatus
            // 
            this.m_grpStatus.Controls.Add(this.m_txtCurrentFailDuration);
            this.m_grpStatus.Controls.Add(this.m_txtLanStatus);
            this.m_grpStatus.Controls.Add(this.m_txtWanStatus);
            this.m_grpStatus.Controls.Add(this.m_pbLanStatus);
            this.m_grpStatus.Controls.Add(this.m_pbWanStatus);
            this.m_grpStatus.Controls.Add(this.m_lblCurrentFailDuration);
            this.m_grpStatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_grpStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_grpStatus.Location = new System.Drawing.Point(16, 33);
            this.m_grpStatus.Margin = new System.Windows.Forms.Padding(8, 1, 8, 8);
            this.m_grpStatus.Name = "m_grpStatus";
            this.m_grpStatus.Padding = new System.Windows.Forms.Padding(0);
            this.m_grpStatus.Size = new System.Drawing.Size(252, 178);
            this.m_grpStatus.TabIndex = 1;
            this.m_grpStatus.TabStop = false;
            this.m_grpStatus.Text = "Status";
            this.m_grpStatus.TitleFont = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // m_txtCurrentFailDuration
            // 
            this.m_txtCurrentFailDuration.AutoSize = true;
            this.m_txtCurrentFailDuration.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtCurrentFailDuration.Location = new System.Drawing.Point(149, 157);
            this.m_txtCurrentFailDuration.Name = "m_txtCurrentFailDuration";
            this.m_txtCurrentFailDuration.Size = new System.Drawing.Size(63, 14);
            this.m_txtCurrentFailDuration.TabIndex = 2;
            this.m_txtCurrentFailDuration.Text = "00:00:00";
            this.m_txtCurrentFailDuration.Visible = false;
            // 
            // m_txtLanStatus
            // 
            this.m_txtLanStatus.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.m_txtLanStatus.Location = new System.Drawing.Point(76, 90);
            this.m_txtLanStatus.Name = "m_txtLanStatus";
            this.m_txtLanStatus.Size = new System.Drawing.Size(155, 62);
            this.m_txtLanStatus.TabIndex = 6;
            this.m_txtLanStatus.Text = "Local Network Failed";
            this.m_txtLanStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtWanStatus
            // 
            this.m_txtWanStatus.AllowDrop = true;
            this.m_txtWanStatus.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtWanStatus.Location = new System.Drawing.Point(76, 22);
            this.m_txtWanStatus.Name = "m_txtWanStatus";
            this.m_txtWanStatus.Size = new System.Drawing.Size(155, 62);
            this.m_txtWanStatus.TabIndex = 5;
            this.m_txtWanStatus.Text = "Internet OK";
            this.m_txtWanStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_pbLanStatus
            // 
            this.m_pbLanStatus.Image = global::NetworkOnlineMonitor.Properties.Resources.LanUp64;
            this.m_pbLanStatus.Location = new System.Drawing.Point(9, 88);
            this.m_pbLanStatus.Name = "m_pbLanStatus";
            this.m_pbLanStatus.Size = new System.Drawing.Size(64, 64);
            this.m_pbLanStatus.TabIndex = 4;
            this.m_pbLanStatus.TabStop = false;
            // 
            // m_pbWanStatus
            // 
            this.m_pbWanStatus.Image = global::NetworkOnlineMonitor.Properties.Resources.WanUp64;
            this.m_pbWanStatus.Location = new System.Drawing.Point(9, 19);
            this.m_pbWanStatus.Name = "m_pbWanStatus";
            this.m_pbWanStatus.Size = new System.Drawing.Size(64, 64);
            this.m_pbWanStatus.TabIndex = 3;
            this.m_pbWanStatus.TabStop = false;
            // 
            // m_lblCurrentFailDuration
            // 
            this.m_lblCurrentFailDuration.AutoSize = true;
            this.m_lblCurrentFailDuration.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblCurrentFailDuration.Location = new System.Drawing.Point(12, 157);
            this.m_lblCurrentFailDuration.Name = "m_lblCurrentFailDuration";
            this.m_lblCurrentFailDuration.Size = new System.Drawing.Size(135, 14);
            this.m_lblCurrentFailDuration.TabIndex = 1;
            this.m_lblCurrentFailDuration.Text = "Current Fail Duration";
            this.m_lblCurrentFailDuration.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 406);
            this.Controls.Add(this.m_grpSettings);
            this.Controls.Add(this.m_grpResults);
            this.Controls.Add(this.m_grpPingTests);
            this.Controls.Add(this.m_grpStatus);
            this.Controls.Add(this.m_MenuStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::NetworkOnlineMonitor.Properties.Resources.favicon;
            this.MainMenuStrip = this.m_MenuStrip;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(493, 445);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(493, 445);
            this.Name = "FormMain";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Text = "Network Online Monitor";
            this.m_ctxTray.ResumeLayout(false);
            this.m_MenuStrip.ResumeLayout(false);
            this.m_MenuStrip.PerformLayout();
            this.m_grpSettings.ResumeLayout(false);
            this.m_grpSettings.PerformLayout();
            this.m_grpResults.ResumeLayout(false);
            this.m_grpResults.PerformLayout();
            this.m_grpPingTests.ResumeLayout(false);
            this.m_grpPingTests.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbTestTarget3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbTestTarget2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbTestTarget1)).EndInit();
            this.m_grpStatus.ResumeLayout(false);
            this.m_grpStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbLanStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbWanStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon m_TrayIcon;
        private System.Windows.Forms.MenuStrip m_MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem m_tsExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_tsSettingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_tsAboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_tsHistoryMenuItem;
        private MyGroupBox m_grpStatus;
        private MyGroupBox m_grpPingTests;
        private MyGroupBox m_grpResults;
        private System.Windows.Forms.Label m_lblCurrentFailDuration;
        private System.Windows.Forms.Label m_lblTestTarget3Resp;
        private System.Windows.Forms.Label m_lblTestTarget3;
        private System.Windows.Forms.PictureBox m_pbTestTarget3;
        private System.Windows.Forms.Label m_lblTestTarget2Resp;
        private System.Windows.Forms.Label m_lblTestTarget2;
        private System.Windows.Forms.PictureBox m_pbTestTarget2;
        private System.Windows.Forms.Label m_lblTestTarget1Resp;
        private System.Windows.Forms.Label m_lblTestTarget1;
        private System.Windows.Forms.Label m_lblResponseTimeTitle;
        private System.Windows.Forms.Label m_lblTestTargetTitle;
        private System.Windows.Forms.PictureBox m_pbTestTarget1;
        private System.Windows.Forms.Label m_lblFailureCount;
        private System.Windows.Forms.Label m_lblLastFailDuration;
        private System.Windows.Forms.Label m_lblLastFailureStart;
        private System.Windows.Forms.Label m_lblMonitorDuration;
        private System.Windows.Forms.ContextMenuStrip m_ctxTray;
        private System.Windows.Forms.ToolStripMenuItem m_ctxTrayMenuOpen;
        private System.Windows.Forms.ToolStripSeparator m_ctxTrayMenuSeparator;
        private System.Windows.Forms.ToolStripMenuItem m_ctxTrayMenuItemExit;
        private System.Windows.Forms.ToolTip m_ToolTips;
        private System.Windows.Forms.Label m_txtCurrentFailDuration;
        private System.Windows.Forms.Label m_txtMonitorStarted;
        private System.Windows.Forms.Label m_lblMonitorStarted;
        private System.Windows.Forms.Label m_txtFailureCount;
        private System.Windows.Forms.Label m_txtLastFailDuration;
        private System.Windows.Forms.Label m_txtLastFailureStart;
        private System.Windows.Forms.Label m_txtMonitorDuration;
        private System.Windows.Forms.Label m_txtLanStatus;
        private System.Windows.Forms.Label m_txtWanStatus;
        private System.Windows.Forms.PictureBox m_pbLanStatus;
        private System.Windows.Forms.PictureBox m_pbWanStatus;
        private MyGroupBox m_grpSettings;
        private System.Windows.Forms.Label m_txtPingTimeout;
        private System.Windows.Forms.Label m_txtOfflineTrigger;
        private System.Windows.Forms.Label m_txtPingInterval;
        private System.Windows.Forms.Label m_txtLogging;
        private System.Windows.Forms.Label m_txtPopupOnFailure;
        private System.Windows.Forms.Label m_lblLogging;
        private System.Windows.Forms.Label m_lblPopupOnFailure;
        private System.Windows.Forms.Label m_lblPingTimeout;
        private System.Windows.Forms.Label m_lblOfflineTrigger;
        private System.Windows.Forms.Label m_lblPingInterval;
    }
}

