using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using ChuckHill2.Forms;

namespace NetworkOnlineMonitor
{
    public class SoundClip : IEquatable<SoundClip>
    {
        public static readonly SoundClip None = new SoundClip("[None]", 0);
        public static readonly SoundClip Custom = new SoundClip("[Custom File]", 0);

        /// <summary>Full path to sound clip</summary>
        [XmlAttribute]
        public string FileName { get; set; } = string.Empty;

        /// <summary>Duration of sound clip in ms</summary>
        [XmlAttribute, DefaultValue(0)]
        public int Duration { get; set; } = 0;

        /// <summary>Volume/Gain that the soundclip is to be played 0-1000</summary>
        [XmlAttribute, DefaultValue(1000)]
        public int Volume { get; set; } = 1000;

        /// <summary>Friendly display name for UI. Same as ToString().</summary>
        [XmlIgnore]
        public string FriendlyName
        {
            get
            {
                if (_FriendlyName == null && FileName.Length==0)
                {
                    //When parameterless constructor is used (as in XML deserialization), this is necessary.
                    if (Duration == 0) _FriendlyName = FileName;
                    else _FriendlyName = $"{Path.GetFileNameWithoutExtension(_FriendlyName)} ({Duration} ms)";
                }
                return _FriendlyName;
            }
        }
        private string _FriendlyName = null;

        public SoundClip() { } //for XML deserialization
        public SoundClip(string filename, int duration, int volume = 1000)
        {
            FileName = filename;
            Duration = duration;
            Volume = volume;
            if (Duration == 0) _FriendlyName = FileName;
            else _FriendlyName = $"{Path.GetFileNameWithoutExtension(FileName)} ({Duration} ms)";
        }

        public bool Equals(SoundClip other)
        {
            if (other == null) return false;
            if (!this.FileName.Equals(other.FileName, StringComparison.CurrentCultureIgnoreCase)) return false;
            if (Math.Abs(this.Duration-other.Duration) > 5) return false; //adjust for computation rounding errors and a little more for good measure.
            return true;
        }

        public override string ToString() => FriendlyName;
        public override bool Equals(object obj) => this.Equals(obj as SoundClip);
        public override int GetHashCode() => this.FileName.ToLower().GetHashCode() ^ this.Duration;
    }
}
