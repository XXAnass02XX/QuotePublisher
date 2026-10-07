using QuotePublisher;
using QuotePublisher.Contracts;

namespace PricingTests;

public class Tests  
{
    private LiveEngine _engine ;
    [SetUp]
    public void Setup()
    {
        _engine = new LiveEngine(5);
    }

    [Test]
    public void Test1()
    {
        var ExBids = new double[] { 100, 101, 102, 103, 104, 100 };
        var ExAsk = new double[] { 101, 102, 103, 104, 105, 101 };
        Quote quote;
        for (int i = 0; i < 6; i++)
        {
            quote = _engine.NextQuoteInQueue() ;
            if (quote.Bid != ExBids[i] && quote.Ask != ExAsk[i])
            {
                Assert.Fail($"Mismatch at index {i}: expected {ExBids[i]}/{ExAsk[i]}, got {quote.Bid}/{quote.Ask}");
            }
        }
        Assert.Pass();
    }
}