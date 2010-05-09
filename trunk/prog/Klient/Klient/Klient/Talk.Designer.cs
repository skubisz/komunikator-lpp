namespace Klient
{
    partial class Talk
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Talk));
            this.talkBox = new System.Windows.Forms.WebBrowser();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.emoticons = new System.Windows.Forms.ToolStripButton();
            this.message = new System.Windows.Forms.RichTextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // talkBox
            // 
            this.talkBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.talkBox.Location = new System.Drawing.Point(0, 0);
            this.talkBox.MinimumSize = new System.Drawing.Size(20, 20);
            this.talkBox.Name = "talkBox";
            this.talkBox.Size = new System.Drawing.Size(502, 247);
            this.talkBox.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emoticons});
            this.toolStrip1.Location = new System.Drawing.Point(0, 247);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(502, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // emoticons
            // 
            this.emoticons.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.emoticons.Image = ((System.Drawing.Image)(resources.GetObject("emoticons.Image")));
            this.emoticons.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.emoticons.Name = "emoticons";
            this.emoticons.Size = new System.Drawing.Size(23, 22);
            this.emoticons.Text = "toolStripButton1";
            // 
            // message
            // 
            this.message.Dock = System.Windows.Forms.DockStyle.Top;
            this.message.Location = new System.Drawing.Point(0, 272);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(502, 50);
            this.message.TabIndex = 2;
            this.message.Text = "";
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(415, 328);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.TabIndex = 3;
            this.sendButton.Text = "Wyślij";
            this.sendButton.UseVisualStyleBackColor = true;
            // 
            // Talk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 354);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.message);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.talkBox);
            this.Name = "Talk";
            this.Text = "Rozmowa z ";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser talkBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton emoticons;
        private System.Windows.Forms.RichTextBox message;
        private System.Windows.Forms.Button sendButton;
    }
}