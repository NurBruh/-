using System;
using System.Collections.Generic;
using System.IO;

public interface IUser
{
    void Register();
    void Login();
}

public abstract class User : IUser
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    protected User(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public abstract void Register();
    public abstract void Login();
}

public class Reader : User
{
    public List<Book> BorrowedBooks { get; set; }

    public Reader(int id, string name, string email) : base(id, name, email)
    {
        BorrowedBooks = new List<Book>();
    }

    public override void Register()
    {
        Console.WriteLine($"Читатель {Name} зарегистрирован.");
    }

    public override void Login()
    {
        Console.WriteLine($"Читатель {Name} вошел в систему.");
    }

    public void BorrowBook(Book book)
    {
        if (!book.IsAvailable)
        {
            Console.WriteLine($"Книга '{book.Title}' недоступна для выдачи.");
            return;
        }
        BorrowedBooks.Add(book);
        book.ChangeAvailability(false);
        Console.WriteLine($"Читатель {Name} взял книгу '{book.Title}'.");
    }

    public void ReturnBook(Book book)
    {
        if (BorrowedBooks.Contains(book))
        {
            BorrowedBooks.Remove(book);
            book.ChangeAvailability(true);
            Console.WriteLine($"Читатель {Name} вернул книгу '{book.Title}'.");
        }
        else
        {
            Console.WriteLine($"Читатель {Name} не брал книгу '{book.Title}'.");
        }
    }
}

public class Librarian : User
{
    public Librarian(int id, string name, string email) : base(id, name, email) { }

    public override void Register()
    {
        Console.WriteLine($"Библиотекарь {Name} зарегистрирован.");
    }

    public override void Login()
    {
        Console.WriteLine($"Библиотекарь {Name} вошел в систему.");
    }

    public void AddBook(Library library, Book book)
    {
        library.AddBook(book);
        Console.WriteLine($"Книга '{book.Title}' добавлена в библиотеку.");
    }
}

public class Author
{
    public string Name { get; set; }

    public Author(string name)
    {
        Name = name;
    }

    public string GetInfo()
    {
        return "Автор: " + Name;
    }

    public string GetAuthor()
    {
        return Name;
    }
}

public class Book
{
    public string Title { get; set; }
    public string ISBN { get; set; }
    public List<Author> Authors { get; set; }
    public int PublicationYear { get; set; }
    public bool IsAvailable { get; set; }

    public Book(string title, string isbn, List<Author> authors, int publicationYear, bool isAvailable = true)
    {
        Title = title;
        ISBN = isbn;
        Authors = authors;
        PublicationYear = publicationYear;
        IsAvailable = isAvailable;
    }

    public void ChangeAvailability(bool status)
    {
        IsAvailable = status;
    }

    public string GetBookInfo()
    {
        string authors = string.Join(", ", Authors.ConvertAll(a => a.GetAuthor()));
        string status = IsAvailable ? "Доступна" : "Недоступна";
        return $"Название: {Title}, ISBN: {ISBN}, Авторы: {authors}, Год: {PublicationYear}, Статус: {status}";
    }
}

public class Loan
{
    public Book Book { get; set; }
    public Reader Reader { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public Loan(Book book, Reader reader)
    {
        Book = book;
        Reader = reader;
        LoanDate = DateTime.Now;
        ReturnDate = null;
    }

    public void ReturnBook()
    {
        Book.ChangeAvailability(true);
        ReturnDate = DateTime.Now;
        Console.WriteLine($"Книга '{Book.Title}' возвращена читателем {Reader.Name}.");
    }
}

public class Library
{
    public List<Book> Books { get; set; }

    public Library()
    {
        Books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        Books.Add(book);
    }

    public List<Book> SearchBooks(string keyword)
    {
        return Books.FindAll(b => b.Title.Contains(keyword));
    }

    public void SaveToFile(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (Book book in Books)
            {
                writer.WriteLine(book.GetBookInfo());
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();
        Librarian librarian = new Librarian(1, "Жандос", "zhan@bk.ru");
        librarian.AddBook(library, new Book("Абай жолы", "12345", new List<Author> { new Author("Мухтар Әуезов") }, 1942));
        librarian.AddBook(library, new Book("Улпан", "67890", new List<Author> { new Author("Габит Мүсырепов") }, 1974));
        librarian.AddBook(library, new Book("Кошпендылер", "11223", new List<Author> { new Author("Илияс Есенберлин") }, 1971));
        librarian.AddBook(library, new Book("Кан мен тер", "33445", new List<Author> { new Author("Абдыжамыл Нурпейсов") }, 1961));
        librarian.AddBook(library, new Book("Дала жыры", "55667", new List<Author> { new Author("Сакен Сейфуллин") }, 1920));

        Reader reader = new Reader(2, "Ерлан", "Ereke78@mail.ru");
        reader.Register();
        reader.Login();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Поиск книги");
            Console.WriteLine("2. Взять книгу");
            Console.WriteLine("3. Вернуть книгу");
            Console.WriteLine("4. Сохранить данные в файл");
            Console.WriteLine("5. Выйти");
            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите ключевое слово для поиска: ");
                    string keyword = Console.ReadLine();
                    List<Book> searchResults = library.SearchBooks(keyword);
                    foreach (Book book in searchResults)
                    {
                        Console.WriteLine(book.GetBookInfo());
                    }
                    break;

                case "2":
                    Console.Write("Введите название книги, которую хотите взять: ");
                    string bookTitle = Console.ReadLine();
                    Book bookToBorrow = library.Books.Find(b => b.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));
                    if (bookToBorrow != null)
                    {
                        reader.BorrowBook(bookToBorrow);
                    }
                    else
                    {
                        Console.WriteLine("Книга не найдена.");
                    }
                    break;

                case "3":
                    Console.Write("Введите название книги, которую хотите вернуть: ");
                    string bookTitleToReturn = Console.ReadLine();
                    Book bookToReturn = library.Books.Find(b => b.Title.Equals(bookTitleToReturn, StringComparison.OrdinalIgnoreCase));
                    if (bookToReturn != null)
                    {
                        reader.ReturnBook(bookToReturn);
                    }
                    else
                    {
                        Console.WriteLine("Книга не найдена.");
                    }
                    break;

                case "4":
                    library.SaveToFile("library.txt");
                    Console.WriteLine("Данные сохранены в файл library.txt");
                    break;

                case "5":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                    break;
            }
        }
    }
}
