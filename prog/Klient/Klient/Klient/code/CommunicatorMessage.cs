
public class CommunicatorMessage
{
    private string _from;
    private string _message;

    public CommunicatorMessage(string from, string message)
    {
        _from = from;
        _message = message;
    }

    public string from
    {
        get { return _from; }
    }

    public string message
    {
        get { return _message; }
    }

    

}