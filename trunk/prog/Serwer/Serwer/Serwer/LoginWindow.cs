using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Npgsql;

namespace Serwer
{
    /// <summary>
    /// Klasa formularza łączącego z bazą PostgreSQL dziedzicząca po klasie Form.
    /// Okno jest modalne.
    /// </summary>
    public class LoginWindow : Form
    {
        // Deklaracja zmiennych.
        private Label txtB4, txtB5;
        private TextBox txtB3, txtB6;
        private Button bOk, bAn;
        private MainForm parent;
        private MenuItem mi;
        public Serwer serwer;

        /// <summary>
        /// Konstruktor klasy LoginWindow.
        /// </summary>
        /// <param name="parent"> Główne okno aplikacji. </param>  
        /// <param name="mi"> Przycisk menu łączenia z bazą. </param>
        public LoginWindow(MainForm parent, MenuItem mi)
        {
            this.mi = mi;
            this.parent = parent;

            configWindow();
        }

        /// <summary>
        /// Metoda konfigurująca okno.
        /// </summary>
        private void configWindow()
        {
            // Konfiguruję ustawienia okna.
            Text = "Łączenie z bazą";
            Size = new Size(500, 250);
            TopMost = true;

            txtB4 = new Label();
            txtB4.Location = new Point(10, 20);
            txtB4.Size = new Size(Width - 150, 25);
            txtB4.Text = "Podaj nazwę użytkownika:";

            txtB3 = new TextBox();
            txtB3.Location = new Point(10, 50);
            txtB3.Size = new Size(Width - 150, 25);
            txtB3.Multiline = false;
            txtB3.Enabled = true;

            txtB5 = new Label();
            txtB5.Location = new Point(10, 80);
            txtB5.Size = new Size(Width - 150, 25);
            txtB5.Text = "Podaj hasło:";

            txtB6 = new TextBox();
            txtB6.Location = new Point(10, 110);
            txtB6.Size = new Size(Width - 150, 25);
            txtB6.Multiline = false;
            txtB6.Enabled = true;
            txtB6.UseSystemPasswordChar = true;

            // Tworzę przycisk OK.
            bOk = new Button();
            bOk.Location = new Point(10, 140);
            bOk.Size = new Size(50, 30);
            bOk.Text = "OK";
            bOk.Click += new EventHandler(bOk_Click);

            // Tworzę przycisk Anuluj.
            bAn = new Button();
            bAn.Location = new Point(100, 140);
            bAn.Size = new Size(50, 30);
            bAn.Text = "Anuluj";
            bAn.Click += new EventHandler(bAn_Click);

            // Dodaję komponenty okna.
            Controls.AddRange(new Control[] { bOk, bAn, txtB4, txtB3, txtB5, txtB6 });
        }

        /// <summary>
        /// Metoda obsługująca wciśnięcie przycisku Anuluj.
        /// </summary>
        /// <param name="sender"> Obiekt będący źródłem zdarzenia. </param>
        /// <param name="e"> Parametr zdarzenia. </param>
        protected void bAn_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        /// <summary>
        /// Metoda obsługująca wciśnięcie przycisku OK.
        /// </summary>
        /// <param name="sender"> Obiekt będący źródłem zdarzenia. </param>
        /// <param name="e"> Parametr zdarzenia. </param>
        protected void bOk_Click(object sender, EventArgs e)
        {
            if (txtB3.Text == "")
            {
                MessageBox.Show("Podaj nazwę użytkownika");
            }
            else if (txtB6.Text == "")
            {
                MessageBox.Show("Podaj hasło");
            }
            else 
            {
                // Połączenie z bazą.
                if (checkpass())
                {
                    NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;User Id=postgres;Password=przemek;Database=template1;");
                    conn.Open();


                    mi.Visible = false;
                    parent.subm2.Visible = true;
                    parent.lab1.Visible = true;
                    parent.lab2.Visible = true;
                    parent.lb1.Visible = true;
                    parent.lb2.Visible = true;
                    parent.but1.Visible = true;
                    QueryMaker qm = new QueryMaker(conn);
                    String[] users = qm.getClients();
                    parent.qm = qm;
                    parent.but1.Enabled = true;
                    for (int i = 0; i < users.Length; i+=2)
                    {
                        if (users[i]!=null)
                            parent.lb1.Items.Add(users[i+1]+" ("+ users[i]+")");
                    }

                    //usunąć to !!!!!!!!
                    /*Dictionary<String, String> data = new Dictionary<String, String>();
                    data["login"] = "Mikolaj";
                    data["haslo"] = "Mikel66";
                    data["status"] = "Niedostepny";
                    data["imie"] = "Mikel";
                    data["nazwisko"] = "Shinoda";
                    data["miasto"] = null;
                    data["kod"] = null;
                    data["email"] = "Mike@gmail.com";
                    data["data"] = null;
                    data["zainteresowania"] = "filmy";
                    if (qm.addClient(data))
                       MessageBox.Show("OK!");
                    else
                        MessageBox.Show("Err!");*/
                    //qm.deleteClient(21);
                    /*qm.makeFriends(18,22,"nick1");
                    qm.makeFriends(18, 23, "nick2");
                    List<Pair<Int32, String>> list = new List<Pair<Int32, String>>();
                    list = qm.getFriends(18);
                    foreach (Pair<Int32, String> p in list)
                    {
                        MessageBox.Show(p.First.ToString()+ " " + p.Second);
                    }*/
                    /*Dictionary<String, String> dict = new Dictionary<String, String>();
                    dict["login"] = "Mikel";
                    dict["status"] = "Dostepny";
                    qm.modifyClient(22, dict);*/
                    //qm.changeStatus(18, "Zaraz wracam");
                    Dispose();
                    serwer = new Serwer("127.0.0.1", 7777);
                    serwer.start();
                }
                else
                {
                    MessageBox.Show("Niepoprawna nazwa użytkownika i/lub hasło!");
                }
            }
        }

        /// <summary>
        /// Metoda sprawdzająca poprawność nazwy użytkownika i hasła.
        /// </summary>
        private bool checkpass()
        {
            return txtB3.Text == "postgres" && txtB6.Text == "przemek";
        }

    }

}
