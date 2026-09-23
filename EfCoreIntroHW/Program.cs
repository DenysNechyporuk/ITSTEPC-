using System;

namespace EfCoreIntroHW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (AirlineDbContext db = new AirlineDbContext())
            {
                db.Database.EnsureCreated();
                Console.WriteLine("Базу даних авіакомпанії створено успішно!");
            }
        }
    }
}
