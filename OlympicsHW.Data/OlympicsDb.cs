using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OlympicsHW.Data
{
    public class OlympicsDb : DbContext
    {
        public OlympicsDb()
        {
        }

        public OlympicsDb(DbContextOptions<OlympicsDb> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"
                                Data Source = (localdb)\MSSQLLocalDB;
                                Initial Catalog = Olympics_Db;
                                Integrated Security = True;
                                Connect Timeout = 5;
                                TrustServerCertificate = True;
                                ");
            }
        }

        public virtual DbSet<OlympicsGame> OlympicsGames { get; set; }
        public virtual DbSet<Sport> Sports { get; set; }
        public virtual DbSet<Athlete> Athletes { get; set; }
        public virtual DbSet<MedalResult> MedalResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MedalResult>()
                .HasOne(m => m.Sport)
                .WithMany(s => s.MedalResults)
                .HasForeignKey(m => m.SportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedalResult>()
                .HasOne(m => m.Athlete)
                .WithMany(a => a.MedalResults)
                .HasForeignKey(m => m.AthleteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedalResult>()
                .HasOne(m => m.OlympicsGame)
                .WithMany(o => o.MedalResults)
                .HasForeignKey(m => m.OlympicsGameId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    [Table("OlympicsGames")]
    public class OlympicsGame
    {
        public OlympicsGame()
        {
            MedalResults = new HashSet<MedalResult>();
        }

        [Key]
        public int Id { get; set; }
        public int Year { get; set; }
        public bool IsSummer { get; set; }

        [Required, MaxLength(50)]
        public string HostCountry { get; set; }

        [Required, MaxLength(50)]
        public string HostCity { get; set; }

        public virtual ICollection<MedalResult> MedalResults { get; set; }
    }

    [Table("Sports")]
    public class Sport
    {
        public Sport()
        {
            Athletes = new HashSet<Athlete>();
            MedalResults = new HashSet<MedalResult>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Athlete> Athletes { get; set; }
        public virtual ICollection<MedalResult> MedalResults { get; set; }
    }

    [Table("Athletes")]
    public class Athlete
    {
        public Athlete()
        {
            MedalResults = new HashSet<MedalResult>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, MaxLength(50)]
        public string Country { get; set; }

        public DateTime BirthDate { get; set; }
        public string PhotoUrl { get; set; }

        public int SportId { get; set; }

        [ForeignKey("SportId")]
        public virtual Sport Sport { get; set; }

        public virtual ICollection<MedalResult> MedalResults { get; set; }
    }

    [Table("MedalResults")]
    public class MedalResult
    {
        [Key]
        public int Id { get; set; }

        public int OlympicsGameId { get; set; }
        [ForeignKey("OlympicsGameId")]
        public virtual OlympicsGame OlympicsGame { get; set; }

        public int AthleteId { get; set; }
        [ForeignKey("AthleteId")]
        public virtual Athlete Athlete { get; set; }

        public int SportId { get; set; }
        [ForeignKey("SportId")]
        public virtual Sport Sport { get; set; }

        [Required, MaxLength(10)]
        public string MedalType { get; set; }
    }
}
