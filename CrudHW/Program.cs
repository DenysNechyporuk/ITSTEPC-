using System;
using System.Configuration;

namespace CrudHW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString;
            LibraryService service = new LibraryService(connStr);

            while (true)
            {
                Console.WriteLine("\n--- МЕНЮ БІБЛІОТЕКИ ---");
                Console.WriteLine("1. Додати нову книгу");
                Console.WriteLine("2. Кількість користувачів");
                Console.WriteLine("3. Список боржників");
                Console.WriteLine("4. Автори книги");
                Console.WriteLine("5. Доступні книги");
                Console.WriteLine("6. Книги на руках у користувача");
                Console.WriteLine("7. Очистити заборгованості");
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть пункт: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                switch (choice)
                {
                    case "1":
                        Console.Write("Назва книги: ");
                        string title = Console.ReadLine();
                        Console.Write("ID автора: ");
                        int authorId = int.Parse(Console.ReadLine());
                        service.AddBook(title, authorId);
                        Console.WriteLine("Книгу додано!");
                        break;
                    case "2":
                        Console.WriteLine($"Кількість: {service.GetVisitorsCount()}");
                        break;
                    case "3":
                        service.PrintDebtors();
                        break;
                    case "4":
                        Console.Write("Назва книги: ");
                        service.PrintBookAuthors(Console.ReadLine());
                        break;
                    case "5":
                        service.PrintAvailableBooks();
                        break;
                    case "6":
                        Console.Write("ID користувача: ");
                        service.PrintUserBooks(int.Parse(Console.ReadLine()));
                        break;
                    case "7":
                        service.ClearDebts();
                        break;
                }
            }
        }
    }
}
