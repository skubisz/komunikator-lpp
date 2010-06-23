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
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void changePasswordButton_Click(object sender, EventArgs e)
        {
            if (newPassword.Text == "" || repeatNewPassword.Text == "")
            {
                MessageBox.Show("Wypełnij wszystkie pola", "Niepoprawne dane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (newPassword.Text != repeatNewPassword.Text)
            {
                MessageBox.Show("Wartości obu pól nie są identyczne", "Niepoprawne dane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Communicator.getInstance().changePassword(newPassword.Text);
                MessageBox.Show("Hasło zostało zmienione", "Hasło zostało zmienione", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }            
        }
    }
}
