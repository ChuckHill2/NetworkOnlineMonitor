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
        private bool WroteSoftDivider = true; //flag to disallow multiple SoftDividers to be written consecutively.
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

            //Write the starting hard divider
            var msg = $"= {Path.GetFileNameWithoutExtension(StaticTools.ExecutableName)} Log {DateTime.Now.ToString("g")} =";
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
            lock (stream)
            {
                //Remove disposing events
                AppDomain.CurrentDomain.DomainUnload -= CurrentDomain_DisposeLog;
                AppDomain.CurrentDomain.ProcessExit -= CurrentDomain_DisposeLog;
                AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_DisposeLog;

                //Close the log editor if it is open
                EditDialog?.Close();
                EditDialog = null;

                //Write the ending hard divider and close the log.
                var msg = "==== End of Log ";
                msg = msg + new string('=', HeaderLength - msg.Length);
                stream.WriteLine(msg);
                stream.Close();
            }
            stream = null;
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
            lock (stream)
            {
                if (stream == null) return;
                WroteSoftDivider = false;
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
