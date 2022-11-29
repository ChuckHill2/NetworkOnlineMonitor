using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace NetworkOnlineMonitor
{
    public enum LogFileOption 
    { 
        /// <summary>
        /// File logging is disabled.
        /// </summary>
        None,
        /// <summary>
        /// A new log file every time the application is started. The filename is hardcoded as  "[exename]Log yyyy-mm-dd hh.mm.ss.txt"
        /// </summary>
        CreateNew,
        /// <summary>
        /// A single log file is appended to when the application is started. The filename is hardcoded as  "[exename]Log.txt"
        /// </summary>
        Append
    }

    [XmlInclude(typeof(LogFileOption))]
    [XmlInclude(typeof(SoundClip))]
    public class Settings: IEquatable<Settings>
    {
        private static readonly string XmlPath = Path.ChangeExtension(StaticTools.ExecutableName, ".xml"); //For serialization/deserialization.

        /// <summary>
        /// Position of window on the screen. This is the saved location from the previous instance.
        /// </summary>
        [XmlElement(Order = 1)]
        public MyPoint MainFormLocation { get; set; } = Point.Empty;

        /// <summary>
        /// Automatically start minimized to the system tray.
        /// </summary>
        [XmlElement(Order = 3), DefaultValue(false)] 
        public bool StartMinimized { get; set; } = false;

        /// <summary>
        /// Run a ping test every this many seconds.
        /// </summary>
        [XmlElement(Order = 4), DefaultValue(5)]
        public int TestInterval { get; set; } = 5;  //seconds

        /// <summary>
        /// How long to wait for a ping response before giving up in ms.
        /// </summary>
        [XmlElement(Order = 5), DefaultValue(200)] 
        public int PingTimeout { get; set; } = 200; //ms

        /// <summary>
        /// The array of the 3 ping targets containing the name associated IPv4 address.
        /// </summary>
        [XmlArray("Targets", Order = 6), XmlArrayItem("Target")]
        public TargetIP[] Targets;

        /// <summary>
        /// Popup the window when a fault (network down) lasts longer than 'LogMinLength'.
        /// </summary>
        [XmlElement(Order = 7), DefaultValue(false)]
        public bool PopUpOnFailure { get; set; } = false;

        /// <summary>
        /// Show and log the fault  (network down) if the fault lasts longer than this amount of time (seconds)
        /// </summary>
        [XmlElement(Order = 8), DefaultValue(5)]
        public int OfflineTrigger { get; set; } = 5; //seconds

        /// <summary>
        /// The folder where the log file is to reside. The name part is hardcoded to "[exename]Log*.txt".
        /// </summary>
        [XmlElement(Order = 9), DefaultValue("")]
        public string LogFileFolder
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_LogFileFolder) || !Directory.Exists(_LogFileFolder))
                    _LogFileFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                return _LogFileFolder;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !Directory.Exists(value)) return;
                _LogFileFolder = value;
            }
        }
        private string _LogFileFolder;

        /// <summary>
        /// Specifies how the Log file should be opened.
        /// </summary>
        [XmlElement(Order = 10), DefaultValue(LogFileOption.CreateNew)]
        public LogFileOption LogFileOption { get; set; } = LogFileOption.CreateNew;

        /// <summary>
        /// Get the full logging file path based upon 'LogFileOption' and 'LogFileFolder'.
        /// The full path is [LogFileFolder]\[this exe name]Log*.txt.
        /// </summary>
        [XmlIgnore]
        public string LogFilePath
        {
            get
            {
                if (LogFileOption== LogFileOption.CreateNew)
                    return Path.Combine(LogFileFolder, $"{Path.GetFileNameWithoutExtension(XmlPath)}Log {DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss")}.txt");

                if (LogFileOption == LogFileOption.Append)
                    return Path.Combine(LogFileFolder, $"{Path.GetFileNameWithoutExtension(XmlPath)}Log.txt");

                return string.Empty;
            }
        }

        /// <summary>
        /// Sound clip to play when network dropped/offline.
        /// </summary>
        [XmlElement(Order = 11)]
        public SoundClip AlertSoundClip { get; set; }

        /// <summary>
        /// Sound clip to play when network reconnected
        /// </summary>
        [XmlElement(Order = 12)]
        public SoundClip ReconnectSoundClip { get; set; }

        /// <summary>
        /// Parameterless constructor for xml deserialization with default values.
        /// </summary>
        public Settings()
        {
            //Add default ping targets. Will be overwritten if user has changed them in the Settings
            Targets = new TargetIP[]
            {
                new TargetIP("Google", "8.8.8.8"),
                new TargetIP("Level3", "4.2.2.2"),
                new TargetIP("Cloudflare", "1.1.1.1")
            };
            AlertSoundClip = SoundClip.None;
            ReconnectSoundClip = SoundClip.None;
        }

        public bool Equals(Settings other)
        {
            if (other == null) return false;

            //Do not compare this.MainFormLocation as this is only set during serialization.
            //if (!this.MainFormLocation.Equals(other.MainFormLocation)) return false;
            if (this.StartMinimized != other.StartMinimized) return false;
            if (this.TestInterval != other.TestInterval) return false;
            if (this.PingTimeout != other.PingTimeout) return false;
            if (!this.Targets.SequenceEqual(other.Targets)) return false;
            if (this.PopUpOnFailure != other.PopUpOnFailure) return false;
            if (this.OfflineTrigger != other.OfflineTrigger) return false;
            if (this.LogFileFolder != other.LogFileFolder) return false;
            if (this.LogFileOption != other.LogFileOption) return false;
            if (!this.AlertSoundClip.Equals(other.AlertSoundClip)) return false;
            if (!this.ReconnectSoundClip.Equals(other.ReconnectSoundClip)) return false;
            return true;
        }
        public override bool Equals(object obj) => Equals(obj as Settings);
        public override int GetHashCode() => base.GetHashCode(); //override as Visual Studio demands it, but not really used.

        #region Serialization/Deserialization

        /// <summary>
        /// XML Serialize this settings object into xml file located in the same directory and same name as this executable.
        /// </summary>
        public void Serialize()
        {
            //Must be called in the FormClosing event or earlier, not the FormClosed event. By the FormClosed event, the Form/Window is already gone.
            FormCollection fc = System.Windows.Forms.Application.OpenForms;
            if (fc != null && fc.Count > 0)
            {
                Form owner = fc[0];
                if (owner.WindowState != FormWindowState.Minimized)
                {
                    this.MainFormLocation = owner.Bounds.Location;
                }
            }

            using (var fs = File.Open(XmlPath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
            {
                new XmlSerializer(typeof(Settings)).Serialize(fs, this);
            }
        }

        /// <summary>
        /// Deserialize xml settings file that is located in the same directory and same name as this executable.
        /// </summary>
        /// <param name="Log">Optional log to write any deserialization errors to.</param>
        /// <returns>Deserialized settings object</returns>
        public static Settings Deserialize(FileLogging Log=null)
        {
            if (!File.Exists(XmlPath)) return new Settings();
            var xs = new XmlSerializer(typeof(Settings));
            try
            {
                var readersettings = new XmlReaderSettings();
                readersettings.CloseInput = true;
                readersettings.IgnoreComments = true;
                readersettings.IgnoreProcessingInstructions = true;
                readersettings.IgnoreWhitespace = true;
                using (var reader = XmlReader.Create(XmlPath, readersettings))
                {
                    var settings = (Settings)xs.Deserialize(reader);
                    return settings;
                }
            }
            catch (Exception ex)
            {
                var msg = $"Settings Deserialize: {ex.GetBaseException().GetType().Name}: {ex.GetBaseException().Message}";
                Log?.WriteLine(msg);
                Debug.WriteLine(msg);
                return new Settings();
            }
        }
        #endregion

        /// <summary>
        /// Xml serializable friendly name/IPAddress pair for pinging.
        /// </summary>
        public class TargetIP : IEquatable<TargetIP>
        {
            /// <summary>
            /// The user-supplied name for this IPv4 address.
            /// </summary>
            [XmlAttribute] 
            public string Name { get; set; } = "Loopback";

            /// <summary>
            /// IPv4 address as a string.
            /// <remarks>
            /// Type IPAddress is not recognized by the XMLSerializer. Didn't want this property, but necessary for type conversion of IPAddress for serialization.
            /// </remarks>
            /// </summary>
            [XmlAttribute("Address")]
            public string AddressStr
            {
                get => Address.ToString();
                set => Address = IPAddress.TryParse(value??"127.0.0,1", out IPAddress a) ? a : IPAddress.Loopback;
            }

            /// <summary>
            /// The IP address in the .NET IPAddress form. This is the backing store for 'AddressStr'.
            /// </summary>
            [XmlIgnore] 
            public IPAddress Address { get; set; } = IPAddress.Loopback;

            public TargetIP() { } //parameterless constructor for xml serialization
            public TargetIP(string name, string ipaddr)
            {
                Name = name ?? string.Empty;
                AddressStr = ipaddr;
            }

            public override string ToString() => $"{AddressStr} - {Name}";
            public override bool Equals(object obj)=> this.Equals(obj as TargetIP);
            public override int GetHashCode() => Name.GetHashCode() ^ AddressStr.GetHashCode();
            public bool Equals(TargetIP other)
            {
                if (other == null) return false;
                if (this.Name != other.Name) return false;
                if (this.AddressStr != other.AddressStr) return false;
                return true;
            }
        }

        /// <summary>
        /// Lightweight Point object that is XML serializable. Will implicitly cast to/from System.Drawing.Point.
        /// </summary>
        public struct MyPoint : IEquatable<MyPoint>
        {
            [XmlAttribute] public int X { get; set; }
            [XmlAttribute] public int Y { get; set; }

            public MyPoint(int x, int y) { X = x; Y = y; }
            public MyPoint(Point pt) { X = pt.X; Y = pt.Y; }

            public static implicit operator MyPoint(Point p) => new MyPoint(p.X, p.Y);
            public static implicit operator Point(MyPoint p) => new Point(p.X, p.Y);
            public override string ToString() => $"{{X={X},Y={Y}}}";
            public override bool Equals(object obj)
            {
                if (obj == null || !(obj is MyPoint)) return false;
                return this.Equals((MyPoint)obj);
            }
            public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();
            public bool Equals(MyPoint other)
            {
                if (this.X != other.X) return false;
                if (this.Y != other.Y) return false;
                return true;
            }
        }
    }
}
