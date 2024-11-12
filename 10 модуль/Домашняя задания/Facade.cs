using System;

public class TV
{
    public void TurnOn()
    {
        Console.WriteLine("Телевизор включён! Попкорн готов?");
    }

    public void TurnOff()
    {
        Console.WriteLine("Телевизор выключен. Спокойной ночи, экран!");
    }

    public void SetChannel(int channel)
    {
        Console.WriteLine($"Канал переключен на {channel}. Как насчёт этого шоу?");
    }
}

public class AudioSystem
{
    public void TurnOn()
    {
        Console.WriteLine("Аудиосистема включена! Музыка нас ждёт!");
    }

    public void TurnOff()
    {
        Console.WriteLine("Аудиосистема выключена. Тишина — новый звук.");
    }

    public void SetVolume(int volume)
    {
        Console.WriteLine($"Громкость установлена на {volume}. Да будет громко!");
    }
}

public class DVDPlayer
{
    public void Play()
    {
        Console.WriteLine("DVD-проигрыватель запущен! Попкорн пошёл в дело!");
    }

    public void Pause()
    {
        Console.WriteLine("Фильм на паузе. Сходи за попкорном.");
    }

    public void Stop()
    {
        Console.WriteLine("Остановка фильма. Всё хорошее когда-нибудь заканчивается.");
    }
}

public class GameConsole
{
    public void TurnOn()
    {
        Console.WriteLine("Игровая консоль включена! Готов к бою?");
    }

    public void StartGame()
    {
        Console.WriteLine("Игра началась! Победа ждёт!");
    }
}

public class HomeTheaterFacade
{
    private TV _tv;
    private AudioSystem _audioSystem;
    private DVDPlayer _dvdPlayer;
    private GameConsole _gameConsole;

    public HomeTheaterFacade(TV tv, AudioSystem audioSystem, DVDPlayer dvdPlayer, GameConsole gameConsole)
    {
        _tv = tv;
        _audioSystem = audioSystem;
        _dvdPlayer = dvdPlayer;
        _gameConsole = gameConsole;
    }

    public void WatchMovie()
    {
        Console.WriteLine("\nПриготовься к сеансу:");
        _tv.TurnOn();
        _audioSystem.TurnOn();
        _audioSystem.SetVolume(50);
        _dvdPlayer.Play();
    }

    public void EndMovieNight()
    {
        Console.WriteLine("\nСеанс окончен:");
        _dvdPlayer.Stop();
        _tv.TurnOff();
        _audioSystem.TurnOff();
    }

    public void PlayGame()
    {
        Console.WriteLine("\nИгровая ночь начинается:");
        _tv.TurnOn();
        _gameConsole.TurnOn();
        _gameConsole.StartGame();
    }

    public void PlayMusic()
    {
        Console.WriteLine("\nНачнём музыкальный марафон:");
        _tv.TurnOn();
        _audioSystem.TurnOn();
        _audioSystem.SetVolume(40);
    }

    public void SetVolume(int volume)
    {
        Console.WriteLine($"\nМеняем громкость на {volume}:");
        _audioSystem.SetVolume(volume);
    }
}

public class Program
{
    public static void Main()
    {
        TV tv = new TV();
        AudioSystem audioSystem = new AudioSystem();
        DVDPlayer dvdPlayer = new DVDPlayer();
        GameConsole gameConsole = new GameConsole();

        HomeTheaterFacade homeTheater = new HomeTheaterFacade(tv, audioSystem, dvdPlayer, gameConsole);

        homeTheater.WatchMovie();
        homeTheater.SetVolume(30);
        homeTheater.EndMovieNight();

        Console.WriteLine();

        homeTheater.PlayGame();
        Console.WriteLine();

        homeTheater.PlayMusic();
        Console.WriteLine();
    }
}
