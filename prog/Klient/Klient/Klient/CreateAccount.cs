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
    public partial class CreateAccount : Form
    {
        private Form mainForm;

        public CreateAccount(Form mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (login.Text == "" || password.Text == "")
            {
                MessageBox.Show("Podaj nazwę użytkownika i hasło");
            }
            else
            {
                Communicator c = Communicator.getInstance();
                if (c.createAccount(login.Text, password.Text, mainForm))
                {
                    Close();
                }
            }
        }
    }
}
