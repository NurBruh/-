using System;
using System.Collections.Generic;

public interface IObserver
{
    void Update(string stockSymbol, double price);
}

public interface ISubject
{
    void RegisterObserver(IObserver observer, string stockSymbol);
    void RemoveObserver(IObserver observer, string stockSymbol);
    void NotifyObservers(string stockSymbol);
}

public class FondovayaBirzha : ISubject
{
    private readonly Dictionary<string, double> _stocks = new();
    private readonly Dictionary<string, HashSet<IObserver>> _observers = new();

    public void SetStockPrice(string stockSymbol, double price)
    {
        _stocks[stockSymbol] = price;
        NotifyObservers(stockSymbol);
    }

    public void AddStock(string stockSymbol, double initialPrice)
    {
        _stocks[stockSymbol] = initialPrice;
        _observers[stockSymbol] = new HashSet<IObserver>();
    }

    public void RegisterObserver(IObserver observer, string stockSymbol)
    {
        if (_observers.ContainsKey(stockSymbol))
        {
            _observers[stockSymbol].Add(observer);
        }
    }

    public void RemoveObserver(IObserver observer, string stockSymbol)
    {
        if (_observers.ContainsKey(stockSymbol))
        {
            _observers[stockSymbol].Remove(observer);
        }
    }

    public void NotifyObservers(string stockSymbol)
    {
        if (_observers.ContainsKey(stockSymbol))
        {
            double newPrice = _stocks[stockSymbol];
            foreach (var observer in _observers[stockSymbol])
            {
                observer.Update(stockSymbol, newPrice);
            }
        }
    }
}

public class Trader : IObserver
{
    private readonly string _name;

    public Trader(string name)
    {
        _name = name;
    }

    public void Update(string stockSymbol, double price)
    {
        Console.WriteLine($"{_name} получил обновление: Цена актива {stockSymbol} изменена на {price}");
    }
}

public class TraderRobot : IObserver
{
    private readonly double _threshold;

    public TraderRobot(double threshold)
    {
        _threshold = threshold;
    }

    public void Update(string stockSymbol, double price)
    {
        if (price > _threshold)
        {
            Console.WriteLine($"Робот продает актив {stockSymbol} по цене {price}");
        }
        else
        {
            Console.WriteLine($"Робот покупает актив {stockSymbol} по цене {price}");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        FondovayaBirzha stockExchange = new FondovayaBirzha();
        stockExchange.AddStock("USDT", 1.0);
        stockExchange.AddStock("DOGE", 0.05);

        Trader trader1 = new Trader("Алимжан Диар");
        Trader trader2 = new Trader("Тлеуханулы Елдос");
        TraderRobot robot = new TraderRobot(0.1);

        stockExchange.RegisterObserver(trader1, "USDT");
        stockExchange.RegisterObserver(trader2, "DOGE");
        stockExchange.RegisterObserver(robot, "DOGE");

        stockExchange.SetStockPrice("USDT", 1.01);
        stockExchange.SetStockPrice("DOGE", 0.06);
        stockExchange.SetStockPrice("DOGE", 0.04);
    }
}
