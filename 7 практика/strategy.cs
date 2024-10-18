using System;

namespace TravelBooking
{
    public interface ICostCalculationStrategy
    {
        double CalculateCost(int passenger, int typeClass, bool hasLuggage);
    }

    public class Airplane : ICostCalculationStrategy
    {
        public double CalculateCost(int passenger, int typeClass, bool hasLuggage)
        {
            double cost;
            if (typeClass == 1)
                cost = passenger * 1000;
            else if (typeClass == 2)
                cost = passenger * 800;
            else
                cost = passenger * 600;

            if (hasLuggage)
                cost *= 2;

            return cost;
        }
    }

    public class Bus : ICostCalculationStrategy
    {
        public double CalculateCost(int passenger, int typeClass, bool hasLuggage)
        {
            double cost;
            if (typeClass == 1)
                cost = passenger * 200;
            else if (typeClass == 2)
                cost = passenger * 100;
            else
                cost = passenger * 50;

            if (hasLuggage)
                cost *= 2;

            return cost;
        }
    }

    public class Train : ICostCalculationStrategy
    {
        public double CalculateCost(int passenger, int typeClass, bool hasLuggage)
        {
            double cost;
            if (typeClass == 1)
                cost = passenger * 1000;
            else if (typeClass == 2)
                cost = passenger * 800;
            else
                cost = passenger * 600;

            if (hasLuggage)
                cost *= 2;

            return cost;
        }
    }

    public class TravelBookingContext
    {
        private ICostCalculationStrategy _calculation;

        public void ChangeCalculationStrategy(ICostCalculationStrategy calculation)
        {
            _calculation = calculation;
        }

        public double CalculateTravelCost(int passenger, int typeClass, bool hasLuggage)
        {
            if (_calculation == null)
                throw new InvalidOperationException("Ошибька.");

            return _calculation.CalculateCost(passenger, typeClass, hasLuggage);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TravelBookingContext context = new TravelBookingContext();

                context.ChangeCalculationStrategy(new Airplane());
                var airplaneResult = context.CalculateTravelCost(1, 1, false);
                Console.WriteLine($"Стоимость полета: {airplaneResult}");

                context.ChangeCalculationStrategy(new Bus());
                var busResult = context.CalculateTravelCost(1, 1, false);
                Console.WriteLine($"Стоимость поездки на автобусе: {busResult}");

                context.ChangeCalculationStrategy(new Train());
                var trainResult = context.CalculateTravelCost(1, 1, false);
                Console.WriteLine($"Стоимость поездки на поезде: {trainResult}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
