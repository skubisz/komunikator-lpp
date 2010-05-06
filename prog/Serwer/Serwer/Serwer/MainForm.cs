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
    /// Klasa głównego formularza dziedzicząca po klasie Form.
    /// </summary>
    public class MainForm : Form
    {
        private MainMenu menu;
        public MenuItem m1, m2, subm1, subm2;
        private StatusBarPanel sbPnlTime, menuTextProvider1, sbPnlDate;
        private LoginWindow lw;
        public Label lab1, lab2;
        public ListBox lb1, lb2;
        public QueryMaker qm;
        public Button but1;
        public Serwer listener;

        /// <summary>
        /// Metoda główna.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.Run(new MainForm());
        }

        /// <summary>
        /// Konstruktor klasy MainForm.
        /// </summary>
        public MainForm()
        {
            // Konfiguruję ustawienia okna.
            Size = new Size(1050, 600);
            Location = new Point(0, 0);
            Text = "Serwer komunikatora internetowego e-Talk";
            
            // Tworzę komponenty okna głównego:
            // - menu główne
            menu = new MainMenu();

            // - komponenty menu głównego
            m1 = new MenuItem("Ustawienia");
            menu.MenuItems.Add(m1);

            m2 = new MenuItem("Wyjście", new EventHandler(MMWyjscieClick), Shortcut.CtrlW);
            m2.Select += new EventHandler(MMExitSelect);
            menu.MenuItems.Add(m2);

            // - podmenu ustawień
            subm1 = new MenuItem("Połącz z bazą", new EventHandler(MMLoginClick), Shortcut.CtrlL);
            subm1.Select += new EventHandler(MMLoginSelect);
            m1.MenuItems.Add(subm1);

            subm2 = new MenuItem("Nasłuchuj", new EventHandler(MMListenClick), Shortcut.CtrlN);
            subm2.Select += new EventHandler(MMListenSelect);
            subm2.Visible = false;
            m1.MenuItems.Add(subm2);

            // Tworzę panel pomocniczego tekstu.
            menuTextProvider1 = new StatusBarPanel();
            menuTextProvider1.Name = "menuTextProvider1";
            menuTextProvider1.Width = Size.Width / 2 + Size.Width / 8;
            menuTextProvider1.Text = "";

            // Przypisuję utworzone menu.
            Menu = menu;

            // Dodaję dolny panel.
            BuildTimerHelpBar();

            // Dodaję wewnętrzne komponenty.
            BuildInnerComponents();

        }

        /// <summary>
        /// Metoda obsługująca wskazanie kursorem na przycisk wyjścia.
        /// </summary>
        /// <param name="sender"> Obiekt będący źródłem zdarzenia. </param>
        /// <param name="e"> Parametr zdarzenia. </param>
        protected void MMExitSelect(object sender, EventArgs e)
        {
            menuTextProvider1.Text = "Wyjście z programu.";
        }

        /// <summary>
        /// Metoda obsługująca wskazanie kursorem na przycisk łączenia z bazą.
        /// </summary>
        /// <param name="sender"> Obiekt będący źródłem zdarzenia. </param>
        /// <param name="e"> Parametr zdarzenia. </param>
        protected void MMLoginSelect(object sender, EventArgs e)
        {
            menuTextProvider1.Text = "Łączy z bazą PostgreSQL";
        }

        /// <summary>
        /// Metoda obsługująca wskazanie kursorem na przycisk nasłuchiwania.
        /// </summary>
        /// <param name="sender"> Obiekt będący źródłem zdarzenia. </param>
        /// <param name="e"> Parametr zdarzenia. </param>
        protected void MMListenSelect(object sender, EventArgs e)
        {
            menuTextProvider1.Text = "Rozpoczyna nasłuchiwanie serwera na połączenia klientów";
        }

        /// <summary>
        /// Metoda obsługująca wciśnięcie przycisku wyjścia.
        /// </summary>
        /// <param name="sender"> Obiekt będący źródłem zdarzenia. </param>
        /// <param name="e"> Parametr zdarzenia. </param>
        protected void MMWyjscieClick(object sender, EventArgs e)
        {
            // Wyświetlam okno dialogowe, aby upewnić się, czy użytkownik chce wyjść.
            DialogResult result = MessageBox.Show("Czy na pewno chcesz wyłączyć serwer komunikatora internetowego e-Talk ?", "Wyjście", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Metoda obsługująca wciśnięcie przycisku łączenia z bazą.
        /// </summary>
        /// <param name="sender"> Obiekt będący źródłem zdarzenia. </param>
        /// <param name="e"> Parametr zdarzenia. </param>
        protected void MMLoginClick(object sender, EventArgs e)
        {
            lw = new LoginWindow(this, subm1);
            lw.ShowDialog();
        }

        /// <summary>
        /// Metoda obsługująca wciśnięcie przycisku nasłuchiwania.
        /// </summary>
        /// <param name="sender"> Obiekt będący źródłem zdarzenia. </param>
        /// <param name="e"> Parametr zdarzenia. </param>
        protected void MMListenClick(object sender, EventArgs e)
        {
            listener = new Serwer("127.0.0.1", 8888);
            listener.start();
        }

        /// <summary>
        /// Metoda obsługująca wciśnięcie przycisku pokaż szczegóły o użytkowniku.
        /// </summary>
        /// <param name="sender"> Obiekt będący źródłem zdarzenia. </param>
        /// <param name="e"> Parametr zdarzenia. </param>
        protected void MMDetailsClick(object sender, EventArgs e)
        {
            String numer = ((String)lb1.SelectedItem).Split((Char[])new Char[] { '(', ')' })[1];
            String[] data = qm.getClientDetail(numer);
            lb2.Items.Clear();
            lb2.Items.AddRange((String[])new String[] { "numer: " + numer, "imię: " + data[0], "nazwisko: " + data[1],
                                                         "miasto: " + data[2], "kod_pocztowy: " + data[3],
                                                         "e_mail: " + data[4], "data_ur: " + data[5], 
                                                         "zainteresowania: " + data[6]});       
        }


        /// <summary>
        /// Metoda obsługująca zegar - nasłuchiwacz obiektu Timer.
        /// </summary>
        /// <param name="sender"> Obiekt będący źródłem zdarzenia. </param>
        /// <param name="e"> Parametr zdarzenia. </param>
        protected void timer1_Tick(object sender, EventArgs e)
        {
            DateTime t = DateTime.Now;
            string s = t.ToLongTimeString();
            sbPnlTime.Text = s;
        }

        /// <summary>
        /// Metoda wyświetlająca zegar i pomoc.
        /// </summary>
        private void BuildTimerHelpBar()
        {
            // Ustawiam zegar.
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Tick += new EventHandler(timer1_Tick);

            // Pobieram datę.
            DateTime CurrTime = DateTime.Now;

            // Ustawiam panel zegara.
            StatusBar statusBar = new StatusBar();
            statusBar.ShowPanels = true;
            sbPnlTime = new StatusBarPanel();
            sbPnlTime.Alignment = HorizontalAlignment.Left;
            sbPnlTime.Width = Size.Width / 8;

            // Ustawiam panel daty.
            sbPnlDate = new StatusBarPanel();
            sbPnlDate.Alignment = HorizontalAlignment.Left;
            sbPnlDate.Width = Size.Width / 4;
            sbPnlDate.Text = CurrTime.DayOfWeek + "\t\t" + CurrTime.Year + "." + CurrTime.Month + "." + CurrTime.Day;

            // Tworzę panel pomocniczego tekstu.
            menuTextProvider1 = new StatusBarPanel();
            menuTextProvider1.Name = "menuTextProvider1";
            menuTextProvider1.Width = Size.Width / 2 + Size.Width / 8;
            menuTextProvider1.Text = "";

            // Dodaję komponenty.
            statusBar.Panels.AddRange((StatusBarPanel[])new StatusBarPanel[] { menuTextProvider1, sbPnlDate, sbPnlTime });
            Controls.Add(statusBar);
        }


        /// <summary>
        /// Metoda wewnętrzne komponenty okna głównego.
        /// </summary>
        private void BuildInnerComponents()
        {
            // Dodaję napisy.
            lab1 = new Label();
            lab1.Location = new Point(50, 50);
            lab1.Size = new Size(250, 20);
            lab1.Text = "Użytkownicy w bazie -- użytkownik(numer)";

            lab2 = new Label();
            lab2.Location = new Point(50, 200);
            lab2.Size = new Size(250, 20);
            lab2.Text = "Szczegółowe dane wybranego użytkownika:";

            // Dodaję ListBoxy.
            lb1 = new ListBox();
            lb1.Location = new Point(50, 70);
            lb1.Width = 300;
            lb1.SelectedIndexChanged += new EventHandler(MMDetailsClick);

            lb2 = new ListBox();
            lb2.Location = new Point(50, 220);
            lb2.Width = 300;

            Controls.AddRange((Control[])new Control[] {lab1, lab2, lb1, lb2});

            lab1.Visible = false;
            lab2.Visible = false;
            lb1.Visible = false;
            lb2.Visible = false;
        }
    }
}
