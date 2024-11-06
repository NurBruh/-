using System;

public abstract class Beverage
{
    public abstract string GetDescription();
    public abstract decimal Cost();
}

public class Espresso : Beverage
{
    public override string GetDescription()
    {
        return "Эспрессо";
    }

    public override decimal Cost()
    {
        return 150;
    }
}

public class Tea : Beverage
{
    public override string GetDescription()
    {
        return "Чай";
    }

    public override decimal Cost()
    {
        return 100;
    }
}

public class Latte : Beverage
{
    public override string GetDescription()
    {
        return "Латте";
    }

    public override decimal Cost()
    {
        return 200;
    }
}

public class Mocha : Beverage
{
    public override string GetDescription()
    {
        return "Мокка";
    }

    public override decimal Cost()
    {
        return 250;
    }
}

public abstract class BeverageDecorator : Beverage
{
    protected Beverage _beverage;

    public BeverageDecorator(Beverage beverage)
    {
        _beverage = beverage;
    }
}

public class Milk : BeverageDecorator
{
    public Milk(Beverage beverage) : base(beverage) { }

    public override string GetDescription()
    {
        return _beverage.GetDescription() + ", Молоко";
    }

    public override decimal Cost()
    {
        return _beverage.Cost() + 50;
    }
}

public class Sugar : BeverageDecorator
{
    public Sugar(Beverage beverage) : base(beverage) { }

    public override string GetDescription()
    {
        return _beverage.GetDescription() + ", Сахар";
    }

    public override decimal Cost()
    {
        return _beverage.Cost() + 20;
    }
}

public class Cream : BeverageDecorator
{
    public Cream(Beverage beverage) : base(beverage) { }

    public override string GetDescription()
    {
        return _beverage.GetDescription() + ", Взбитые сливки";
    }

    public override decimal Cost()
    {
        return _beverage.Cost() + 70;
    }
}

public class Sirop : BeverageDecorator
{
    public Sirop(Beverage beverage) : base(beverage) { }

    public override string GetDescription()
    {
        return _beverage.GetDescription() + ", Сироп";
    }

    public override decimal Cost()
    {
        return _beverage.Cost() + 60;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Beverage beverage = new Latte();
        beverage = new Milk(beverage);
        beverage = new Sugar(beverage);
        beverage = new Cream(beverage);

        Console.WriteLine($"Описание: {beverage.GetDescription()}");
        Console.WriteLine($"Итоговая стоимость: {beverage.Cost()} тенге");

        Beverage anotherBeverage = new Mocha();
        anotherBeverage = new Sirop(anotherBeverage);
        anotherBeverage = new Milk(anotherBeverage);

        Console.WriteLine($"Описание: {anotherBeverage.GetDescription()}");
        Console.WriteLine($"Итоговая стоимость: {anotherBeverage.Cost()} тенге");
    }
}

