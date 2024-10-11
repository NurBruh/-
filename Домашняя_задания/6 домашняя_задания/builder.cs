using System;
using System.IO;

public interface IReportBuilder
{
    void SetHeader(string header);
    void SetContent(string content);
    void SetFooter(string footer);
    Report GetReport();
}

public class TextReportBuilder : IReportBuilder
{
    private Report report = new Report();

    public void SetHeader(string header)
    {
        report.Header = "==== " + header + " ====";
    }

    public void SetContent(string content)
    {
        report.Content = content;
    }

    public void SetFooter(string footer)
    {
        report.Footer = "==== " + footer + " ====";
    }

    public Report GetReport()
    {
        return report;
    }
}

public class HtmlReportBuilder : IReportBuilder
{
    private Report report = new Report();

    public void SetHeader(string header)
    {
        report.Header = "<h1>" + header + "</h1>";
    }

    public void SetContent(string content)
    {
        report.Content = "<p>" + content + "</p>";
    }

    public void SetFooter(string footer)
    {
        report.Footer = "<footer>" + footer + "</footer>";
    }

    public Report GetReport()
    {
        return report;
    }
}

public class ReportDirector
{
    public void ConstructReport(IReportBuilder builder)
    {
        builder.SetHeader("Компьютер");
        builder.SetContent("Железо компьютера: процессор, материнская плата, оперативная память, видеокарта, жесткий диск.");
        builder.SetFooter("Защищено авторскими правами © 2024");
    }
}

public class Report
{
    public string Header { get; set; }
    public string Content { get; set; }
    public string Footer { get; set; }

    public void SaveToHtmlFile(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("<html>");
            writer.WriteLine("<head>");
            writer.WriteLine("<style>");
            writer.WriteLine("body { font-family: Arial, sans-serif; background-color: #f4f4f4; }");
            writer.WriteLine("h1 { color: #333; }");
            writer.WriteLine("footer { margin-top: 20px; font-size: 12px; color: #666; }");
            writer.WriteLine("</style>");
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");
            writer.WriteLine(Header);
            writer.WriteLine(Content);
            writer.WriteLine(Footer);
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }
    }

    public void Display()
    {
        Console.WriteLine(Header);
        Console.WriteLine(Content);
        Console.WriteLine(Footer);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        ReportDirector director = new ReportDirector();

        IReportBuilder textBuilder = new TextReportBuilder();
        director.ConstructReport(textBuilder);
        Report textReport = textBuilder.GetReport();
        Console.WriteLine("Text Report:");
        textReport.Display();


        IReportBuilder htmlBuilder = new HtmlReportBuilder();
        director.ConstructReport(htmlBuilder);
        Report htmlReport = htmlBuilder.GetReport();
        string htmlFilePath = "computer_report.html";
        htmlReport.SaveToHtmlFile(htmlFilePath);
        Console.WriteLine($"\nHTML Report saved to: {htmlFilePath}");
    }
}
