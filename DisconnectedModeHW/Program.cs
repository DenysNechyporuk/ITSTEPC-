using System;
using System.Collections.Generic;
using System.Linq;

namespace DisconnectedModeHW
{
    internal class Program
    {
        private static List<User> users = new List<User>();
        private static int nextId = 1;

        static void Main(string[] args)
        {
            users.Add(new User { Id = nextId++, Login = "admin", Password = "123", Address = "Київ", Phone = "0991112233", IsAdmin = true });
            users.Add(new User { Id = nextId++, Login = "user1", Password = "qwerty", Address = "Львів", Phone = "0974445566", IsAdmin = false });

            while (true)
            {
                Console.WriteLine("\n--- УПРАВЛІННЯ КОРИСТУВАЧАМИ ---");
                Console.WriteLine("1. Показати всіх користувачів");
                Console.WriteLine("2. Додати користувача");
                Console.WriteLine("3. Редагувати користувача");
                Console.WriteLine("4. Видалити користувача");
                Console.WriteLine("5. Показати лише адміністраторів");
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть пункт: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                switch (choice)
                {
                    case "1": ShowUsers(users); break;
                    case "2": AddUser(); break;
                    case "3": EditUser(); break;
                    case "4": DeleteUser(); break;
                    case "5": ShowUsers(users.Where(u => u.IsAdmin).ToList()); break;
                }
            }
        }

        static void ShowUsers(List<User> list)
        {
            foreach (var u in list)
            {
                string role = u.IsAdmin ? "Адмін" : "Користувач";
                Console.WriteLine($"[{u.Id}] Логін: {u.Login}, Адреса: {u.Address}, Тел: {u.Phone}, Роль: {role}");
            }
        }

        static void AddUser()
        {
            Console.Write("Логін: ");
            string login = Console.ReadLine().Trim();

            if (users.Any(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Помилка: логін вже зайнятий!");
                return;
            }

            Console.Write("Пароль: ");
            string password = Console.ReadLine();
            Console.Write("Адреса: ");
            string address = Console.ReadLine();
            Console.Write("Телефон: ");
            string phone = Console.ReadLine();
            Console.Write("Адмін (1 - Так, 0 - Ні): ");
            bool isAdmin = Console.ReadLine() == "1";

            users.Add(new User
            {
                Id = nextId++,
                Login = login,
                Password = password,
                Address = address,
                Phone = phone,
                IsAdmin = isAdmin
            });

            Console.WriteLine("Користувача додано!");
        }

        static void EditUser()
        {
            Console.Write("ID користувача для редагування: ");
            int id = int.Parse(Console.ReadLine());

            var u = users.FirstOrDefault(x => x.Id == id);
            if (u == null)
            {
                Console.WriteLine("Користувача не знайдено.");
                return;
            }

            Console.Write("Новий пароль: ");
            u.Password = Console.ReadLine();
            Console.Write("Нова адреса: ");
            u.Address = Console.ReadLine();
            Console.Write("Новий телефон: ");
            u.Phone = Console.ReadLine();
            Console.Write("Адмін (1 - Так, 0 - Ні): ");
            u.IsAdmin = Console.ReadLine() == "1";

            Console.WriteLine("Дані оновлено!");
        }

        static void DeleteUser()
        {
            Console.Write("ID користувача для видалення: ");
            int id = int.Parse(Console.ReadLine());

            var u = users.FirstOrDefault(x => x.Id == id);
            if (u != null)
            {
                users.Remove(u);
                Console.WriteLine("Користувача видалено!");
            }
        }
    }
}
