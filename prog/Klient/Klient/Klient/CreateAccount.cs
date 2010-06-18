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
        private MainForm mainForm;

        public CreateAccount(MainForm mainForm)
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
            if (login.Text == "" || password.Text == "" || name.Text == ""
                || surname.Text == "" || email.Text == "")
            {
                MessageBox.Show("Wypełnij wszystkie pola.");
            }
            else if (login.Text.Contains(';'))
            {
                MessageBox.Show("Login nie może zawierać średnika.");
            }
            else
            {
                Communicator c = Communicator.getInstance();
                if (c.createAccount(login.Text, password.Text, name.Text, surname.Text, email.Text, mainForm))
                {
                    Close();
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
