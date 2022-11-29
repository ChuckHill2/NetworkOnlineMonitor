using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ChuckHill2.Forms;

namespace NetworkOnlineMonitor
{
    /// <summary>
    /// Perform Developer logging via a instance or static calls. May be used in a multi-threadded environment.
    /// </summary>
    /// <remarks>
    /// This class has been named XFileLogging because the name 'FileLogging' is all too common!!
    /// </remarks>
    [Serializable]
    public class FileLogging: MarshalByRefObject, IDisposable
    {
        private StreamWriter stream;
        public readonly string OutputFile;
        public readonly bool Append;
        private int HeaderLength;
        private FormLogEditor EditDialog = null;

        public FileLogging(string outputFile=null, bool append=false)
        {
            OutputFile = outputFile;
            Append = append;

            if (string.IsNullOrWhiteSpace(outputFile)) return;

            //If file is too big, rename the old one and start a new one.
            var fi = new FileInfo(outputFile);
            if (fi.Exists && fi.Length > 1024*1024*100) // >100MB
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
            stream = new StreamWriter(fs, System.Text.Encoding.UTF8, 1024, false);

            var msg = $"{(fs.Length == 0 ? string.Empty : Environment.NewLine)}==== {Path.GetFileNameWithoutExtension(StaticTools.ExecutableName)} Log {DateTime.Now.ToString("g")} ====";
            HeaderLength = msg.Length;
            stream.WriteLine(msg);
            stream.Flush();
            stream.BaseStream.FlushAsync();

            //Handle if something bad happens.
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_DomainUnload;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_DomainUnload;
        }

        private void CurrentDomain_DomainUnload(object sender, EventArgs e) => Dispose();

        /// <summary>
        /// Flush and close the log file. If not used, it will be safely closed upon exit.
        /// </summary>
        public void Dispose()
        {
            if (stream == null) return;
            lock (stream)
            {
                EditDialog?.Close();
                EditDialog = null;
                var msg = "==== End of Log ";
                msg = msg + new string('=', HeaderLength - msg.Length);
                stream.WriteLine(msg);
                AppDomain.CurrentDomain.DomainUnload -= CurrentDomain_DomainUnload;
                AppDomain.CurrentDomain.ProcessExit -= CurrentDomain_DomainUnload;
                AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_DomainUnload;
                stream.Close();
            }
            stream = null;
        }

        /// <summary>
        /// Determine if this instance has already been disposed.
        /// </summary>
        public bool IsDisposed => stream == null;

        /// <summary>
        /// Write a message to the log file.
        /// </summary>
        /// <param name="msg">the message string to write (using string interpolation?)</param>
        public void WriteLine(string msg)
        {
            if (msg == null) return;
            if (stream == null) return;
            lock (stream)
            {
                if (stream == null) return;
                if (EditDialog != null && !EditDialog.IsDisposed) EditDialog.AppendLine(msg);
                stream.BaseStream.Seek(0, SeekOrigin.End); //Incase someone edited the file, externally (not FormLogEditor).
                stream.WriteLine(msg);
                //Always flush to disk so external editing is always current.
                stream.Flush();
                stream.BaseStream.FlushAsync();
            }
        }

        public void LogEdit()
        {
            if (EditDialog != null && !EditDialog.IsDisposed)
            {
                //MiniMessageBox.ShowDialog(EditDialog.Owner, "Log Editor is already open.", "Log Edit", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                EditDialog.Focus();
                return;
            }

            //Show editor dialog as modalless (i.e. returns immediately) and owned by the form that called this method (e.g. FormMain)
            //It is closed by the editor dialog close button (e.g. the user) or automatically when the log file is closed.

            EditDialog = new FormLogEditor(OutputFile);
            EditDialog.Show(Application.OpenForms[Application.OpenForms.Count - 1]);
        }
   }
}
