using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public class ConfigurationManager
{
    private static readonly Lazy<ConfigurationManager> lazyInstance = new Lazy<ConfigurationManager>(() => new ConfigurationManager());
    private Dictionary<string, string> settings = new Dictionary<string, string>();
    private readonly object lockObject = new object();
    private string configFilePath = "config.txt";

    private ConfigurationManager()
    {
        LoadSettingsFromFile();
    }

    public static ConfigurationManager GetInstance()
    {
        return lazyInstance.Value;
    }

    public string GetSetting(string key)
    {
        lock (lockObject)
        {
            if (settings.TryGetValue(key, out string value))
            {
                return value;
            }
            throw new KeyNotFoundException($"Key: '{key}' not found.");
        }
    }

    public void SetSetting(string key, string value)
    {
        lock (lockObject)
        {
            settings[key] = value;
            Save();
        }
    }

    private void LoadSettingsFromFile()
    {
        lock (lockObject)
        {
            if (File.Exists(configFilePath))
            {
                string[] lines = File.ReadAllLines(configFilePath);
                foreach (string line in lines)
                {
                    var parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        settings[parts[0].Trim()] = parts[1].Trim();
                    }
                }
            }
        }
    }

    private void Save()
    {
        lock (lockObject)
        {
            var lines = new string[settings.Count];
            int i = 0;
            foreach (var kvp in settings)
            {
                lines[i++] = $"{kvp.Key}={kvp.Value}";
            }
            File.WriteAllLines(configFilePath, lines);
        }
    }
}

public class Program
{
    public static void Main()
    {
        var config1 = ConfigurationManager.GetInstance();
        var config2 = ConfigurationManager.GetInstance();
        Console.WriteLine($"Are config1 and config2 the same instance? {ReferenceEquals(config1, config2)}");

        config1.SetSetting("DatabaseConnectionString", "Server=myServer;Database=myDataBase;User Id=myUser;Password=myPassword;");
        Console.WriteLine($"Database Connection String: {config1.GetSetting("DatabaseConnectionString")}");

        var thread1 = new Thread(() =>
        {
            var configThread1 = ConfigurationManager.GetInstance();
            configThread1.SetSetting("Thread1Setting", "Value from Thread 1");
            Console.WriteLine($"Thread 1: {configThread1.GetSetting("Thread1Setting")}");
        });

        var thread2 = new Thread(() =>
        {
            var configThread2 = ConfigurationManager.GetInstance();
            configThread2.SetSetting("Thread2Setting", "Value from Thread 2");
            Console.WriteLine($"Thread 2: {configThread2.GetSetting("Thread2Setting")}");
        });

        thread1.Start();
        thread2.Start();
        thread1.Join();
        thread2.Join();

        Console.WriteLine("All tests completed.");
    }
}
