namespace Klient
{
    partial class SelectProfile
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
            this.actionLogin = new System.Windows.Forms.Button();
            this.actionCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.login = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // actionLogin
            // 
            this.actionLogin.Location = new System.Drawing.Point(28, 61);
            this.actionLogin.Name = "actionLogin";
            this.actionLogin.Size = new System.Drawing.Size(83, 23);
            this.actionLogin.TabIndex = 0;
            this.actionLogin.Text = "Wybierz";
            this.actionLogin.UseVisualStyleBackColor = true;
            // 
            // actionCancel
            // 
            this.actionCancel.Location = new System.Drawing.Point(117, 61);
            this.actionCancel.Name = "actionCancel";
            this.actionCancel.Size = new System.Drawing.Size(83, 23);
            this.actionCancel.TabIndex = 1;
            this.actionCancel.Text = "Anuluj";
            this.actionCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Numer:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hasło:";
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(75, 10);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(144, 20);
            this.login.TabIndex = 4;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(75, 35);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(144, 20);
            this.password.TabIndex = 5;
            // 
            // SelectProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 90);
            this.Controls.Add(this.password);
            this.Controls.Add(this.login);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.actionCancel);
            this.Controls.Add(this.actionLogin);
            this.Name = "SelectProfile";
            this.Text = "Wybierz profil";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button actionLogin;
        private System.Windows.Forms.Button actionCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox login;
        private System.Windows.Forms.TextBox password;
    }
}