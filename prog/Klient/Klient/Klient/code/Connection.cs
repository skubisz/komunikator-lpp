
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

class ConnectionException : Exception
{
}

class Connection
{
    static Connection instance = null;    

    private Connection()
    {        
    }

    public static Connection getInstance()
    {
        if (instance == null)
        {
            instance = new Connection();
        }

        return instance;
    }

    public string sendMessage(string message)
    {
        TcpClient client = new TcpClient();
        try
        {
            client.Connect(Settings.serverIpAddress, Settings.serverPort);
        }
        catch(Exception)
        {
            throw new ConnectionException();
        }

        if (client.Connected)
        {            
            NetworkStream stream = client.GetStream();            
                        
            byte[] byteMessage = Encoding.ASCII.GetBytes(message);
            stream.Write(byteMessage, 0, byteMessage.Length);

            byte[] buffer = new byte[(int)client.ReceiveBufferSize];

            stream.Read(buffer, 0, (int)client.ReceiveBufferSize);
            
            stream.Close();
            client.Close();            

            return System.Text.ASCIIEncoding.ASCII.GetString(buffer);
        }
        else
        {
            throw new ConnectionException();
            /*
            System.Windows.Forms.MessageBox.Show(
                "Nie udało się połączyć z serwerem", 
                "Brak połączenia", 
                System.Windows.Forms.MessageBoxButtons.OK, 
                System.Windows.Forms.MessageBoxIcon.Error);
             */
        }

        

    }
}