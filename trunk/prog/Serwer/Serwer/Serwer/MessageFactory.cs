using System.Web;
using System;
using System.Collections.Generic;
using System.Data;

namespace Serwer
{
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
                HttpUtility.HtmlEncode(result)
            );
        }

        public string createAccountMessage(string response)
        {
            return string.Format(
                "<response>" +
                "   <type>createAccount</type>" +
                "   <params>" +
                "       <param name=\"result\" value=\"{0}\" />" +
                "   </params>" +
                "</response>",
                HttpUtility.HtmlEncode(response)
            );
        }

        public string getMessagesMessage(int messages, List<Pair<String, String>> list)
        {
            String msg = string.Format(
                "<response>" +
                "   <type>getMessages</type>" +
                "   <params>" +
                "       <param name=\"messages\" value=\"{0}\" />", HttpUtility.HtmlEncode(messages.ToString()));

            for (int i = 1; i <= messages; i++)
            {
                Console.WriteLine("{0} --> {1}", HttpUtility.HtmlEncode(list[i - 1].Second), HttpUtility.HtmlEncode(list[i - 1].First));
                msg += string.Format("<param name=\"{0}\" value=\"{1}\" extra=\"{2}\" />", HttpUtility.HtmlEncode(i.ToString()), HttpUtility.HtmlEncode(list[i-1].Second), HttpUtility.HtmlEncode(list[i-1].First));
            }

            msg += string.Format("</params>" +
                "</response>");

            return msg;
        }

        public string sendMessageMessage()
        {
            return string.Format(
                "<response>" +
                "   <type>sendMessage</type>" +
                "</response>"
            );
        }

        public string changePasswordMessage()
        {
            return string.Format(
                "<response>" +
                "   <type>changePassword</type>" +
                "</response>"
            );
        }

        public string changeStatusMessage()
        {
            return string.Format(
                "<response>" +
                "   <type>changeStatus</type>" +
                "</response>"
            );
        }

    }
}