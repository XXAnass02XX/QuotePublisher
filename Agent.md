# Agent Guide: QuotePublisher

This document provides operational context, architectural goals, and development guidelines for AI agents and developers working on the `QuotePublisher` codebase.

---

## 1. Project Mission

The primary goal of `QuotePublisher` is to generate and publish simulated financial market data (quotes/ticks/order books) driven by quantitative pricing models and market strategies.

Downstream systems (algorithmic execution engines, backtesters, risk management systems, and monitoring dashboards) rely on these published quotes for realistic market data feeds without needing live exchange connections.

---

## 2. Core Concepts & Domain Terminology

- **Instrument / Ticker**: The financial asset being quoted (e.g., `MSFT`, `AAPL`, `EURUSD`, `BTCUSD`).
- **Quote / Tick**: Top-of-book or trade event containing timestamp, symbol, bid price, ask price, bid size, ask size, and/or last trade price.
- **Pricing Model**: The underlying stochastic or deterministic process governing asset price movement over time.
- **Strategy / Quote Engine**: The orchestration layer applying models, market conditions (spreads, liquidity, jump events, latency simulation), and publishing rules for one or more instruments.
- **Transport Layer**: The distribution mechanism (currently ZeroMQ via NetMQ Pub/Sub) responsible for broadcasting data to subscribers.

---

## 3. Desired Architectural Structure

As this project evolves from a minimal prototype, agents should guide code changes toward a clean, decoupled architecture:

```
QuotePublisher/
├── Models/              # Mathematical & stochastic models (GBM, Ornstein-Uhlenbeck, Jump Diffusion)
├── Strategies/          # Quote generation strategies, spread logic, tick dynamics
├── MarketData/          # Domain entities: Quote, Tick, OrderBook, Instrument
├── Transport/           # NetMQ publisher wrappers, serialization (JSON, Protobuf, or MemoryPack)
├── Configuration/       # Options for instruments, model parameters, socket endpoints
└── Program.cs           # Host bootstrapping, DI container, and lifecycle management
```

### Architectural Principles

1. **Separation of Concerns**:
   - Model calculations must remain pure and free from networking dependencies.
   - Sockets and transport logic should only deal with serializing and transmitting generated quote events.
2. **Strategy Extensibility**:
   - Introduce interfaces such as `IQuoteStrategy` and `IPricingModel`.
   - Allow dynamic configuration of model parameters (drift $\mu$, volatility $\sigma$, mean-reversion speed $\theta$, long-term mean $\mu$, mean jump size, etc.).
3. **Performance & Low Allocation**:
   - Market data simulation can generate high tick volumes.
   - Use `readonly struct` or `record struct` for ticks/quotes where appropriate to minimize heap allocations and GC pressure.
   - Favor span-based parsing/formatting or efficient binary serializers as message throughput increases.
4. **Reproducibility & Testing**:
   - Allow models to accept seedable random number generators (`Random` or custom pseudo-random generators) for deterministic testing and backtesting replay.

---

## 4. Financial Models & Strategies to Support

When extending the project, prioritize the following pricing models and strategy capabilities:

1. **Geometric Brownian Motion (GBM)**:
   $$dS_t = \mu S_t dt + \sigma S_t dW_t$$
   Standard continuous-time model for equity price dynamics.
2. **Ornstein-Uhlenbeck (Mean-Reverting)**:
   $$dX_t = \theta (\mu - X_t) dt + \sigma dW_t$$
   Ideal for pairs trading, fixed income spreads, or commodities exhibiting mean reversion.
3. **Merton Jump Diffusion**:
   Adds discrete random jump events to simulate earnings surprises or market shocks.
4. **Spread & Microstructure Simulation**:
   Dynamic bid/ask spread models based on volatility, order flow imbalance, and minimum tick sizes.

---

## 5. Development Guidelines & Conventions

- **Language & Target**: C# on .NET 10.0. Use modern language features (pattern matching, file-scoped namespaces, top-level statements or minimal hosting).
- **Socket Lifecycle**: Ensure `NetMQSocket` instances and `NetMQPoller` (if used) are properly disposed or run within managed application lifetimes (`IHostedService` / `BackgroundService`).
- **Cancellation**: Long-running publishing loops must honor `CancellationToken`.
- **Configurability**: Avoid hardcoding ports and ticker parameters in code; prefer strongly-typed configuration options.
