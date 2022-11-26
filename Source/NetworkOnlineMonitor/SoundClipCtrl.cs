using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChuckHill2.Forms;

namespace NetworkOnlineMonitor
{
    public partial class SoundClipCtrl : UserControl
    {
        public SoundClipCtrl()
        {
            InitializeComponent();
            m_cmbSoundClip.Items.AddRange(GetSoundFileItems());
            m_cmbSoundClip.SelectedIndex = 0; //there will ALWAYS be at least 2  items.
            m_cmbSoundClip.SelectedValueChanged += (s, e) => { if (m_cmbSoundClip.IsHandleCreated) m_cmbSoundClip.BeginInvoke((Action)(() => m_cmbSoundClip.Select(0, 0))); };
            m_cmbSoundClip.GotFocus += (s, e) => m_cmbSoundClip.Select(0, 0);
            m_cmbSoundClip.KeyDown += ComboBoxReadOnly_KeyDownUp;
            m_cmbSoundClip.KeyUp += ComboBoxReadOnly_KeyDownUp;
            m_cmbSoundClip.SelectedIndexChanged += (s, e) =>
            {
                int selectedIndex = m_cmbSoundClip.SelectedIndex;
                m_csVolume.Enabled = selectedIndex > 0;
                m_btnSoundClipFileSelect.Enabled = selectedIndex == 1;
                m_txtSoundClipFile.Enabled = selectedIndex == 1;
                m_btnSoundClipPlay.Enabled = selectedIndex > 0;
            };

            m_csVolume.Enabled = false;
            m_btnSoundClipFileSelect.Enabled = false;
            m_txtSoundClipFile.Enabled = false;
            m_btnSoundClipPlay.Enabled = false;
        }

        private void ComboBoxReadOnly_KeyDownUp(object sender, KeyEventArgs e)
        {
            int k = (int)e.KeyCode;
            if (k == 8 || k == 32 || (k >= 45 && k <= 90) || (k >= 96 && k <= 111) || (k >= 186 && k <= 254))
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
        }

        public SoundClip SoundClip
        {
            get
            {
                int selectedIndex = m_cmbSoundClip.SelectedIndex;
                if (selectedIndex == -1 || selectedIndex == 0) return SoundClip.None;
                if (selectedIndex == 1)  //User-supplied
                {
                    var f = m_txtSoundClipFile.Text.Trim();
                    if (f.Length == 0 || !File.Exists(f)) return SoundClip.None;
                    int dur = Sound.MediaDuration(f);
                    if (dur == 0) return SoundClip.None;
                    return new SoundClip(f, dur, (int)m_csVolume.Value);
                }
                else
                {
                    var v = (SoundClip)m_cmbSoundClip.SelectedItem;
                    return new SoundClip(v.FileName, v.Duration, (int)m_csVolume.Value);
                }
            }
            set
            {
                if (value==null || value == SoundClip.None)
                {
                    m_cmbSoundClip.SelectedIndex = 0;
                    return;
                }

                var i = m_cmbSoundClip.Items.IndexOf(value);
                if (i==-1)
                {
                    m_cmbSoundClip.SelectedIndex = 1;
                    m_txtSoundClipFile.Text = value.FileName;
                    m_csVolume.Value = value.Volume;
                }
                else
                {
                    m_cmbSoundClip.SelectedIndex = i;
                    m_csVolume.Value = value.Volume;
                }
            }
        }

        private void m_btnSoundClipPlay_Click(object sender, EventArgs e)
        {
            string f = string.Empty;
            int selectedIndex = m_cmbSoundClip.SelectedIndex;
            int vol = (int)m_csVolume.Value;
            if (selectedIndex == -1 || selectedIndex == 0) return;
            if (selectedIndex == 1)
            {
                f = m_txtSoundClipFile.Text.Trim();
                if (f.Length == 0 || !File.Exists(f)) return;
            }
            else
            {
                var v = (SoundClip)m_cmbSoundClip.SelectedItem;
                f = v.FileName;
            }

            Sound.Play(f, vol);
        }

        private void m_btnSoundClipFileSelect_Click(object sender, EventArgs e)
        {
            string filename = BrowseFile(this, true,
                "All Audio Files|*.aac;*.adt;*.adts;*.aif;*.aifc;*.aiff;*.au;*.flac;*.m4a;*.mka;*.mp2;*.mp3;*.snd;*.wav;*.wax;*.wma" +
                "|All Files(*.*)|*.*",
                "Select Sound File",
                m_txtSoundClipFile.Text,
                Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));

