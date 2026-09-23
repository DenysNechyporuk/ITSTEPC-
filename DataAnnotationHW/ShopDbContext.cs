using Microsoft.EntityFrameworkCore;

namespace DataAnnotationHW
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"
                            Data Source = (localdb)\MSSQLLocalDB;
                            Initial Catalog = Shop_Db;
                            Integrated Security = True;
                            Connect Timeout = 2;
                            ");
        }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
