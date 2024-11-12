using System;

public class RoomBookingSystem
{
    public void Book(DateTime startTime, DateTime endTime, int people)
    {
        Console.WriteLine($"Номер забронирован с {startTime} по {endTime} для {people} человек.");
    }

    public bool CheckAvailability()
    {
        Console.WriteLine("Проверка наличия свободных номеров... Повезло, они свободны!");
        return true;
    }

    public void Cancel()
    {
        Console.WriteLine("Увы, бронирование номера отменено.");
    }
}

public class RestaurantSystem
{
    public void BookTable(int people)
    {
        Console.WriteLine($"Для компании из {people} человек столик успешно забронирован.");
    }

    public void OrderFood(int roomNumber)
    {
        Console.WriteLine($"Ваш заказ с доставкой в номер {roomNumber} готовится.");
    }
}

public class EventManagementSystem
{
    public void BookEventHall()
    {
        Console.WriteLine("Конференц-зал забронирован для вашего грандиозного мероприятия.");
    }

    public void OrderEquipment()
    {
        Console.WriteLine("Оборудование установлено. Всё готово к началу мероприятия!");
    }
}

public class CleaningService
{
    public void ScheduleCleaning(int roomNumber)
    {
        Console.WriteLine($"Уборка для номера {roomNumber} запланирована и будет выполнена по расписанию.");
    }

    public void CleanRoom(int roomNumber)
    {
        Console.WriteLine($"Комната {roomNumber} сияет чистотой.");
    }
}

public class HotelFacade
{
    private RoomBookingSystem roomBookingSystem = new RoomBookingSystem();
    private RestaurantSystem restaurantSystem = new RestaurantSystem();
    private EventManagementSystem eventManagementSystem = new EventManagementSystem();
    private CleaningService cleaningService = new CleaningService();

    public void BookRoomWithServices(DateTime startDate, DateTime endDate, int people, int roomNumber)
    {
        if (roomBookingSystem.CheckAvailability())
        {
            roomBookingSystem.Book(startDate, endDate, people);
            restaurantSystem.OrderFood(roomNumber);
            cleaningService.ScheduleCleaning(roomNumber);
        }
        else
        {
            Console.WriteLine("К сожалению, все номера заняты.");
        }
    }

    public void OrganizeEvent(int peopleNum)
    {
        eventManagementSystem.BookEventHall();
        eventManagementSystem.OrderEquipment();
        roomBookingSystem.Book(DateTime.Now, DateTime.Now.AddDays(1), peopleNum);
    }

    public void BookRestaurantTableWithTaxi(int people)
    {
        restaurantSystem.BookTable(people);
        Console.WriteLine("Такси для вашей компании уже в пути.");
    }

    public void CancelBooking()
    {
        roomBookingSystem.Cancel();
    }

    public void RequestCleaning(int roomNumber)
    {
        cleaningService.CleanRoom(roomNumber);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        HotelFacade hotelFacade = new HotelFacade();

        hotelFacade.BookRoomWithServices(DateTime.Now, DateTime.Now.AddDays(2), 2, 101);
        hotelFacade.OrganizeEvent(5);
        hotelFacade.BookRestaurantTableWithTaxi(3);
        hotelFacade.CancelBooking();
        hotelFacade.RequestCleaning(101);
    }
}
