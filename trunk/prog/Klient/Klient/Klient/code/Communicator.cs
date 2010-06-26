using System.Windows.Forms;
using Klient;
using System.Collections.Generic;

class Communicator
{
    private User _user;
    private Contacts _contacts;
    private Archive _archive;

    static Communicator instance = null;    

    private Communicator()
    {
        _user = new User();
        _contacts = new Contacts();
        _contacts.loadFromFile(contactFile);

        _archive = new Archive();

        //_archive.createNewTalk("a", 3);
        //_archive.addMessage(3, "a", "a", "A", new System.DateTime(), "nowa wiadomo<b>a</b>sc\n\ndef");
        //_archive.addMessage(3, "a", "x", "X", new System.DateTime(), "nowa wiadomo<b>a</b>sc\n\ndef");
    }

    public static Communicator getInstance()
    {
        if (instance == null)
        {
            instance = new Communicator();
        }

        return instance;
    }

    public bool login(string login, string password, MainForm form)
    {                
        try
        {
            if (_user.login(login, password))
            {
                form.Text = string.Format("{0} - E-Talk", _user.logedUser);

                _contacts.loadFromFile(contactFile);
                form.enableMenuItems();
                _archive.createNewArchive(login);
                return true;
            }
            else
            {
                form.disableMenuItems();
                MessageBox.Show("Podałeś nieprawidłowe dane");
            }
        }
        catch (ConnectionException)
        {
            connectionFail();   
        }

        form.Text = "E-Talk";
        return false;
    }

    public bool createAccount(string login, string password, string name, string surname, string email, MainForm form)
    {        
        try
        {
            string result = _user.createAccount(login, password, name, surname, email);
            if (result == "usernameExists")
            {
                MessageBox.Show("Ta nazwa użytkownika jest już zarezerwowana.");
            }
            else if (result == "success")
            {
                return this.login(login, password, form);                
            }
            else
            {
                MessageBox.Show("Nie udało się założyć nowego konta.");
            }
        }
        catch (ConnectionException)
        {
            connectionFail();
        }

        
        return false;
    }

    public void connectionFail()
    {
        System.Windows.Forms.MessageBox.Show(
            "Nie udało się połączyć z serwerem",
            "Brak połączenia",
            System.Windows.Forms.MessageBoxButtons.OK,
            System.Windows.Forms.MessageBoxIcon.Error);
    }

    public Contacts contacts
    {
        get
        {
            return _contacts;
        }
    }

    public string contactFile
    {
        get
        {
            return string.Format("contacts_{0}.txt", _user.logedUser);
        }
    }

    public List<CommunicatorMessage> readMessages()
    {
        Connection conn = Connection.getInstance();
        MessageFactory messageFactory = MessageFactory.getInstance();
        string message = messageFactory.getMessagesMessage(_user.logedUser);
        string response = conn.sendMessage(message);

        ServerResponse serverResponse = new ServerResponse(response);

        ServerResponseParams par = serverResponse.getParams();        
        int n = int.Parse(par["messages"]);

        List<CommunicatorMessage> result = new List<CommunicatorMessage>();

        for (int i = 1; i <= n; i++)
        {
            result.Add(new CommunicatorMessage(par.getExtraData(i), par[i]));
        }

        return result;
    }

    public Archive archive
    {
        get { return _archive; }
    }

    public string getLogedUser()
    {
        return _user.logedUser;
    }

    public string getBasePath()
    {
        string path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
        path = path.Substring(0, path.Length - 10);
        return path;
    }

    public void sendMessage(string message, string to)
    {
        Connection conn = Connection.getInstance();
        MessageFactory messageFactory = MessageFactory.getInstance();
        string messageToServer = messageFactory.sendMessageMessage(_user.logedUser, message, to);
        string response = conn.sendMessage(messageToServer);                
    }

    public void changePassword(string newPassword)
    {
        Connection conn = Connection.getInstance();
        MessageFactory messageFactory = MessageFactory.getInstance();
        string messageToServer = messageFactory.changePasswordMessage(_user.logedUser, newPassword);
        string response = conn.sendMessage(messageToServer);
        
    }

    public void refreshContactsStatus(MainForm main)
    {
        List<Contact> list = contacts.getList();
        List<string> usernames = new List<string>();

        foreach (Contact user in list)
        {
            usernames.Add(user.login);
        }

        Connection conn = Connection.getInstance();
        MessageFactory messageFactory = MessageFactory.getInstance();
        string message = messageFactory.refreshContactsStatusMessage(usernames);
        string response = conn.sendMessage(message);

        ServerResponse serverResponse = new ServerResponse(response);

        ServerResponseParams par = serverResponse.getParams();
        int index = 0;
        foreach (string login in usernames)
        {
            //MessageBox.Show(login + " " + par[login]);
            main.updateStatus(login, index, par[login]);
            index++;
        }        
    }

    public void changeStatus(string newStatus)
    {
        if (newStatus == "dostepny" || newStatus == "niedostepny" || newStatus == "niewidoczny")
        {
            Connection conn = Connection.getInstance();
            MessageFactory messageFactory = MessageFactory.getInstance();
            string message = messageFactory.changeStatusMessage(_user.logedUser, newStatus);
            string response = conn.sendMessage(message);
        }
    }
}