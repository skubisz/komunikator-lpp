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
using Npgsql;
using NpgsqlTypes;


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
            TcpClient clientSocket;

            serverSocket.Start();

            int counter = 0;

            while (true)
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
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
        private NetworkStream ns;
        private StreamWriter sw;
        private String request = "";
        private String type;
        private String response;

        /// <summary>
        /// Tworzymy nowy wątek dla klienta.
        /// </summary>
        /// <param name="inClientSocket"> Obiekt reprezentujący klienta. </param>  
        /// <param name="clientNo"> Numer klienta w kolejce. </param>
        public void startClient(TcpClient inClientSocket, string clineNo)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            Thread ctThread = new Thread(getMessage);
            ctThread.Start();
        }

        private void getMessage()
        {
            ns = clientSocket.GetStream();
            sw = new StreamWriter(ns);

            StringBuilder stringBuilderResult = new StringBuilder();

            byte[] buffer = new byte[(int)clientSocket.ReceiveBufferSize];

            ns.Read(buffer, 0, (int)clientSocket.ReceiveBufferSize);

            request = System.Text.ASCIIEncoding.ASCII.GetString(buffer);

            //MessageBox.Show(request);

            ClientRequest cr = new ClientRequest(request);
            type = cr.getType();
            ClientRequestParams param = cr.getParams();

            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;User Id=postgres;Password=przemek;Database=template1;");
            conn.Open();
            QueryMaker qm = new QueryMaker(conn);

            if (type.CompareTo("login") == 0)
            {
                MessageFactory mf = MessageFactory.getInstance();
                if (qm.login(param["number"], param["password"]))
                    response = mf.loginMessage("success");
                else
                   response = mf.loginMessage("fail");

                sendMessage(response);
            }
        }

        private void sendMessage(String response)
        {
            byte[] byteMessage = Encoding.ASCII.GetBytes(response);
            ns.Write(byteMessage, 0, byteMessage.Length);
        }

        private void doChat()
        {
            //TODO
        }
    } 

}
