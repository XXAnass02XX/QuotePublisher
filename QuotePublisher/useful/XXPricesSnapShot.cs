using QuotePublisher.Contracts;

namespace QuotePublisher.useful;

public class XXPricesSnapShot : QuoteRing
{
    public XXPricesSnapShot()
    {
        Quotes = new Quote[20];
        for (int i = 0; i < Quotes.Length; i++)
        {
            Quotes[i] = new Quote(100 + i, 101 + i); 
        }
    }
}