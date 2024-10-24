using System;
using System.Collections.Generic;

public interface ICommand
{
    void Execute();
    void Undo();
}

public class Light
{
    public void On() { Console.WriteLine("Свет включен."); }
    public void Off() { Console.WriteLine("Свет выключен."); }
}

public class AirConditioner
{
    public void TurnOn() { Console.WriteLine("Кондиционер включен."); }
    public void TurnOff() { Console.WriteLine("Кондиционер выключен."); }
}

public class Television
{
    public void PowerOn() { Console.WriteLine("Телевизор включен."); }
    public void PowerOff() { Console.WriteLine("Телевизор выключен."); }
}

public class LightOnCommand : ICommand
{
    private Light _light;
    public LightOnCommand(Light light) { _light = light; }
    public void Execute() { _light.On(); }
    public void Undo() { _light.Off(); }
}

public class LightOffCommand : ICommand
{
    private Light _light;
    public LightOffCommand(Light light) { _light = light; }
    public void Execute() { _light.Off(); }
    public void Undo() { _light.On(); }
}

public class ACOnCommand : ICommand
{
    private AirConditioner _ac;
    public ACOnCommand(AirConditioner ac) { _ac = ac; }
    public void Execute() { _ac.TurnOn(); }
    public void Undo() { _ac.TurnOff(); }
}

public class ACOffCommand : ICommand
{
    private AirConditioner _ac;
    public ACOffCommand(AirConditioner ac) { _ac = ac; }
    public void Execute() { _ac.TurnOff(); }
    public void Undo() { _ac.TurnOn(); }
}

public class TVOnCommand : ICommand
{
    private Television _tv;
    public TVOnCommand(Television tv) { _tv = tv; }
    public void Execute() { _tv.PowerOn(); }
    public void Undo() { _tv.PowerOff(); }
}

public class TVOffCommand : ICommand
{
    private Television _tv;
    public TVOffCommand(Television tv) { _tv = tv; }
    public void Execute() { _tv.PowerOff(); }
    public void Undo() { _tv.PowerOn(); }
}

public class RemoteControl
{
    private ICommand[] _onCommands;
    private ICommand[] _offCommands;
    private ICommand _lastCommand;

    public RemoteControl()
    {
        _onCommands = new ICommand[5];
        _offCommands = new ICommand[5];
        _lastCommand = null;
    }

    public void SetCommand(int slot, ICommand onCommand, ICommand offCommand)
    {
        _onCommands[slot] = onCommand;
        _offCommands[slot] = offCommand;
    }

    public void PressOnButton(int slot)
    {
        if (_onCommands[slot] != null)
        {
            _onCommands[slot].Execute();
            _lastCommand = _onCommands[slot];
        }
        else { Console.WriteLine("Команда не назначена."); }
    }

    public void PressOffButton(int slot)
    {
        if (_offCommands[slot] != null)
        {
            _offCommands[slot].Execute();
            _lastCommand = _offCommands[slot];
        }
        else { Console.WriteLine("Команда не назначена."); }
    }

    public void PressUndoButton()
    {
        if (_lastCommand != null)
        {
            Console.WriteLine("Отмена последней команды.");
            _lastCommand.Undo();
        }
        else { Console.WriteLine("Нечего отменять."); }
    }
}

public class MacroCommand : ICommand
{
    private List<ICommand> _commands;
    public MacroCommand(List<ICommand> commands) { _commands = commands; }
    public void Execute() { foreach (ICommand command in _commands) { command.Execute(); } }
    public void Undo() { for (int i = _commands.Count - 1; i >= 0; i--) { _commands[i].Undo(); } }
}

class Program
{
    static void Main(string[] args)
    {
        Light livingRoomLight = new Light();
        AirConditioner ac = new AirConditioner();
        Television tv = new Television();

        ICommand lightOn = new LightOnCommand(livingRoomLight);
        ICommand lightOff = new LightOffCommand(livingRoomLight);
        ICommand acOn = new ACOnCommand(ac);
        ICommand acOff = new ACOffCommand(ac);
        ICommand tvOn = new TVOnCommand(tv);
        ICommand tvOff = new TVOffCommand(tv);

        RemoteControl remote = new RemoteControl();
        remote.SetCommand(0, lightOn, lightOff);
        remote.SetCommand(1, acOn, acOff);
        remote.SetCommand(2, tvOn, tvOff);

        remote.PressOnButton(0);
        remote.PressOffButton(0);
        remote.PressUndoButton();

        remote.PressOnButton(1);
        remote.PressOnButton(2);

        var partyMacro = new MacroCommand(new List<ICommand> { lightOn, acOn, tvOn });
        Console.WriteLine("\nВыполнение макрокоманды:");
        partyMacro.Execute();

        Console.WriteLine("\nОтмена макрокоманды:");
        partyMacro.Undo();
    }
}
