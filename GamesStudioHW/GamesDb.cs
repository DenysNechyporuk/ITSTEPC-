using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesStudioHW
{
    public class GamesDb : DbContext
    {
        public GamesDb()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"
                            Data Source = (localdb)\MSSQLLocalDB;
                            Initial Catalog = Games_Db;
                            Integrated Security = True;
                            Connect Timeout = 5;
                            TrustServerCertificate = True;
                            ");
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Studio> Studios { get; set; }
        public virtual DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Studio>()
                .HasOne(s => s.City)
                .WithMany(c => c.BranchStudios)
                .HasForeignKey(s => s.CityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    [Table("Countries")]
    public class Country
    {
        public Country()
        {
            Studios = new HashSet<Studio>();
            Cities = new HashSet<City>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Studio> Studios { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }

    [Table("Cities")]
    public class City
    {
        public City()
        {
            BranchStudios = new HashSet<Studio>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
        public virtual ICollection<Studio> BranchStudios { get; set; }
    }

    [Table("Studios")]
    public class Studio
    {
        public Studio()
        {
            Games = new HashSet<Game>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }

    [Table("Games")]
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Title { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }

        public int StudioId { get; set; }
        [ForeignKey("StudioId")]
        public virtual Studio Studio { get; set; }
    }
}