            if (filename != null) m_txtSoundClipFile.Text = filename;
        }

        private void m_txtSoundClipFile_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            var file = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            if (ValidateFile(file))
            {
                e.Effect = DragDropEffects.Link;
            }
        }

        private void m_txtSoundClipFile_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            var file = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            if (ValidateFile(file))
            {
                m_txtSoundClipFile.Text = file;
            }
        }

        private bool ValidateFile(string file)
        {
            var exts = new string[] { ".aac", ".adt", ".adts", ".aif", ".aifc", ".aiff", ".au", ".flac", ".m4a", ".mka", ".mp2", ".mp3", ".snd", ".wav", ".wax", ".wma" };
            return exts.Any(m => file.EndsWith(m, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Canned UI prompt for a single file. Extends System.Windows.Forms.OpenFileDialog().
        /// Unlike SaveFileDialog(), does not include prompt to overwrite file.
        /// </summary>
        /// <remarks>
        /// Example: 
        /// <code>
        /// string filename = BrowseFile(this, true, 
        ///         "Audio Formats|*.wav;*.mp3|All Files(*.*)|*.*", 
        ///         "Select Sound File", 
        ///         textBox1.Text, 
        ///         Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
        /// </code>
        /// </remarks>
        /// <param name="owner">The Form that owns this dialog.</param>
        /// <param name="mustExist">Specified file must exist.</param>
        /// <param name="filter">Sets the current file name filter string, which determines the choices that appear in the "Files of type" box in the dialog box.</param>
        /// <param name="title">Title for this dialog</param>
        /// <param name="existingFilename">preselected default file to use or null.</param>
        /// <param name="defaultFolder">default folder to use if 'existingFilename's folder-part does not exist. or null to use current user's documents folder.</param>
        /// <returns>filename or null if dialog cancelled</returns>
        private static string BrowseFile(IWin32Window owner, bool mustExist = true, string filter = "All Files(*.*)|*.*", string title = "Select Existing File", string existingFilename = null, string defaultFolder = null)
        {
            if (string.IsNullOrWhiteSpace(defaultFolder) || !Directory.Exists(defaultFolder))
                defaultFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var p = existingFilename;
            string filename = p;
            var dir = defaultFolder;
            if (!string.IsNullOrWhiteSpace(p))
            {
                dir = Path.GetDirectoryName(p);
                if (!Directory.Exists(dir)) dir = defaultFolder;
                if (!File.Exists(filename)) filename = null;
            }
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = filter;
                ofd.AddExtension = false;
                ofd.CheckFileExists = mustExist;
                ofd.DereferenceLinks = true;
                ofd.Multiselect = false;
                ofd.InitialDirectory = dir;
                ofd.RestoreDirectory = true;
                ofd.Multiselect = false;
                ofd.Title = title;
                ofd.ValidateNames = true;
                ofd.AutoUpgradeEnabled = false;
                if (filename != null) ofd.FileName = filename;

                if (ofd.ShowDialog(owner) != DialogResult.OK) return null;
                filename = ofd.FileName;
            }

            return filename;
        }

        private static WeakReference<SoundClip[]> _SoundFileItems = new WeakReference<SoundClip[]>(null);
        private SoundClip[] GetSoundFileItems()
        {
            if (!_SoundFileItems.TryGetTarget(out var arr))
            {
                var list = new System.Collections.Concurrent.ConcurrentBag<SoundClip>();
                IEnumerable<string> files = StaticTools.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.Windows) + @"\Media")
                    .Concat(StaticTools.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\Microsoft Office\root", SearchOption.AllDirectories));

                Parallel.ForEach(files, f =>
                {
                    if (!f.EndsWith(".wav", StringComparison.OrdinalIgnoreCase)) return;
                    int duration = Sound.WaveDuration(f);
                    if (duration < 10 || duration > 5000) return;
                    list.Add(new SoundClip(f, duration));
                });

                arr = list.OrderBy(m => m.FriendlyName, StringComparer.OrdinalIgnoreCase).Prepend(SoundClip.Custom).Prepend(SoundClip.None).ToArray();

                _SoundFileItems.SetTarget(arr);
                return arr;
            }

            //Array.ForEach(arr, m => m.Volume = AudioPlayer.VolumeMaxValue);
            return arr;
        }
    }
}
