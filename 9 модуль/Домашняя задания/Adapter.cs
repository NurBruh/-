using System;

public interface IPaymentProcessor
{
    void ProcessPayment(double amount);
}

public class PayPalPaymentProcessor : IPaymentProcessor
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Оплата через PayPal: {amount} долларов успешно проведена.");
    }
}

public class StripePaymentService
{
    public void MakeTransaction(double totalAmount)
    {
        Console.WriteLine($"Оплата через Stripe: {totalAmount} долларов успешно проведена.");
    }
}

public class StripePaymentAdapter : IPaymentProcessor
{
    private readonly StripePaymentService _stripeService;

    public StripePaymentAdapter(StripePaymentService stripeService)
    {
        _stripeService = stripeService;
    }

    public void ProcessPayment(double amount)
    {
        _stripeService.MakeTransaction(amount);
    }
}

public class QiwiPaymentService
{
    public void Pay(double paymentAmount)
    {
        Console.WriteLine($"Оплата через Qiwi: {paymentAmount} тенге успешно проведена.");
    }
}

public class QiwiPaymentAdapter : IPaymentProcessor
{
    private readonly QiwiPaymentService _qiwiService;

    public QiwiPaymentAdapter(QiwiPaymentService qiwiService)
    {
        _qiwiService = qiwiService;
    }

    public void ProcessPayment(double amount)
    {
        _qiwiService.Pay(amount);
    }
}

public class PayeerPaymentService
{
    public void TransferAmount(double amount)
    {
        Console.WriteLine($"Оплата через Payeer: {amount} тенге успешно проведена.");
    }
}

public class PayeerPaymentAdapter : IPaymentProcessor
{
    private readonly PayeerPaymentService _payeerService;

    public PayeerPaymentAdapter(PayeerPaymentService payeerService)
    {
        _payeerService = payeerService;
    }

    public void ProcessPayment(double amount)
    {
        _payeerService.TransferAmount(amount);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        IPaymentProcessor paypalProcessor = new PayPalPaymentProcessor();
        paypalProcessor.ProcessPayment(350);

        IPaymentProcessor stripeProcessor = new StripePaymentAdapter(new StripePaymentService());
        stripeProcessor.ProcessPayment(740);

        IPaymentProcessor qiwiProcessor = new QiwiPaymentAdapter(new QiwiPaymentService());
        qiwiProcessor.ProcessPayment(2500);

        IPaymentProcessor payeerProcessor = new PayeerPaymentAdapter(new PayeerPaymentService());
        payeerProcessor.ProcessPayment(3000);
    }
}
