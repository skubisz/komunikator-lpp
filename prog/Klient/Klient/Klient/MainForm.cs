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
        private Communicator _communicator;
        

        public MainForm()
        {
            InitializeComponent();
            
            _communicator = Communicator.getInstance();                        
        }

        private void refreschContacts()
        {
            contactList.Items.Clear();

            List<Contact> list = _communicator.contacts.getList();

            foreach (Contact contact in list)
            {                
                contactList.Items.Add(new ListViewItem(new String[] {"dostepny", contact.name} ));                
            }            
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
            SelectProfile selectProfileForm = new SelectProfile(this);
            selectProfileForm.ShowDialog();
                        
            refreschContacts();
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
            dialog.ShowDialog();

            refreschContacts();
            _communicator.contacts.saveToFile(_communicator.contactFile);
        }

        private void edytujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contactList.SelectedItems.Count == 1)
            {
                Contact contact = _communicator.contacts.getList()[contactList.SelectedItems[0].Index];

                EditContact dialog = new EditContact(contact.name, contact.login);
                dialog.ShowDialog();

                refreschContacts();
                _communicator.contacts.saveToFile(_communicator.contactFile);
            }
            else if (contactList.SelectedItems.Count > 1)
            {
                MessageBox.Show("Wybierz tylko jeden kontakt, aby zmienić dane.");
            }            
        }

        private void usuńZListyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Czy na pewno usunąć wybrane kontakty?", "Potwierdzenie usunięcia", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {                
                List<string> logins = new List<string>();
                foreach (ListViewItem item in contactList.SelectedItems)
                {
                    logins.Add(_communicator.contacts.getList()[item.Index].login);
                }

                foreach (string login in logins)
                {
                    _communicator.contacts.removeContact(login);
                }

                refreschContacts();
                _communicator.contacts.saveToFile(_communicator.contactFile);
            }                
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

        private void createNewProfileStripMenuItem_Click(object sender, EventArgs e)
        {            
            CreateAccount createAccount = new CreateAccount(this);
            createAccount.Show();

            refreschContacts();
        }

        

    }
}
