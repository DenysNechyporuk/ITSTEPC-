using Microsoft.EntityFrameworkCore;

namespace EfCoreIntroHW
{
    public class AirlineDbContext : DbContext
    {
        public AirlineDbContext()
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
}
