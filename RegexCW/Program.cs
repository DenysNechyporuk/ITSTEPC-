using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Enter email :: ");
        string email = Console.ReadLine();

        Console.Write("Enter password :: ");
        string password = Console.ReadLine();

        var emailRegex = new Regex(@"^[a-zA-Z0-9._-]{4,}@[a-zA-Z0-9]{2,}\.[a-zA-Z0-9]{2,}$");
        var passwordRegex = new Regex(@"^(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[_-])[a-zA-Z0-9_-]{6,}$");

        Console.WriteLine(
            emailRegex.IsMatch(email)
                ? $"Email '{email}' is valid"
                : $"Email '{email}' is NOT valid"
        );

        Console.WriteLine(
            passwordRegex.IsMatch(password)
                ? $"Password '{password}' is valid"
                : $"Password '{password}' is NOT valid"
        );
    }
}