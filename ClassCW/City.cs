namespace ClassCW;

public class City
{
    public string Name { get; set; }
    public string Country { get; set; }
    public int Population { get; set; }
    public string PhoneCode { get; set; }
    public string Districts { get; set; }

    public void InputData()
    {
        Console.Write("Enter city name :: ");
        Name = Console.ReadLine();

        Console.Write("Enter country name :: ");
        Country = Console.ReadLine();

        Console.Write("Enter population :: ");
        Population = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter city phone code :: ");
        PhoneCode = Console.ReadLine();

        Console.Write("Enter city districts :: ");
        Districts = Console.ReadLine();
    }

    public void OutputData()
    {
        Console.WriteLine("City name :: " + Name);
        Console.WriteLine("Country name :: " + Country);
        Console.WriteLine("Population :: " + Population);
        Console.WriteLine("City phone code :: " + PhoneCode);
        Console.WriteLine("City districts :: " + Districts);
    }
}
