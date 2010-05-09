using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Klient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void contactList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //contactList.ContextMenuStrip.Show(contactList, new System.Drawing.Point(0, 0));            
        }

        private void selectProfileStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectProfile selectProfileForm = new SelectProfile();
            selectProfileForm.ShowDialog();
        }

        private void closeStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void archiveStripMenuItem_Click(object sender, EventArgs e)
        {
            Archive archiveForm = new Archive();
            archiveForm.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void currentStatus_Click(object sender, EventArgs e)
        {
            Point point = new System.Drawing.Point(currentStatus.Width / 2, currentStatus.Height / 2);
            currentStatus.ContextMenuStrip.Show(currentStatus, point);
        }

        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        private void addContactStripMenuItem_Click(object sender, EventArgs e)
        {
            EditContact dialog = new EditContact();
            dialog.setMode("add");
            dialog.ShowDialog();
        }

        private void edytujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditContact dialog = new EditContact();
            dialog.setMode("edit");
            dialog.ShowDialog();
        }

        private void contactList_DoubleClick(object sender, EventArgs e)
        {
            if (contactList.SelectedItems.Count == 1)
            {
                Talk talk = new Talk();
                talk.Show();
            }
            else if (contactList.SelectedItems.Count > 1) 
            {
                MessageBox.Show("W tej chwili komunikator nie obsługuje rozmów konferencyjnych");
            }
            
        }

        private void contactActions_Opening(object sender, CancelEventArgs e)
        {

        }

        private void rozpocznijRozmowęToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contactList_DoubleClick(sender, e);
        }

    }
}
