
namespace NetworkOnlineMonitor
{
    partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.m_pnlTest = new System.Windows.Forms.Panel();
            this.m_txtGatewayIPAddress = new System.Windows.Forms.Label();
            this.m_txtLanIPAddress = new System.Windows.Forms.Label();
            this.m_txtComputerName = new System.Windows.Forms.Label();
            this.m_lblGateayIPAddress = new System.Windows.Forms.Label();
            this.m_lblLanIPAddress = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_lblTargetServers = new System.Windows.Forms.Label();
            this.m_btnRestoreDefTargets = new System.Windows.Forms.Button();
            this.m_TargetServerControl3 = new NetworkOnlineMonitor.TargetServerControl();
            this.m_TargetServerControl2 = new NetworkOnlineMonitor.TargetServerControl();
            this.m_TargetServerControl1 = new NetworkOnlineMonitor.TargetServerControl();
            this.m_numTestInterval = new System.Windows.Forms.NumericUpDown();
            this.m_numPingResp = new System.Windows.Forms.NumericUpDown();
            this.m_lblTestInterval2 = new System.Windows.Forms.Label();
            this.m_lblPingResp2 = new System.Windows.Forms.Label();
            this.m_lblPingResp = new System.Windows.Forms.Label();
            this.m_lblTestInterval = new System.Windows.Forms.Label();
            this.m_lblTestSettings = new System.Windows.Forms.Label();
            this.m_lblStartupSettings = new System.Windows.Forms.Label();
            this.m_pnlStartup = new System.Windows.Forms.Panel();
            this.m_chkSystemTray = new System.Windows.Forms.CheckBox();
            this.m_chkStartWithWindows = new System.Windows.Forms.CheckBox();
            this.m_pnlAlertLog = new System.Windows.Forms.Panel();
            this.m_numLogFailure = new System.Windows.Forms.NumericUpDown();
            this.m_grpReconnectSound = new System.Windows.Forms.GroupBox();
            this.m_soundclipReconnect = new NetworkOnlineMonitor.SoundClipCtrl();
            this.m_grpFailureAlertSound = new System.Windows.Forms.GroupBox();
            this.m_soundclipAlert = new NetworkOnlineMonitor.SoundClipCtrl();
            this.m_grpLogFileOption = new System.Windows.Forms.GroupBox();
            this.m_txtLogFileLocation = new ChuckHill2.Forms.LabeledTextBox();
            this.m_btnLogFileLocation = new System.Windows.Forms.Button();
            this.m_radNoLogFile = new System.Windows.Forms.RadioButton();
            this.m_radAppendLogFile = new System.Windows.Forms.RadioButton();
            this.m_radNewLogFile = new System.Windows.Forms.RadioButton();
            this.m_lblLogFailure2 = new System.Windows.Forms.Label();
            this.m_lblLogFailure = new System.Windows.Forms.Label();
            this.m_chkPopup = new System.Windows.Forms.CheckBox();
            this.m_lblAlertLogSettiings = new System.Windows.Forms.Label();
            this.m_ToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.m_pnlTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numTestInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numPingResp)).BeginInit();
            this.m_pnlStartup.SuspendLayout();
            this.m_pnlAlertLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numLogFailure)).BeginInit();
            this.m_grpReconnectSound.SuspendLayout();
            this.m_grpFailureAlertSound.SuspendLayout();
            this.m_grpLogFileOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnlTest
            // 
            this.m_pnlTest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_pnlTest.Controls.Add(this.m_txtGatewayIPAddress);
            this.m_pnlTest.Controls.Add(this.m_txtLanIPAddress);
            this.m_pnlTest.Controls.Add(this.m_txtComputerName);
            this.m_pnlTest.Controls.Add(this.m_lblGateayIPAddress);
            this.m_pnlTest.Controls.Add(this.m_lblLanIPAddress);
            this.m_pnlTest.Controls.Add(this.label1);
            this.m_pnlTest.Controls.Add(this.label2);
            this.m_pnlTest.Controls.Add(this.m_lblTargetServers);
            this.m_pnlTest.Controls.Add(this.m_btnRestoreDefTargets);
            this.m_pnlTest.Controls.Add(this.m_TargetServerControl3);
            this.m_pnlTest.Controls.Add(this.m_TargetServerControl2);
            this.m_pnlTest.Controls.Add(this.m_TargetServerControl1);
            this.m_pnlTest.Controls.Add(this.m_numTestInterval);
            this.m_pnlTest.Controls.Add(this.m_numPingResp);
            this.m_pnlTest.Controls.Add(this.m_lblTestInterval2);
            this.m_pnlTest.Controls.Add(this.m_lblPingResp2);
            this.m_pnlTest.Controls.Add(this.m_lblPingResp);
            this.m_pnlTest.Controls.Add(this.m_lblTestInterval);
            this.m_pnlTest.Controls.Add(this.m_lblTestSettings);
            this.m_pnlTest.Location = new System.Drawing.Point(14, 99);
            this.m_pnlTest.Name = "m_pnlTest";
            this.m_pnlTest.Size = new System.Drawing.Size(374, 362);
            this.m_pnlTest.TabIndex = 1;
            // 
            // m_txtGatewayIPAddress
            // 
            this.m_txtGatewayIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtGatewayIPAddress.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_txtGatewayIPAddress.Location = new System.Drawing.Point(250, 131);
            this.m_txtGatewayIPAddress.Name = "m_txtGatewayIPAddress";
            this.m_txtGatewayIPAddress.Size = new System.Drawing.Size(106, 13);
            this.m_txtGatewayIPAddress.TabIndex = 21;
            this.m_txtGatewayIPAddress.Text = "255.255.255.255";
            this.m_txtGatewayIPAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtLanIPAddress
            // 
            this.m_txtLanIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtLanIPAddress.Location = new System.Drawing.Point(131, 131);
            this.m_txtLanIPAddress.Name = "m_txtLanIPAddress";
            this.m_txtLanIPAddress.Size = new System.Drawing.Size(106, 13);
            this.m_txtLanIPAddress.TabIndex = 20;
            this.m_txtLanIPAddress.Text = "255.255.255.255";
            this.m_txtLanIPAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_txtComputerName
            // 
            this.m_txtComputerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtComputerName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_txtComputerName.Location = new System.Drawing.Point(12, 131);
            this.m_txtComputerName.Name = "m_txtComputerName";
            this.m_txtComputerName.Size = new System.Drawing.Size(106, 13);
            this.m_txtComputerName.TabIndex = 19;
            this.m_txtComputerName.Text = "1234567890123456";
            this.m_txtComputerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblGateayIPAddress
            // 
            this.m_lblGateayIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblGateayIPAddress.AutoSize = true;
            this.m_lblGateayIPAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblGateayIPAddress.Location = new System.Drawing.Point(259, 113);
            this.m_lblGateayIPAddress.Name = "m_lblGateayIPAddress";
            this.m_lblGateayIPAddress.Size = new System.Drawing.Size(103, 13);
            this.m_lblGateayIPAddress.TabIndex = 18;
            this.m_lblGateayIPAddress.Text = "Gateway IP (Router)";
            // 
            // m_lblLanIPAddress
            // 
            this.m_lblLanIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblLanIPAddress.AutoSize = true;
            this.m_lblLanIPAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblLanIPAddress.Location = new System.Drawing.Point(132, 113);
            this.m_lblLanIPAddress.Name = "m_lblLanIPAddress";
            this.m_lblLanIPAddress.Size = new System.Drawing.Size(106, 13);
            this.m_lblLanIPAddress.TabIndex = 17;
            this.m_lblLanIPAddress.Text = "Computer IP Address";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Computer Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Local Network Info…";
            // 
            // m_lblTargetServers
            // 
            this.m_lblTargetServers.AutoSize = true;
            this.m_lblTargetServers.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblTargetServers.Location = new System.Drawing.Point(2, 167);
            this.m_lblTargetServers.Name = "m_lblTargetServers";
            this.m_lblTargetServers.Size = new System.Drawing.Size(104, 17);
            this.m_lblTargetServers.TabIndex = 14;
            this.m_lblTargetServers.Text = "Target Servers…";
            this.m_ToolTips.SetToolTip(this.m_lblTargetServers, "Enter your own choices of target \r\nservers for the ping tests.  ");
            // 
            // m_btnRestoreDefTargets
            // 
            this.m_btnRestoreDefTargets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnRestoreDefTargets.Location = new System.Drawing.Point(13, 279);
            this.m_btnRestoreDefTargets.Name = "m_btnRestoreDefTargets";
            this.m_btnRestoreDefTargets.Size = new System.Drawing.Size(347, 25);
            this.m_btnRestoreDefTargets.TabIndex = 13;
            this.m_btnRestoreDefTargets.Text = "Restore Default Target Servers";
            this.m_ToolTips.SetToolTip(this.m_btnRestoreDefTargets, resources.GetString("m_btnRestoreDefTargets.ToolTip"));
            this.m_btnRestoreDefTargets.UseVisualStyleBackColor = true;
            this.m_btnRestoreDefTargets.Click += new System.EventHandler(this.m_btnRestoreDefTargets_Click);
            // 
            // m_TargetServerControl3
            // 
            this.m_TargetServerControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_TargetServerControl3.IPAddress = "255.255.255.255";
            this.m_TargetServerControl3.Location = new System.Drawing.Point(14, 249);
            this.m_TargetServerControl3.Margin = new System.Windows.Forms.Padding(0);
            this.m_TargetServerControl3.Name = "m_TargetServerControl3";
            this.m_TargetServerControl3.Size = new System.Drawing.Size(346, 21);
            this.m_TargetServerControl3.TabIndex = 12;
            // 
            // m_TargetServerControl2
            // 
            this.m_TargetServerControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_TargetServerControl2.IPAddress = "255.255.255.255";
            this.m_TargetServerControl2.Location = new System.Drawing.Point(14, 221);
            this.m_TargetServerControl2.Margin = new System.Windows.Forms.Padding(0);
            this.m_TargetServerControl2.Name = "m_TargetServerControl2";
            this.m_TargetServerControl2.Size = new System.Drawing.Size(346, 21);
            this.m_TargetServerControl2.TabIndex = 11;
            // 
            // m_TargetServerControl1
            // 
            this.m_TargetServerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_TargetServerControl1.IPAddress = "255.255.255.255";
            this.m_TargetServerControl1.Location = new System.Drawing.Point(14, 193);
            this.m_TargetServerControl1.Margin = new System.Windows.Forms.Padding(0);
            this.m_TargetServerControl1.Name = "m_TargetServerControl1";
            this.m_TargetServerControl1.Size = new System.Drawing.Size(346, 21);
            this.m_TargetServerControl1.TabIndex = 10;
            // 
            // m_numTestInterval
            // 
            this.m_numTestInterval.Location = new System.Drawing.Point(78, 24);
            this.m_numTestInterval.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.m_numTestInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_numTestInterval.Name = "m_numTestInterval";
            this.m_numTestInterval.Size = new System.Drawing.Size(43, 20);
            this.m_numTestInterval.TabIndex = 6;
            this.m_numTestInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_ToolTips.SetToolTip(this.m_numTestInterval, "How frequently the ping \r\ntest should be performed.");
            this.m_numTestInterval.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.m_numTestInterval.ValueChanged += new System.EventHandler(this.m_numTestInterval_ValidateXX);
            this.m_numTestInterval.Leave += new System.EventHandler(this.m_numTestInterval_ValidateXX);
            // 
            // m_numPingResp
            // 
            this.m_numPingResp.Location = new System.Drawing.Point(131, 48);
            this.m_numPingResp.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.m_numPingResp.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.m_numPingResp.Name = "m_numPingResp";
            this.m_numPingResp.Size = new System.Drawing.Size(52, 20);
            this.m_numPingResp.TabIndex = 3;
            this.m_numPingResp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_ToolTips.SetToolTip(this.m_numPingResp, "How long this application should wait for a ping \r\nresponse before it is consider" +
        "ed a timeout/failure.");
            this.m_numPingResp.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // m_lblTestInterval2
            // 
            this.m_lblTestInterval2.AutoSize = true;
            this.m_lblTestInterval2.Location = new System.Drawing.Point(122, 27);
            this.m_lblTestInterval2.Name = "m_lblTestInterval2";
            this.m_lblTestInterval2.Size = new System.Drawing.Size(91, 13);
            this.m_lblTestInterval2.TabIndex = 7;
            this.m_lblTestInterval2.Text = "Seconds (1 to 60)";
            this.m_ToolTips.SetToolTip(this.m_lblTestInterval2, "How frequently the ping \r\ntest should be performed.");
            // 
            // m_lblPingResp2
            // 
            this.m_lblPingResp2.AutoSize = true;
            this.m_lblPingResp2.Location = new System.Drawing.Point(184, 51);
            this.m_lblPingResp2.Name = "m_lblPingResp2";
            this.m_lblPingResp2.Size = new System.Drawing.Size(130, 13);
            this.m_lblPingResp2.TabIndex = 5;
            this.m_lblPingResp2.Text = "Milliseconds (100 to 2000)";
            this.m_ToolTips.SetToolTip(this.m_lblPingResp2, "How long this application should wait for a ping \r\nresponse before it is consider" +
        "ed a timeout/failure.");
            // 
            // m_lblPingResp
            // 
            this.m_lblPingResp.AutoSize = true;
            this.m_lblPingResp.Location = new System.Drawing.Point(12, 50);
            this.m_lblPingResp.Name = "m_lblPingResp";
            this.m_lblPingResp.Size = new System.Drawing.Size(119, 13);
            this.m_lblPingResp.TabIndex = 4;
            this.m_lblPingResp.Text = "Wait for Ping Response";
            this.m_lblPingResp.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.m_ToolTips.SetToolTip(this.m_lblPingResp, "How long this application should wait for a ping \r\nresponse before it is consider" +
        "ed a timeout/failure.");
            // 
            // m_lblTestInterval
            // 
            this.m_lblTestInterval.AutoSize = true;
            this.m_lblTestInterval.Location = new System.Drawing.Point(12, 27);
            this.m_lblTestInterval.Name = "m_lblTestInterval";
            this.m_lblTestInterval.Size = new System.Drawing.Size(66, 13);
            this.m_lblTestInterval.TabIndex = 2;
            this.m_lblTestInterval.Text = "Test Interval";
            this.m_lblTestInterval.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.m_ToolTips.SetToolTip(this.m_lblTestInterval, "How frequently the ping \r\ntest should be performed.");
            // 
            // m_lblTestSettings
            // 
            this.m_lblTestSettings.AutoSize = true;
            this.m_lblTestSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblTestSettings.Location = new System.Drawing.Point(2, 2);
            this.m_lblTestSettings.Name = "m_lblTestSettings";
            this.m_lblTestSettings.Size = new System.Drawing.Size(95, 17);
            this.m_lblTestSettings.TabIndex = 1;
            this.m_lblTestSettings.Text = "Test Settings…";
            // 
            // m_lblStartupSettings
            // 
            this.m_lblStartupSettings.AutoSize = true;
            this.m_lblStartupSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblStartupSettings.Location = new System.Drawing.Point(2, 2);
            this.m_lblStartupSettings.Margin = new System.Windows.Forms.Padding(0);
            this.m_lblStartupSettings.Name = "m_lblStartupSettings";
            this.m_lblStartupSettings.Size = new System.Drawing.Size(115, 17);
            this.m_lblStartupSettings.TabIndex = 0;
            this.m_lblStartupSettings.Text = "Startup Settings…";
            // 
            // m_pnlStartup
            // 
            this.m_pnlStartup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_pnlStartup.Controls.Add(this.m_chkSystemTray);
            this.m_pnlStartup.Controls.Add(this.m_chkStartWithWindows);
            this.m_pnlStartup.Controls.Add(this.m_lblStartupSettings);
            this.m_pnlStartup.Location = new System.Drawing.Point(14, 14);
            this.m_pnlStartup.Name = "m_pnlStartup";
            this.m_pnlStartup.Size = new System.Drawing.Size(374, 70);
            this.m_pnlStartup.TabIndex = 0;
            // 
            // m_chkSystemTray
            // 
            this.m_chkSystemTray.AutoSize = true;
            this.m_chkSystemTray.Location = new System.Drawing.Point(219, 33);
            this.m_chkSystemTray.Name = "m_chkSystemTray";
            this.m_chkSystemTray.Size = new System.Drawing.Size(138, 17);
            this.m_chkSystemTray.TabIndex = 2;
            this.m_chkSystemTray.Text = "Start Minimized in Tray?";
            this.m_ToolTips.SetToolTip(this.m_chkSystemTray, "Start this application minimized\r\nin the system/notification tray");
            this.m_chkSystemTray.UseVisualStyleBackColor = true;
            // 
            // m_chkStartWithWindows
            // 
            this.m_chkStartWithWindows.AutoSize = true;
            this.m_chkStartWithWindows.Location = new System.Drawing.Point(16, 33);
            this.m_chkStartWithWindows.Name = "m_chkStartWithWindows";
            this.m_chkStartWithWindows.Size = new System.Drawing.Size(160, 17);
            this.m_chkStartWithWindows.TabIndex = 1;
            this.m_chkStartWithWindows.Text = "Start when Windows Starts?";
            this.m_ToolTips.SetToolTip(this.m_chkStartWithWindows, "Autostart this application\r\nupon login");
            this.m_chkStartWithWindows.UseVisualStyleBackColor = true;
            // 
            // m_pnlAlertLog
            // 
            this.m_pnlAlertLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_pnlAlertLog.Controls.Add(this.m_numLogFailure);
            this.m_pnlAlertLog.Controls.Add(this.m_grpReconnectSound);
            this.m_pnlAlertLog.Controls.Add(this.m_grpFailureAlertSound);
            this.m_pnlAlertLog.Controls.Add(this.m_grpLogFileOption);
            this.m_pnlAlertLog.Controls.Add(this.m_lblLogFailure2);
            this.m_pnlAlertLog.Controls.Add(this.m_lblLogFailure);
            this.m_pnlAlertLog.Controls.Add(this.m_chkPopup);
            this.m_pnlAlertLog.Controls.Add(this.m_lblAlertLogSettiings);
            this.m_pnlAlertLog.Location = new System.Drawing.Point(404, 14);
            this.m_pnlAlertLog.Name = "m_pnlAlertLog";
            this.m_pnlAlertLog.Size = new System.Drawing.Size(422, 447);
            this.m_pnlAlertLog.TabIndex = 2;
            // 
            // m_numLogFailure
            // 
            this.m_numLogFailure.Location = new System.Drawing.Point(188, 28);
            this.m_numLogFailure.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.m_numLogFailure.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_numLogFailure.Name = "m_numLogFailure";
            this.m_numLogFailure.Size = new System.Drawing.Size(40, 20);
            this.m_numLogFailure.TabIndex = 4;
            this.m_numLogFailure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_ToolTips.SetToolTip(this.m_numLogFailure, "Upon network failure after the specified time\r\nPerform the following actions as s" +
        "pecified:\r\n    • Popup this window \r\n    • Log the event\r\n    • Make a sound");
            this.m_numLogFailure.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.m_numLogFailure.ValueChanged += new System.EventHandler(this.m_numLogFailure_ValidateXX);
            this.m_numLogFailure.Leave += new System.EventHandler(this.m_numLogFailure_ValidateXX);
            // 
            // m_grpReconnectSound
            // 
            this.m_grpReconnectSound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_grpReconnectSound.Controls.Add(this.m_soundclipReconnect);
            this.m_grpReconnectSound.Location = new System.Drawing.Point(13, 308);
            this.m_grpReconnectSound.Name = "m_grpReconnectSound";
            this.m_grpReconnectSound.Size = new System.Drawing.Size(395, 125);
            this.m_grpReconnectSound.TabIndex = 12;
            this.m_grpReconnectSound.TabStop = false;
            this.m_grpReconnectSound.Text = "Reconnect Sound";
            // 
            // m_soundclipReconnect
            // 
            this.m_soundclipReconnect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_soundclipReconnect.BackColor = System.Drawing.Color.Transparent;
            this.m_soundclipReconnect.Location = new System.Drawing.Point(14, 21);
            this.m_soundclipReconnect.Name = "m_soundclipReconnect";
            this.m_soundclipReconnect.Size = new System.Drawing.Size(363, 89);
            this.m_soundclipReconnect.SoundClip = SoundClip.None;
            this.m_soundclipReconnect.TabIndex = 0;
            // 
            // m_grpFailureAlertSound
            // 
            this.m_grpFailureAlertSound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_grpFailureAlertSound.Controls.Add(this.m_soundclipAlert);
            this.m_grpFailureAlertSound.Location = new System.Drawing.Point(13, 175);
            this.m_grpFailureAlertSound.Name = "m_grpFailureAlertSound";
            this.m_grpFailureAlertSound.Size = new System.Drawing.Size(395, 125);
            this.m_grpFailureAlertSound.TabIndex = 10;
            this.m_grpFailureAlertSound.TabStop = false;
            this.m_grpFailureAlertSound.Text = "Failure Alert Sound";
            // 
            // m_soundclipAlert
            // 
            this.m_soundclipAlert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_soundclipAlert.BackColor = System.Drawing.Color.Transparent;
            this.m_soundclipAlert.Location = new System.Drawing.Point(14, 21);
            this.m_soundclipAlert.Name = "m_soundclipAlert";
            this.m_soundclipAlert.Size = new System.Drawing.Size(363, 89);
            this.m_soundclipAlert.SoundClip = SoundClip.None;
            this.m_soundclipAlert.TabIndex = 0;
            // 
            // m_grpLogFileOption
            // 
            this.m_grpLogFileOption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_grpLogFileOption.Controls.Add(this.m_txtLogFileLocation);
            this.m_grpLogFileOption.Controls.Add(this.m_btnLogFileLocation);
            this.m_grpLogFileOption.Controls.Add(this.m_radNoLogFile);
            this.m_grpLogFileOption.Controls.Add(this.m_radAppendLogFile);
            this.m_grpLogFileOption.Controls.Add(this.m_radNewLogFile);
            this.m_grpLogFileOption.Location = new System.Drawing.Point(13, 81);
            this.m_grpLogFileOption.Name = "m_grpLogFileOption";
            this.m_grpLogFileOption.Size = new System.Drawing.Size(395, 85);
            this.m_grpLogFileOption.TabIndex = 9;
            this.m_grpLogFileOption.TabStop = false;
            this.m_grpLogFileOption.Text = "Log File Option";
            // 
            // m_txtLogFileLocation
            // 
            this.m_txtLogFileLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtLogFileLocation.Location = new System.Drawing.Point(15, 53);
            this.m_txtLogFileLocation.Margin = new System.Windows.Forms.Padding(0);
            this.m_txtLogFileLocation.Name = "m_txtLogFileLocation";
            this.m_txtLogFileLocation.Size = new System.Drawing.Size(342, 20);
            this.m_txtLogFileLocation.TabIndex = 13;
            this.m_txtLogFileLocation.TextLabel = "Log File Location";
            // 
            // m_btnLogFileLocation
            // 
            this.m_btnLogFileLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnLogFileLocation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnLogFileLocation.Image = global::NetworkOnlineMonitor.Properties.Resources.SelectFolderBlue15;
            this.m_btnLogFileLocation.Location = new System.Drawing.Point(355, 53);
            this.m_btnLogFileLocation.Margin = new System.Windows.Forms.Padding(0);
            this.m_btnLogFileLocation.Name = "m_btnLogFileLocation";
            this.m_btnLogFileLocation.Size = new System.Drawing.Size(20, 20);
            this.m_btnLogFileLocation.TabIndex = 12;
            this.m_ToolTips.SetToolTip(this.m_btnLogFileLocation, "Browse for destination where\r\nthe log file will reside.");
            this.m_btnLogFileLocation.UseVisualStyleBackColor = true;
            this.m_btnLogFileLocation.Click += new System.EventHandler(this.m_btnLogFileLocation_Click);
            // 
            // m_radNoLogFile
            // 
            this.m_radNoLogFile.AutoSize = true;
            this.m_radNoLogFile.Location = new System.Drawing.Point(326, 22);
            this.m_radNoLogFile.Name = "m_radNoLogFile";
            this.m_radNoLogFile.Size = new System.Drawing.Size(51, 17);
            this.m_radNoLogFile.TabIndex = 2;
            this.m_radNoLogFile.TabStop = true;
            this.m_radNoLogFile.Text = "None";
            this.m_ToolTips.SetToolTip(this.m_radNoLogFile, "Disable logging altogether.");
            this.m_radNoLogFile.UseVisualStyleBackColor = true;
            this.m_radNoLogFile.CheckedChanged += new System.EventHandler(this.m_radLogFile_CheckedChanged);
            // 
            // m_radAppendLogFile
            // 
            this.m_radAppendLogFile.AutoSize = true;
            this.m_radAppendLogFile.Location = new System.Drawing.Point(172, 22);
            this.m_radAppendLogFile.Name = "m_radAppendLogFile";
            this.m_radAppendLogFile.Size = new System.Drawing.Size(114, 17);
            this.m_radAppendLogFile.TabIndex = 1;
            this.m_radAppendLogFile.TabStop = true;
            this.m_radAppendLogFile.Text = "Add to Existing File";
            this.m_ToolTips.SetToolTip(this.m_radAppendLogFile, "Always append to existing log file \r\nwith the name \"[exename]Log.txt\"");
            this.m_radAppendLogFile.UseVisualStyleBackColor = true;
            this.m_radAppendLogFile.CheckedChanged += new System.EventHandler(this.m_radLogFile_CheckedChanged);
            // 
            // m_radNewLogFile
            // 
            this.m_radNewLogFile.AutoSize = true;
            this.m_radNewLogFile.Location = new System.Drawing.Point(15, 22);
            this.m_radNewLogFile.Name = "m_radNewLogFile";
            this.m_radNewLogFile.Size = new System.Drawing.Size(117, 17);
            this.m_radNewLogFile.TabIndex = 0;
            this.m_radNewLogFile.TabStop = true;
            this.m_radNewLogFile.Text = "New File Each Run";
            this.m_ToolTips.SetToolTip(this.m_radNewLogFile, "Create a new log file every time upon startup of this application\r\nwith the name " +
        "\"[exename]Log yyyy-mm-dd hh.mm.ss.txt\"");
            this.m_radNewLogFile.UseVisualStyleBackColor = true;
            this.m_radNewLogFile.CheckedChanged += new System.EventHandler(this.m_radLogFile_CheckedChanged);
            // 
            // m_lblLogFailure2
            // 
            this.m_lblLogFailure2.AutoSize = true;
            this.m_lblLogFailure2.Location = new System.Drawing.Point(229, 31);
            this.m_lblLogFailure2.Name = "m_lblLogFailure2";
            this.m_lblLogFailure2.Size = new System.Drawing.Size(94, 13);
            this.m_lblLogFailure2.TabIndex = 5;
            this.m_lblLogFailure2.Text = "Seconds  (1 to 60)";
            this.m_ToolTips.SetToolTip(this.m_lblLogFailure2, "Upon network failure after the specified time\r\nPerform the following actions as s" +
        "pecified:\r\n    • Popup this window \r\n    • Log the event\r\n    • Make a sound");
            // 
            // m_lblLogFailure
            // 
            this.m_lblLogFailure.AutoSize = true;
            this.m_lblLogFailure.Location = new System.Drawing.Point(12, 31);
            this.m_lblLogFailure.Name = "m_lblLogFailure";
            this.m_lblLogFailure.Size = new System.Drawing.Size(177, 13);
            this.m_lblLogFailure.TabIndex = 3;
            this.m_lblLogFailure.Text = "Alert and Log Failure If Longer Than";
            this.m_lblLogFailure.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.m_ToolTips.SetToolTip(this.m_lblLogFailure, "Upon network failure after the specified time\r\nPerform the following actions as s" +
        "pecified:\r\n    • Popup this window \r\n    • Log the event\r\n    • Make a sound");
            // 
            // m_chkPopup
            // 
            this.m_chkPopup.AutoSize = true;
            this.m_chkPopup.Location = new System.Drawing.Point(14, 53);
            this.m_chkPopup.Margin = new System.Windows.Forms.Padding(0);
            this.m_chkPopup.Name = "m_chkPopup";
            this.m_chkPopup.Size = new System.Drawing.Size(117, 17);
            this.m_chkPopup.TabIndex = 2;
            this.m_chkPopup.Text = "Pop Up on Failure?";
            this.m_ToolTips.SetToolTip(this.m_chkPopup, "Popup this window when if ping timeouts \r\nexceed the following failure time.");
            this.m_chkPopup.UseVisualStyleBackColor = true;
            // 
            // m_lblAlertLogSettiings
            // 
            this.m_lblAlertLogSettiings.AutoSize = true;
            this.m_lblAlertLogSettiings.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblAlertLogSettiings.Location = new System.Drawing.Point(2, 2);
            this.m_lblAlertLogSettiings.Name = "m_lblAlertLogSettiings";
            this.m_lblAlertLogSettiings.Size = new System.Drawing.Size(138, 17);
            this.m_lblAlertLogSettiings.TabIndex = 1;
            this.m_lblAlertLogSettiings.Text = "Alert && Log Settings…";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 475);
            this.Controls.Add(this.m_pnlAlertLog);
            this.Controls.Add(this.m_pnlTest);
            this.Controls.Add(this.m_pnlStartup);
            this.Icon = global::NetworkOnlineMonitor.Properties.Resources.favicon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(856, 468);
            this.Name = "FormSettings";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.m_pnlTest.ResumeLayout(false);
            this.m_pnlTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numTestInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numPingResp)).EndInit();
            this.m_pnlStartup.ResumeLayout(false);
            this.m_pnlStartup.PerformLayout();
            this.m_pnlAlertLog.ResumeLayout(false);
            this.m_pnlAlertLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numLogFailure)).EndInit();
            this.m_grpReconnectSound.ResumeLayout(false);
            this.m_grpFailureAlertSound.ResumeLayout(false);
            this.m_grpLogFileOption.ResumeLayout(false);
            this.m_grpLogFileOption.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_pnlTest;
        private System.Windows.Forms.Label m_lblStartupSettings;
        private System.Windows.Forms.Panel m_pnlStartup;
        private System.Windows.Forms.Label m_lblTestSettings;
        private System.Windows.Forms.Panel m_pnlAlertLog;
        private System.Windows.Forms.Label m_lblAlertLogSettiings;
        private System.Windows.Forms.CheckBox m_chkSystemTray;
        private System.Windows.Forms.CheckBox m_chkStartWithWindows;
        private System.Windows.Forms.Label m_lblTestInterval2;
        private System.Windows.Forms.NumericUpDown m_numTestInterval;
        private System.Windows.Forms.Label m_lblPingResp2;
        private System.Windows.Forms.Label m_lblPingResp;
        private System.Windows.Forms.NumericUpDown m_numPingResp;
        private System.Windows.Forms.Label m_lblTestInterval;
        private System.Windows.Forms.Label m_lblLogFailure2;
        private System.Windows.Forms.NumericUpDown m_numLogFailure;
        private System.Windows.Forms.Label m_lblLogFailure;
        private System.Windows.Forms.CheckBox m_chkPopup;
        private System.Windows.Forms.GroupBox m_grpFailureAlertSound;
        private System.Windows.Forms.GroupBox m_grpLogFileOption;
        private System.Windows.Forms.RadioButton m_radAppendLogFile;
        private System.Windows.Forms.RadioButton m_radNewLogFile;
        private System.Windows.Forms.GroupBox m_grpReconnectSound;
        private System.Windows.Forms.ToolTip m_ToolTips;
        private System.Windows.Forms.RadioButton m_radNoLogFile;
        private System.Windows.Forms.Label m_lblTargetServers;
        private System.Windows.Forms.Button m_btnRestoreDefTargets;
        private TargetServerControl m_TargetServerControl3;
        private TargetServerControl m_TargetServerControl2;
        private TargetServerControl m_TargetServerControl1;
        private ChuckHill2.Forms.LabeledTextBox m_txtLogFileLocation;
        private System.Windows.Forms.Button m_btnLogFileLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label m_txtLanIPAddress;
        private System.Windows.Forms.Label m_txtComputerName;
        private System.Windows.Forms.Label m_lblGateayIPAddress;
        private System.Windows.Forms.Label m_lblLanIPAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label m_txtGatewayIPAddress;
        private SoundClipCtrl m_soundclipAlert;
        private SoundClipCtrl m_soundclipReconnect;
    }
}
