using System;
using System.Collections.Generic;
using System.Threading;

RaceGame game = new RaceGame();

game.AddCar(new SportCar("Sport car", 15, 25));
game.AddCar(new PassengerCar("Passenger car", 10, 20));
game.AddCar(new Truck("Truck", 7, 15));
game.AddCar(new Bus("Bus", 8, 16));

game.Start();

delegate void RaceDelegate();

abstract class Car
{
    private static Random random = new Random();

    public string Name { get; set; }
    public int Speed { get; set; }
    public int Position { get; set; }
    public int MinSpeed { get; set; }
    public int MaxSpeed { get; set; }

    public event RaceDelegate? Finished;

    public Car(string name, int minSpeed, int maxSpeed)
    {
        Name = name;
        MinSpeed = minSpeed;
        MaxSpeed = maxSpeed;
    }

    public virtual void Go()
    {
        Speed = random.Next(MinSpeed, MaxSpeed + 1);
        Position += Speed;

        if (Position >= 100)
        {
            Position = 100;
            Finished?.Invoke();
        }
    }
}

class SportCar : Car
{
    public SportCar(string name, int minSpeed, int maxSpeed) : base(name, minSpeed, maxSpeed)
    {
    }
}

class PassengerCar : Car
{
    public PassengerCar(string name, int minSpeed, int maxSpeed) : base(name, minSpeed, maxSpeed)
    {
    }
}

class Truck : Car
{
    public Truck(string name, int minSpeed, int maxSpeed) : base(name, minSpeed, maxSpeed)
    {
    }
}

class Bus : Car
{
    public Bus(string name, int minSpeed, int maxSpeed) : base(name, minSpeed, maxSpeed)
    {
    }
}

class RaceGame
{
    private List<Car> cars = new List<Car>();
    private bool isFinished = false;
    private string winner = "";

    public void AddCar(Car car)
    {
        car.Finished += () => Finish(car);
        cars.Add(car);
    }

    public void Start()
    {
        RaceDelegate startRace = ShowStart;
        RaceDelegate moveCars = MoveCars;

        startRace();

        while (!isFinished)
        {
            moveCars();
            Console.WriteLine();
            Thread.Sleep(500);
        }

        Console.WriteLine($"Race finished. Winner :: {winner}");
    }

    private void ShowStart()
    {
        Console.WriteLine("Cars on start");

        foreach (Car car in cars)
        {
            Console.WriteLine($"{car.Name} :: position {car.Position}");
        }

        Console.WriteLine();
    }

    private void MoveCars()
    {
        foreach (Car car in cars)
        {
            if (!isFinished)
            {
                car.Go();
                Console.WriteLine($"{car.Name} :: speed {car.Speed}, position {car.Position}");
            }
        }
    }

    private void Finish(Car car)
    {
        if (!isFinished)
        {
            isFinished = true;
            winner = car.Name;
        }
    }
}
