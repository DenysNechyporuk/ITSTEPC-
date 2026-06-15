using System;

namespace ClassCW
{
    class Program
    {
        static void Main(string[] args)
        {
            City city = new City();
            city.InputData();
            Console.WriteLine();
            city.OutputData();

            Console.WriteLine();

            Employee employee = new Employee();
            employee.InputData();
            Console.WriteLine();
            employee.OutputData();
        }
    }
}