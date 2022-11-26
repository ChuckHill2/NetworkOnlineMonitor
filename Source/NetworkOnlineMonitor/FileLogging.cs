//--------------------------------------------------------------------------
// <summary>
//   
// </summary>
// <copyright file="XFileLogging.cs" company="Chuck Hill">
// Copyright (c) 2020 Chuck Hill.
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public License
// as published by the Free Software Foundation; either version 2.1
// of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// The GNU Lesser General Public License can be viewed at
// http://www.opensource.org/licenses/lgpl-license.php. If
// you unfamiliar with this license or have questions about
// it, here is an http://www.gnu.org/licenses/gpl-faq.html.
//
// All code and executables are provided "as is" with no warranty
// either express or implied. The author accepts no liability for
// any damage or loss of business that this product may cause.
// </copyright>
// <repository>https://github.com/ChuckHill2/ChuckHill2.Utilities</repository>
// <author>Chuck Hill</author>
//--------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.IO;

namespace NetworkOnlineMonitor
{
    /// <summary>
    /// Perform Developer logging via a instance or static calls. May be used in a multi-threadded environment.
    /// </summary>
    /// <remarks>
    /// This class has been named XFileLogging because the name 'FileLogging' is all too common!!
    /// </remarks>
    [Serializable]
    public class XFileLogging: MarshalByRefObject, IDisposable
    {
        private StreamWriter stream;
        public readonly string OutputFile;
        public readonly bool Append;
        private int HeaderLength;

        public XFileLogging(string outputFile=null, bool append=false)
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

            var msg = $"==== {Path.GetFileNameWithoutExtension(StaticTools.ExecutableName)} Log {DateTime.Now.ToString("g")} ====";
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
                stream.WriteLine(msg);
                //Always flush to disk so external editing is always current.
                stream.Flush();
                stream.BaseStream.FlushAsync();
            }
        }
   }
}
