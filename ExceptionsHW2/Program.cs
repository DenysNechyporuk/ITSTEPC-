using System.Text.RegularExpressions;

namespace ExceptionsHW2
{
    class AccountException : Exception
    {
        public AccountException(string message) : base(message) { }
    }

    class Account
    {
        private string email;
        private string password;

        public string Email
        {
            get => email;
            set
            {
                if (value.Length < 4 || value.Length > 50)
                    throw new AccountException("Email: length must be between 4 and 50 characters");
                if (!Regex.IsMatch(value, @"^[a-zA-Z0-9_.@]+$"))
                    throw new AccountException("Email: only letters, digits and '_' are allowed");
                int atIndex = value.IndexOf('@');
                if (atIndex <= 0 || atIndex >= value.Length - 1)
                    throw new AccountException("Email: '@' must be in the middle");
                email = value;
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (value.Length < 6)
                    throw new AccountException("Password: minimum 6 characters required");
                if (!Regex.IsMatch(value, @"[0-9]"))
                    throw new AccountException("Password: at least one digit required");
                if (!Regex.IsMatch(value, @"[a-zA-Z]"))
                    throw new AccountException("Password: at least one letter required");
                password = value;
            }
        }

        public Account(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public override string ToString() => $"Email: {email}, Password: {password}";
    }

    class Program
    {
        static void Main()
        {
            try
            {
                Account a1 = new Account("user@mail.com", "pass123");
                Console.WriteLine(a1);

                Account a2 = new Account("badmail", "pass123");
                Console.WriteLine(a2);
            }
            catch (AccountException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            try
            {
                Account a3 = new Account("user@mail.com", "123456");
                Console.WriteLine(a3);
            }
            catch (AccountException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}