Calculator calculator = new Calculator();

Console.Write("Enter first number :: ");
double firstNumber = Convert.ToDouble(Console.ReadLine());

Console.Write("Enter second number :: ");
double secondNumber = Convert.ToDouble(Console.ReadLine());

Console.WriteLine("Choose operation");
Console.WriteLine("1 :: Plus");
Console.WriteLine("2 :: Minus");
Console.WriteLine("3 :: Mult");
Console.WriteLine("4 :: Div");
Console.Write("Your choice :: ");

int choice = Convert.ToInt32(Console.ReadLine());
Operation operation;

if (choice == 1)
{
    operation = Operation.Plus;
}
else if (choice == 2)
{
    operation = Operation.Minus;
}
else if (choice == 3)
{
    operation = Operation.Mult;
}
else if (choice == 4)
{
    operation = Operation.Div;
}
else
{
    Console.WriteLine("Wrong operation");
    return;
}

calculator.SetOperation(operation);

try
{
    double result = calculator.Calculate(firstNumber, secondNumber);
    Console.WriteLine($"Result :: {result}");
}
catch (DivideByZeroException exception)
{
    Console.WriteLine(exception.Message);
}

enum Operation
{
    Plus,
    Minus,
    Mult,
    Div
}

class Calculator
{
    private Func<double, double, double> func;

    public Calculator()
    {
        func = (one, two) => one + two;
    }

    public void SetOperation(Operation operation)
    {
        if (operation == Operation.Plus)
        {
            func = (one, two) => one + two;
        }
        else if (operation == Operation.Minus)
        {
            func = (one, two) => one - two;
        }
        else if (operation == Operation.Mult)
        {
            func = (one, two) => one * two;
        }
        else if (operation == Operation.Div)
        {
            func = Divide;
        }
    }

    public double Calculate(double one, double two)
    {
        return func(one, two);
    }

    private double Divide(double one, double two)
    {
        if (two == 0)
        {
            throw new DivideByZeroException("Can not divide by zero");
        }

        return one / two;
    }
}
