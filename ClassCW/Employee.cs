using System;

namespace ClassCW;


public class Employee
{
    public string FullName { get; set; }
    public string BirthDate { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Position { get; set; }
    public string Duties { get; set; }

    public void InputData()
    {
        Console.Write("Enter full name :: ");
        FullName = Console.ReadLine();

        Console.Write("Enter birth date :: ");
        BirthDate = Console.ReadLine();

        Console.Write("Enter phone :: ");
        Phone = Console.ReadLine();

        Console.Write("Enter work email :: ");
        Email = Console.ReadLine();

        Console.Write("Enter position :: ");
        Position = Console.ReadLine();

        Console.Write("Enter duties :: ");
        Duties = Console.ReadLine();
    }

    public void OutputData()
    {
        Console.WriteLine("Full name :: " + FullName);
        Console.WriteLine("Birth date :: " + BirthDate);
        Console.WriteLine("Phone :: " + Phone);
        Console.WriteLine("Work email :: " + Email);
        Console.WriteLine("Position :: " + Position);
        Console.WriteLine("Duties :: " + Duties);
    }
}
