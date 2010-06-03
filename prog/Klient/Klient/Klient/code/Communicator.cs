using System.Windows.Forms;

class Communicator
{
    private User user;

    static Communicator instance = null;    

    private Communicator()
    {
        user = new User();
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
        User u = new User();
        try
        {
            if (u.login(login, password))
            {
                form.Text = string.Format("{0} - E-Talk", u.logedUser);
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

    public void connectionFail()
    {
        System.Windows.Forms.MessageBox.Show(
            "Nie udało się połączyć z serwerem",
            "Brak połączenia",
            System.Windows.Forms.MessageBoxButtons.OK,
            System.Windows.Forms.MessageBoxIcon.Error);
    }
}