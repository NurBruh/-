using System;
using System.Collections.Generic;

public interface IBook
{
    string GetBookInfo();
}

public interface ICatalog
{
    void AddBook(Book book);
    List<Book> SearchBooks(string keyword);
    void DisplayBooks();
}

public interface IAccountingSystem
{
    void RecordTransaction(string bookTitle, string readerName, string action);
}

public class Book : IBook
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public string ISBN { get; set; }

    public Book(string title, string author, string genre, string isbn)
    {
        Title = title;
        Author = author;
        Genre = genre;
        ISBN = isbn;
    }

    public string GetBookInfo()
    {
        return $"{Title} by {Author}, Genre: {Genre}, ISBN: {ISBN}";
    }
}

public class Reader
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TicketNumber { get; set; }

    public Reader(string firstName, string lastName, string ticketNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        TicketNumber = ticketNumber;
    }

    public string GetReaderInfo()
    {
        return $"{FirstName} {LastName}, Ticket Number: {TicketNumber}";
    }
}

public class Catalog : ICatalog
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        books.Add(book);
        Console.WriteLine($"Book added: {book.Title}");
    }

    public List<Book> SearchBooks(string keyword)
    {
        return books.FindAll(b => b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                   b.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

    public void DisplayBooks()
    {
        Console.WriteLine("Books in Catalog:");
        foreach (var book in books)
        {
            Console.WriteLine(book.GetBookInfo());
        }
    }
}

public class Librarian
{
    private readonly IAccountingSystem _accountingSystem;

    public Librarian(IAccountingSystem accountingSystem)
    {
        _accountingSystem = accountingSystem;
    }

    public void IssueBook(Book book, Reader reader)
    {
        Console.WriteLine($"Issuing book: {book.Title} to {reader.FirstName}");
        _accountingSystem.RecordTransaction(book.Title, reader.FirstName, "Issued");
    }

    public void ReturnBook(Book book, Reader reader)
    {
        Console.WriteLine($"Returning book: {book.Title} from {reader.FirstName}");
        _accountingSystem.RecordTransaction(book.Title, reader.FirstName, "Returned");
    }
}

public class AccountingSystem : IAccountingSystem
{
    public void RecordTransaction(string bookTitle, string readerName, string action)
    {
        Console.WriteLine($"Transaction: {action} - '{bookTitle}' by {readerName}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        ICatalog catalog = new Catalog();
        IAccountingSystem accountingSystem = new AccountingSystem();
        Librarian librarian = new Librarian(accountingSystem);
        catalog.AddBook(new Book("Криптография и безопасность данных", "Уильям Столлингс", "Информационная безопасность", "978-5-8459-1495-8"));
        catalog.AddBook(new Book("Основы информационной безопасности", "Николас Хэнкок", "Информационная безопасность", "978-5-699-53484-9"));
        catalog.AddBook(new Book("Защита компьютерных систем", "Джонатан Уайсман", "ИБ", "978-5-93286-086-7"));
        catalog.AddBook(new Book("Этика хакеров и взломщиков", "Джордж Смит", "ИБ", "978-5-699-51918-7"));
        catalog.AddBook(new Book("Киберугрозы и защита от них", "Ирина Петрова", "ИБ", "978-5-17-104303-9"));
        catalog.AddBook(new Book("Практическое руководство по ИБ", "Дэн Браун", "Информационная безопасность", "978-5-17-083242-8"));
        catalog.AddBook(new Book("Информационная война", "Эдвард Льюис", "ИБ", "978-5-8459-0845-1"));

        Reader reader = new Reader("Ержан", "Нурбеков", "R001");

        catalog.DisplayBooks();
        librarian.IssueBook(new Book("Криптография и безопасность данных", "Уильям Столлингс", "ИБ", "978-5-8459-1495-8"), reader);
        librarian.ReturnBook(new Book("Криптография и безопасность данных", "Уильям Столлингс", "ИБ", "978-5-8459-1495-8"), reader);
    }
}
