using QuotePublisher.Contracts;

namespace QuotePublisher.useful;

public class XXPricesSnapShot
{
    private Quote[] quotes =  new Quote[20];

    public XXPricesSnapShot()
    {
        for (int i = 0; i < quotes.Length; i++)
        {
            quotes[i] = new Quote(100 + i, 101 + i); 
        }
    }
}