




using SimpleTCP;
using System.Text;

List<string> citation = new List<string>() { "Life is what happens when you’re busy making other plans",
"When the going gets tough, the tough get going", "You must be the change you wish to see in the world",
"You only live once, but if you do it right, once is enough", "Tough times never last but tough people do",
"Get busy living or get busy dying", "Whether you think you can or you think you can’t, you’re right", "Tis better to have loved and lost than to have never loved at all"};


if(!File.Exists("log.txt"))
{
    File.Create("log.txt");
}


var server = new SimpleTcpServer();

server.ClientConnected += (sender, e) =>
{
    using (StreamWriter writer = File.AppendText("log.txt"))
    {
         writer.WriteLine($"\n\nClient ({e.Client.RemoteEndPoint}) connected!\nTime: {DateTime.Now.ToShortTimeString()}");
    }
};

server.ClientDisconnected += (sender, e) =>
{
    using (StreamWriter writer = File.AppendText("log.txt"))
    {
        writer.WriteLine($"\n\nClient ({e.Client.RemoteEndPoint}) disconnected!\nTime: {DateTime.Now.ToShortTimeString()}");
    }
};

server.DataReceived += (sender, e) =>
{
    var ep = e.TcpClient.Client.RemoteEndPoint;
    var msg = Encoding.UTF8.GetString(e.Data);
    if (!msg.Contains("disconnect"))
    {
        Random rn = new Random();
        
        ;
        string s = citation[rn.Next(0, citation.Count - 1)];
        e.Reply(s);
        using (StreamWriter writer = File.AppendText("log.txt"))
        {
            writer.WriteLine($"\n\nClient {ep} recieved:   {s}");
        }
    }
    else
    {
        e.Reply("-1");
    }
};


server.Start(5000);

Console.ReadLine();
