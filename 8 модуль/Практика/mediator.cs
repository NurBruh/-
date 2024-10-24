using System;
using System.Collections.Generic;

public interface IMediator
{
    void RegisterUser(User user);
    void SendMessage(string message, User sender, User recipient = null);
}

public class ChatRoom : IMediator
{
    private readonly List<User> _users = new List<User>();

    public void RegisterUser(User user)
    {
        if (!_users.Contains(user))
        {
            _users.Add(user);
            user.SetMediator(this);
            NotifyUsers($"{user.Name} присоединился к чату.");
        }
    }

    public void SendMessage(string message, User sender, User recipient = null)
    {
        if (recipient == null)
        {
            foreach (var user in _users)
            {
                if (user != sender)
                {
                    user.ReceiveMessage($"{sender.Name}: {message}");
                }
            }
        }
        else
        {
            recipient.ReceiveMessage($"{sender.Name} (личное сообщение): {message}");
        }
    }

    private void NotifyUsers(string message)
    {
        foreach (var user in _users)
        {
            user.ReceiveMessage($"[Система]: {message}");
        }
    }
}

public class User
{
    public string Name { get; }
    private IMediator _mediator;

    public User(string name)
    {
        Name = name;
    }

    public void SetMediator(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void SendMessage(string message, User recipient = null)
    {
        if (_mediator == null)
        {
            Console.WriteLine($"{Name} не может отправить сообщение. Необходимо присоединиться к чату.");
            return;
        }

        _mediator.SendMessage(message, this, recipient);
    }

    public void ReceiveMessage(string message)
    {
        Console.WriteLine($"{Name} получил сообщение: {message}");
    }
}

class Program
{
    static void Main()
    {
        IMediator chatRoom = new ChatRoom();

        User aigerim = new User("Айгерым");
        User alikhan = new User("Алихан");
        User dinara = new User("Динара");

        chatRoom.RegisterUser(aigerim);
        chatRoom.RegisterUser(alikhan);
        chatRoom.RegisterUser(dinara);

        aigerim.SendMessage("Всем салют!");
        alikhan.SendMessage("Салем Айгерым!");
        dinara.SendMessage("Как дела, ребятишки?");

        alikhan.SendMessage("Привет", dinara);

        User danel = new User("Данел");
        danel.SendMessage("Всем хай!");
    }
}
