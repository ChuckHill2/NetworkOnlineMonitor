using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChuckHill2.Forms;
using System.Xml.Serialization;

namespace NetworkOnlineMonitor
{
    public partial class FormSettings : Form
    {
        private Settings OldSettings;
        private CaptionBarCloseButton CloseButtonHandler;

        public static Settings Show(IWin32Window owner, Settings settings)
        {
            using(var dialog = new FormSettings(settings))
            {
                dialog.ShowDialog(owner);
                var newsettings = dialog.GetNewSettings();
                return settings.Equals(newsettings) ? null : newsettings;
            }
        }

        private FormSettings(Settings settings)
        {
            OldSettings = settings;
            InitializeComponent();

            CloseButtonHandler = new CaptionBarCloseButton(this, () => this.Close(), "Save and exit.");

            m_chkStartWithWindows.Checked = StaticTools.AutostartExists(); //not stored in Settings

            m_chkSystemTray.Checked = settings.StartMinimized;
            m_numTestInterval.Value = settings.TestInterval;
            m_numTestInterval.Tag = settings.TestInterval; //for undo
            m_numPingResp.Value = settings.PingTimeout;

            var lanInfo = StaticTools.GetLanInfo();
            m_txtComputerName.Text = lanInfo.ComputerName;
            m_txtLanIPAddress.Text = lanInfo.ComputerAddress;
            m_txtGatewayIPAddress.Text = lanInfo.GatewayAddess;

            if (settings.Targets.Length > 0) m_TargetServerControl1.Value = settings.Targets[0];
            if (settings.Targets.Length > 1) m_TargetServerControl2.Value = settings.Targets[1];
            if (settings.Targets.Length > 2) m_TargetServerControl3.Value = settings.Targets[2];

            m_chkPopup.Checked = settings.PopUpOnFailure;
            m_numLogFailure.Value = settings.OfflineTrigger;
            m_numLogFailure.Tag = settings.OfflineTrigger; //for undo

            m_radNewLogFile.Checked = settings.LogFileOption == LogFileOption.CreateNew;
            m_radAppendLogFile.Checked = settings.LogFileOption == LogFileOption.Append;
            m_radNoLogFile.Checked = settings.LogFileOption == LogFileOption.None;
            if (!m_radNoLogFile.Checked) m_txtLogFileLocation.Text = settings.LogFileFolder;

            m_soundclipAlert.SoundClip = settings.AlertSoundClip;
            m_soundclipReconnect.SoundClip = settings.ReconnectSoundClip;
        }

        private Settings GetNewSettings()
        {
            var settings = new Settings();

            StaticTools.SetAutoStart(m_chkStartWithWindows.Checked); //not stored in Settings

            settings.StartMinimized = m_chkSystemTray.Checked;
            settings.TestInterval = (int)m_numTestInterval.Value;
            settings.PingTimeout = (int)m_numPingResp.Value;

            settings.Targets = new Settings.TargetIP[]
            {
                m_TargetServerControl1.Value,
                m_TargetServerControl2.Value,
                m_TargetServerControl3.Value
            };

            settings.PopUpOnFailure = m_chkPopup.Checked;
            settings.OfflineTrigger = (int)m_numLogFailure.Value;

            if (m_radNewLogFile.Checked) settings.LogFileOption = LogFileOption.CreateNew;
            if (m_radAppendLogFile.Checked) settings.LogFileOption = LogFileOption.Append;
            if (m_radNoLogFile.Checked) settings.LogFileOption = LogFileOption.None;
            if (!m_radNoLogFile.Checked) settings.LogFileFolder = m_txtLogFileLocation.Text.Trim();

            settings.AlertSoundClip = m_soundclipAlert.SoundClip;
            settings.ReconnectSoundClip = m_soundclipReconnect.SoundClip;

            return settings == OldSettings ? null : settings;
        }

        private void m_radLogFile_CheckedChanged(object sender, EventArgs e)
        {
            m_txtLogFileLocation.Enabled = sender != m_radNoLogFile;
            m_btnLogFileLocation.Enabled = sender != m_radNoLogFile;
        }

        private void m_btnRestoreDefTargets_Click(object sender, EventArgs e)
        {
            var s = new Settings();
            if (s.Targets.Length > 0) m_TargetServerControl1.Value = s.Targets[0];
            if (s.Targets.Length > 1) m_TargetServerControl2.Value = s.Targets[1];
            if (s.Targets.Length > 2) m_TargetServerControl3.Value = s.Targets[2];
        }

        private void m_btnLogFileLocation_Click(object sender, EventArgs e)
        {
            var folder = FolderSelectDialog.Show(this, "Select Log File Location", m_txtLogFileLocation.Text);
            if (folder != null) m_txtLogFileLocation.Text = folder;
        }

        private void m_numLogFailure_ValidateXX(object sender, EventArgs e)
        {
            if (m_numLogFailure.Value < m_numTestInterval.Value)
            {
                MiniMessageBox.ShowDialog(m_numLogFailure, "This cannot be smaller than \"Test Interval\".", "Alert and Log Failure", MiniMessageBox.Buttons.OK, MiniMessageBox.Symbol.Warning);
                m_numLogFailure.Value = (int)m_numTestInterval.Tag;
                return;
            }
            m_numLogFailure.Tag = (int)m_numLogFailure.Value; //save for undo
        }

        private void m_numTestInterval_ValidateXX(object sender, EventArgs e)
        {
            if (m_numTestInterval.Value > m_numLogFailure.Value)
            {
                MiniMessageBox.ShowDialog(m_numTestInterval, "This cannot be larger than \"Alert and Log Failure\".", "Test Interval", MiniMessageBox.Buttons.OK, MiniMessageBox.Symbol.Warning);
                m_numTestInterval.Value = (int)m_numTestInterval.Tag;
                return;
            }
            m_numTestInterval.Tag = (int)m_numTestInterval.Value; //save for undo
        }
    }
}
