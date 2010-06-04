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

    public string loginMessage(string result)
    {
        return string.Format(
            "<response>" +
            "   <type>login</type>" +
            "   <params>" +
            "       <param name=\"result\" value=\"{0}\" />" +
            "   </params>" +
            "</response>",
            result
        );
    }
}