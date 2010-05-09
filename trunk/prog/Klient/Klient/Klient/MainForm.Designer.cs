namespace Klient
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("aaa");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("bbb");
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.komunikatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectProfileStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewProfileStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kontaktyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addContactStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.archiveStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.currentStatusDescription = new System.Windows.Forms.Label();
            this.currentStatus = new System.Windows.Forms.PictureBox();
            this.setStatus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dostępnyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.niewidocznyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.niedostępnyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.contactList = new System.Windows.Forms.ListView();
            this.contactStatus = new System.Windows.Forms.ColumnHeader();
            this.contactName = new System.Windows.Forms.ColumnHeader();
            this.contactActions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.rozpocznijRozmowęToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.edytujToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuńZListyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oProgramieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentStatus)).BeginInit();
            this.setStatus.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contactActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.komunikatorToolStripMenuItem,
            this.kontaktyToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(275, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // komunikatorToolStripMenuItem
            // 
            this.komunikatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectProfileStripMenuItem,
            this.createNewProfileStripMenuItem,
            this.changePasswordStripMenuItem,
            this.toolStripMenuItem1,
            this.oProgramieToolStripMenuItem,
            this.closeStripMenuItem});
            this.komunikatorToolStripMenuItem.Name = "komunikatorToolStripMenuItem";
            this.komunikatorToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.komunikatorToolStripMenuItem.Text = "Komunikator";
            // 
            // selectProfileStripMenuItem
            // 
            this.selectProfileStripMenuItem.Name = "selectProfileStripMenuItem";
            this.selectProfileStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.selectProfileStripMenuItem.Text = "Wybierz profil";
            this.selectProfileStripMenuItem.Click += new System.EventHandler(this.selectProfileStripMenuItem_Click);
            // 
            // createNewProfileStripMenuItem
            // 
            this.createNewProfileStripMenuItem.Name = "createNewProfileStripMenuItem";
            this.createNewProfileStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.createNewProfileStripMenuItem.Text = "Załóż nowy profil";
            // 
            // changePasswordStripMenuItem
            // 
            this.changePasswordStripMenuItem.Name = "changePasswordStripMenuItem";
            this.changePasswordStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.changePasswordStripMenuItem.Text = "Zmień hasło";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(162, 6);
            // 
            // closeStripMenuItem
            // 
            this.closeStripMenuItem.Name = "closeStripMenuItem";
            this.closeStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.closeStripMenuItem.Text = "Zakończ";
            this.closeStripMenuItem.Click += new System.EventHandler(this.closeStripMenuItem_Click);
            // 
            // kontaktyToolStripMenuItem
            // 
            this.kontaktyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addContactStripMenuItem,
            this.toolStripMenuItem2,
            this.archiveStripMenuItem});
            this.kontaktyToolStripMenuItem.Name = "kontaktyToolStripMenuItem";
            this.kontaktyToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.kontaktyToolStripMenuItem.Text = "Kontakty";
            // 
            // addContactStripMenuItem
            // 
            this.addContactStripMenuItem.Name = "addContactStripMenuItem";
            this.addContactStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addContactStripMenuItem.Text = "Dodaj kontakt";
            this.addContactStripMenuItem.Click += new System.EventHandler(this.addContactStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // archiveStripMenuItem
            // 
            this.archiveStripMenuItem.Name = "archiveStripMenuItem";
            this.archiveStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.archiveStripMenuItem.Text = "Archiwum";
            this.archiveStripMenuItem.Click += new System.EventHandler(this.archiveStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.currentStatusDescription);
            this.panel1.Controls.Add(this.currentStatus);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(275, 30);
            this.panel1.TabIndex = 1;
            // 
            // currentStatusDescription
            // 
            this.currentStatusDescription.AutoSize = true;
            this.currentStatusDescription.Location = new System.Drawing.Point(107, 8);
            this.currentStatusDescription.Name = "currentStatusDescription";
            this.currentStatusDescription.Size = new System.Drawing.Size(50, 13);
            this.currentStatusDescription.TabIndex = 2;
            this.currentStatusDescription.Text = "dostępny";
            // 
            // currentStatus
            // 
            this.currentStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.currentStatus.ContextMenuStrip = this.setStatus;
            this.currentStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.currentStatus.Location = new System.Drawing.Point(81, 5);
            this.currentStatus.Name = "currentStatus";
            this.currentStatus.Size = new System.Drawing.Size(20, 20);
            this.currentStatus.TabIndex = 1;
            this.currentStatus.TabStop = false;
            this.currentStatus.Click += new System.EventHandler(this.currentStatus_Click);
            // 
            // setStatus
            // 
            this.setStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dostępnyToolStripMenuItem,
            this.niewidocznyToolStripMenuItem,
            this.niedostępnyToolStripMenuItem});
            this.setStatus.Name = "setStatus";
            this.setStatus.Size = new System.Drawing.Size(143, 70);
            // 
            // dostępnyToolStripMenuItem
            // 
            this.dostępnyToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dostępnyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.dostępnyToolStripMenuItem.Name = "dostępnyToolStripMenuItem";
            this.dostępnyToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.dostępnyToolStripMenuItem.Text = "Dostępny";
            // 
            // niewidocznyToolStripMenuItem
            // 
            this.niewidocznyToolStripMenuItem.Name = "niewidocznyToolStripMenuItem";
            this.niewidocznyToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.niewidocznyToolStripMenuItem.Text = "Niewidoczny";
            // 
            // niedostępnyToolStripMenuItem
            // 
            this.niedostępnyToolStripMenuItem.Name = "niedostępnyToolStripMenuItem";
            this.niedostępnyToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.niedostępnyToolStripMenuItem.Text = "Niedostępny";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Twój status:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.contactList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 54);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(275, 484);
            this.panel2.TabIndex = 2;
            // 
            // contactList
            // 
            this.contactList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.contactStatus,
            this.contactName});
            this.contactList.ContextMenuStrip = this.contactActions;
            this.contactList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contactList.FullRowSelect = true;
            this.contactList.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            this.contactList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.contactList.Location = new System.Drawing.Point(0, 0);
            this.contactList.Name = "contactList";
            this.contactList.Size = new System.Drawing.Size(275, 484);
            this.contactList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.contactList.TabIndex = 0;
            this.contactList.UseCompatibleStateImageBehavior = false;
            this.contactList.View = System.Windows.Forms.View.Details;
            this.contactList.SelectedIndexChanged += new System.EventHandler(this.contactList_SelectedIndexChanged);
            this.contactList.DoubleClick += new System.EventHandler(this.contactList_DoubleClick);
            // 
            // contactStatus
            // 
            this.contactStatus.Text = "Status";
            this.contactStatus.Width = 51;
            // 
            // contactName
            // 
            this.contactName.Text = "Nazwa";
            this.contactName.Width = 219;
            // 
            // contactActions
            // 
            this.contactActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rozpocznijRozmowęToolStripMenuItem,
            this.toolStripMenuItem4,
            this.edytujToolStripMenuItem,
            this.usuńZListyToolStripMenuItem});
            this.contactActions.Name = "contactActions";
            this.contactActions.Size = new System.Drawing.Size(184, 98);
            this.contactActions.Opening += new System.ComponentModel.CancelEventHandler(this.contactActions_Opening);
            // 
            // rozpocznijRozmowęToolStripMenuItem
            // 
            this.rozpocznijRozmowęToolStripMenuItem.Name = "rozpocznijRozmowęToolStripMenuItem";
            this.rozpocznijRozmowęToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.rozpocznijRozmowęToolStripMenuItem.Text = "Rozpocznij rozmowę";
            this.rozpocznijRozmowęToolStripMenuItem.Click += new System.EventHandler(this.rozpocznijRozmowęToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(180, 6);
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // edytujToolStripMenuItem
            // 
            this.edytujToolStripMenuItem.Name = "edytujToolStripMenuItem";
            this.edytujToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.edytujToolStripMenuItem.Text = "Edytuj";
            this.edytujToolStripMenuItem.Click += new System.EventHandler(this.edytujToolStripMenuItem_Click);
            // 
            // usuńZListyToolStripMenuItem
            // 
            this.usuńZListyToolStripMenuItem.Name = "usuńZListyToolStripMenuItem";
            this.usuńZListyToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.usuńZListyToolStripMenuItem.Text = "Usuń z listy";
            // 
            // oProgramieToolStripMenuItem
            // 
            this.oProgramieToolStripMenuItem.Name = "oProgramieToolStripMenuItem";
            this.oProgramieToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.oProgramieToolStripMenuItem.Text = "O programie";
            this.oProgramieToolStripMenuItem.Click += new System.EventHandler(this.oProgramieToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 538);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "e-Talk";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentStatus)).EndInit();
            this.setStatus.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.contactActions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem komunikatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectProfileStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewProfileStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kontaktyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addContactStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem archiveStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView contactList;
        private System.Windows.Forms.ColumnHeader contactStatus;
        private System.Windows.Forms.ColumnHeader contactName;
        private System.Windows.Forms.ContextMenuStrip contactActions;
        private System.Windows.Forms.ToolStripMenuItem rozpocznijRozmowęToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem edytujToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuńZListyToolStripMenuItem;
        private System.Windows.Forms.Label currentStatusDescription;
        private System.Windows.Forms.PictureBox currentStatus;
        private System.Windows.Forms.ContextMenuStrip setStatus;
        private System.Windows.Forms.ToolStripMenuItem dostępnyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem niewidocznyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem niedostępnyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oProgramieToolStripMenuItem;
    }
}

