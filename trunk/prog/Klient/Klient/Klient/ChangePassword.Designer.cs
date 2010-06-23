namespace Klient
{
    partial class ChangePassword
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.newPassword = new System.Windows.Forms.MaskedTextBox();
            this.repeatNewPassword = new System.Windows.Forms.MaskedTextBox();
            this.changePasswordButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nowe hasło:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Powtórz hasło:";
            // 
            // newPassword
            // 
            this.newPassword.Location = new System.Drawing.Point(96, 6);
            this.newPassword.Name = "newPassword";
            this.newPassword.Size = new System.Drawing.Size(141, 20);
            this.newPassword.TabIndex = 2;
            // 
            // repeatNewPassword
            // 
            this.repeatNewPassword.Location = new System.Drawing.Point(96, 32);
            this.repeatNewPassword.Name = "repeatNewPassword";
            this.repeatNewPassword.Size = new System.Drawing.Size(141, 20);
            this.repeatNewPassword.TabIndex = 3;
            // 
            // changePasswordButton
            // 
            this.changePasswordButton.Location = new System.Drawing.Point(50, 58);
            this.changePasswordButton.Name = "changePasswordButton";
            this.changePasswordButton.Size = new System.Drawing.Size(75, 23);
            this.changePasswordButton.TabIndex = 4;
            this.changePasswordButton.Text = "Zmień";
            this.changePasswordButton.UseVisualStyleBackColor = true;
            this.changePasswordButton.Click += new System.EventHandler(this.changePasswordButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(131, 58);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Anuluj";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // ChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 87);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.changePasswordButton);
            this.Controls.Add(this.repeatNewPassword);
            this.Controls.Add(this.newPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ChangePassword";
            this.Text = "ChangePassword";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox newPassword;
        private System.Windows.Forms.MaskedTextBox repeatNewPassword;
        private System.Windows.Forms.Button changePasswordButton;
        private System.Windows.Forms.Button cancelButton;
    }
}