using QuotePublisher.Contracts;

namespace QuotePublisher.useful;

public class RandomPricesSnapShot : QuoteRing
{
    public RandomPricesSnapShot()
    {
        int randomNumber = Random.Shared.Next(100);
        Quotes = new Quote[randomNumber];
        for (int i = 0; i < randomNumber; i++)
        {
            Quotes[i] = new Quote(100 + i, 101 + i); 
        }
    }
}