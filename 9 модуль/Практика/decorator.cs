using System;

public interface IReport
{
    string Generate();
}

public class SalesReport : IReport
{
    public string Generate()
    {
        return "Отчет по продажам";
    }
}

public class UserReport : IReport
{
    public string Generate()
    {
        return "Отчет по пользователям";
    }
}

public abstract class ReportDecorator : IReport
{
    protected IReport _report;

    public ReportDecorator(IReport report)
    {
        _report = report;
    }

    public virtual string Generate()
    {
        return _report.Generate();
    }
}

public class DateFilterDecorator : ReportDecorator
{
    private DateTime _startDate;
    private DateTime _endDate;

    public DateFilterDecorator(IReport report, DateTime startDate, DateTime endDate) : base(report)
    {
        _startDate = startDate;
        _endDate = endDate;
    }

    public override string Generate()
    {
        return $"{base.Generate()}\nФильтр по дате: {_startDate.ToShortDateString()} - {_endDate.ToShortDateString()}";
    }
}

public class SortingDecorator : ReportDecorator
{
    private string _sortCriterion;

    public SortingDecorator(IReport report, string sortCriterion) : base(report)
    {
        _sortCriterion = sortCriterion;
    }

    public override string Generate()
    {
        return $"{base.Generate()}\nСортировка по: {_sortCriterion}";
    }
}

public class CsvExportDecorator : ReportDecorator
{
    public CsvExportDecorator(IReport report) : base(report) { }

    public override string Generate()
    {
        return $"{base.Generate()}\nЭкспортировано в CSV";
    }
}

public class PdfExportDecorator : ReportDecorator
{
    public PdfExportDecorator(IReport report) : base(report) { }

    public override string Generate()
    {
        return $"{base.Generate()}\nЭкспортировано в PDF";
    }
}

public class AmountFilterDecorator : ReportDecorator
{
    private decimal _minAmount;
    private decimal _maxAmount;

    public AmountFilterDecorator(IReport report, decimal minAmount, decimal maxAmount) : base(report)
    {
        _minAmount = minAmount;
        _maxAmount = maxAmount;
    }

    public override string Generate()
    {
        return $"{base.Generate()}\nФильтр по сумме: {_minAmount} - {_maxAmount}";
    }
}

public static class ReportConfigurator
{
    public static IReport CreateReportWithDecorators(IReport report, bool filterByDate, bool sortByAmount, bool exportCsv, bool exportPdf)
    {
        if (filterByDate)
        {
            report = new DateFilterDecorator(report, DateTime.Now.AddMonths(-1), DateTime.Now);
        }
        if (sortByAmount)
        {
            report = new SortingDecorator(report, "Сумма");
        }
        if (exportCsv)
        {
            report = new CsvExportDecorator(report);
        }
        if (exportPdf)
        {
            report = new PdfExportDecorator(report);
        }
        return report;
    }
}

public class Program
{
    static void Main(string[] args)
    {
        IReport salesReport = new SalesReport();
        IReport customReport = ReportConfigurator.CreateReportWithDecorators(salesReport, filterByDate: true, sortByAmount: true, exportCsv: true, exportPdf: false);
        Console.WriteLine(customReport.Generate());

        IReport salesReportDatePDF = new SalesReport();
        IReport customReport1 = ReportConfigurator.CreateReportWithDecorators(salesReportDatePDF, filterByDate: true, sortByAmount: false, exportCsv: false, exportPdf: true);
        Console.WriteLine(customReport1.Generate());

        IReport userReportSortAge = new UserReport();
        IReport customReport2 = ReportConfigurator.CreateReportWithDecorators(userReportSortAge, filterByDate: false, sortByAmount: true, exportCsv: true, exportPdf: false);
        Console.WriteLine(customReport2.Generate());

        IReport salesReportCSV = new SalesReport();
        IReport customReport3 = ReportConfigurator.CreateReportWithDecorators(salesReportCSV, filterByDate: true, sortByAmount: false, exportCsv: true, exportPdf: false);
        Console.WriteLine(customReport3.Generate());

        IReport userReport = new UserReport();
        userReport = new PdfExportDecorator(userReport);
        Console.WriteLine(userReport.Generate());
    }
}
