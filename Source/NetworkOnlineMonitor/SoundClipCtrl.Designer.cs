
namespace NetworkOnlineMonitor
{
    partial class SoundClipCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_cmbSoundClip = new System.Windows.Forms.ComboBox();
            this.m_btnSoundClipFileSelect = new System.Windows.Forms.Button();
            this.m_btnSoundClipPlay = new System.Windows.Forms.Button();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.m_txtSoundClipFile = new ChuckHill2.Forms.LabeledTextBox();
            this.m_csVolume = new ColorTrackbar.ColorTrackbar();
            this.SuspendLayout();
            // 
            // m_cmbSoundClip
            // 
            this.m_cmbSoundClip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbSoundClip.FormattingEnabled = true;
            this.m_cmbSoundClip.IntegralHeight = false;
            this.m_cmbSoundClip.Location = new System.Drawing.Point(0, 38);
            this.m_cmbSoundClip.MaxDropDownItems = 20;
            this.m_cmbSoundClip.Name = "m_cmbSoundClip";
            this.m_cmbSoundClip.Size = new System.Drawing.Size(342, 21);
            this.m_cmbSoundClip.TabIndex = 19;
            this.m_ToolTip.SetToolTip(this.m_cmbSoundClip, "Select the system sound to play\r\nor select [Custom File] to use your own sound cl" +
        "ip\r\nor select [None] to not play anything.");
            // 
            // m_btnSoundClipFileSelect
            // 
            this.m_btnSoundClipFileSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSoundClipFileSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnSoundClipFileSelect.Image = global::NetworkOnlineMonitor.Properties.Resources.SelectFileBlue15;
            this.m_btnSoundClipFileSelect.Location = new System.Drawing.Point(343, 69);
            this.m_btnSoundClipFileSelect.Margin = new System.Windows.Forms.Padding(0);
            this.m_btnSoundClipFileSelect.Name = "m_btnSoundClipFileSelect";
            this.m_btnSoundClipFileSelect.Size = new System.Drawing.Size(20, 20);
            this.m_btnSoundClipFileSelect.TabIndex = 17;
            this.m_ToolTip.SetToolTip(this.m_btnSoundClipFileSelect, "Select the sound file\r\nyou want to play.");
            this.m_btnSoundClipFileSelect.UseVisualStyleBackColor = true;
            this.m_btnSoundClipFileSelect.Click += new System.EventHandler(this.m_btnSoundClipFileSelect_Click);
            // 
            // m_btnSoundClipPlay
            // 
            this.m_btnSoundClipPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSoundClipPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.m_btnSoundClipPlay.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnSoundClipPlay.Image = global::NetworkOnlineMonitor.Properties.Resources.PlaySound14;
            this.m_btnSoundClipPlay.Location = new System.Drawing.Point(343, 38);
            this.m_btnSoundClipPlay.Margin = new System.Windows.Forms.Padding(0);
            this.m_btnSoundClipPlay.Name = "m_btnSoundClipPlay";
            this.m_btnSoundClipPlay.Size = new System.Drawing.Size(21, 21);
            this.m_btnSoundClipPlay.TabIndex = 21;
            this.m_ToolTip.SetToolTip(this.m_btnSoundClipPlay, "Play the selected \r\nsound clip.");
            this.m_btnSoundClipPlay.UseVisualStyleBackColor = true;
            this.m_btnSoundClipPlay.Click += new System.EventHandler(this.m_btnSoundClipPlay_Click);
            // 
            // m_txtSoundClipFile
            // 
            this.m_txtSoundClipFile.AllowDrop = true;
            this.m_txtSoundClipFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtSoundClipFile.Location = new System.Drawing.Point(0, 69);
            this.m_txtSoundClipFile.Margin = new System.Windows.Forms.Padding(0);
            this.m_txtSoundClipFile.Name = "m_txtSoundClipFile";
            this.m_txtSoundClipFile.Size = new System.Drawing.Size(342, 20);
            this.m_txtSoundClipFile.TabIndex = 18;
            this.m_txtSoundClipFile.TextLabel = "Custom Sound File";
            this.m_ToolTip.SetToolTip(this.m_txtSoundClipFile, "Enter or click n\'drag your\r\nsound file you want to play.");
            this.m_txtSoundClipFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_txtSoundClipFile_DragDrop);
            this.m_txtSoundClipFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_txtSoundClipFile_DragEnter);
            // 
            // m_csVolume
            // 
            this.m_csVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_csVolume.BackColor = System.Drawing.Color.Transparent;
            this.m_csVolume.BarInnerColor = System.Drawing.Color.Silver;
            this.m_csVolume.BarPenColorBottom = System.Drawing.Color.Gray;
            this.m_csVolume.BarPenColorTop = System.Drawing.Color.Gainsboro;
            this.m_csVolume.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.m_csVolume.ElapsedInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(143)))), ((int)(((byte)(197)))));
            this.m_csVolume.ElapsedPenColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(68)))), ((int)(((byte)(158)))));
            this.m_csVolume.ElapsedPenColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(218)))), ((int)(((byte)(236)))));
            this.m_csVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.m_csVolume.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_csVolume.LargeChange = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.m_csVolume.Location = new System.Drawing.Point(1, -8);
            this.m_csVolume.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.m_csVolume.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_csVolume.Name = "m_csVolume";
            this.m_csVolume.Padding = 1;
            this.m_csVolume.ScaleDivisions = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.m_csVolume.ScaleSubDivisions = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_csVolume.ShowDivisionsText = true;
            this.m_csVolume.ShowSmallScale = true;
            this.m_csVolume.Size = new System.Drawing.Size(360, 67);
            this.m_csVolume.SmallChange = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.m_csVolume.TabIndex = 22;
            this.m_csVolume.Text = "m_csSlider";
            this.m_csVolume.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(56)))), ((int)(((byte)(152)))));
            this.m_csVolume.ThumbPenColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(56)))), ((int)(((byte)(152)))));
            this.m_csVolume.ThumbRoundRectSize = new System.Drawing.Size(16, 16);
            this.m_csVolume.ThumbSize = new System.Drawing.Size(16, 16);
            this.m_csVolume.TickAdd = 0F;
            this.m_csVolume.TickColor = System.Drawing.SystemColors.ControlText;
            this.m_csVolume.TickDivide = 0F;
            this.m_ToolTip.SetToolTip(this.m_csVolume, "Set volume to play the sound clip at.\r\nClick the sound play button to verify.");
            this.m_csVolume.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // SoundClipCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.m_btnSoundClipFileSelect);
            this.Controls.Add(this.m_btnSoundClipPlay);
            this.Controls.Add(this.m_cmbSoundClip);
            this.Controls.Add(this.m_txtSoundClipFile);
            this.Controls.Add(this.m_csVolume);
            this.Name = "SoundClipCtrl";
            this.Size = new System.Drawing.Size(364, 89);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox m_cmbSoundClip;
        private System.Windows.Forms.Button m_btnSoundClipFileSelect;
        private ChuckHill2.Forms.LabeledTextBox m_txtSoundClipFile;
        private System.Windows.Forms.Button m_btnSoundClipPlay;
        private ColorTrackbar.ColorTrackbar m_csVolume;
        private System.Windows.Forms.ToolTip m_ToolTip;
    }
}
