

using QuotePublisher.Contracts;

namespace QuotePublisher.useful;

public class QuoteRing
{
    public Quote[] Quotes;
    private int idx = 0;
    
    public Quote Next()
    {
        var PrevIdx = idx;
        idx = (idx + 1) % Quotes.Length;
        return Quotes[PrevIdx];
    }
}