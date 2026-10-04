namespace QuotePublisher.Contracts;

public class Quote : Iquote
{
    private double bid{get;}
    private double ask{get;}
    private double volume;
    private string instrument;

    public Quote(double bid, double ask)
    {
        this.bid = bid;
        this.ask = ask;
        this.volume = 1000000;
        this.instrument = "Default";
    }
    public Quote(double bid, double ask, double volume, string instrument)
    {
        this.bid = bid;
        this.ask = ask;
        this.volume = volume;
        this.instrument = instrument;
    }
    
    public bool isEqual(Iquote quote)
    {
        if (quote is Quote otherQuote)
        {
            return (this.bid == otherQuote.bid) && (this.ask == otherQuote.ask);
        }
        return false;
    }
}