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

        public EditContact()
        {
            InitializeComponent();
            currentMode = "edit";
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
    }
}
