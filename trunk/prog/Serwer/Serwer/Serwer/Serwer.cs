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
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;User Id=postgres;Password=przemek;Database=template1;");
            conn.Open();
            QueryMaker qm = new QueryMaker(conn);

            serverSocket = new TcpListener(ip, port);
            TcpClient clientSocket;

            serverSocket.Start();

            int counter = 0;

            while (true)
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
                HandleClient client = new HandleClient(clientSocket, Convert.ToString(counter), qm);
                Thread thread = new Thread(new ThreadStart(client.getMessage));
                thread.Start();
            }
            //conn.Close();
            clientSocket.Close();
            serverSocket.Stop();

        }
    }

    /// <summary>
    /// Klasa obsługująca każdego klienta oddzielnie jako osobny wątek.
    /// </summary>    
    public class HandleClient
    {
        TcpClient clientSocket;
        string clNo;
        private NetworkStream ns;
        private StreamWriter sw;
        private String request = "";
        private String type;
        private String response;
        private QueryMaker qm;

        /// <summary>
        /// Tworzymy nowy wątek dla klienta.
        /// </summary>
        /// <param name="inClientSocket"> Obiekt reprezentujący klienta. </param>  
        /// <param name="clientNo"> Numer klienta w kolejce. </param>
        public HandleClient(TcpClient inClientSocket, string clineNo, QueryMaker qm)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            this.qm = qm;
        }

        public void getMessage()
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

            if (type.CompareTo("login") == 0)
            {
                MessageFactory mf = MessageFactory.getInstance();
                if (qm.login(param["number"], param["password"]))
                    response = mf.loginMessage("success");
                else
                   response = mf.loginMessage("fail");

                sendMessage(response);
            }
            else if (type.CompareTo("createAccount") == 0)
            {
                MessageFactory mf = MessageFactory.getInstance();
                Dictionary<String, String> d = new Dictionary<String, String>();
                d.Add("login", param["username"]);
                d.Add("haslo", param["password"]);
                d.Add("email", param["email"]);
                d.Add("imie", param["name"]);
                d.Add("nazwisko", param["surname"]);
                d.Add("status", "Niedostepny");
                d.Add("zainteresowania", null);
                d.Add("data", null);
                d.Add("kod", null);
                d.Add("miasto", null);
                int result = qm.addClient(d);

                if (result == 0)
                    response = mf.createAccountMessage("success");
                else if (result == 1)
                    response = mf.createAccountMessage("fail");
                else if (result == 2)
                    response = mf.createAccountMessage("usernameExists");

                sendMessage(response);
            }
            else if (type.CompareTo("sendMessage") == 0)
            {
                qm.saveMessage(param["username"], param["to"], param["message"]);
                MessageFactory mf = MessageFactory.getInstance();
                response = mf.sendMessageMessage();
                sendMessage(response);
            }
            else if (type.CompareTo("getMessages") == 0)
            {
                List<Pair<String, String>> list = qm.getMessage(param["username"]);
                MessageFactory mf = MessageFactory.getInstance();
                response = mf.getMessagesMessage(list.Count,list);
                sendMessage(response);
            }
            else if (type.CompareTo("changeStatus") == 0)
            {
                qm.changeStatus(param["username"], param["newStatus"]);
                MessageFactory mf = MessageFactory.getInstance();
                response = mf.changeStatusMessage();
                sendMessage(response);
            }
            else if (type.CompareTo("changePassword") == 0)
            {
                Dictionary<String, String> d = new Dictionary<String, String>();
                d.Add("haslo", param["newPassword"]);
                qm.modifyClient(param["username"], d);
                MessageFactory mf = MessageFactory.getInstance();
                response = mf.changePasswordMessage();
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
