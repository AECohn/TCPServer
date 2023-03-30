using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Set the server port");
int port = int.Parse(Console.ReadLine());

TcpListener server = new TcpListener(IPAddress.Any, port);
TcpClient client;


try
{
    server.Start();
    Console.WriteLine("Server started, awaiting connection");
}
catch (SocketException e)
{
    Console.WriteLine("SocketException: {0}", e);
    server.Stop();
    Console.WriteLine("Press any key to exit");
    Console.ReadKey();
    return;
}

try
{
    client = server.AcceptTcpClient(); //waits for a client to connect
    Console.WriteLine("Client connected");
}
catch (Exception e)
{
    Console.WriteLine("Exception: {0}", e);
    server.Stop();
    Console.WriteLine("Press any key to exit");
    Console.ReadKey();
    return; //exit the program
}

while (true)
{
    //get the client's stream
    NetworkStream stream = client.GetStream();

    //read the message sent by the client
    byte[] buffer = new byte[1024];
    int bytesRead = stream.Read(buffer, 0, buffer.Length);
    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
    Console.WriteLine("Received: " + message);

    //send a response to the client
    string response = $"Your message had {message.Length} characters";
    byte[] data = Encoding.UTF8.GetBytes(response);
    stream.Write(data, 0, data.Length);
    Console.WriteLine("Sent: " + response);

}

