using System;

namespace InterfacesHW2
{
    interface ILocated
    {
        int X { get; set; }
        int Y { get; set; }
    }

    interface IColored
    {
        ConsoleColor Color { get; set; }
    }

    interface IPrintable
    {
        char PrintChar { get; set; }
        void Print();
    }

    interface IMovable
    {
        void Move(int dx, int dy);
    }

    abstract class Shape : ILocated, IColored, IPrintable, IMovable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }
        public char PrintChar { get; set; }

        public abstract double Area();
        public abstract double Perimeter();
        public abstract void Print();

        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }
    }

    class Rectangle : Shape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle(int x, int y, int width, int height, ConsoleColor color, char ch)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Color = color;
            PrintChar = ch;
        }

        public override double Area()
        {
            return Width * Height;
        }

        public override double Perimeter()
        {
            return 2 * (Width + Height);
        }

        public override void Print()
        {
            Console.ForegroundColor = Color;
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    Console.Write(PrintChar);
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }

    class Triangle : Shape
    {
        public int Size { get; set; }

        public Triangle(int x, int y, int size, ConsoleColor color, char ch)
        {
            X = x;
            Y = y;
            Size = size;
            Color = color;
            PrintChar = ch;
        }

        public override double Area()
        {
            return 0.5 * Size * Size;
        }

        public override double Perimeter()
        {
            return Size * 2 + Size * Math.Sqrt(2);
        }

        public override void Print()
        {
            Console.ForegroundColor = Color;
            for (int row = 1; row <= Size; row++)
            {
                for (int col = 0; col < row; col++)
                {
                    Console.Write(PrintChar);
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }

    class Square : Shape
    {
        public int Side { get; set; }

        public Square(int x, int y, int side, ConsoleColor color, char ch)
        {
            X = x;
            Y = y;
            Side = side;
            Color = color;
            PrintChar = ch;
        }

        public override double Area()
        {
            return Side * Side;
        }

        public override double Perimeter()
        {
            return 4 * Side;
        }

        public override void Print()
        {
            Console.ForegroundColor = Color;
            for (int row = 0; row < Side; row++)
            {
                for (int col = 0; col < Side; col++)
                {
                    Console.Write(PrintChar);
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }

    class Program
    {
        static void Main()
        {
            Rectangle rect = new Rectangle(0, 0, 10, 4, ConsoleColor.Green, '#');
            Triangle tri = new Triangle(0, 0, 6, ConsoleColor.Cyan, '*');
            Square sq = new Square(0, 0, 5, ConsoleColor.Yellow, '@');

            Console.WriteLine("Rectangle:");
            rect.Print();
            Console.WriteLine("Area: " + rect.Area() + " Perimeter: " + rect.Perimeter());

            Console.WriteLine("\nTriangle:");
            tri.Print();
            Console.WriteLine("Area: " + tri.Area() + " Perimeter: " + tri.Perimeter());

            Console.WriteLine("\nSquare:");
            sq.Print();
            Console.WriteLine("Area: " + sq.Area() + " Perimeter: " + sq.Perimeter());

            Console.WriteLine("\nMove rectangle by (5, 3):");
            rect.Move(5, 3);
            Console.WriteLine("New position: (" + rect.X + ", " + rect.Y + ")");
        }
    }
}