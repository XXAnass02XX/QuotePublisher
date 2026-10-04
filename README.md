# QuotePublisher

`QuotePublisher` is a high-performance .NET service designed to simulate and publish real-time financial market quotes according to various mathematical models and market strategies.

## Overview & Objective

The primary objective of this project is to model, simulate, and broadcast realistic financial market data feeds. Rather than relying solely on static random price generators, `QuotePublisher` is built to implement configurable pricing models and simulation strategies (e.g., stochastic processes, volatility dynamics, and order-book microstructure) and broadcast these quotes to downstream consumers (such as trading bots, analytical dashboards, risk engines, and execution simulators).

## Key Features & Goals

- **Strategy- & Model-Driven Pricing**:
  - Support mathematical and statistical models such as Geometric Brownian Motion (GBM), Mean-Reverting (Ornstein-Uhlenbeck) processes, Jump Diffusion, and regime-switching volatility.
  - Model realistic bid/ask spreads, market depth, tick sizes, and liquidity characteristics.
- **Low-Latency Messaging**:
  - Distribute quotes using lightweight, high-throughput pub/sub messaging via [NetMQ](https://github.com/zeromq/netmq) (ZeroMQ for .NET).
  - Enable multiple subscribers to ingest filtered quote topics with minimal latency.
- **Extensible Architecture**:
  - Modular separation between pricing models/strategies, quote generation, and network publishing.
  - Easy addition of new instruments (equities, FX, commodities, crypto) and market regimes.

## Technology Stack

- **Runtime**: .NET 10.0 (C#)
- **Messaging**: NetMQ (ZeroMQ lightweight messaging protocol)

## Getting Started

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/)

### Building the Project

```bash
dotnet build
```

### Running the Publisher

```bash
dotnet run --project quotePublisher.csproj
```

The service initializes the publisher socket and begins broadcasting quote ticks over NetMQ.

### Subscribing to Quotes

Subscribers can connect via NetMQ `SubscriberSocket` using the endpoint emitted by the publisher, subscribing to the `"Quotes"` topic frame:

```csharp
using var subSocket = new SubscriberSocket();
subSocket.Connect("tcp://localhost:<PORT>");
subSocket.Subscribe("Quotes");

while (true)
{
    string topic = subSocket.ReceiveFrameString();
    string quote = subSocket.ReceiveFrameString();
    Console.WriteLine($"[{topic}] {quote}");
}
```

## Project Roadmap

- [ ] Abstract strategy and pricing model interfaces (`IPricingModel`, `IQuoteStrategy`).
- [ ] Implement standard quantitative models (Geometric Brownian Motion, Mean Reversion).
- [ ] Support multi-asset feeds with configurable drift, volatility, and tick frequency.
- [ ] Introduce structured quote payloads (Level 1 top-of-book, Level 2 depth) and binary/JSON serialization.
- [ ] Add configurable network endpoints via `appsettings.json` or CLI arguments.
