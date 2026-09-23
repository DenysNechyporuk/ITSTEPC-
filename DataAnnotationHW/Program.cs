using System;

namespace DataAnnotationHW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ShopDbContext db = new ShopDbContext())
            {
                db.Database.EnsureCreated();
                Console.WriteLine("Базу даних магазину створено!");
            }
        }
    }
}
