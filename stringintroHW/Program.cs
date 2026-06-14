using System;

class Program
{
    static void Task1()
    {
        Console.Write("Enter position :: ");
        string position = Console.ReadLine();

        Console.Write("Enter hours worked :: ");
        double hours = double.Parse(Console.ReadLine());

        double rate;
        switch (position)
        {
            case "Senior":
                rate = 500;
                break;
            case "Middle":
                rate = 400;
                break;
            case "Junior":
                rate = 250;
                break;
            default:
                rate = 200;
                break;
        }

        double salary = rate * hours;
        Console.WriteLine("Salary :: " + salary);
    }

    static void Task2()
    {
        Console.Write("Enter A :: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Enter B :: ");
        int b = int.Parse(Console.ReadLine());

        for (int i = a; i <= b; i++)
        {
            for (int j = 0; j < i; j++)
                Console.Write(i + " ");
            Console.WriteLine();
        }
    }

    static void Task3()
    {
        Console.Write("Enter N :: ");
        int n = int.Parse(Console.ReadLine());

        int reversed = 0;
        while (n > 0)
        {
            int digit = n % 10;
            reversed = reversed * 10 + digit;
            n = n / 10;
        }

        Console.WriteLine("Reversed :: " + reversed);
    }

    static void Task4()
    {
        Console.WriteLine("Enter characters, end with '.' :: ");

        int spaces = 0;
        int digits = 0;
        int letters = 0;
        int others = 0;

        int code;
        while ((code = Console.Read()) != -1)
        {
            char c = (char)code;

            if (c == '.')
                break;

            if (c == ' ')
                spaces++;
            else if (char.IsDigit(c))
                digits++;
            else if (char.IsLetter(c))
                letters++;
            else if (c != '\r' && c != '\n')
                others++;
        }

        Console.WriteLine("Spaces :: " + spaces);
        Console.WriteLine("Digits :: " + digits);
        Console.WriteLine("Letters :: " + letters);
        Console.WriteLine("Other characters :: " + others);
    }

    static void Task5()
    {
        Console.WriteLine("Enter characters :: ");

        int code;
        while ((code = Console.Read()) != -1)
        {
            char c = (char)code;

            if (c == '.')
                break;

            if (char.IsUpper(c))
                Console.Write(char.ToLower(c));
            else if (char.IsLower(c))
                Console.Write(char.ToUpper(c));
            else
                Console.Write(c);
        }
        Console.WriteLine();
    }

    static void Main()
    {
        Task1();
        Console.WriteLine();

        Task2();
        Console.WriteLine();

        Task3();
        Console.WriteLine();

        Task4();
        Console.WriteLine();

        Task5();
    }
}