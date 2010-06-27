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
    public partial class Archive : Form
    {
        public Archive()
        {
            InitializeComponent();

            Communicator communicatior = Communicator.getInstance();
            string logedUser = communicatior.getLogedUser();
            if (logedUser != "")
            {                
                foreach (string login in communicatior.archive.readTalker(logedUser))
                {
                    Contact contact = communicatior.contacts.findContact(login);
                    contactList.Items.Add(contact == null ? login : contact.name);
                }                
            }
        }

        private void Archive_Load(object sender, EventArgs e)
        {
            talkView.DocumentText = "<b>asdasda</b>";
        }

        private void contactList_SelectedIndexChanged(object sender, EventArgs e)
        {
            talkList.Items.Clear();
            if (contactList.SelectedItems.Count != 1)
            {
                return;
            }

            Communicator communicatior = Communicator.getInstance();
            string logedUser = communicatior.getLogedUser();

            string login = communicatior.archive.readTalker(logedUser)[contactList.SelectedItems[0].Index];
            
            List<global::Talk> talks = communicatior.archive.readTalks(logedUser, login);

            foreach (global::Talk talk in talks)
            {
                talkList.Items.Add(new ListViewItem(new String[] { talk.date, talk.firstMessage }));                
            }
        }

        private void talkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            talkView.Navigate("about:blank");
            if (talkList.SelectedItems.Count != 1)
            {
                return;
            }

            Communicator communicatior = Communicator.getInstance();
            string logedUser = communicatior.getLogedUser();

            string login = communicatior.archive.readTalker(logedUser)[contactList.SelectedItems[0].Index];

            List<global::Talk> talks = communicatior.archive.readTalks(logedUser, login);

            int talkId = talks[talkList.SelectedItems[0].Index].id;
            string url = string.Format(Communicator.getInstance().getBasePath() + "archive/{0}/{1}.html", logedUser, talkId);
            talkView.Navigate(url);            
        }
    }
}
