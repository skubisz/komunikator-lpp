using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
            sharpGG.GGStatus = GGStatusType.Available;
            // Szukam działającego serwera.
            sharpGG.GGLogin(sharpGG.GGGetActiveServer());
            sharpGG.GGDescription = "nowy opis";
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