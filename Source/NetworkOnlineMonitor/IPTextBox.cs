using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ChuckHill2.Forms
{
    public partial class IPTextBox : UserControl
    {
        private const string errorMessage = "Please specify a value between 0 and 255.";
        private readonly TextBox[] Fields;
        private Color SavedBackColor;
        private Color DisabledBackColor = SystemColors.Control;

        public IPTextBox()
        {
            InitializeComponent();
            Fields = new TextBox[] { firstBox, secondBox, thirdBox, fourthBox };
            int index = 0;
            foreach (var f in Fields) 
            {
                //f.BackColor = base.BackColor;
                f.Text = "0"; 
                f.Tag = index++;
            }
            SavedBackColor = base.BackColor;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Font = this.Font;
        }

        public override Color BackColor 
        { 
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                SavedBackColor = value;
                //if (Fields!=null) foreach(var f in Fields) { f.BackColor = value; }
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.BackColor = this.Enabled ? SavedBackColor : DisabledBackColor;
            base.OnEnabledChanged(e);
        }

        [DefaultValue("")]
        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance"), Description("IPv4 Address in the form of 0.0.0.0")]
        public override string Text
        {
            get => string.Join(".", Fields.Select(m => m.Text));
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;
                var items = value.Trim().Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Select(m => (int.TryParse(m, out int i) ? i : 0)).Select(m => m > 255 ? 255 : m).Select(m => m < 0 ? 0 : m);
                var index = 0;
                foreach (var item in items)
                {
                    Fields[index++].Text = item.ToString();
                    if (index == Fields.Length) break;
                }
                for (; index < Fields.Length; index++) Fields[index].Text = "0";
            }
        }

        private void m_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            var ctrl = (TextBox)sender;
            int index = (int)ctrl.Tag;

            if (e.KeyChar == 0x0008) return; //backspace

            if (e.KeyChar == '.' || e.KeyChar == '\r')
            {
                if (index < 3)
                {
                    var tb = Fields[index + 1];
                    tb.Focus();
                    tb.SelectAll();
                }
                e.Handled = true;
                return;
            }

            if (e.KeyChar < '0' || e.KeyChar > '9' || ctrl.TextLength == 3)
            {
                e.Handled = true;
                return;
            }
        }

        private void m_txt_Leave(object sender, EventArgs e)
        {
            var ctrl = (TextBox)sender;
            if (ctrl.TextLength == 0) { ctrl.Text = "0"; return; }
            if (!int.TryParse(ctrl.Text, out var i) || i < 0 || i > 255)
            {
                MiniMessageBox.ShowDialog(ctrl, errorMessage, null, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ctrl.Focus();
            }
        }

        private void m_txt_KeyDown(object sender, KeyEventArgs e)
        {
            var ctrl = (TextBox)sender;
            int index = (int)ctrl.Tag;

            if (e.KeyCode == System.Windows.Forms.Keys.Right && ctrl.SelectionStart == ctrl.TextLength && index < 3)
            {
                var tb = Fields[index + 1];
                tb.Focus();
                tb.SelectAll();
            }

            if (e.KeyCode == System.Windows.Forms.Keys.Left && ctrl.SelectionStart == 0 && index > 0)
            {
                var tb = Fields[index - 1];
                tb.Focus();
                tb.Select(3, 0);
            }
        }
    }
}
