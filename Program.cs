using NetMQ;
using NetMQ.Sockets;

Console.WriteLine("Starting Quote Publisher...");

using (var pubSocket = new PublisherSocket("@tcp://localhost:0"))
{
    while (true)
    {
        string quote = $"MSFT {Random.Shared.Next(300, 350)}";
        pubSocket.SendMoreFrame("Quotes").SendFrame(quote);
        Console.WriteLine($"Published: {quote}");
        Console.WriteLine($"Publishing on: {pubSocket.Options.LastEndpoint}");
        Thread.Sleep(1000); 
    }
}