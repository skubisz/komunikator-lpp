
using System.Xml;
using System;

class ServerResponseException :  Exception
{
    public ServerResponseException(string message) : base(message)
    {       
    }
}

class ServerResponse
{
    protected XmlDocument responseXml;
    
    public ServerResponse(string response)
    {
        responseXml = new XmlDocument();
        responseXml.LoadXml(response);        
    }

    public string getType()
    {
        if (responseXml.FirstChild.FirstChild.Name == "type")
        {
            return responseXml.FirstChild.FirstChild.FirstChild.Value;
        }
        else
        {
            throw new ServerResponseException("Nieprawidłowa wiadomość");
        }
    }

    public ServerResponseParams getParams()
    {
        return new ServerResponseParams(responseXml.FirstChild.ChildNodes[1]);
    }
}

class ServerResponseParams
{
    private XmlNode paramNode;

    public ServerResponseParams(XmlNode paramNode)
    {
        this.paramNode = paramNode;
    }

    public string this[int name]
    {
        get
        {
            return this[name.ToString()];
        }
    }

    public string this[string name]
    {        
        get 
        {
            foreach (XmlNode node in paramNode.ChildNodes)
            {
                if (node.Name == "param")
                {
                    if (node.Attributes["name"].Value == name)
                    {
                        return node.Attributes["value"].Value;
                    }
                }
            }

            throw new Exception();
        }
    }

    public string getExtraData(int name)
    {
        return getExtraData(name.ToString());
    }

    public string getExtraData(string name)
    {
        
        foreach (XmlNode node in paramNode.ChildNodes)
        {
            if (node.Name == "param")
            {
                if (node.Attributes["name"].Value == name)
                {
                    return node.Attributes["extra"].Value;
                }
            }
        }

        throw new Exception();
    }          
}