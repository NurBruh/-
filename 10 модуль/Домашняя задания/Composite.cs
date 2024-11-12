using System;
using System.Collections.Generic;

public abstract class FileSystemComponent
{
    public string Name { get; set; }

    public abstract void Display();
    public abstract int GetSize();
}

public class File : FileSystemComponent
{
    public int Size { get; set; }

    public File(string name, int size)
    {
        Name = name;
        Size = size;
    }

    public override void Display()
    {
        Console.WriteLine($"Файл: {Name}, Размер: {Size} MB");
    }

    public override int GetSize()
    {
        return Size;
    }
}

public class Directory : FileSystemComponent
{
    private List<FileSystemComponent> components = new List<FileSystemComponent>();

    public Directory(string name)
    {
        Name = name;
    }

    public void AddComponent(FileSystemComponent component)
    {
        components.Add(component);
    }

    public void RemoveComponent(FileSystemComponent component)
    {
        components.Remove(component);
    }

    public override void Display()
    {
        Console.WriteLine($"Папка: {Name}");
        foreach (var component in components)
        {
            component.Display();
        }
    }

    public override int GetSize()
    {
        int totalSize = 0;
        foreach (var component in components)
        {
            totalSize += component.GetSize();
        }
        return totalSize;
    }
}

public class Program
{
    public static void Main()
    {
        File file1 = new File("Need for speed underground", 980);
        File file2 = new File("C&C Generals", 1000);
        File file3 = new File("Terraria", 320);

        Directory documents = new Directory("Documents");
        documents.AddComponent(file1);
        documents.AddComponent(file2);

        Directory games = new Directory("Games");
        games.AddComponent(file3);

        Directory mainDirectory = new Directory("Main");
        mainDirectory.AddComponent(documents);
        mainDirectory.AddComponent(games);

        mainDirectory.Display();
        Console.WriteLine($"Общий размер папки 'Main': {mainDirectory.GetSize()} MB");
    }
}
