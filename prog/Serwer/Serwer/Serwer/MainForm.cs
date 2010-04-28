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
    public class MainForm : Form
    {
        private MainMenu menu;
        private MenuItem m1, m2, subm1;
        private StatusBarPanel sbPnlTime, menuTextProvider1, sbPnlDate;
        private LoginWindow lw;

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

            // Tworzę panel pomocniczego tekstu.
            menuTextProvider1 = new StatusBarPanel();
            menuTextProvider1.Name = "menuTextProvider1";
            menuTextProvider1.Width = Size.Width / 2 + Size.Width / 8;
            menuTextProvider1.Text = "";

            // Przypisuję utworzone menu.
            Menu = menu;

            // Dodaję dolny panel.
            BuildTimerHelpBar();

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
    }
}
