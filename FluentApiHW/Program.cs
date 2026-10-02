using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FluentApiHW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (GymDb db = new GymDb())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Console.WriteLine("--- СПИСОК СПОРТЗАЛІВ ТА ТРЕНУВАНЬ (Fluent API) ---");
                var gyms = db.Gyms.Include(g => g.Members).ThenInclude(m => m.Workouts).ThenInclude(w => w.Trainer).ToList();

                foreach (var g in gyms)
                {
                    Console.WriteLine($"Спортзал: {g.Name} ({g.Address})");
                    foreach (var m in g.Members)
                    {
                        Console.WriteLine($"\tУчасник: {m.Name}, Тел: {m.Phone}");
                        foreach (var w in m.Workouts)
                        {
                            Console.WriteLine($"\t\tТренування: {w.Title}, Тренер: {w.Trainer.Name}, Дата: {w.Date.ToShortDateString()}");
                        }
                    }
                }
            }
        }
    }
}
