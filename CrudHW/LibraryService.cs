using Microsoft.Data.SqlClient;
using System;

namespace CrudHW
{
    public class LibraryService
    {
        private string connectionString;

        public LibraryService(string connStr)
        {
            connectionString = connStr;
        }

        public void AddBook(string title, int authorId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string queryBook = "INSERT INTO Books (Title) OUTPUT INSERTED.Id VALUES (@title)";
                int bookId;
                using (SqlCommand command = new SqlCommand(queryBook, connection))
                {
                    command.Parameters.AddWithValue("@title", title);
                    bookId = (int)command.ExecuteScalar();
                }

                string queryAuthor = "INSERT INTO BookAuthors (BookId, AuthorId) VALUES (@bookId, @authorId)";
                using (SqlCommand command = new SqlCommand(queryAuthor, connection))
                {
                    command.Parameters.AddWithValue("@bookId", bookId);
                    command.Parameters.AddWithValue("@authorId", authorId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public int GetVisitorsCount()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Visitors";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public void PrintDebtors()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, FirstName, LastName FROM Visitors WHERE IsDebtor = 1";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["Id"]}, {reader["FirstName"]} {reader["LastName"]}");
                        }
                    }
                }
            }
        }

        public void PrintBookAuthors(string bookTitle)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT a.FirstName, a.LastName 
                                FROM Authors a 
                                JOIN BookAuthors ba ON a.Id = ba.AuthorId 
                                JOIN Books b ON b.Id = ba.BookId 
                                WHERE b.Title = @title";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", bookTitle);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Автор: {reader["FirstName"]} {reader["LastName"]}");
                        }
                    }
                }
            }
        }

        public void PrintAvailableBooks()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT b.Id, b.Title FROM Books b 
                                WHERE b.Id NOT IN (SELECT BookId FROM VisitorBooks WHERE ReturnDate IS NULL)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["Id"]}, Назва: {reader["Title"]}");
                        }
                    }
                }
            }
        }

        public void PrintUserBooks(int visitorId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT b.Title, vb.TakeDate FROM Books b 
                                JOIN VisitorBooks vb ON b.Id = vb.BookId 
                                WHERE vb.VisitorId = @visitorId AND vb.ReturnDate IS NULL";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@visitorId", visitorId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Книга: {reader["Title"]}, Взято: {reader["TakeDate"]}");
                        }
                    }
                }
            }
        }

        public void ClearDebts()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Visitors SET IsDebtor = 0";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Заборгованості очищено.");
                }
            }
        }
    }
}
