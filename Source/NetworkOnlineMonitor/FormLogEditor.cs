using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NetworkOnlineMonitor
{
    //Exclusivly called an 'owned' by FileLogging.LogEdit() as FileLogging.WriteLine also calls FormLogEdit.AppendLine in order to keep them both synchronized.
    public partial class FormLogEditor : Form
    {
        private static Rectangle FormBounds;

        private string LogFile;
        private bool Changed = false;

        public FormLogEditor(string logFile)
        {
            InitializeComponent();
            LogFile = logFile;

            this.Text = "Edit Log " + Path.GetFileName(logFile);

            if (FormBounds.IsEmpty)
            {
                this.StartPosition = FormStartPosition.CenterParent;
            }
            else
            {
                this.StartPosition = FormStartPosition.Manual;
                this.DesktopBounds = FormBounds;
            }

            using (var fs = new FileStream(LogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 4096, FileOptions.SequentialScan))
            using (var sr = new StreamReader(fs))
            {
                m_txtLog.Text = sr.ReadToEnd();
            }
            m_txtLog.SelectionStart = m_txtLog.MaxLength;

            m_txtLog.TextChanged += (s, e) =>
            {
                this.Text = "*Edit Log " + Path.GetFileName(logFile);
                Changed = true;
            };
        }
        protected override void OnShown(EventArgs e) => m_txtLog.ScrollToCaret();
        protected override void OnFormClosed(FormClosedEventArgs e) => this.Dispose();
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            FormBounds = this.DesktopBounds;

            if (Changed)
                using (var fs = new FileStream(LogFile, FileMode.Open, FileAccess.Write, FileShare.ReadWrite, 4096, FileOptions.SequentialScan))
                using (var sw = new StreamWriter(fs, System.Text.Encoding.UTF8, 1024, false))
                    sw.Write(m_txtLog.Text);
        }

        public void AppendLine(string s)
        {
            if (s == null) return;
            if (this.InvokeRequired)
            {
                this.BeginInvoke((Action)(()=>AppendLine(s)));
                return;
            }

            int prevSelectionStart = m_txtLog.SelectionStart;
            int prevSelectionLength = m_txtLog.SelectionLength;
            bool prevChanged = Changed; 

            m_txtLog.SelectionStart = m_txtLog.MaxLength;
            m_txtLog.AppendText(s);
            if (s.Length < 1 || s[s.Length - 1] != '\n') m_txtLog.AppendText(Environment.NewLine);

            m_txtLog.Select(prevSelectionStart, prevSelectionLength);
            Changed = prevChanged; //Don't need to mark as 'changed' because this is also being written to the live log file.
        }
    }
}
