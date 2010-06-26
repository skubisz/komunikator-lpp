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

        private List<Talk> _activeTalks;
        

        public MainForm()
        {
            InitializeComponent();            

            _communicator = Communicator.getInstance();

            _activeTalks = new List<Talk>();            
        }

        private void refreschContacts()
        {
            contactList.Items.Clear();

            List<Contact> list = _communicator.contacts.getList();

            foreach (Contact contact in list)
            {                
                contactList.Items.Add(new ListViewItem(new String[] {"niedostepny", contact.name} ));                
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
                Contact contact = _communicator.contacts.getList()[contactList.SelectedItems[0].Index];
                
                foreach (Talk talk in _activeTalks)
                {
                    if (talk.contactLogin == contact.login)
                    {
                        talk.Focus();
                        return;
                    }
                }

                int talkId = _communicator.archive.getNextId();
                Talk newTalk = new Talk(this, _communicator.getLogedUser(), talkId, contact.login, contact.name);
                _activeTalks.Add(newTalk);
                newTalk.Show();                
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

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        public void enableMenuItems()
        {
            changePasswordStripMenuItem.Enabled = true;
            archiveStripMenuItem.Enabled = true;
            addContactStripMenuItem.Enabled = true;

            timer.Enabled = true;
            contactTimer.Enabled = true;

            setStatus.Enabled = true;
            currentStatus.Enabled = true;
            dostępnyToolStripMenuItem_Click(null, null);
        }

        public void disableMenuItems()
        {
            changePasswordStripMenuItem.Enabled = false;
            archiveStripMenuItem.Enabled = false;
            addContactStripMenuItem.Enabled = false;

            timer.Enabled = false;
            contactTimer.Enabled = false;

            setStatus.Enabled = false;
            currentStatus.Enabled = false;
            niedostępnyToolStripMenuItem_Click(null, null);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            
            timer.Enabled = false;
            
            List<CommunicatorMessage> messages = _communicator.readMessages();

            foreach (CommunicatorMessage message in messages)
            {
                bool exists = false;
                foreach (Talk talk in _activeTalks)
                {                    
                    if (talk.addMessage(message.from, talk.contactName, message.message))
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    Contact contact = _communicator.contacts.findContact(message.from);
                    string contactName = contact != null ? contact.name : message.from;

                    int talkId = _communicator.archive.getNextId();
                    Talk newTalk = new Talk(this, _communicator.getLogedUser(), talkId, message.from, contactName);
                    _activeTalks.Add(newTalk);
                    newTalk.Show();
                    newTalk.addMessage(message.from, contactName, message.message);
                }
            }

            timer.Enabled = true;            
            
        }

        public void closeTalk(Talk talk)
        {
            _activeTalks.Remove(talk);            
        }

        private void changePasswordStripMenuItem_Click(object sender, EventArgs e)
        {
            new ChangePassword().ShowDialog();
        }

        private void contactTimer_Tick(object sender, EventArgs e)
        {
            return;
            contactTimer.Enabled = false;

            _communicator.refreshContactsStatus(this);
            
            contactTimer.Enabled = true;
        }

        public void updateStatus(string login, int index, string status)
        {
            string newStatus;
            if (status == "dostepny")
            {
                newStatus = "dostępny";
            }
            else
            {
                newStatus = "niedostępny";
            }
            contactList.Items[index].SubItems[0].Text = newStatus;
        }

        private void dostępnyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _communicator.changeStatus("dostepny");
            currentStatusDescription.Text = "dostępny";
            currentStatus.BackColor = Color.PaleGreen;
        }

        private void niewidocznyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _communicator.changeStatus("niewidoczny");
            currentStatusDescription.Text = "niewidoczny";
            currentStatus.BackColor = Color.Gray;
        }

        private void niedostępnyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _communicator.changeStatus("niedostepny");
            currentStatusDescription.Text = "niedostępny";
            currentStatus.BackColor = Color.Red;
        }       
    }
}
