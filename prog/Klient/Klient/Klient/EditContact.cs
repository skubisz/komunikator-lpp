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
    public partial class EditContact : Form
    {
        private string currentMode;
        private string oldLogin;        

        public EditContact()
        {
            InitializeComponent();
            currentMode = "add";
            oldLogin = "";

            Text = "Dodaj nowy kontakt";
            confirmButton.Text = "Dodaj";
        }

        public EditContact(string name, string login)
        {
            InitializeComponent();

            currentMode = "edit";                        
            oldLogin = login;

            contactLogin.Text = login;
            contactName.Text = name;

            Text = "Edycja kontaktu";
            confirmButton.Text = "Zmień";  
        }

        public void setMode(string newMode)
        {
            if (newMode == "edit")
            {
                currentMode = "edit";
                Text = "Edycja kontaktu";
                confirmButton.Text = "Zmień";
            }
            else if (newMode == "add")
            {
                currentMode = "add";
                Text = "Dodaj nowy kontakt";
                confirmButton.Text = "Dodaj";
            }
            else
            {
                throw new Exception("Nieprawidłowy tryb działania");
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            string name = contactName.Text.Trim();
            string login = contactLogin.Text.Trim();

            Contacts contacts = Communicator.getInstance().contacts;

            if (name == "" || login == "")
            {
                MessageBox.Show("Wypełnij wszystkie pola", "Niepoprawne dane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (currentMode == "edit")
            {
                if (contacts.updateContact(name, login, oldLogin))
                {
                    Close();
                }
                else
                {
                    MessageBox.Show("Ten login jest już na liście kontaktów", "Niepoprawne dane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                if (contacts.addContact(name, login))
                {
                    Close();
                }
                else
                {
                    MessageBox.Show("Ten login jest już na liście kontaktów", "Niepoprawne dane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }
    }
}
