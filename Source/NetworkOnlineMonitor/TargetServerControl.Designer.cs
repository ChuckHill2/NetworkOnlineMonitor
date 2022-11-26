
namespace NetworkOnlineMonitor
{
    partial class TargetServerControl
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
            this.m_txtName = new ChuckHill2.Forms.LabeledTextBox();
            this.m_txtIPAddress = new ChuckHill2.Forms.IPTextBox();
            this.m_btnTest = new System.Windows.Forms.Button();
            this.m_ToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // m_txtName
            // 
            this.m_txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtName.Location = new System.Drawing.Point(0, 0);
            this.m_txtName.Margin = new System.Windows.Forms.Padding(0);
            this.m_txtName.Name = "m_txtName";
            this.m_txtName.Size = new System.Drawing.Size(130, 20);
            this.m_txtName.TabIndex = 4;
            this.m_txtName.TextLabel = "Name";
            this.m_ToolTips.SetToolTip(this.m_txtName, "Target DNS \r\nserver name");
            // 
            // m_txtIPAddress
            // 
            this.m_txtIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtIPAddress.BackColor = System.Drawing.SystemColors.Window;
            this.m_txtIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtIPAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtIPAddress.Location = new System.Drawing.Point(133, 0);
            this.m_txtIPAddress.Margin = new System.Windows.Forms.Padding(0);
            this.m_txtIPAddress.Name = "m_txtIPAddress";
            this.m_txtIPAddress.Size = new System.Drawing.Size(103, 20);
            this.m_txtIPAddress.TabIndex = 0;
            this.m_txtIPAddress.Text = "255.255.255.255";
            this.m_ToolTips.SetToolTip(this.m_txtIPAddress, "Target DNS server \r\nIP address");
            // 
            // m_btnTest
            // 
            this.m_btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnTest.Location = new System.Drawing.Point(239, -1);
            this.m_btnTest.Margin = new System.Windows.Forms.Padding(0);
            this.m_btnTest.Name = "m_btnTest";
            this.m_btnTest.Size = new System.Drawing.Size(47, 22);
            this.m_btnTest.TabIndex = 5;
            this.m_btnTest.Text = "Test";
            this.m_ToolTips.SetToolTip(this.m_btnTest, "Validate/Ping\r\nIP address");
            this.m_btnTest.UseVisualStyleBackColor = true;
            this.m_btnTest.Click += new System.EventHandler(this.m_btnTest_Click);
            // 
            // TargetServerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_btnTest);
            this.Controls.Add(this.m_txtName);
            this.Controls.Add(this.m_txtIPAddress);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TargetServerControl";
            this.Size = new System.Drawing.Size(285, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ChuckHill2.Forms.LabeledTextBox m_txtName;
        private ChuckHill2.Forms.IPTextBox m_txtIPAddress;
        private System.Windows.Forms.Button m_btnTest;
        private System.Windows.Forms.ToolTip m_ToolTips;
    }
}
