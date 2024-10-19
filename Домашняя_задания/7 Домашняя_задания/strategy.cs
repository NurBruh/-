using System;

public interface IPaymentStrategy
{
    void Pay(double amount);
}

public class CreditCardPayment : IPaymentStrategy
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Оплата банковской картой: {amount} EUR");
    }
}

public class PayPalPayment : IPaymentStrategy
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Оплата через PayPal: {amount} EUR");
    }
}

public class CryptoPayment : IPaymentStrategy
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Оплата криптовалютой: {amount} EUR");
    }
}

public class PaymentContext
{
    private IPaymentStrategy _paymentStrategy;

    public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }

    public void ExecutePayment(double amount)
    {
        if (_paymentStrategy == null)
        {
            Console.WriteLine("Способ оплаты не выбрана.");
            return;
        }
        _paymentStrategy.Pay(amount);
    }
}

public class Program
{
    public static void Main()
    {
        PaymentContext paymentContext = new PaymentContext();

        Console.WriteLine("Выберите способ оплаты: 1 - Банковская карта, 2 - PayPal, 3 - Криптовалюта");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                paymentContext.SetPaymentStrategy(new CreditCardPayment());
                break;
            case 2:
                paymentContext.SetPaymentStrategy(new PayPalPayment());
                break;
            case 3:
                paymentContext.SetPaymentStrategy(new CryptoPayment());
                break;
            default:
                Console.WriteLine("Неверный выбор.");
                return;
        }

        Console.WriteLine("Введите сумму оплаты:");
        double amount = double.Parse(Console.ReadLine());

        paymentContext.ExecutePayment(amount);
    }
}
