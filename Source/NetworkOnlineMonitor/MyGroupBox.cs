using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NetworkOnlineMonitor
{
    /// <summary>
    /// Same as GroupBox but border may be any color plus title text may
    /// be any color or font. Ambient properties continue to be passed through
    /// to the child controls independent of the title text properties.
    /// Enable/Disable is properly supported for both title and border.
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
            base.OnParentFontChanged(e);
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance"), Description("Color of the GroupBox title.")]
        public Color TitleColor
        {
            get => __TitleColor;
            set
            {
                if (__TitleColor == value) return;
                __TitleColor = value;
                this.Invalidate();
            }
        }
        private Color __TitleColor = Control.DefaultForeColor;
        private bool ShouldSerializeTitleColor() => TitleColor != Control.DefaultForeColor;
        private void ResetTitleColor() => TitleColor = Control.DefaultForeColor;

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
