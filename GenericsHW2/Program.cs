namespace GenericsHW2
{
    class Program
    {
        static void Main()
        {
            Dictionary<string, string> employees = new Dictionary<string, string>();

            employees.Add("john", "pass123");
            employees.Add("mary", "qwerty1");
            employees.Add("bob", "hello99");

            Console.WriteLine("1 - Add");
            Console.WriteLine("2 - Remove");
            Console.WriteLine("3 - Update");
            Console.WriteLine("4 - Get password");
            Console.WriteLine("0 - Exit");

            while (true)
            {
                Console.Write("\nChoice :: ");
                string choice = Console.ReadLine();

                if (choice == "0")
                    break;

                if (choice == "1")
                {
                    Console.Write("Login :: ");
                    string login = Console.ReadLine();
                    Console.Write("Password :: ");
                    string password = Console.ReadLine();

                    if (employees.ContainsKey(login))
                        Console.WriteLine("Login already exists");
                    else
                    {
                        employees.Add(login, password);
                        Console.WriteLine("Added");
                    }
                }
                else if (choice == "2")
                {
                    Console.Write("Login :: ");
                    string login = Console.ReadLine();

                    if (employees.Remove(login))
                        Console.WriteLine("Removed");
                    else
                        Console.WriteLine("Login not found");
                }
                else if (choice == "3")
                {
                    Console.Write("Login :: ");
                    string login = Console.ReadLine();

                    if (employees.ContainsKey(login))
                    {
                        Console.Write("New login :: ");
                        string newLogin = Console.ReadLine();
                        Console.Write("New password :: ");
                        string newPassword = Console.ReadLine();

                        employees.Remove(login);
                        employees.Add(newLogin, newPassword);
                        Console.WriteLine("Updated");
                    }
                    else
                        Console.WriteLine("Login not found");
                }
                else if (choice == "4")
                {
                    Console.Write("Login :: ");
                    string login = Console.ReadLine();

                    if (employees.ContainsKey(login))
                        Console.WriteLine("Password :: " + employees[login]);
                    else
                        Console.WriteLine("Login not found");
                }
            }
        }
    }
}