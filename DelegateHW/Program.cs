namespace DelegateHW
{
    delegate void DrawDelegate(uint height, ConsoleColor color, char ch);

    class Program
    {
        static void DrawSquare(uint height, ConsoleColor color, char ch)
        {
            Console.ForegroundColor = color;
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < height; col++)
                {
                    Console.Write(ch);
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        static void DrawTriangle(uint height, ConsoleColor color, char ch)
        {
            Console.ForegroundColor = color;
            for (int row = 1; row <= height; row++)
            {
                for (int col = 0; col < row; col++)
                {
                    Console.Write(ch);
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        static void Main()
        {
            DrawDelegate deleg = DrawSquare;
            Console.WriteLine("Square:");
            deleg(5, ConsoleColor.Green, '#');

            Console.WriteLine("\nTriangle:");
            deleg = DrawTriangle;
            deleg(5, ConsoleColor.Cyan, '*');

            Console.WriteLine("\nBoth:");
            deleg = DrawSquare;
            deleg += DrawTriangle;
            deleg(4, ConsoleColor.Yellow, '@');
        }
    }
}