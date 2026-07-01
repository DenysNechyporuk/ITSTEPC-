namespace EnumsCW
{
    struct Wagon : IComparable<Wagon>, ICloneable, IEquatable<Wagon>
    {
        public int Number { get; set; }
        public WagonType Type { get; set; }
        public int Seats { get; set; }
        public int Passengers { get; set; }

        public int FreeSeats => Seats - Passengers;

        public Wagon(int number, WagonType type, int seats, int passengers)
        {
            Number = number;
            Type = type;
            Seats = seats;
            Passengers = passengers;
        }

        public void AddPassengers(int count)
        {
            if (Passengers + count <= Seats)
                Passengers += count;
        }

        public void RemovePassengers(int count)
        {
            if (Passengers - count >= 0)
                Passengers -= count;
        }

        public int CompareTo(Wagon other)
        {
            return FreeSeats.CompareTo(other.FreeSeats);
        }

        public object Clone()
        {
            return new Wagon(Number, Type, Seats, Passengers);
        }

        public bool Equals(Wagon other)
        {
            return Number == other.Number && Type == other.Type;
        }

        public override bool Equals(object? obj)
        {
            return obj is Wagon wagon && Equals(wagon);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Number, Type);
        }

        public override string ToString()
        {
            return $"Wagon number: {Number}, type: {Type}, seats: {Seats}, passengers: {Passengers}, free seats: {FreeSeats}";
        }
    }
}
