using System;

public abstract class ReportGenerator
{
    public void GenerateReport()
    {
        CollectData();
        FormatData();
        if (!SaveIfNeeded())
        {
            Console.WriteLine("Отчет отменен.");
            return;
        }
        SendReport();
    }

    protected abstract void CollectData();
    protected abstract void FormatData();
    protected abstract void SaveReport();

    protected virtual bool CustomerWantsSave()
    {
        Console.Write("Хотите сохранить отчет? (y/n): ");
        string input = Console.ReadLine();
        return input.ToLower() == "y";
    }

    private bool SaveIfNeeded()
    {
        if (CustomerWantsSave())
        {
            SaveReport();
            return true;
        }
        return false;
    }

    protected virtual void SendReport()
    {
        Console.WriteLine("Отчет отправлен по электронной почте.");
    }
}

public class PdfReport : ReportGenerator
{
    protected override void CollectData()
    {
        Console.WriteLine("Сбор данных для PDF-отчета.");
    }

    protected override void FormatData()
    {
        Console.WriteLine("Форматирование данных для PDF.");
    }

    protected override void SaveReport()
    {
        Console.WriteLine("PDF-отчет сохранен.");
    }
}

public class ExcelReport : ReportGenerator
{
    protected override void CollectData()
    {
        Console.WriteLine("Сбор данных для Excel-отчета.");
    }

    protected override void FormatData()
    {
        Console.WriteLine("Форматирование данных для Excel.");
    }

    protected override void SaveReport()
    {
        Console.WriteLine("Excel-отчет сохранен.");
    }

    protected override void SendReport()
    {
        Console.WriteLine("Excel-отчет отправлен в корпоративную систему.");
    }
}

public class HtmlReport : ReportGenerator
{
    protected override void CollectData()
    {
        Console.WriteLine("Сбор данных для HTML-отчета.");
    }

    protected override void FormatData()
    {
        Console.WriteLine("Форматирование данных для HTML.");
    }

    protected override void SaveReport()
    {
        Console.WriteLine("HTML-отчет сохранен.");
    }
}

public class CsvReport : ReportGenerator
{
    protected override void CollectData()
    {
        Console.WriteLine("Сбор данных для CSV-отчета.");
    }

    protected override void FormatData()
    {
        Console.WriteLine("Форматирование данных для CSV.");
    }

    protected override void SaveReport()
    {
        Console.WriteLine("CSV-отчет сохранен.");
    }

    protected override bool CustomerWantsSave()
    {
        Console.WriteLine("CSV-отчеты всегда сохраняются.");
        return true;
    }
}

class Program
{
    static void Main()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n--- Меню ---");
            Console.WriteLine("1. Создать PDF-отчет");
            Console.WriteLine("2. Создать Excel-отчет");
            Console.WriteLine("3. Создать HTML-отчет");
            Console.WriteLine("4. Создать CSV-отчет");
            Console.WriteLine("5. Выйти");

            Console.Write("Выберите действие: ");
            string input = Console.ReadLine();

            ReportGenerator report = null;

            switch (input)
            {
                case "1":
                    report = new PdfReport();
                    break;
                case "2":
                    report = new ExcelReport();
                    break;
                case "3":
                    report = new HtmlReport();
                    break;
                case "4":
                    report = new CsvReport();
                    break;
                case "5":
                    running = false;
                    Console.WriteLine("Выход из программы.");
                    continue;
                default:
                    Console.WriteLine("Некорректный выбор!");
                    continue;
            }

            report.GenerateReport();
        }
    }
}
