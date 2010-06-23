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
            this.talkBox = new System.Windows.Forms.WebBrowser();
            this.message = new System.Windows.Forms.RichTextBox();
            this.sendButton = new System.Windows.Forms.Button();
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
            // message
            // 
            this.message.Dock = System.Windows.Forms.DockStyle.Top;
            this.message.Location = new System.Drawing.Point(0, 247);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(502, 50);
            this.message.TabIndex = 2;
            this.message.Text = "";
            this.message.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.message_KeyPress);
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(415, 328);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.TabIndex = 3;
            this.sendButton.Text = "Wyślij";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // Talk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 354);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.message);
            this.Controls.Add(this.talkBox);
            this.Name = "Talk";
            this.Text = "Rozmowa z ";
            this.Load += new System.EventHandler(this.Talk_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Talk_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser talkBox;
        private System.Windows.Forms.RichTextBox message;
        private System.Windows.Forms.Button sendButton;
    }
}