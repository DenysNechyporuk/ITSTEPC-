using Microsoft.Data.SqlClient;
using System;
using System.Configuration;

namespace ConnectedModeHW
{
    internal class Program
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["SalesDb"].ConnectionString;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n--- МЕНЮ ---");
                Console.WriteLine("1. Додати нову продажу");
                Console.WriteLine("2. Відобразити всі продажі за період");
                Console.WriteLine("3. Остання покупка покупця (Ім'я, Прізвище)");
                Console.WriteLine("4. Видалити продавця або покупця по ID");
                Console.WriteLine("5. Продавець з найбільшою сумою продажів");
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть пункт: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                switch (choice)
                {
                    case "1":
                        AddSale();
                        break;
                    case "2":
                        ShowSalesByPeriod();
                        break;
                    case "3":
                        ShowLastPurchase();
                        break;
                    case "4":
                        DeletePerson();
                        break;
                    case "5":
                        ShowTopSeller();
                        break;
                }
            }
        }

        static void AddSale()
        {
            Console.Write("Введіть ID покупця: ");
            int buyerId = int.Parse(Console.ReadLine());
            Console.Write("Введіть ID продавця: ");
            int sellerId = int.Parse(Console.ReadLine());
            Console.Write("Введіть суму: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Sales (BuyerId, SellerId, Amount, SaleDate) VALUES (@buyerId, @sellerId, @amount, @saleDate)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@buyerId", buyerId);
                    command.Parameters.AddWithValue("@sellerId", sellerId);
                    command.Parameters.AddWithValue("@amount", amount);
                    command.Parameters.AddWithValue("@saleDate", DateTime.Now);

                    command.ExecuteNonQuery();
                    Console.WriteLine("Продажу додано!");
                }
            }
        }

        static void ShowSalesByPeriod()
        {
            Console.Write("Введіть початкову дату (yyyy-MM-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Введіть кінцеву дату (yyyy-MM-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Sales WHERE SaleDate BETWEEN @start AND @end";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@start", startDate);
                    command.Parameters.AddWithValue("@end", endDate);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["Id"]}, BuyerID: {reader["BuyerId"]}, SellerID: {reader["SellerId"]}, Сума: {reader["Amount"]}, Дата: {reader["SaleDate"]}");
                        }
                    }
                }
            }
        }

        static void ShowLastPurchase()
        {
            Console.Write("Введіть ім'я покупця: ");
            string firstName = Console.ReadLine();
            Console.Write("Введіть прізвище покупця: ");
            string lastName = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT TOP 1 s.Id, s.Amount, s.SaleDate 
                                FROM Sales s 
                                JOIN Buyers b ON s.BuyerId = b.Id 
                                WHERE b.FirstName = @firstName AND b.LastName = @lastName 
                                ORDER BY s.SaleDate DESC";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"Остання покупка - ID: {reader["Id"]}, Сума: {reader["Amount"]}, Дата: {reader["SaleDate"]}");
                        }
                        else
                        {
                            Console.WriteLine("Покупок не знайдено.");
                        }
                    }
                }
            }
        }

        static void DeletePerson()
        {
            Console.WriteLine("Кого видалити? 1 - Покупця, 2 - Продавця: ");
            string type = Console.ReadLine();
            Console.Write("Введіть ID: ");
            int id = int.Parse(Console.ReadLine());

            string table = type == "1" ? "Buyers" : "Sellers";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"DELETE FROM {table} WHERE Id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int rows = command.ExecuteNonQuery();
                    Console.WriteLine($"Видалено записів: {rows}");
                }
            }
        }

        static void ShowTopSeller()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT TOP 1 s.Id, s.FirstName, s.LastName, SUM(sa.Amount) AS TotalSum
                                FROM Sellers s
                                JOIN Sales sa ON s.Id = sa.SellerId
                                GROUP BY s.Id, s.FirstName, s.LastName
                                ORDER BY TotalSum DESC";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"Топ продавець: {reader["FirstName"]} {reader["LastName"]}, Загальна сума: {reader["TotalSum"]}");
                        }
                    }
                }
            }
        }
    }
}
