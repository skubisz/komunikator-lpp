
using System.Collections.Generic;
using System.IO;

public class Contact 
{
    private string _name;
    private string _login;

    public Contact(string contactString)
    {
        string[] split = contactString.Split(';');
        _name = split[0];
        _login = split[1];
    }

    public Contact(string name, string login)
    {
        _name = name;
        _login = login;
    }

    public string saveString()
    {
        return _name + ";" + _login;
    }

    public void update(string name, string login)
    {
        _name = name;
        _login = login;
    }

    public string login
    {
        get
        {
            return _login;
        }
    }

    public string name
    {
        get
        {
            return _name;
        }
    }
}

public class Contacts
{
    private List<Contact> list;

    public Contacts()
    {
        list = new List<Contact>();
    }

    public void loadFromFile(string file)
    {
        list.Clear();

        TextReader text;
        try
        {
            text = new StreamReader(file);
        }
        catch (FileNotFoundException )
        {
            TextWriter t = new StreamWriter(file);
            t.Close();
            text = new StreamReader(file);
        }

        string line;
        while ((line = text.ReadLine()) != null)
        {
            if (line != "")
            {
                list.Add(new Contact(line));
            }
        }

        text.Close();
    }

    public void saveToFile(string file)
    {        
        TextWriter text = new StreamWriter(file);

        foreach (Contact c in list)
        {
            text.WriteLine(c.saveString());            
        }

        text.Close();
    }

    public bool addContact(string name, string login)
    {
        login = login.Trim();
        if (findContact(login) == null)
        {
            list.Add(new Contact(name, login));
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool removeContact(string login)
    {
        login = login.Trim();
        Contact contact = findContact(login);
        if (contact == null)
        {
            return false;
        }
        else
        {
            list.Remove(contact);
            return true;
        }
    }

    public bool updateContact(string name, string login, string oldLogin)
    {
        oldLogin = oldLogin.Trim();
        login = login.Trim();

        Contact contact = findContact(oldLogin);
        if (contact == null)
        {
            return false;
        }

        if (oldLogin != login)
        {
            if (findContact(login) != null)
            {
                return false;
            }
        }

        contact.update(name, login);
        return true;
    }

    public Contact findContact(string login)
    {        
        foreach (Contact c in list)
        {
            if (login == c.login)
            {
                return c;
            }                    
        }

        return null;

    }

    public List<Contact> getList()
    {
        return list;
    }
}