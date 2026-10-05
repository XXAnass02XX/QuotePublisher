namespace QuotePublisher.Contracts;

public class Quote : Iquote
{
    public double Bid{get;}
    public double Ask{get;}
    public double Volume;
    public string Instrument;

    public Quote(double bid, double ask)
    {
        this.Bid = bid;
        this.Ask = ask;
        this.Volume = 1000000;
        this.Instrument = "Default";
    }
    public Quote(double bid, double ask, double volume, string instrument)
    {
        this.Bid = bid;
        this.Ask = ask;
        this.Volume = volume;
        this.Instrument = instrument;
    }
    
    public bool isEqual(Iquote quote)
    {
        if (quote is Quote otherQuote)
        {
            return (this.Bid == otherQuote.Bid) && (this.Ask == otherQuote.Ask);
        }
        return false;
    }
    
    public override string ToString()
    {
        return $"bid : \"{Bid}\" - ask \"{Ask}\" instrument : {Instrument}";
    }
}