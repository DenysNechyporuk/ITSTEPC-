using System;

namespace EfCoreIntroHW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (AirlineDb context = new AirlineDb())
            {
                context.Database.EnsureCreated();
                Console.WriteLine("Базу даних авіакомпанії створено!");
            }
        }
    }
}
