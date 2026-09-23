using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreIntroHW
{
    public class AirlineDb : DbContext
    {
        public AirlineDb()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@" 
                            Data Source = (localdb)\MSSQLLocalDB;
                            Initial Catalog = Airline_Db;
                            Integrated Security = True;
                            Connect Timeout = 2;
                            ");
        }

        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }

    [Table("Airplanes")]
    public class Airplane
    {
        public Airplane()
        {
            Flights = new HashSet<Flight>();
        }
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Model { get; set; }
        [Required, MaxLength(50)]
        public string Type { get; set; }
        public int MaxPassengers { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
    }

    [Table("Flights")]
    public class Flight
    {
        public Flight()
        {
            Clients = new HashSet<Client>();
        }
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string FlightNumber { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string DeparturePlace { get; set; }
        public string ArrivalPlace { get; set; }

        public int AirplaneId { get; set; }
        [ForeignKey("AirplaneId")]
        public virtual Airplane Airplane { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }

    [Table("Clients")]
    public class Client
    {
        public Client()
        {
            Flights = new HashSet<Flight>();
        }
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }

        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
    }

    [Table("Accounts")]
    public class Account
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Login { get; set; }
        [Required, MaxLength(50)]
        public string Password { get; set; }

        public virtual Client Client { get; set; }
    }
}
