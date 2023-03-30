// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Which port should the server be at?");
int port = int.Parse(Console.ReadLine());
//create a TCP Server
TcpListener server = new TcpListener(IPAddress.Any, port);
server.Start();
TcpClient client = server.AcceptTcpClient();
Console.WriteLine("Client connected");

//print received messages to the console
while (true)
{
    //wait for a client to connect


    //get the client's stream
    NetworkStream stream = client.GetStream();

    //read the message sent by the client
    byte[] buffer = new byte[1024];
    int bytesRead = stream.Read(buffer, 0, buffer.Length);
    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
    Console.WriteLine("Received: " + message);

    //send a response to the client
    string response = "Hello from the server";
    byte[] data = Encoding.UTF8.GetBytes(response);
    stream.Write(data, 0, data.Length);
    Console.WriteLine("Sent: " + response);

    /*
    //close the connection
    client.Close();
    Console.WriteLine("Client disconnected");
    */
}

