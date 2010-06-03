
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
            number, 
            password
        );
    }
}