using System;
using System.Collections.Generic;
using System.Linq;

public class Product : ICloneable
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public Product(string name, decimal price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public object Clone()
    {
        return new Product(Name, Price, Quantity);
    }
}

public class Discount : ICloneable
{
    public string Name { get; set; }
    public decimal Percentage { get; set; }

    public Discount(string name, decimal percentage)
    {
        Name = name;
        Percentage = percentage;
    }

    public object Clone()
    {
        return new Discount(Name, Percentage);
    }
}

public class Order : ICloneable
{
    public List<Product> Products { get; set; }
    public decimal DeliveryCost { get; set; }
    public List<Discount> Discounts { get; set; }
    public string PaymentMethod { get; set; }

    public Order(List<Product> products, decimal deliveryCost, List<Discount> discounts, string paymentMethod)
    {
        Products = products;
        DeliveryCost = deliveryCost;
        Discounts = discounts;
        PaymentMethod = paymentMethod;
    }

    public object Clone()
    {
        List<Product> clonedProducts = Products.Select(p => (Product)p.Clone()).ToList();
        List<Discount> clonedDiscounts = Discounts.Select(d => (Discount)d.Clone()).ToList();

        return new Order(clonedProducts, DeliveryCost, clonedDiscounts, PaymentMethod);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        List<Product> products = new List<Product>
        {
            new Product("Процессор Intel cc150", 300, 1),
            new Product("Видеокарта RTX A400", 500, 1)
        };

        List<Discount> discounts = new List<Discount>
        {
            new Discount("Скидка за объем", 5)
        };

        Order originalOrder = new Order(products, 20, discounts, "Карта");
        Order clonedOrder = (Order)originalOrder.Clone();

        Console.WriteLine($"Оригинальный заказ: Products count = {originalOrder.Products.Count}, DeliveryCost = {originalOrder.DeliveryCost}");
        Console.WriteLine($"Клонированный заказ: Products count = {clonedOrder.Products.Count}, DeliveryCost = {clonedOrder.DeliveryCost}");

        clonedOrder.Products[0].Quantity = 2;
        clonedOrder.DeliveryCost = 30;

        Console.WriteLine($"\nПосле клона:");
        Console.WriteLine($"Оригинал заказ: Products count = {originalOrder.Products.Count}, DeliveryCost = {originalOrder.DeliveryCost}, Product 0 quantity = {originalOrder.Products[0].Quantity}");
        Console.WriteLine($"Клонированн заказ: Products count = {clonedOrder.Products.Count}, DeliveryCost = {clonedOrder.DeliveryCost}, Product 0 quantity = {clonedOrder.Products[0].Quantity}");
    }
}
