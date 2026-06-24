using System;

class Program
{
    class MyException : Exception
    {
        public MyException(string message) : base(message) { }
    }

    class Worker
    {
        private string name = "";
        private int age;
        private double salary;
        private DateTime date;

        public string Name
        {
            get { return name; }
            set
            {
                if (value == "")
                    throw new MyException("Invalid surname");

                name = value;
            }
        }

        public int Age
        {
            get { return age; }
            set
            {
                if (value < 18 || value > 100)
                    throw new MyException("Invalid age");

                age = value;
            }
        }

        public double Salary
        {
            get { return salary; }
            set
            {
                if (value <= 0)
                    throw new MyException("Invalid salary");

                salary = value;
            }
        }

        public DateTime Date
        {
            get { return date; }
            set
            {
                if (value > DateTime.Now)
                    throw new MyException("Invalid date");

                date = value;
            }
        }

        public int GetExperience()
        {
            return DateTime.Now.Year - date.Year;
        }
    }

    static void Main()
    {
        Worker[] workers = new Worker[5];

        for (int i = 0; i < workers.Length; i++)
        {
            try
            {
                workers[i] = new Worker();

                Console.WriteLine("Worker " + (i + 1));

                Console.Write("Name: ");
                workers[i].Name = Console.ReadLine()!;

                Console.Write("Age: ");
                workers[i].Age = int.Parse(Console.ReadLine()!);

                Console.Write("Salary: ");
                workers[i].Salary = double.Parse(Console.ReadLine()!);

                Console.Write("Date mm.dd.yyyy: ");
                workers[i].Date = DateTime.Parse(Console.ReadLine()!);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                i--;
            }
        }

        for (int i = 0; i < workers.Length - 1; i++)
        {
            for (int j = i + 1; j < workers.Length; j++)
            {
                if (workers[i].Name.CompareTo(workers[j].Name) > 0)
                {
                    Worker temp = workers[i];
                    workers[i] = workers[j];
                    workers[j] = temp;
                }
            }
        }

        Console.Write("Enter experience: ");
        int exp = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Result:");

        for (int i = 0; i < workers.Length; i++)
        {
            if (workers[i].GetExperience() > exp)
            {
                Console.WriteLine(workers[i].Name);
            }
        }
    }
}
