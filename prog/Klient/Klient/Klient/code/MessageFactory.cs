
using System.Web;
using System.Collections.Generic;

class MessageFactory
{
    static MessageFactory instance = null;    

    private MessageFactory()
    {            
    }

    static public MessageFactory getInstance()
    {
        if (instance == null)
        {
            instance = new MessageFactory();
        }
        return instance;
    }

    public string loginMessage(string number, string password)
    {        
        return string.Format(
            "<request>" + 
            "   <type>login</type>" + 
            "   <params>" + 
            "       <param name=\"number\" value=\"{0}\" />" + 
            "       <param name=\"password\" value=\"{1}\" />" + 
            "   </params>" + 
            "</request>",
            HttpUtility.HtmlEncode(number), 
            HttpUtility.HtmlEncode(password)
        );
    }

    public string createAccountMessage(string login, string password, string name, string surname, string email)
    {
        return string.Format(
            "<request>" +
            "   <type>createAccount</type>" +
            "   <params>" +
            "       <param name=\"username\" value=\"{0}\" />" +
            "       <param name=\"password\" value=\"{1}\" />" +
            "       <param name=\"name\" value=\"{2}\" />" +
            "       <param name=\"surname\" value=\"{3}\" />" +
            "       <param name=\"email\" value=\"{4}\" />" +
            "   </params>" +
            "</request>",
            HttpUtility.HtmlEncode(login),
            HttpUtility.HtmlEncode(password),
            HttpUtility.HtmlEncode(name),
            HttpUtility.HtmlEncode(surname),
            HttpUtility.HtmlEncode(email)
        );
    }

    public string getMessagesMessage(string login)
    {
        return string.Format(
            "<request>" +
            "   <type>getMessages</type>" +
            "   <params>" +
            "       <param name=\"username\" value=\"{0}\" />" +            
            "   </params>" +
            "</request>",
            HttpUtility.HtmlEncode(login)            
        );
    }

    public string sendMessageMessage(string login, string message, string to)
    {
        return string.Format(
            "<request>" +
            "   <type>sendMessage</type>" +
            "   <params>" +
            "       <param name=\"username\" value=\"{0}\" />" +
            "       <param name=\"message\" value=\"{1}\" />" +
            "       <param name=\"to\" value=\"{2}\" />" +
            "   </params>" +
            "</request>",
            HttpUtility.HtmlEncode(login),
            HttpUtility.HtmlEncode(message),
            HttpUtility.HtmlEncode(to)
        );
    }

    public string changePasswordMessage(string login, string newPassword)
    {
        return string.Format(
            "<request>" +
            "   <type>changePassword</type>" +
            "   <params>" +
            "       <param name=\"username\" value=\"{0}\" />" +
            "       <param name=\"newPassword\" value=\"{1}\" />" +            
            "   </params>" +
            "</request>",
            HttpUtility.HtmlEncode(login),
            HttpUtility.HtmlEncode(newPassword)            
        );
    }

    public string refreshContactsStatusMessage(List<string> friends)
    {
        string result = "<request>" +
            "   <type>refreshContactsStatus</type>" +
            "   <params>\n";

        result += string.Format("       <param name=\"users\" value=\"{0}\" />\n",
                HttpUtility.HtmlEncode(friends.Count.ToString())
            );    

        int index = 1;
        foreach (string username in friends)
        {
            result += string.Format("       <param name=\"username{0}\" value=\"{1}\" />\n",
                index,
                HttpUtility.HtmlEncode(username)
            );
            index++;
        }

        result += "   </params>" +
            "</request>";

        return result;        
    }

    public string changeStatusMessage(string login, string newStatus)
    {
        return string.Format(
            "<request>" +
            "   <type>changeStatus</type>" +
            "   <params>" +
            "       <param name=\"username\" value=\"{0}\" />" +
            "       <param name=\"newStatus\" value=\"{1}\" />" +
            "   </params>" +
            "</request>",
            HttpUtility.HtmlEncode(login),
            HttpUtility.HtmlEncode(newStatus)
        );
    }
}
