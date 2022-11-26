using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NetworkOnlineMonitor
{
    /// <summary>
    /// Same as GroupBox but border may be any color plus title text may be any color or font. 
    /// Enable/Disable is properly supported for both title and border. This control is NOT themed.
    /// </summary>
    public class MyGroupBox : GroupBox
    {
        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance"), Description("Color of the GroupBox border.")]
        public Color BorderColor
        {
            get => __BorderColor;
            set
            {
                if (__BorderColor == value) return;
                __BorderColor = value;
                this.Invalidate();
            }
        }
        private Color __BorderColor = Color.Gainsboro;
        private bool ShouldSerializeBorderColor() => BorderColor != Color.Gainsboro;
        private void ResetBorderColor() => BorderColor = Color.Gainsboro;

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance"), Description("Font to use for the GroupBox title")]
        public Font TitleFont
        {
            get
            {
                if (__TitleFont == null) return AmbientTitleFont;
                return __TitleFont;
            }
            set
            {
                if (__TitleFont == value) return;
                if (__TitleFont == AmbientTitleFont) __TitleFont = null;
                else __TitleFont = value;
                this.Invalidate();
            }
        }
        private Font AmbientTitleFont = Control.DefaultFont;
        private Font __TitleFont = null;
        private bool ShouldSerializeTitleFont() => TitleFont != AmbientTitleFont;
        private void ResetTitleFont() => TitleFont = AmbientTitleFont;
        protected override void OnParentFontChanged(EventArgs e)
        {
            AmbientTitleFont = this.Parent?.Font ?? Control.DefaultFont;
            AmbientTitleColor = this.Parent?.ForeColor ?? Control.DefaultForeColor;
            base.OnParentFontChanged(e);
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance"), Description("Color of the GroupBox title.")]
        public Color TitleColor
        {
            get
            {
                if (__TitleColor == Color.Empty) return AmbientTitleColor;
                return __TitleColor;
            }
            set
            {
                if (__TitleColor == value) return;
                if (__TitleColor == AmbientTitleColor) __TitleColor = Color.Empty;
                else __TitleColor = value;
                this.Invalidate();
            }
        }
        private Color AmbientTitleColor = Control.DefaultForeColor;
        private Color __TitleColor = Color.Empty;
        private bool ShouldSerializeTitleColor() => TitleColor != AmbientTitleColor;
        private void ResetTitleColor() => TitleColor = AmbientTitleColor;

        #region Hidden/Disabled Properties
        private const string NOTUSED = "Not used in " + nameof(MyGroupBox) + ".";
        //! @cond DOXYGENHIDE
        #pragma warning disable CS0067, CS0809, CS0109 //Properties and Events never used
        [Obsolete(NOTUSED, true), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FlatStyle FlatStyle { get => base.FlatStyle; set { } }
        [Obsolete(NOTUSED, true), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override RightToLeft RightToLeft { get => base.RightToLeft; set { } }
        [Obsolete(NOTUSED, true), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new event EventHandler RightToLeftChanged;
        #pragma warning restore CS0067, CS0809, CS0109 //Properties and Events never used
        //! @endcond
        #endregion Hidden/Disabled Properties

        public MyGroupBox() : base() { }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e); --we do all our own drawing...

            var g = e.Graphics;

            var h = TitleFont.Height;
            using (var p = new Pen(base.Enabled ? BorderColor : Color.Gainsboro, 1))
                g.DrawRectangle(p, 0, h / 2, this.Width - 1, this.Height - h / 2 - 1);

            TextRenderer.DrawText(g, base.Text + " ", TitleFont, new Point(10, 0), base.Enabled ? TitleColor : SystemColors.ButtonShadow, base.BackColor);
        }
    }
}
