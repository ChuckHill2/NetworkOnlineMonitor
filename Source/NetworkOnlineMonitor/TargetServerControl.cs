using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChuckHill2.Forms;

namespace NetworkOnlineMonitor
{
    public partial class TargetServerControl : UserControl
    {
        public TargetServerControl()
        {
            InitializeComponent();
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text 
        { 
            get => m_txtName.Text; 
            set => m_txtName.Text = value; 
        }

        [DefaultValue("")]
        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Data"), Description("Name/Identifier for the IPv4 address.")]
        public string IPName
        {
            get => m_txtName.Text;
            set => m_txtName.Text = value;
        }

        [DefaultValue("0.0.0.0")]
        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Data"), Description("IPv4 Address in the form of 0.0.0.0")]
        public string IPAddress
        {
            get => m_txtIPAddress.Text;
            set => m_txtIPAddress.Text = value;
        }


        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Settings.TargetIP Value
        {
            get => new Settings.TargetIP(m_txtName.Text, m_txtIPAddress.Text);
            set
            {
                m_txtName.Text = value.Name;
                m_txtIPAddress.Text = value.Address.ToString();
            }
        }

        public override Font Font 
        { 
            get => base.Font; 
            set { base.Font = value; m_txtIPAddress.Font = value; }
        }

        private void m_btnTest_Click(object sender, EventArgs e)
        {
            var myPing = new Ping();
            PingReply reply = myPing.Send(m_txtIPAddress.Text, 1000);
            if (reply != null)
                MiniMessageBox.ShowDialog(this,
                    $"{reply.Status}\n{(reply.RoundtripTime==0?"": reply.RoundtripTime.ToString()+" ms ")}for {reply.Address?.ToString()??m_txtIPAddress.Text}",
                    "Ping " + m_txtName.Text,
                    MiniMessageBox.Buttons.OK,
                    reply.Status == IPStatus.Success ? MiniMessageBox.Symbol.Information : MiniMessageBox.Symbol.Error);
            else
                MiniMessageBox.ShowDialog(this, "Unknown Error", "Ping " + m_txtName.Text,
                    MiniMessageBox.Buttons.OK,
                    MiniMessageBox.Symbol.Error);
        }
    }
}
