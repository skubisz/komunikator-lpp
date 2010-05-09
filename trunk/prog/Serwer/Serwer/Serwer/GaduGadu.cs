using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using HAKGERSoft;

namespace Serwer
{
    /// <summary>
    /// Klasa obsługująca Gadu-Gadu.
    /// </summary>
    public class GaduGadu
    {
        private String passwd;
        private Int32 numer;
        private sHGG sharpGG;

        /// <summary>
        /// Konstruktor klasy Gadu.
        /// </summary>
        /// <param name="numer"> Numer GG. </param>
        /// <param name="passwd"> Hasło dla powyższego numeru. </param>
        public GaduGadu(Int32 numer, String passwd)
        {
            this.numer = numer;
            this.passwd = passwd;
        }

        /// <summary>
        /// Łączenienie i uwierzytelnianie z serwerem GG.
        /// </summary>
        public void connect()
        {
            sharpGG = new sHGG();
            // numer GG
            sharpGG.GGNumber = numer.ToString();
            // hasło GG
            sharpGG.GGPassword = passwd;
            // Szukam działającego serwera.
            sharpGG.GGStatus = GGStatusType.Available; // status na start
            sharpGG.GGLogin(sharpGG.GGGetActiveServer()); // zalogowanie
            // Obsługa zdarzenia odebrania wiadomości.
            sharpGG.GGMessageReceive += new sHGG.GenericEventHandler<sHGG.MessageReceiveEventArgs>(gadu_GGMessageReceive);
        }

        /// <summary>
        /// Rozłączane się z serwerem i wylogowywanie.
        /// </summary>
        public void disconnect()
        {
            Thread.Sleep(20000);
            // Wylogowujemy się.
            sharpGG.GGLogout();
        }

        /// <summary>
        /// Wysyłanie wiadomości.
        /// </summary>
        /// <param name="numer"> Numer GG, na który wysyłamy wiadomość. </param>
        /// <param name="wiadomosc"> Wysyłana wiadomość. </param>
        public void sendMessage(Int32 numer, String wiadomosc)
        {
            sharpGG.GGSendMessage(numer, wiadomosc);
        }

        /// <summary>
        /// Odbieranie wiadomości.
        /// </summary>
        /// <param name="sender"> Źródło zdarzenia. </param>
        /// <param name="args"> Informacje o wiadomości. </param>
        public static void gadu_GGMessageReceive(object sender, sHGG.MessageReceiveEventArgs args)
        {
            MessageBox.Show("Odebralem wiadomosc: " + args.Message + " od: " + args.Number);
            getMsg(args.Number, args.Message);
        }

        /// <summary>
        /// Zmiana statusu.
        /// </summary>
        /// <param name="status"> Nowy status. </param>
        /// <param name="opis"> Nowy opis. </param>
        public void changeStatus(String status, String opis)
        {
            if (status.CompareTo("Dostepny") == 0)
                sharpGG.GGStatus = GGStatusType.Available;
            else if (status.CompareTo("Niedostepny") == 0)
                sharpGG.GGStatus = GGStatusType.NotAvailable;
            else if (status.CompareTo("Zajety") == 0)
                sharpGG.GGStatus = GGStatusType.Busy;
            else if (status.CompareTo("Niewidoczny") == 0)
                sharpGG.GGStatus = GGStatusType.Invisible;

            sharpGG.GGDescription = opis;      
        }

        /// <summary>
        /// Wysyła obrazek z wiadmością tekstową.
        /// </summary>
        /// <param name="numer"> Numer adresata. </param>
        /// <param name="pos"> Pozycja obrazka. </param>
        /// <param name="img"> Obrazek. </param>
        /// <param name="msg"> Wiadmość tekstowa. </param>
        public void sendImage(Int32 numer, Int32 pos, Image img, String msg)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);

            sharpGG.GGSendImage(numer, msg, pos, ms);
        }

        /// <summary>
        /// Zwracanie wiadomości.
        /// </summary>
        /// <param name="sender"> Numer nadawcy. </param>
        /// <param name="msg"> Treść wiadmości. </param>
        /// <returns> Zwraca parę (nadawca, wiadomość). </returns>
        public static Pair<Int32, String> getMsg(Int32 sender, String msg)
        {
            return new Pair<Int32, String>(sender, msg);
        }
    }

    /// <summary>
    /// Klasa generyczna para (brakuje takiej w standardzie .NET !!11!).
    /// </summary>
    public class Pair<T, U>
    {
        /// <summary>
        /// Konstruktor klasy Pair.
        /// </summary>
        public Pair()
        {
        }

        /// <summary>
        /// Konstruktor klasy Pair.
        /// </summary>
        /// <param name="first"> Pierwszy element pary. </param>
        /// <param name="second"> Drugi element pary. </param>
        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }

        // Settery i gettery ... 
        public T First { get; set; }
        public U Second { get; set; }
    };

}