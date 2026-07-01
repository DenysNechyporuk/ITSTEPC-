namespace EnumsCW
{
    class Train : IEnumerable<Wagon>, IComparable<Train>, ICloneable, IEquatable<Train>
    {
        private Wagon[] wagons;

        public int Number { get; set; }
        public TrainType Type { get; set; }
        public string Route { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }

        public int WagonsCount => wagons.Length;
        public int TotalFreeSeats => wagons.Sum(w => w.FreeSeats);
        public double AveragePassengers => wagons.Length == 0 ? 0 : wagons.Average(w => w.Passengers);
        public TimeSpan TimeToArrival => ArrivalDate - DateTime.Now;

        public Train(int number, TrainType type, string route, DateTime departureDate, DateTime arrivalDate, Wagon[] wagons)
        {
            Number = number;
            Type = type;
            Route = route;
            DepartureDate = departureDate;
            ArrivalDate = arrivalDate;
            this.wagons = wagons;
        }

        public void AddWagon(Wagon wagon)
        {
            Array.Resize(ref wagons, wagons.Length + 1);
            wagons[wagons.Length - 1] = wagon;
        }

        public void AddPassengers(int wagonNumber, int count)
        {
            for (int i = 0; i < wagons.Length; i++)
            {
                if (wagons[i].Number == wagonNumber)
                    wagons[i].AddPassengers(count);
            }
        }

        public void RemovePassengers(int wagonNumber, int count)
        {
            for (int i = 0; i < wagons.Length; i++)
            {
                if (wagons[i].Number == wagonNumber)
                    wagons[i].RemovePassengers(count);
            }
        }

        public IEnumerator<Wagon> GetEnumerator()
        {
            foreach (Wagon wagon in wagons)
                yield return wagon;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int CompareTo(Train? other)
        {
            if (other == null)
                return 1;

            return WagonsCount.CompareTo(other.WagonsCount);
        }

        public object Clone()
        {
            Wagon[] copy = new Wagon[wagons.Length];

            for (int i = 0; i < wagons.Length; i++)
                copy[i] = (Wagon)wagons[i].Clone();

            return new Train(Number, Type, Route, DepartureDate, ArrivalDate, copy);
        }

        public bool Equals(Train? other)
        {
            return other != null && WagonsCount == other.WagonsCount && Type == other.Type;
        }

        public override bool Equals(object? obj)
        {
            return obj is Train train && Equals(train);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(WagonsCount, Type);
        }

        public override string ToString()
        {
            return $"Train number: {Number}, type: {Type}, route: {Route}, wagons: {WagonsCount}, free seats: {TotalFreeSeats}, average passengers: {AveragePassengers:F1}";
        }
    }
}
