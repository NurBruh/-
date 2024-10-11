using System;
using System.Collections.Generic;
using System.IO;

public class ReportStyle
{
    public string BackgroundColor { get; set; }
    public string FontColor { get; set; }
    public int FontSize { get; set; }
}

public interface IReportBuilder
{
    void SetHeader(string header);
    void SetContent(string content);
    void SetFooter(string footer);
    void AddSection(string sectionName, string sectionContent);
    void SetStyle(ReportStyle style);
    Report GetReport();
}
public class Report
{
    public string Header { get; set; }
    public string Content { get; set; }
    public string Footer { get; set; }
    public List<(string SectionName, string SectionContent)> Sections { get; } = new List<(string, string)>();
    public ReportStyle Style { get; set; }

    public void Export(string format)
    {
        switch (format.ToLower())
        {
            case "text":
                ExportToText();
                break;
            case "html":
                ExportToHtml();
                break;
            default:
                Console.WriteLine("ERROR.");
                break;
        }
    }

    private void ExportToText()
    {
        string filePath = "report.txt";
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine(Header);
            writer.WriteLine(Content);
            writer.WriteLine(Footer);
            foreach (var section in Sections)
            {
                writer.WriteLine($"{section.SectionName}: {section.SectionContent}");
            }
        }
        Console.WriteLine($"Отчет сохранен в {filePath}.");
    }

    private void ExportToHtml()
    {
        string filePath = "report.html"; 
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("<html>");
            writer.WriteLine("<head><title>ОТЧЕТ</title></head>");
            writer.WriteLine("<body>");
            writer.WriteLine("<style>\r\n body {\r\n " +
                "font-family: Arial, sans-serif;\r\n " +
                " margin: 20px;\r\n padding: 0;\r\n  " +
                "background-color: #f4f4f4;\r\n }\r\n " +
                "h1, h2 {\r\n  color: #333;\r\n }\r\n  " +
                "p {\r\n margin: 10px 0;\r\n  }\r\n   " +
                "footer {\r\n margin-top: 20px;\r\n   " +
                "padding: 10px;\r\n background-color: #ddd;\r\n      " +
                "text-align: center;\r\n}\r\n    " +
                "</style>");
            writer.WriteLine($"<h1>{Header}</h1>");
            writer.WriteLine($"<p>{Content}</p>");
            writer.WriteLine($"<footer>{Footer}</footer>");
            foreach (var section in Sections)
            {
                writer.WriteLine($"<h2>{section.SectionName}</h2>");
                writer.WriteLine($"<p>{section.SectionContent}</p>");
            }
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }
        Console.WriteLine($"Отчет находится тута ==> {filePath}.");
    }
}

public class TextReportBuilder : IReportBuilder
{
    private Report _report = new Report();

    public void SetHeader(string header) => _report.Header = header;
    public void SetContent(string content) => _report.Content = content;
    public void SetFooter(string footer) => _report.Footer = footer;
    public void AddSection(string sectionName, string sectionContent) => _report.Sections.Add((sectionName, sectionContent));
    public void SetStyle(ReportStyle style) => _report.Style = style;
    public Report GetReport() => _report;
}

public class HtmlReportBuilder : IReportBuilder
{
    private Report _report = new Report();

    public void SetHeader(string header) => _report.Header = $"<h1>{header}</h1>";
    public void SetContent(string content) => _report.Content = $"<p>{content}</p>";
    public void SetFooter(string footer) => _report.Footer = $"<footer>{footer}</footer>";
    public void AddSection(string sectionName, string sectionContent) => _report.Sections.Add((sectionName, $"<h2>{sectionName}</h2><p>{sectionContent}</p>"));
    public void SetStyle(ReportStyle style) => _report.Style = style;
    public Report GetReport() => _report;
}

public class ReportDirector
{
    public void ConstructReport(IReportBuilder builder, ReportStyle style)
    {
        builder.SetStyle(style);
        builder.SetHeader("Отчет");
        builder.SetContent("Основа.");
        builder.SetFooter("SCP-087-В");
        builder.AddSection("1", "2b2t");
        builder.AddSection("2", "R@ndar");
    }
}
internal class Program
{
    static void Main(string[] args)
    {
        ReportDirector director = new ReportDirector();
        ReportStyle style = new ReportStyle { BackgroundColor = "White", FontColor = "Black", FontSize = 12 };





        TextReportBuilder textBuilder = new TextReportBuilder();
        director.ConstructReport(textBuilder, style);
        Report textReport = textBuilder.GetReport();
        textReport.Export("text");

        HtmlReportBuilder htmlBuilder = new HtmlReportBuilder();
        director.ConstructReport(htmlBuilder, style);
        Report htmlReport = htmlBuilder.GetReport();
        htmlReport.Export("html");
    }
}
