using System.Windows.Forms;

class Communicator
{
    private User _user;
    private Contacts _contacts;

    static Communicator instance = null;    

    private Communicator()
    {
        _user = new User();
        _contacts = new Contacts();
        _contacts.loadFromFile(contactFile);
    }

    public static Communicator getInstance()
    {
        if (instance == null)
        {
            instance = new Communicator();
        }

        return instance;
    }

    public bool login(string login, string password, Form form)
    {                
        try
        {
            if (_user.login(login, password))
            {
                form.Text = string.Format("{0} - E-Talk", _user.logedUser);

                _contacts.loadFromFile(contactFile);
                return true;
            }
            else
            {
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

    public bool createAccount(string login, string password, Form form)
    {        
        try
        {
            string result = _user.createAccount(login, password);
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
}