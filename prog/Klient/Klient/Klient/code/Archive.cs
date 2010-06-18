
using System;
using System.IO;
using System.Web;
using System.Collections.Generic;

public class Talk
{
    public string login;
    public int id;
    public string firstMessage;

    public Talk(int id, string login, string firstMessage)
    {
        this.id = id;
        this.login = login;
        this.firstMessage = firstMessage;
    }
}

public class Archive
{

    public string addMessage(int talkId, string login, string senderLogin, string senderName, DateTime time, string message )
    {
        FileInfo info = new FileInfo("archive/" + login + "/" + talkId.ToString() + ".txt");
        if (info.Length == 0)
        {
            string firstMessage = message.Replace('\n', ' ').Replace("\r", "");
            File.WriteAllText("archive/" + login + "/" + talkId.ToString() + ".txt", firstMessage);            
        }

        string text = String.Format("<div style=\"background: #{0};\"><strong>{1}</strong>, <i>{2}</i><br />{3}</div>",
            login == senderLogin ? "ffffff" : "faf0ab",
            senderName, time, HttpUtility.HtmlEncode(message).Replace("\r", "").Replace("\n", "<br />"));

        string path = "archive/" + login + "/" + talkId.ToString() + ".html";

        StreamWriter file = File.AppendText(path);
        file.Write(text);
        file.Close();        

        return text;
    }

    public string readTalk(int talkId, string login)
    {
        return File.ReadAllText("archive/" + login + "/" + talkId.ToString() + ".html");
    }

    public void createNewArchive(string login)
    {
        if (!Directory.Exists("archive/" + login))
        {
            Directory.CreateDirectory("archive/" + login);
        }
    }

    public void createNewTalk(string login, int talkId)
    {
        if (!File.Exists("archive/" + login + "/" + talkId.ToString() + ".html"))
        {
            File.Create("archive/" + login + "/" + talkId.ToString() + ".html");
            File.Create("archive/" + login + "/" + talkId.ToString() + ".txt");

            StreamWriter file = File.AppendText("archive/archive.txt");
            file.WriteLine(String.Format("{0};{1}", login, talkId));
            file.Close();
        }
    }

    public List<string> readTalker()
    {
        HashSet<string> logins = new HashSet<string>();
        string[] lines = File.ReadAllLines("archive/archive.txt");

        for (int i = 0; i < lines.Length; i++)
        {
            logins.Add(lines[i].Split(';')[0]);
        }
        List<string> result = new List<string>(logins);
        result.Sort();
        return  result;
    }

    public List<Talk> readTalks(string login, string talker)
    {
        List<Talk> result = new List<Talk>();
        string[] lines = File.ReadAllLines("archive/archive.txt");

        for (int i = 0; i < lines.Length; i++)
        {
            string[] split = lines[i].Split(';');
            if (split[0] == talker)
            {
                int id = int.Parse(split[1]);
                string firstMessage = File.ReadAllText("archive/" + login + "/" + id.ToString() + ".txt");
                result.Add(new Talk(id, login, firstMessage));                
            }
        }        
        
        return result;
    }

}