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
using System.Net.Sockets;


namespace Serwer
{
    /// <summary>
    /// Klasa wielowątkowego serwera TCP.
    /// </summary>
    public class Serwer
    {
        private IPAddress ip;
        private Int32 port;
        public TcpListener serverSocket;

        /// <summary>
        /// Konstruktor klasy serwera -- wersja z adresem IP jako typ IPAddress.
        /// </summary>
        /// <param name="ip"> Adres ip serwera. </param>  
        /// <param name="port"> Numer portu, na którym ma nasłuchiwać serwer. </param>
        public Serwer(IPAddress ip, Int32 port)
        {
            this.ip = ip;
            this.port = port;
        }

        /// <summary>
        /// Konstruktor klasy serwera -- wersja z adresem IP jako typ String.
        /// </summary>
        /// <param name="ip"> Adres ip serwera. </param>  
        /// <param name="port"> Numer portu, na którym ma nasłuchiwać serwer. </param>
        public Serwer(String ip, Int32 port)
        {
            this.ip = IPAddress.Parse(ip);
            this.port = port;
        }


        /// <summary>
        /// Pojedyńczy wątek serwera.
        /// </summary>
        public void start()
        {
            serverSocket = new TcpListener(ip, port);
            TcpClient clientSocket = default(TcpClient);

            serverSocket.Start();

            int counter = 0;

            while (true)
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
                MessageBox.Show("Klient numer:" + Convert.ToString(counter) + " połączył się!");
                HandleClinet client = new HandleClinet();
                client.startClient(clientSocket, Convert.ToString(counter));
            }

            clientSocket.Close();
            serverSocket.Stop();

        }
    }

    /// <summary>
    /// Klasa obsługująca każdego klienta oddzielnie jako osobny wątek.
    /// </summary>    
    public class HandleClinet
    {
        TcpClient clientSocket;
        string clNo;

        /// <summary>
        /// Tworzymy nowy wątek dla klienta.
        /// </summary>
        /// <param name="inClientSocket"> Obiekt reprezentujący klienta. </param>  
        /// <param name="clientNo"> Numer klienta w kolejce. </param>
        public void startClient(TcpClient inClientSocket, string clineNo)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            Thread ctThread = new Thread(doChat);
            ctThread.Start();
        }
        private void doChat()
        {
            //TODO
        }
    } 

}
