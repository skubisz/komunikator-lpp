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
using System.Net;
using System.Text.RegularExpressions;
using Npgsql;

namespace Serwer
{
    /// <summary>
    /// Klasa formularza ustalającego gniazdo nasłuchujące serwera, dziedzicząca po klasie Form.
    /// Okno jest modalne.
    /// </summary>
    public class ListenerWindow : Form
    {
        // Deklaracja zmiennych.
        private MainForm parent;
        private Label txtB4, txtB5;
        private TextBox txtB3, txtB6;
        private Button bOk, bAn, bGetIP, bGetLocalhost;

        /// <summary>
        /// Konstruktor klasy ListenerWindow.
        /// </summary>
        /// <param name="parent"> Główne okno aplikacji. </param> 
        public ListenerWindow(MainForm parent)
        {
            this.parent = parent;
            configWindow();
        }

        /// <summary>
        /// Metoda konfigurująca okno.
        /// </summary>
        private void configWindow()
        {
            // Konfiguruję ustawienia okna.
            Text = "Uruchamianie serwera";
            Size = new Size(500, 250);
            TopMost = true;

            txtB4 = new Label();
            txtB4.Location = new Point(10, 20);
            txtB4.Size = new Size(Width - 150, 25);
            txtB4.Text = "Podaj adres IP:";

            txtB3 = new TextBox();
            txtB3.Location = new Point(10, 50);
            txtB3.Size = new Size(Width - 150, 25);
            txtB3.Multiline = false;
            txtB3.Enabled = true;

            bGetIP = new Button();
            bGetIP.Location = new Point(Width - 130, 38);
            bGetIP.Size = new Size(80, 25);
            bGetIP.Text = "Moje IP";
            bGetIP.Click += new EventHandler(bGetIP_Click);

            bGetLocalhost = new Button();
            bGetLocalhost.Location = new Point(Width - 130, 68);
            bGetLocalhost.Size = new Size(80, 25);
            bGetLocalhost.Text = "Localhost";
            bGetLocalhost.Click += new EventHandler(bGetLocalhost_Click);

            txtB5 = new Label();
            txtB5.Location = new Point(10, 80);
            txtB5.Size = new Size(Width - 150, 25);
            txtB5.Text = "Podaj numer portu:";

            txtB6 = new TextBox();
            txtB6.Location = new Point(10, 110);
            txtB6.Size = new Size(Width - 150, 25);
            txtB6.Multiline = false;
            txtB6.Enabled = true;

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
            Controls.AddRange(new Control[] { bOk, bAn, bGetIP, bGetLocalhost, txtB4, txtB3, txtB5, txtB6 });
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
        /// Metoda obsługująca wciśnięcie przycisku Moje IP.
        /// </summary>
        /// <param name="sender"> Obiekt będący źródłem zdarzenia. </param>
        /// <param name="e"> Parametr zdarzenia. </param>
        protected void bGetIP_Click(object sender, EventArgs e)
        {
            txtB3.Text = Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[0].ToString();
        }

        /// <summary>
        /// Metoda obsługująca wciśnięcie przycisku Localhost.
        /// </summary>
        /// <param name="sender"> Obiekt będący źródłem zdarzenia. </param>
        /// <param name="e"> Parametr zdarzenia. </param>
        protected void bGetLocalhost_Click(object sender, EventArgs e)
        {
            txtB3.Text = "127.0.0.1";
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
                MessageBox.Show("Podaj IP");
            }
            else if (txtB6.Text == "")
            {
                MessageBox.Show("Podaj numer portu");
            }
            else
            {
                if ((isIPAddress(txtB3.Text)) && (isPortNumber(txtB6.Text)))
                {
                    Serwer listener = new Serwer(IPAddress.Parse(txtB3.Text), Int32.Parse(txtB6.Text));
                    listener.start();
                    parent.subm2.Visible = false;
                    Dispose();
                }
                else
                    MessageBox.Show("Wprowadzone dane nie są poprawne");
            }
        }

        /// <summary>
        /// Metoda sprawdzająca, czy podany adres IP jest poprawny.
        /// </summary>
        /// <param name="ip"> Sprawdzany adres IP. </param>
        /// <returns> Wartość boolowska, określająca czy adres IP jest poprawny. </returns>
        protected Boolean isIPAddress(String ip)
        {
            if (Regex.IsMatch(ip, @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$"))
            {
                string[] iP = ip.Split('.');
                if (int.Parse(iP[0]) > 255 || int.Parse(iP[1]) > 255 || int.Parse(iP[2]) > 255 || int.Parse(iP[3]) > 255)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Metoda sprawdzająca, czy podany numer portu jest poprawny.
        /// </summary>
        /// <param name="ip"> Sprawdzany numer portu. </param>
        /// <returns> Wartość boolowska, określająca czy numer portu jest poprawny. </returns>
        protected Boolean isPortNumber(String port)
        {
            int iPort = 0;

            if (Int32.TryParse(port, out iPort))
            {
                if ((iPort >= 0) && (iPort <= 65535))
                    return true;
            }
            
            return false;
        }
    }
}
