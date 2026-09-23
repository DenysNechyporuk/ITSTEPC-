using System;

namespace DataAnnotationHW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ShopDb db = new ShopDb())
            {
                db.Database.EnsureCreated();
                Console.WriteLine("Базу даних магазину створено!");
            }
        }
    }
}
