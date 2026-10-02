using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GamesStudioHW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (GamesDb db = new GamesDb())
            {
                db.Database.EnsureCreated();
                SeedData(db);

                while (true)
                {
                    Console.WriteLine("\n--- УПРАВЛІННЯ СТУДІЯМИ ІГОР ---");
                    Console.WriteLine("1. Переглянути всі студії");
                    Console.WriteLine("2. Додати нову студію");
                    Console.WriteLine("3. Змінити дані студії");
                    Console.WriteLine("4. Видалити студію по назві");
                    Console.WriteLine("0. Вихід");
                    Console.Write("Оберіть пункт: ");

                    string choice = Console.ReadLine();
                    if (choice == "0") break;

                    switch (choice)
                    {
                        case "1": ShowStudios(db); break;
                        case "2": AddStudio(db); break;
                        case "3": EditStudio(db); break;
                        case "4": DeleteStudio(db); break;
                    }
                }
            }
        }

        static void SeedData(GamesDb db)
        {
            if (!db.Countries.Any())
            {
                Country usa = new Country { Name = "США" };
                Country ukraine = new Country { Name = "Україна" };
                db.Countries.AddRange(usa, ukraine);
                db.SaveChanges();

                City kyiv = new City { Name = "Київ", CountryId = ukraine.Id };
                City losAngeles = new City { Name = "Лос-Анджелес", CountryId = usa.Id };
                db.Cities.AddRange(kyiv, losAngeles);
                db.SaveChanges();

                Studio gsc = new Studio { Name = "GSC Game World", CountryId = ukraine.Id, CityId = kyiv.Id };
                Studio naughtyDog = new Studio { Name = "Naughty Dog", CountryId = usa.Id, CityId = losAngeles.Id };
                db.Studios.AddRange(gsc, naughtyDog);
                db.SaveChanges();

                db.Games.Add(new Game { Title = "S.T.A.L.K.E.R. 2", Genre = "Shooter", ReleaseYear = 2024, StudioId = gsc.Id });
                db.Games.Add(new Game { Title = "The Last of Us", Genre = "Action", ReleaseYear = 2013, StudioId = naughtyDog.Id });
                db.SaveChanges();
            }
        }

        static void ShowStudios(GamesDb db)
        {
            var list = db.Studios.Include(s => s.Country).Include(s => s.City).Include(s => s.Games).ToList();
            foreach (var s in list)
            {
                Console.WriteLine($"[ID: {s.Id}] Студія: {s.Name} | Країна: {s.Country?.Name} | Філія у місті: {s.City?.Name}");
                foreach (var g in s.Games)
                {
                    Console.WriteLine($"\tІгра: {g.Title} ({g.Genre}, {g.ReleaseYear})");
                }
            }
        }

        static void AddStudio(GamesDb db)
        {
            Console.Write("Введіть назву студії: ");
            string name = Console.ReadLine().Trim();

            if (db.Studios.Any(s => s.Name.ToLower() == name.ToLower()))
            {
                Console.WriteLine("Помилка: Студія з такою назвою вже існує!");
                return;
            }

            Console.Write("Назва країни: ");
            string countryName = Console.ReadLine().Trim();
            var country = db.Countries.FirstOrDefault(c => c.Name.ToLower() == countryName.ToLower());
            if (country == null)
            {
                country = new Country { Name = countryName };
                db.Countries.Add(country);
                db.SaveChanges();
            }

            Console.Write("Назва міста філії: ");
            string cityName = Console.ReadLine().Trim();
            var city = db.Cities.FirstOrDefault(c => c.Name.ToLower() == cityName.ToLower());
            if (city == null)
            {
                city = new City { Name = cityName, CountryId = country.Id };
                db.Cities.Add(city);
                db.SaveChanges();
            }

            Studio newStudio = new Studio { Name = name, CountryId = country.Id, CityId = city.Id };
            db.Studios.Add(newStudio);
            db.SaveChanges();
            Console.WriteLine("Студію додано успішно!");
        }

        static void EditStudio(GamesDb db)
        {
            Console.Write("Введіть ID студії для редагування: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            var studio = db.Studios.FirstOrDefault(s => s.Id == id);
            if (studio == null)
            {
                Console.WriteLine("Студію не знайдено!");
                return;
            }

            Console.Write($"Нова назва студії ({studio.Name}): ");
            string newName = Console.ReadLine().Trim();
            if (!string.IsNullOrEmpty(newName)) studio.Name = newName;

            Console.Write("Нова назва країни: ");
            string countryName = Console.ReadLine().Trim();
            if (!string.IsNullOrEmpty(countryName))
            {
                var country = db.Countries.FirstOrDefault(c => c.Name.ToLower() == countryName.ToLower());
                if (country == null)
                {
                    country = new Country { Name = countryName };
                    db.Countries.Add(country);
                    db.SaveChanges();
                }
                studio.CountryId = country.Id;
            }

            Console.Write("Нова назва міста: ");
            string cityName = Console.ReadLine().Trim();
            if (!string.IsNullOrEmpty(cityName))
            {
                var city = db.Cities.FirstOrDefault(c => c.Name.ToLower() == cityName.ToLower());
                if (city == null)
                {
                    city = new City { Name = cityName, CountryId = studio.CountryId };
                    db.Cities.Add(city);
                    db.SaveChanges();
                }
                studio.CityId = city.Id;
            }

            db.SaveChanges();
            Console.WriteLine("Дані студії оновлено!");
        }

        static void DeleteStudio(GamesDb db)
        {
            Console.Write("Введіть назву студії для видалення: ");
            string name = Console.ReadLine().Trim();

            var studio = db.Studios.FirstOrDefault(s => s.Name.ToLower() == name.ToLower());
            if (studio == null)
            {
                Console.WriteLine("Студію з такою назвою не знайдено!");
                return;
            }

            Console.Write($"Ви дійсно бажаєте видалити студію '{studio.Name}'? (y/n): ");
            string confirm = Console.ReadLine().Trim().ToLower();
            if (confirm == "y" || confirm == "yes" || confirm == "так")
            {
                db.Studios.Remove(studio);
                db.SaveChanges();
                Console.WriteLine("Студію видалено!");
            }
            else
            {
                Console.WriteLine("Видалення скасовано.");
            }
        }
    }
}
