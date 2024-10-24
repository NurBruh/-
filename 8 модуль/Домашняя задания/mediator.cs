using System;
using System.Collections.Generic;

public abstract class ChatMediatorBase
{
    protected Dictionary<string, List<IUser>> Channels = new();

    public virtual void AddUserToChannel(IUser user, string channelName)
    {
        if (!Channels.ContainsKey(channelName))
        {
            Channels[channelName] = new List<IUser>();
        }
        Channels[channelName].Add(user);
        NotifyUserJoin(user, channelName);
    }

    public virtual void RemoveUserFromChannel(IUser user, string channelName)
    {
        if (Channels.ContainsKey(channelName) && Channels[channelName].Remove(user))
        {
            NotifyUserLeave(user, channelName);
        }
    }

    public abstract void SendMessage(string message, IUser sender, string channelName);
    public abstract void NotifyUserJoin(IUser user, string channelName);
    public abstract void NotifyUserLeave(IUser user, string channelName);
}

public class ChatMediator : ChatMediatorBase
{
    public override void SendMessage(string message, IUser sender, string channelName)
    {
        if (Channels.ContainsKey(channelName))
        {
            foreach (var user in Channels[channelName])
            {
                if (user != sender)
                {
                    user.ReceiveMessage($"[{channelName}] {sender.Name}: {message}");
                }
            }
        }
        else
        {
            Console.WriteLine($"Канал {channelName} не существует.");
        }
    }

    public override void NotifyUserJoin(IUser user, string channelName)
    {
        SendMessage($"пользователь {user.Name} присоединился к каналу.", user, channelName);
    }

    public override void NotifyUserLeave(IUser user, string channelName)
    {
        SendMessage($"пользователь {user.Name} покинул канал.", user, channelName);
    }
}

public interface IUser
{
    string Name { get; }
    void ReceiveMessage(string message);
    void SendMessage(string message, string channelName);
}

public class User : IUser
{
    public string Name { get; private set; }
    private ChatMediatorBase _mediator;

    public User(string name, ChatMediatorBase mediator)
    {
        Name = name;
        _mediator = mediator;
    }

    public void ReceiveMessage(string message)
    {
        Console.WriteLine($"{Name} получил сообщение: {message}");
    }

    public void SendMessage(string message, string channelName)
    {
        Console.WriteLine($"{Name} отправил сообщение в канал {channelName}: {message}");
        _mediator.SendMessage(message, this, channelName);
    }
}

class Program
{
    static void Main()
    {
        ChatMediatorBase mediator = new ChatMediator();

        IUser aidos = new User("Айдос", mediator);
        IUser dana = new User("Дана", mediator);
        IUser erlan = new User("Ерлан", mediator);

        mediator.AddUserToChannel(aidos, "Семья");
        mediator.AddUserToChannel(dana, "Семья");
        mediator.AddUserToChannel(erlan, "Семья");

        aidos.SendMessage("Всем добряк семья!", "Семья");
        dana.SendMessage("Хеллоу!", "Семья");

        erlan.SendMessage("Захватил пакеты?", "Клуб анонимных хацкеров");

        mediator.RemoveUserFromChannel(erlan, "Семья");
        dana.SendMessage("Предатель ты Ерлан! До свидания!", "Семья");
    }
}
