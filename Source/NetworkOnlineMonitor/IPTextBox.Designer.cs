
namespace ChuckHill2.Forms
{
    public partial class IPTextBox
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
            this.firstBox = new System.Windows.Forms.TextBox();
            this.m_lblDot12 = new System.Windows.Forms.Label();
            this.secondBox = new System.Windows.Forms.TextBox();
            this.m_lblDot23 = new System.Windows.Forms.Label();
            this.thirdBox = new System.Windows.Forms.TextBox();
            this.m_lblDot34 = new System.Windows.Forms.Label();
            this.fourthBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // firstBox
            // 
            this.firstBox.BackColor = System.Drawing.SystemColors.Window;
            this.firstBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.firstBox.Location = new System.Drawing.Point(4, 3);
            this.firstBox.Margin = new System.Windows.Forms.Padding(0);
            this.firstBox.MaxLength = 3;
            this.firstBox.Name = "firstBox";
            this.firstBox.Size = new System.Drawing.Size(19, 13);
            this.firstBox.TabIndex = 0;
            this.firstBox.Text = "255";
            this.firstBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.firstBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txt_KeyDown);
            this.firstBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txt_KeyPress);
            this.firstBox.Leave += new System.EventHandler(this.m_txt_Leave);
            // 
            // m_lblDot12
            // 
            this.m_lblDot12.AutoSize = true;
            this.m_lblDot12.BackColor = System.Drawing.Color.Transparent;
            this.m_lblDot12.Location = new System.Drawing.Point(20, 6);
            this.m_lblDot12.Margin = new System.Windows.Forms.Padding(0);
            this.m_lblDot12.Name = "m_lblDot12";
            this.m_lblDot12.Size = new System.Drawing.Size(13, 13);
            this.m_lblDot12.TabIndex = 1;
            this.m_lblDot12.Text = "•";
            this.m_lblDot12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // secondBox
            // 
            this.secondBox.BackColor = System.Drawing.SystemColors.Window;
            this.secondBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.secondBox.Location = new System.Drawing.Point(29, 3);
            this.secondBox.Margin = new System.Windows.Forms.Padding(0);
            this.secondBox.MaxLength = 3;
            this.secondBox.Name = "secondBox";
            this.secondBox.Size = new System.Drawing.Size(19, 13);
            this.secondBox.TabIndex = 2;
            this.secondBox.Text = "255";
            this.secondBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.secondBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txt_KeyDown);
            this.secondBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txt_KeyPress);
            this.secondBox.Leave += new System.EventHandler(this.m_txt_Leave);
            // 
            // m_lblDot23
            // 
            this.m_lblDot23.AutoSize = true;
            this.m_lblDot23.BackColor = System.Drawing.Color.Transparent;
            this.m_lblDot23.Location = new System.Drawing.Point(45, 6);
            this.m_lblDot23.Margin = new System.Windows.Forms.Padding(0);
            this.m_lblDot23.Name = "m_lblDot23";
            this.m_lblDot23.Size = new System.Drawing.Size(13, 13);
            this.m_lblDot23.TabIndex = 3;
            this.m_lblDot23.Text = "•";
            this.m_lblDot23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // thirdBox
            // 
            this.thirdBox.BackColor = System.Drawing.SystemColors.Window;
            this.thirdBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.thirdBox.Location = new System.Drawing.Point(54, 3);
            this.thirdBox.Margin = new System.Windows.Forms.Padding(0);
            this.thirdBox.MaxLength = 3;
            this.thirdBox.Name = "thirdBox";
            this.thirdBox.Size = new System.Drawing.Size(19, 13);
            this.thirdBox.TabIndex = 4;
            this.thirdBox.Text = "255";
            this.thirdBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.thirdBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txt_KeyDown);
            this.thirdBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txt_KeyPress);
            this.thirdBox.Leave += new System.EventHandler(this.m_txt_Leave);
            // 
            // m_lblDot34
            // 
            this.m_lblDot34.AutoSize = true;
            this.m_lblDot34.BackColor = System.Drawing.Color.Transparent;
            this.m_lblDot34.Location = new System.Drawing.Point(70, 6);
            this.m_lblDot34.Margin = new System.Windows.Forms.Padding(0);
            this.m_lblDot34.Name = "m_lblDot34";
            this.m_lblDot34.Size = new System.Drawing.Size(13, 13);
            this.m_lblDot34.TabIndex = 5;
            this.m_lblDot34.Text = "•";
            this.m_lblDot34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fourthBox
            // 
            this.fourthBox.BackColor = System.Drawing.SystemColors.Window;
            this.fourthBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fourthBox.Location = new System.Drawing.Point(79, 3);
            this.fourthBox.Margin = new System.Windows.Forms.Padding(0);
            this.fourthBox.MaxLength = 3;
            this.fourthBox.Name = "fourthBox";
            this.fourthBox.Size = new System.Drawing.Size(19, 13);
            this.fourthBox.TabIndex = 6;
            this.fourthBox.Text = "255";
            this.fourthBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fourthBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txt_KeyDown);
            this.fourthBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txt_KeyPress);
            this.fourthBox.Leave += new System.EventHandler(this.m_txt_Leave);
            // 
            // IPTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.firstBox);
            this.Controls.Add(this.secondBox);
            this.Controls.Add(this.thirdBox);
            this.Controls.Add(this.fourthBox);
            this.Controls.Add(this.m_lblDot12);
            this.Controls.Add(this.m_lblDot23);
            this.Controls.Add(this.m_lblDot34);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "IPTextBox";
            this.Size = new System.Drawing.Size(102, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox firstBox;
        private System.Windows.Forms.Label m_lblDot12;
        private System.Windows.Forms.TextBox secondBox;
        private System.Windows.Forms.Label m_lblDot23;
        private System.Windows.Forms.TextBox thirdBox;
        private System.Windows.Forms.Label m_lblDot34;
        private System.Windows.Forms.TextBox fourthBox;
    }
}
