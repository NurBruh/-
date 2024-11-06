using System;

public interface IInternalDeliveryService
{
    void DeliverOrder(string orderID);
    string GetDeliveryStatus(string orderID);
    decimal CalculateDeliveryCost();
}

public class InternalDeliveryService : IInternalDeliveryService
{
    public void DeliverOrder(string orderID)
    {
        Console.WriteLine($"Внутренняя доставка заказа {orderID} начата.");
    }

    public string GetDeliveryStatus(string orderID)
    {
        return $"Статус доставки для заказа {orderID}: В пути.";
    }

    public decimal CalculateDeliveryCost()
    {
        return 500; 
    }
}

public class ExternalLogisticsServiceA
{
    public void ShipItem(int itemID)
    {
        Console.WriteLine($"Отправка товара с ID {itemID} через Службу A.");
    }

    public void TrackShipment(int shipmentId)
    {
        Console.WriteLine($"Отслеживание отправки с ID {shipmentId} через Службу A.");
    }

    public decimal GetShippingCost()
    {
        return 1000;
    }
}

public class LogisticsAdapterA : IInternalDeliveryService
{
    private ExternalLogisticsServiceA _serviceA;

    public LogisticsAdapterA(ExternalLogisticsServiceA serviceA)
    {
        _serviceA = serviceA;
    }

    public void DeliverOrder(string orderID)
    {
        int itemId = Convert.ToInt32(orderID);
        _serviceA.ShipItem(itemId);
        Console.WriteLine("Заказ успешно отправлен через Службы A.");
    }

    public string GetDeliveryStatus(string orderID)
    {
        return $"Статус доставки для заказа {orderID} через Службы A: В пути.";
    }

    public decimal CalculateDeliveryCost()
    {
        return _serviceA.GetShippingCost();
    }
}

public class ExternalLogisticsServiceB
{
    public void SendPackage(string packageInfo)
    {
        Console.WriteLine($"Отправка посылки: {packageInfo} через Службу B.");
    }

    public void CheckPackageStatus(string trackingCode)
    {
        Console.WriteLine($"Статус посылки с кодом {trackingCode} через Службу B: Доставлено.");
    }

    public decimal CalculateShippingFee()
    {
        return 1200; 
    }
}

public class LogisticsAdapterB : IInternalDeliveryService
{
    private ExternalLogisticsServiceB _serviceB;

    public LogisticsAdapterB(ExternalLogisticsServiceB serviceB)
    {
        _serviceB = serviceB;
    }

    public void DeliverOrder(string orderID)
    {
        _serviceB.SendPackage($"Заказ {orderID}");
        Console.WriteLine("Заказ успешно отправлен через Службы B.");
    }

    public string GetDeliveryStatus(string orderID)
    {
        return $"Статус доставки для заказа {orderID} через Службы B: Доставлено.";
    }

    public decimal CalculateDeliveryCost()
    {
        return _serviceB.CalculateShippingFee();
    }
}

public static class DeliveryServiceFactory
{
    public static IInternalDeliveryService CreateDeliveryService(string serviceType)
    {
        if (serviceType == "Internal")
        {
            return new InternalDeliveryService();
        }
        else if (serviceType == "ServiceA")
        {
            return new LogisticsAdapterA(new ExternalLogisticsServiceA());
        }
        else if (serviceType == "ServiceB")
        {
            return new LogisticsAdapterB(new ExternalLogisticsServiceB());
        }
        else
        {
            throw new ArgumentException("Данный тип службы доставки не поддерживается.");
        }
    }
}

public class Program
{
    static void Main(string[] args)
    {
        IInternalDeliveryService deliveryService = DeliveryServiceFactory.CreateDeliveryService("ServiceA");
        deliveryService.DeliverOrder("00001");
        Console.WriteLine(deliveryService.GetDeliveryStatus("00001"));
        Console.WriteLine($"Стоимость доставки: {deliveryService.CalculateDeliveryCost()} тенге");

        deliveryService = DeliveryServiceFactory.CreateDeliveryService("ServiceB");
        deliveryService.DeliverOrder("00100");
        Console.WriteLine(deliveryService.GetDeliveryStatus("00100"));
        Console.WriteLine($"Стоимость доставки: {deliveryService.CalculateDeliveryCost()} тенге");
    }
}
