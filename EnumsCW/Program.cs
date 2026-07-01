namespace EnumsCW
{
    class Program
    {
        static void Main()
        {
            Wagon[] wagons =
            {
                new Wagon(3, WagonType.Coupe, 36, 25),
                new Wagon(1, WagonType.Lux, 18, 12),
                new Wagon(2, WagonType.Sitting, 60, 45)
            };

            Train train1 = new Train(105, TrainType.Passenger, "Kyiv - Lviv", DateTime.Now, DateTime.Now.AddHours(5), wagons);
            Train train2 = new Train(210, TrainType.Express, "Odesa - Kharkiv", DateTime.Now, DateTime.Now.AddHours(7), new Wagon[]
            {
                new Wagon(1, WagonType.Coupe, 36, 30),
                new Wagon(2, WagonType.Coupe, 36, 28)
            });

            Console.WriteLine(train1);
            Console.WriteLine("Time to arrival: " + train1.TimeToArrival);

            Console.WriteLine("\nAll wagons:");
            foreach (Wagon wagon in train1)
                Console.WriteLine(wagon);

            train1.AddPassengers(1, 3);
            train1.RemovePassengers(2, 5);
            train1.AddWagon(new Wagon(4, WagonType.Platzkart, 54, 40));

            Console.WriteLine("\nAfter changes:");
            Console.WriteLine(train1);

            Wagon[] sortedWagons = wagons.ToArray();
            Array.Sort(sortedWagons);

            Console.WriteLine("\nSorted wagons by free seats:");
            foreach (Wagon wagon in sortedWagons)
                Console.WriteLine(wagon);

            Array.Sort(sortedWagons, new WagonNumberComparer());

            Console.WriteLine("\nSorted wagons by number:");
            foreach (Wagon wagon in sortedWagons)
                Console.WriteLine(wagon);

            Train clone = (Train)train1.Clone();

            Console.WriteLine("\nTrain compare: " + train1.CompareTo(train2));
            Console.WriteLine("Train equals: " + train1.Equals(train2));
            Console.WriteLine("Clone equals: " + train1.Equals(clone));
            Console.WriteLine("Wagon equals: " + wagons[0].Equals(new Wagon(3, WagonType.Coupe, 50, 10)));
        }
    }
}
