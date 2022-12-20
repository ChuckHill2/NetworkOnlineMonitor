using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NetworkOnlineMonitor
{
    public partial class FormAbout : Form
    {
        public new static void Show(IWin32Window owner)
        {
            using (var dialog = new FormAbout())
            {
                dialog.ShowDialog(owner);
            }
        }

        private static readonly WeakReference<string> AboutInfo = new WeakReference<string>(null);
        private static Rectangle FormBounds = Rectangle.Empty;  //remember size and position on screen.

        private FormAbout()
        {
            InitializeComponent();
            this.FormClosing += (s, e) => FormBounds = this.Bounds;
            if (FormBounds != Rectangle.Empty)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Size = FormBounds.Size;
                this.Location = FormBounds.Location;
            }

            if (!AboutInfo.TryGetTarget(out string aboutInfo))
            {
                Assembly asm = Assembly.GetEntryAssembly();
                var config = Attribute<AssemblyConfigurationAttribute>(asm);
                config = config.Equals("DEBUG", StringComparison.CurrentCultureIgnoreCase) ? " (DEBUG)" : String.Empty;

                aboutInfo = Regex.Replace(global::NetworkOnlineMonitor.Properties.Resources.AboutInfo, @"\[VERSION\]|\[BUILDDATE\]", m =>
                {
                    if (m.Value == "[VERSION]") return $"Version {asm.GetName().Version}{config}";
                    if (m.Value == "[BUILDDATE]") return $"Build Date {ExecutableTimestamp(asm.Location):g}";
                    return "Ack! Should not get here!";
                });

                AboutInfo.SetTarget(aboutInfo);
            }

            m_wbDocument.DocumentText = aboutInfo;
        }

        //Goto link in a new browser window.
        private void m_wbDocument_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            var url = e.Url.AbsoluteUri;
            if (url.Equals("about:blank",StringComparison.CurrentCultureIgnoreCase)) return;
            e.Cancel = true;
            Process.Start(url);
        }

        private static string Attribute<T>(Assembly asm) where T : Attribute
        {
            foreach (var data in asm.CustomAttributes)
            {
                if (typeof(T) != data.AttributeType) continue;
                if (data.ConstructorArguments.Count > 0) return data.ConstructorArguments[0].Value.ToString();
                break;
            }
            return string.Empty;
        }

        private static DateTime ExecutableTimestamp(string filePath)
        {
            const int IMAGE_DOS_SIGNATURE = 0x5A4D;  // 'MZ'
            const int IMAGE_NT_SIGNATURE = 0x00004550;  // 'PE00'
            const int MIN_EXE_SIZE = 1024 * 5;  //this has been empirically derived.

            uint TimeDateStamp = 0;
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if (stream.Length < MIN_EXE_SIZE) return File.GetCreationTime(filePath);
                var reader = new BinaryReader(stream);
                stream.Position = 0;  //starts with struct IMAGE_DOS_HEADER 
                if (reader.ReadInt16() != IMAGE_DOS_SIGNATURE) return File.GetCreationTime(filePath);

                stream.Seek(64 - 4, SeekOrigin.Begin); //read last field IMAGE_DOS_HEADER.e_lfanew. This is the offset where the IMAGE_NT_HEADER begins
                int offset = reader.ReadInt32();
                stream.Seek(offset, SeekOrigin.Begin);
                if (reader.ReadInt32() != IMAGE_NT_SIGNATURE) return File.GetCreationTime(filePath);

                stream.Position += 4; //offset of IMAGE_FILE_HEADER.TimeDateStamp
                TimeDateStamp = reader.ReadUInt32(); //unix-style time_t value
            }

            DateTime returnValue = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(TimeDateStamp);
            returnValue = returnValue.ToLocalTime();

            if (returnValue < new DateTime(2000, 1, 1) || returnValue > DateTime.Now)
            {
                //PEHeader link timestamp field is random junk because csproj property "Deterministic" == true
                //so we just return the 2nd best "build" time (iffy, unreliable).
                return File.GetCreationTime(filePath);
            }

            return returnValue;
        }
    }
}
