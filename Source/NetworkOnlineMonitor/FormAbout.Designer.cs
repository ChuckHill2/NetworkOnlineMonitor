﻿
namespace NetworkOnlineMonitor
{
    partial class FormAbout
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.m_wbDocument = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(478, 450);
            this.webBrowser1.TabIndex = 0;
            // 
            // m_wbDocument
            // 
            this.m_wbDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wbDocument.Location = new System.Drawing.Point(0, 0);
            this.m_wbDocument.MinimumSize = new System.Drawing.Size(20, 20);
            this.m_wbDocument.Name = "m_wbDocument";
            this.m_wbDocument.Size = new System.Drawing.Size(478, 450);
            this.m_wbDocument.TabIndex = 1;
            this.m_wbDocument.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.m_wbDocument_Navigating);
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 450);
            this.Controls.Add(this.m_wbDocument);
            this.Controls.Add(this.webBrowser1);
            this.Icon = global::NetworkOnlineMonitor.Properties.Resources.favicon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.WebBrowser m_wbDocument;
    }
}