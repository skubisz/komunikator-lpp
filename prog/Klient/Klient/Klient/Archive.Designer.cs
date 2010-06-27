namespace Klient
{
    partial class Archive
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
            this.contactList = new System.Windows.Forms.ListView();
            this.talkList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.talkView = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // contactList
            // 
            this.contactList.Dock = System.Windows.Forms.DockStyle.Left;
            this.contactList.Location = new System.Drawing.Point(0, 0);
            this.contactList.MultiSelect = false;
            this.contactList.Name = "contactList";
            this.contactList.Size = new System.Drawing.Size(147, 331);
            this.contactList.TabIndex = 0;
            this.contactList.UseCompatibleStateImageBehavior = false;
            this.contactList.View = System.Windows.Forms.View.List;
            this.contactList.SelectedIndexChanged += new System.EventHandler(this.contactList_SelectedIndexChanged);
            // 
            // talkList
            // 
            this.talkList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.talkList.Dock = System.Windows.Forms.DockStyle.Top;
            this.talkList.FullRowSelect = true;
            this.talkList.Location = new System.Drawing.Point(147, 0);
            this.talkList.Name = "talkList";
            this.talkList.Size = new System.Drawing.Size(498, 97);
            this.talkList.TabIndex = 1;
            this.talkList.UseCompatibleStateImageBehavior = false;
            this.talkList.View = System.Windows.Forms.View.Details;
            this.talkList.SelectedIndexChanged += new System.EventHandler(this.talkList_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Data";
            this.columnHeader1.Width = 121;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Początek rozmowy";
            this.columnHeader2.Width = 373;
            // 
            // talkView
            // 
            this.talkView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.talkView.Location = new System.Drawing.Point(147, 97);
            this.talkView.MinimumSize = new System.Drawing.Size(20, 20);
            this.talkView.Name = "talkView";
            this.talkView.Size = new System.Drawing.Size(498, 234);
            this.talkView.TabIndex = 2;
            // 
            // Archive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 331);
            this.Controls.Add(this.talkView);
            this.Controls.Add(this.talkList);
            this.Controls.Add(this.contactList);
            this.Name = "Archive";
            this.Text = "Archiwum rozmów";
            this.Load += new System.EventHandler(this.Archive_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView contactList;
        private System.Windows.Forms.ListView talkList;
        private System.Windows.Forms.WebBrowser talkView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}