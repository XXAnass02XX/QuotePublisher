using NetMQ;
using NetMQ.Sockets;
using QuotePublisher.Contracts;
using QuotePublisher.useful;

namespace QuotePublisher;

public class LiveEngine
{
    private QuoteRing  _quotes;

    public LiveEngine()
    {
        _quotes = new XXPricesSnapShot();
    }
    public LiveEngine(int len)
    {
        _quotes = new XXPricesSnapShot(len);
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

    public Quote NextQuoteInQueue()
    {
        return _quotes.Next();
    }
    
}