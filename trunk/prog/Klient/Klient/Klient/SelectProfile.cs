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
    public partial class SelectProfile : Form
    {
        private MainForm mainForm;

        public SelectProfile(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void actionLogin_Click(object sender, EventArgs e)
        {
            if (login.Text == "" || password.Text == "")
            {
                MessageBox.Show("Podaj nazwę użytkownika i hasło");
            }            
            else
            {
                Communicator c = Communicator.getInstance();
                if (c.login(login.Text, password.Text, mainForm))
                {
                    Close();

                }
            }
        }

        private void actionCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
