using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace OlympicsHW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (OlympicsDb db = new OlympicsDb())
            {
                db.Database.EnsureCreated();
                Seed(db);

                while (true)
                {
                    Console.WriteLine("\n===== ДОДАТОК «ОЛІМПІАДА» =====");
                    Console.WriteLine("1. Таблиця медального заліку за країнами");
                    Console.WriteLine("2. Медалісти з видів спорту");
                    Console.WriteLine("3. Країна з найбільшою кількістю золотих медалей");
                    Console.WriteLine("4. Країна з найбільшою кількістю медалей у конкретному виді спорту");
                    Console.WriteLine("5. Спортсмен з найбільшою кількістю золотих медалей");
                    Console.WriteLine("6. Країна, яка найчастіше приймала Олімпіаду");
                    Console.WriteLine("7. Склад олімпіадної команди країни");
                    Console.WriteLine("8. Статистика виступу країни");
                    Console.WriteLine("9. Додати нового спортсмена");
                    Console.WriteLine("0. Вихід");
                    Console.Write("Оберіть пункт: ");

                    string choice = Console.ReadLine();
                    if (choice == "0") break;

                    switch (choice)
                    {
                        case "1": ShowMedalTable(db); break;
                        case "2": ShowMedalistsBySport(db); break;
                        case "3": ShowTopGoldCountry(db); break;
                        case "4": ShowTopCountryInSport(db); break;
                        case "5": ShowTopGoldAthleteInSport(db); break;
                        case "6": ShowMostFrequentHost(db); break;
                        case "7": ShowCountryTeam(db); break;
                        case "8": ShowCountryStats(db); break;
                        case "9": AddAthlete(db); break;
                    }
                }
            }
        }

        static void Seed(OlympicsDb db)
        {
            if (!db.OlympicsGames.Any())
            {
                var o1 = new OlympicsGame { Year = 2024, IsSummer = true, HostCountry = "Франція", HostCity = "Париж" };
                var o2 = new OlympicsGame { Year = 2022, IsSummer = false, HostCountry = "Китай", HostCity = "Пекін" };
                db.OlympicsGames.AddRange(o1, o2);
                db.SaveChanges();

                var s1 = new Sport { Name = "Плавання" };
                var s2 = new Sport { Name = "Легка атлетика" };
                db.Sports.AddRange(s1, s2);
                db.SaveChanges();

                var a1 = new Athlete { FullName = "Олександр Желтяков", Country = "Україна", BirthDate = new DateTime(2005, 11, 15), PhotoUrl = "photo1.jpg", SportId = s1.Id };
                var a2 = new Athlete { FullName = "Ярослава Магучіх", Country = "Україна", BirthDate = new DateTime(2001, 9, 19), PhotoUrl = "photo2.jpg", SportId = s2.Id };
                var a3 = new Athlete { FullName = "Кетлеб Дрессел", Country = "США", BirthDate = new DateTime(1996, 8, 16), PhotoUrl = "photo3.jpg", SportId = s1.Id };
                db.Athletes.AddRange(a1, a2, a3);
                db.SaveChanges();

                db.MedalResults.Add(new MedalResult { OlympicsGameId = o1.Id, AthleteId = a2.Id, SportId = s2.Id, MedalType = "Gold" });
                db.MedalResults.Add(new MedalResult { OlympicsGameId = o1.Id, AthleteId = a3.Id, SportId = s1.Id, MedalType = "Gold" });
                db.MedalResults.Add(new MedalResult { OlympicsGameId = o1.Id, AthleteId = a1.Id, SportId = s1.Id, MedalType = "Bronze" });
                db.SaveChanges();
            }
        }

        static void ShowMedalTable(OlympicsDb db)
        {
            Console.Write("Введіть рік Олімпіади (або 0 для всієї історії): ");
            int year = int.Parse(Console.ReadLine());

            var query = db.MedalResults.AsQueryable();
            if (year != 0) query = query.Where(m => m.OlympicsGame.Year == year);

            var stats = query.ToList()
                .GroupBy(m => m.Athlete.Country)
                .Select(g => new
                {
                    Country = g.Key,
                    Gold = g.Count(x => x.MedalType == "Gold"),
                    Silver = g.Count(x => x.MedalType == "Silver"),
                    Bronze = g.Count(x => x.MedalType == "Bronze"),
                    Total = g.Count()
                })
                .OrderByDescending(x => x.Gold).ThenByDescending(x => x.Total);

            Console.WriteLine($"\n--- МЕДАЛЬНИЙ ЗАЛІК ({(year == 0 ? "Вся історія" : year.ToString())}) ---");
            foreach (var item in stats)
            {
                Console.WriteLine($"{item.Country,-15} | Золото: {item.Gold} | Срібло: {item.Silver} | Бронза: {item.Bronze} | Всього: {item.Total}");
            }
        }

        static void ShowMedalistsBySport(OlympicsDb db)
        {
            Console.Write("Назва виду спорту: ");
            string sport = Console.ReadLine().Trim();

            var medalists = db.MedalResults
                .Include(m => m.Athlete)
                .Include(m => m.OlympicsGame)
                .Where(m => m.Sport.Name.ToLower() == sport.ToLower())
                .ToList();

            Console.WriteLine($"\n--- МЕДАЛІСТИ У ВИДІ СПОРТУ: {sport} ---");
            foreach (var m in medalists)
            {
                Console.WriteLine($"{m.Athlete.FullName} ({m.Athlete.Country}) - {m.MedalType} | Олімпіада {m.OlympicsGame.Year} ({m.OlympicsGame.HostCity})");
            }
        }

        static void ShowTopGoldCountry(OlympicsDb db)
        {
            var topCountry = db.MedalResults
                .Where(m => m.MedalType == "Gold")
                .ToList()
                .GroupBy(m => m.Athlete.Country)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            if (topCountry != null)
            {
                Console.WriteLine($"\nКраїна з найбільшою кількістю золотих медалей: {topCountry.Key} ({topCountry.Count()} золота)");
            }
        }

        static void ShowTopCountryInSport(OlympicsDb db)
        {
            Console.Write("Назва виду спорту: ");
            string sport = Console.ReadLine().Trim();

            var topCountry = db.MedalResults
                .Where(m => m.Sport.Name.ToLower() == sport.ToLower())
                .ToList()
                .GroupBy(m => m.Athlete.Country)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            if (topCountry != null)
            {
                Console.WriteLine($"Країна з найбільшою кількістю медалей у {sport}: {topCountry.Key} ({topCountry.Count()} медалей)");
            }
        }

        static void ShowTopGoldAthleteInSport(OlympicsDb db)
        {
            Console.Write("Назва виду спорту: ");
            string sport = Console.ReadLine().Trim();

            var topAthlete = db.MedalResults
                .Where(m => m.Sport.Name.ToLower() == sport.ToLower() && m.MedalType == "Gold")
                .ToList()
                .GroupBy(m => m.Athlete)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            if (topAthlete != null)
            {
                Console.WriteLine($"Спортсмен з найбільшою кількістю золотих медалей у {sport}: {topAthlete.Key.FullName} ({topAthlete.Key.Country}) - {topAthlete.Count()} золота");
            }
        }

        static void ShowMostFrequentHost(OlympicsDb db)
        {
            var host = db.OlympicsGames
                .ToList()
                .GroupBy(g => g.HostCountry)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            if (host != null)
            {
                Console.WriteLine($"Країна, яка найчастіше приймала Олімпіаду: {host.Key} ({host.Count()} раз(ів))");
            }
        }

        static void ShowCountryTeam(OlympicsDb db)
        {
            Console.Write("Назва країни: ");
            string country = Console.ReadLine().Trim();

            var athletes = db.Athletes.Include(a => a.Sport).Where(a => a.Country.ToLower() == country.ToLower()).ToList();

            Console.WriteLine($"\n--- СКЛАД КОМАНДИ КРАЇНИ {country} ---");
            foreach (var a in athletes)
            {
                Console.WriteLine($"{a.FullName}, Вид спорту: {a.Sport.Name}, Дата нар.: {a.BirthDate.ToShortDateString()}");
            }
        }

        static void ShowCountryStats(OlympicsDb db)
        {
            Console.Write("Назва країни: ");
            string country = Console.ReadLine().Trim();

            var medals = db.MedalResults
                .Include(m => m.OlympicsGame)
                .Where(m => m.Athlete.Country.ToLower() == country.ToLower())
                .ToList();

            Console.WriteLine($"\n--- СТАТИСТИКА ВИСТУПУ {country} ---");
            Console.WriteLine($"Всього здобуто медалей за всю історію: {medals.Count}");
            Console.WriteLine($"Золото: {medals.Count(m => m.MedalType == "Gold")}, Срібло: {medals.Count(m => m.MedalType == "Silver")}, Бронза: {medals.Count(m => m.MedalType == "Bronze")}");
        }

        static void AddAthlete(OlympicsDb db)
        {
            Console.Write("ПІБ спортсмена: ");
            string name = Console.ReadLine().Trim();
            Console.Write("Країна: ");
            string country = Console.ReadLine().Trim();

            var sport = db.Sports.FirstOrDefault();
            if (sport == null)
            {
                sport = new Sport { Name = "Загальний спорт" };
                db.Sports.Add(sport);
                db.SaveChanges();
            }

            Athlete athlete = new Athlete
            {
                FullName = name,
                Country = country,
                BirthDate = DateTime.Now.AddYears(-20),
                PhotoUrl = "photo.jpg",
                SportId = sport.Id
            };

            db.Athletes.Add(athlete);
            db.SaveChanges();
            Console.WriteLine("Спортсмена додано!");
        }
    }
}
