using System.Xml;
using System;

namespace Serwer
{

    class ClientRequestException : Exception
    {
        public ClientRequestException(string message)
            : base(message)
        {
        }
    }

    class ClientRequest
    {
        protected XmlDocument responseXml;

        public ClientRequest(string request)
        {
            responseXml = new XmlDocument();
            responseXml.LoadXml(request);
        }

        public string getType()
        {
            if (responseXml.FirstChild.FirstChild.Name == "type")
            {
                return responseXml.FirstChild.FirstChild.FirstChild.Value;
            }
            else
            {
                throw new ClientRequestException("Nieprawidłowa wiadomość");
            }
        }

        public ClientRequestParams getParams()
        {
            return new ClientRequestParams(responseXml.FirstChild.ChildNodes[1]);
        }
    }

    class ClientRequestParams
    {
        private XmlNode paramNode;

        public ClientRequestParams(XmlNode paramNode)
        {
            this.paramNode = paramNode;
        }

        public string this[string name]
        {
            get
            {
                foreach (XmlNode node in paramNode.ChildNodes)
                {
                    if (node.Name.CompareTo("param")==0)
                    {
                        if (node.Attributes["name"].Value.CompareTo(name)==0)
                        {
                            return node.Attributes["value"].Value;
                        }
                    }
                }

                throw new Exception();
            }
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
}