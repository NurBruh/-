using System;
using System.Collections.Generic;

public interface IObserver
{
    void Update(string currency, double rate);
}

public interface ISubject
{
    void RegisterObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers();
}

public class CurrencyExchange : ISubject
{
    private Dictionary<string, double> _rates = new Dictionary<string, double>();
    private List<IObserver> _observers = new List<IObserver>();

    public void SetRate(string currency, double rate)
    {
        _rates[currency] = rate;
        NotifyObservers();
    }

    public double GetRate(string currency)
    {
        return _rates.TryGetValue(currency, out var rate) ? rate : 0.0;
    }

    public void RegisterObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            foreach (var kvp in _rates)
            {
                observer.Update(kvp.Key, kvp.Value);
            }
        }
    }
}

public class Bank : IObserver
{
    public void Update(string currency, double rate)
    {
        Console.WriteLine($"Банк получил обновление: {currency} = {rate} USD");
    }
}

public class ForexTrader : IObserver
{
    public void Update(string currency, double rate)
    {
        Console.WriteLine($"Трейдер анализирует: {currency} = {rate} USD");
    }
}

public class MobileApp : IObserver
{
    public void Update(string currency, double rate)
    {
        Console.WriteLine($"Мобильное приложение уведомляет: {currency} = {rate} USD");
    }
}

public class Program
{
    public static void Main()
    {
        CurrencyExchange exchange = new CurrencyExchange();

        Bank bank = new Bank();
        ForexTrader trader = new ForexTrader();
        MobileApp app = new MobileApp();

        exchange.RegisterObserver(bank);
        exchange.RegisterObserver(trader);
        exchange.RegisterObserver(app);

        exchange.SetRate("USD", 1.0);
        exchange.SetRate("EUR", 1.2);
        exchange.SetRate("BTC", 30000.0);

        exchange.RemoveObserver(trader);

        exchange.SetRate("EUR", 1.3);
    }
}
