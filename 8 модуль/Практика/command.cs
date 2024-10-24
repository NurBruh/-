using System;
using System.Collections.Generic;

public interface ICommand
{
    void Execute();
    void Undo();
}

public class Light
{
    public void TurnOn() => Console.WriteLine("Свет включен.");
    public void TurnOff() => Console.WriteLine("Свет выключен.");
}

public class LightCommand : ICommand
{
    private readonly Light _light;
    private readonly bool _turnOn;

    public LightCommand(Light light, bool turnOn)
    {
        _light = light;
        _turnOn = turnOn;
    }

    public void Execute()
    {
        if (_turnOn)
            _light.TurnOn();
        else
            _light.TurnOff();
    }

    public void Undo()
    {
        Execute();
    }
}

public class Door
{
    public void Open() => Console.WriteLine("Дверь открыта.");
    public void Close() => Console.WriteLine("Дверь закрыта.");
}

public class DoorCommand : ICommand
{
    private readonly Door _door;
    private readonly bool _open;

    public DoorCommand(Door door, bool open)
    {
        _door = door;
        _open = open;
    }

    public void Execute()
    {
        if (_open)
            _door.Open();
        else
            _door.Close();
    }

    public void Undo()
    {
        Execute();
    }
}

public class Thermostat
{
    public void SetTemperature(int temperature)
    {
        Console.WriteLine($"Температура установлена на {temperature} градусов.");
    }
}

public class TemperatureCommand : ICommand
{
    private readonly Thermostat _thermostat;
    private readonly int _temperature;

    public TemperatureCommand(Thermostat thermostat, int temperature)
    {
        _thermostat = thermostat;
        _temperature = temperature;
    }

    public void Execute() => _thermostat.SetTemperature(_temperature);

    public void Undo()
    {
        Console.WriteLine($"Температура возвращена к предыдущему значению.");
    }
}

public class AlarmSystem
{
    public void Activate() => Console.WriteLine("Сигнализация активирована.");
    public void Deactivate() => Console.WriteLine("Сигнализация деактивирована.");
}

public class AlarmCommand : ICommand
{
    private readonly AlarmSystem _alarmSystem;
    private readonly bool _activate;

    public AlarmCommand(AlarmSystem alarmSystem, bool activate)
    {
        _alarmSystem = alarmSystem;
        _activate = activate;
    }

    public void Execute()
    {
        if (_activate)
            _alarmSystem.Activate();
        else
            _alarmSystem.Deactivate();
    }

    public void Undo()
    {
        Execute();
    }
}

public class RemoteControl
{
    private readonly Stack<ICommand> _commandHistory = new();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _commandHistory.Push(command);
    }

    public void UndoLastCommand()
    {
        if (_commandHistory.Count > 0)
        {
            var lastCommand = _commandHistory.Pop();
            lastCommand.Undo();
        }
        else
        {
            Console.WriteLine("Нет команд для отмены.");
        }
    }

    public void UndoLastCommandWithErrorHandling()
    {
        if (_commandHistory.Count > 0)
        {
            var lastCommand = _commandHistory.Pop();
            lastCommand.Undo();
        }
        else
        {
            Console.WriteLine("Ошибка: Нет команд для отмены.");
        }
    }
}

class Program
{
    static void Main()
    {
        var light = new Light();
        var door = new Door();
        var thermostat = new Thermostat();
        var alarmSystem = new AlarmSystem();

        var lightOn = new LightCommand(light, true);
        var lightOff = new LightCommand(light, false);
        var doorOpen = new DoorCommand(door, true);
        var doorClose = new DoorCommand(door, false);
        var setTemperature = new TemperatureCommand(thermostat, 22);
        var alarmOn = new AlarmCommand(alarmSystem, true);
        var alarmOff = new AlarmCommand(alarmSystem, false);

        var remote = new RemoteControl();

        remote.ExecuteCommand(lightOn);
        remote.ExecuteCommand(doorOpen);
        remote.ExecuteCommand(setTemperature);
        remote.ExecuteCommand(alarmOn);

        remote.UndoLastCommand();
        remote.UndoLastCommand();
        remote.UndoLastCommand();
        remote.UndoLastCommand();
        remote.UndoLastCommandWithErrorHandling();
    }
}
