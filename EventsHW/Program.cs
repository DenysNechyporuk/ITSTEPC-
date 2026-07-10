namespace EventsHW
{
    delegate void HotHouseDeleg(HotHouse house);

    class HotHouse
    {
        private int temperature;
        private int minTemp;
        private int maxTemp;

        public event HotHouseDeleg TooHot;
        public event HotHouseDeleg TooCold;
        public event HotHouseDeleg Well;

        public int Temperature
        {
            get => temperature;
            set
            {
                temperature = value;
                if (temperature > maxTemp)
                    TooHot?.Invoke(this);
                else if (temperature < minTemp)
                    TooCold?.Invoke(this);
                else
                    Well?.Invoke(this);
            }
        }

        public HotHouse(int temp, int min, int max)
        {
            temperature = temp;
            minTemp = min;
            maxTemp = max;
        }
    }

    class Heater
    {
        public void Warm(HotHouse h)
        {
            Console.WriteLine("Heater: warming up by 5");
            h.Temperature += 5;
        }
    }

    class Cooler
    {
        public void Cool(HotHouse h)
        {
            Console.WriteLine("Cooler: cooling down by 5");
            h.Temperature -= 5;
        }
    }

    class Program
    {
        static void Main()
        {
            HotHouse house = new HotHouse(20, 15, 25);
            Heater heater = new Heater();
            Cooler cooler = new Cooler();

            house.TooHot += (h) =>
            {
                Console.WriteLine("Too hot! Temp: " + h.Temperature);
                cooler.Cool(h);
            };

            house.TooCold += (h) =>
            {
                Console.WriteLine("Too cold! Temp: " + h.Temperature);
                heater.Warm(h);
            };

            house.Well += (h) =>
            {
                Console.WriteLine("All good. Temp: " + h.Temperature);
            };

            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                int change = rnd.Next(-2, 3);
                Console.WriteLine("\nWeather change: " + change);
                house.Temperature += change;
                Console.ReadKey();
            }
        }
    }
}