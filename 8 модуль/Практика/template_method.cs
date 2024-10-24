using System;

public abstract class Beverage
{
    public void PrepareRecipe()
    {
        BoilWater();
        Brew();
        PourInCup();
        AddCondiments(); 
    }

    protected abstract void Brew();
    protected abstract void AddCondiments();

    private void BoilWater()
    {
        Console.WriteLine("Кипятим воду.");
    }

    private void PourInCup()
    {
        Console.WriteLine("Наливаем в чашку.");
    }
}

public class Tea : Beverage
{
    protected override void Brew()
    {
        Console.WriteLine("Заварить чай.");
    }

    protected override void AddCondiments()
    {
        if (CustomerWantsCondiments())
        {
            Console.WriteLine("Добавляем лимон.");
        }
    }

    private bool CustomerWantsCondiments()
    {
        Console.Write("Хотите добавить лимон? (y/n): ");
        string answer = Console.ReadLine();
        return answer?.ToLower() == "y";
    }
}

public class Coffee : Beverage
{
    protected override void Brew()
    {
        Console.WriteLine("Заварить кофе.");
    }

    protected override void AddCondiments()
    {
        if (CustomerWantsCondiments())
        {
            Console.WriteLine("Добавляем молоко и сахар.");
        }
    }

    private bool CustomerWantsCondiments()
    {
        Console.Write("Хотите добавить молоко и сахар в кофе? (y/n): ");
        string answer = Console.ReadLine();
        return answer?.ToLower() == "y";
    }
}

public class HotChocolate : Beverage
{
    protected override void Brew()
    {
        Console.WriteLine("Смешиваем порошок какао с горячей водой.");
    }

    protected override void AddCondiments()
    {
        Console.WriteLine("Добавляем взбитые сливки.");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Приготовление чая:");
        Beverage tea = new Tea();
        tea.PrepareRecipe();

        Console.WriteLine("\nПриготовление кофе:");
        Beverage coffee = new Coffee();
        coffee.PrepareRecipe();

        Console.WriteLine("\nПриготовление горячего шоколада:");
        Beverage hotChocolate = new HotChocolate();
        hotChocolate.PrepareRecipe();
    }
}
