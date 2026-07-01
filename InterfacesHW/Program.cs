namespace InterfacesHW
{
    class Program
    {
        static void Main()
        {
            Array array = new Array(new int[] { 8, 3, 15, 6, 10 });

            Console.WriteLine("Task 1");
            array.Show();
            array.Show("Array output with message");

            Console.WriteLine();
            Console.WriteLine("Task 2");
            Console.WriteLine("Max: " + array.Max());
            Console.WriteLine("Min: " + array.Min());
            Console.WriteLine("Avg: " + array.Avg());
            Console.WriteLine("Search 15: " + array.Search(15));
            Console.WriteLine("Search 7: " + array.Search(7));
        }
    }
}
