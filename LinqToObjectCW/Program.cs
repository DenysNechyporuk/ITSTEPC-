namespace LinqToObjectCW
{
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Pages { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return $"{Title} | {Author} | {Genre} | {Pages} pages | {Year}";
        }
    }

    class Program
    {
        static void Main()
        {
            Book[] books =
            {
                new Book { Title = "Kobzar", Author = "Taras Shevchenko", Genre = "Poetry", Pages = 320, Year = 1993 },
                new Book { Title = "Ivanhoe", Author = "Walter Scott", Genre = "Historical", Pages = 450, Year = 2002 },
                new Book { Title = "Dracula", Author = "Bram Stoker", Genre = "Horror", Pages = 418, Year = 1997 },
                new Book { Title = "1984", Author = "George Orwell", Genre = "Satire", Pages = 328, Year = 2002 },
                new Book { Title = "Hamlet", Author = "William Shakespeare", Genre = "Tragedy", Pages = 240, Year = 1995 },
                new Book { Title = "Don Juan", Author = "George Byron", Genre = "Poem", Pages = 540, Year = 2001 }
            };

            Console.WriteLine("All pages > 100: " + books.All(b => b.Pages > 100));
            Console.WriteLine("All genre Historical or Satire: " + books.All(b => b.Genre == "Historical" || b.Genre == "Satire"));
            Console.WriteLine("Any Horror: " + books.Any(b => b.Genre == "Horror"));
            Console.WriteLine("Any Shakespeare: " + books.Any(b => b.Author == "William Shakespeare"));
            Console.WriteLine("Contains Byron: " + books.Contains(books[5]));

            Book? first1993 = books.FirstOrDefault(b => b.Year == 1993);
            Console.WriteLine("First 1993: " + (first1993 != null ? first1993.ToString() : "Not found"));

            Book? last2002 = books.LastOrDefault(b => b.Year == 2002);
            Console.WriteLine("Last 2002: " + (last2002 != null ? last2002.ToString() : "Not found"));
        }
    }
}