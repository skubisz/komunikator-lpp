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
    public partial class Talk : Form
    {
        private int _talkId;
        private MainForm _parentForm;
        private string _contactLogin;
        private string _contactName;
        private string _login;

        public Talk(MainForm parentForm, string login, int talkId, string contactLogin, string contactName)
        {
            InitializeComponent();
            _talkId = talkId;
            _parentForm = parentForm;
            _contactLogin = contactLogin;
            _contactName = contactName;
            _login = login;

            Text += contactName;

            Communicator.getInstance().archive.createNewTalk(_login, _talkId, contactLogin);
            string url = string.Format(Communicator.getInstance().getBasePath() + "archive/{0}/{1}.html", _login, _talkId);
            
            //url = "file:///C:/komunikator/prog/Klient/Klient/Klient/bin/Debug/archive/c/35.html";
            talkBox.Navigate(url);            
        }

        private void Talk_Load(object sender, EventArgs e)
        {

        }

        private void Talk_FormClosed(object sender, FormClosedEventArgs e)
        {
            _parentForm.closeTalk(this);
        }

        public bool addMessage(string from, string fromName, string message)
        {
            if (from == _contactLogin)
            {
                //talkBox.DocumentText = message;

                Communicator.getInstance().archive.addMessage(_talkId, _login, from, fromName, DateTime.Now, message);
                string url = string.Format(Communicator.getInstance().getBasePath() + "archive/{0}/{1}.html", _login, _talkId);
                talkBox.Navigate(url);                

                return true;
            }
            else
            {
                return false;
            }
        }

        public string contactLogin
        {
            get { return contactLogin; }
        }

        public string contactName
        {
            get { return _contactName; }
        }
    }
}
