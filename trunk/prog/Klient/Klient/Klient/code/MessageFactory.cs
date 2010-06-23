
using System.Web;

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

    
}
