using System.Configuration;
using System.Text;

namespace connectedmode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string conn = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            SqlConnection connection = new SqlConnection(conn);
            connection.Open();

            Console.WriteLine("Connected");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n---- МЕНЮ ----");
                Console.WriteLine("1. Додати нову продажу");
                Console.WriteLine("2. Показати всі продажі за період");
                Console.WriteLine("3. Показати останню покупку покупця");
                Console.WriteLine("4. Видалити продавця або покупця по id");
                Console.WriteLine("5. Показати продавця з найбільшою сумою продажів");
                Console.WriteLine("0. Вихід");
                Console.Write("Enter your choice :: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddSale(connection);
                        break;
                    case "2":
                        ShowSalesByPeriod(connection);
                        break;
                    case "3":
                        ShowLastPurchase(connection);
                        break;
                    case "4":
                        DeleteById(connection);
                        break;
                    case "5":
                        ShowTopSeller(connection);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Невірний вибір");
                        break;
                }
            }

            connection.Close();
        }

        // Task 1
        static void AddSale(SqlConnection connection)
        {
            Console.Write("Enter pokupets id :: ");
            int pokupetsId = int.Parse(Console.ReadLine());

            Console.Write("Enter prodavets id :: ");
            int prodavetsId = int.Parse(Console.ReadLine());

            Console.Write("Enter suma :: ");
            decimal suma = decimal.Parse(Console.ReadLine());

            Console.Write("Enter data (yyyy-MM-dd) :: ");
            DateTime data = DateTime.Parse(Console.ReadLine());

            string cmdText = @"insert into Prodazhi (PokupetsId, ProdavetsId, Suma, DataProdazhi)
                                values (@pokupetsId, @prodavetsId, @suma, @data)";
            SqlCommand command = new SqlCommand(cmdText, connection);
            command.Parameters.AddWithValue("@pokupetsId", pokupetsId);
            command.Parameters.AddWithValue("@prodavetsId", prodavetsId);
            command.Parameters.AddWithValue("@suma", suma);
            command.Parameters.AddWithValue("@data", data);

            int rows = command.ExecuteNonQuery();
            Console.WriteLine(rows + " rows affected");
        }

        // Task 2
        static void ShowSalesByPeriod(SqlConnection connection)
        {
            Console.Write("Enter start date (yyyy-MM-dd) :: ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter end date (yyyy-MM-dd) :: ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            string cmdText = @"select s.Id, b.Imya as PokupetsImya, b.Prizvyshe as PokupetsPrizvyshe,
                                       p.Imya as ProdavetsImya, p.Prizvyshe as ProdavetsPrizvyshe,
                                       s.Suma, s.DataProdazhi
                                from Prodazhi s
                                join Pokupci b on s.PokupetsId = b.Id
                                join Prodavci p on s.ProdavetsId = p.Id
                                where s.DataProdazhi between @startDate and @endDate";
            SqlCommand command = new SqlCommand(cmdText, connection);
            command.Parameters.AddWithValue("@startDate", startDate);
            command.Parameters.AddWithValue("@endDate", endDate);

            var reader = command.ExecuteReader();

            Console.WriteLine("\n--------------------------------");
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write($"{reader.GetName(i),-18}");
            }
            Console.WriteLine();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($"{reader[i],-18}");
                }
                Console.WriteLine();
            }
            reader.Close();
        }

        // Task 3
        static void ShowLastPurchase(SqlConnection connection)
        {
            Console.Write("Enter imya :: ");
            string imya = Console.ReadLine();

            Console.Write("Enter prizvyshe :: ");
            string prizvyshe = Console.ReadLine();

            string cmdText = @"select top 1 s.Id, s.Suma, s.DataProdazhi, p.Imya as ProdavetsImya, p.Prizvyshe as ProdavetsPrizvyshe
                                from Prodazhi s
                                join Pokupci b on s.PokupetsId = b.Id
                                join Prodavci p on s.ProdavetsId = p.Id
                                where b.Imya = @imya and b.Prizvyshe = @prizvyshe
                                order by s.DataProdazhi desc";
            SqlCommand command = new SqlCommand(cmdText, connection);
            command.Parameters.AddWithValue("@imya", imya);
            command.Parameters.AddWithValue("@prizvyshe", prizvyshe);

            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                Console.WriteLine($"Id продажі :: {reader["Id"]}");
                Console.WriteLine($"Сума :: {reader["Suma"]}");
                Console.WriteLine($"Дата :: {reader["DataProdazhi"]}");
                Console.WriteLine($"Продавець :: {reader["ProdavetsImya"]} {reader["ProdavetsPrizvyshe"]}");
            }
            else
            {
                Console.WriteLine("Покупок не знайдено");
            }
            reader.Close();
        }

        // Task 4
        static void DeleteById(SqlConnection connection)
        {
            Console.Write("Delete pokupets or prodavets (p/s) :: ");
            string type = Console.ReadLine();

            Console.Write("Enter id :: ");
            int id = int.Parse(Console.ReadLine());

            string cmdText;
            if (type == "p")
            {
                cmdText = "delete from Pokupci where Id = @id";
            }
            else
            {
                cmdText = "delete from Prodavci where Id = @id";
            }

            SqlCommand command = new SqlCommand(cmdText, connection);
            command.Parameters.AddWithValue("@id", id);

            try
            {
                int rows = command.ExecuteNonQuery();
                Console.WriteLine(rows + " rows affected");
            }
            catch (SqlException)
            {
                Console.WriteLine("Неможливо видалити, є пов'язані продажі");
            }
        }

        // Task 5
        static void ShowTopSeller(SqlConnection connection)
        {
            string cmdText = @"select top 1 p.Id, p.Imya, p.Prizvyshe, SUM(s.Suma) as ZagalnaSuma
                                from Prodazhi s
                                join Prodavci p on s.ProdavetsId = p.Id
                                group by p.Id, p.Imya, p.Prizvyshe
                                order by ZagalnaSuma desc";
            SqlCommand command = new SqlCommand(cmdText, connection);

            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                Console.WriteLine($"Продавець :: {reader["Imya"]} {reader["Prizvyshe"]}");
                Console.WriteLine($"Загальна сума продажів :: {reader["ZagalnaSuma"]}");
            }
            reader.Close();
        }
    }

    internal class SqlException : Exception
    {
    }

    internal class SqlCommand
    {
        public SqlCommand(string cmdText, SqlConnection connection)
        {
            throw new NotImplementedException();
        }

        public object ExecuteReader()
        {
            throw new NotImplementedException();
        }
    }

    internal class SqlConnection
    {
        public SqlConnection(string conn)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}