namespace EnumsCW
{
    class WagonNumberComparer : IComparer<Wagon>
    {
        public int Compare(Wagon x, Wagon y)
        {
            return x.Number.CompareTo(y.Number);
        }
    }
}
