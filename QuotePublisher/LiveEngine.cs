using NetMQ;
using NetMQ.Sockets;
using QuotePublisher.useful;

namespace QuotePublisher;

public class LiveEngine
{
    private QuoteRing  _quotes;

    public LiveEngine()
    {
        _quotes = new XXPricesSnapShot();
    }

    public void Run(PublisherSocket publisherSocket)
    {
        while (true)
        {
            //string quote = $"MSFT {Random.Shared.Next(300, 350)}";
            var quote = _quotes.Next();
            publisherSocket.SendMoreFrame("Quotes").SendFrame(quote.ToString());
            Console.WriteLine($"Published: {quote}");
            Console.WriteLine($"Publishing on: {publisherSocket.Options.LastEndpoint}");
            Thread.Sleep(1000); 
        }
    }
    
}