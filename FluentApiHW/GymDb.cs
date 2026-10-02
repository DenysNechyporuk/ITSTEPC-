using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace FluentApiHW
{
    public class GymDb : DbContext
    {
        public GymDb()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"
                            Data Source = (localdb)\MSSQLLocalDB;
                            Initial Catalog = Gym_Db;
                            Integrated Security = True;
                            Connect Timeout = 2;
                            ");
        }

        public virtual DbSet<Gym> Gyms { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Trainer> Trainers { get; set; }
        public virtual DbSet<Workout> Workouts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Gym>(entity =>
            {
                entity.ToTable("Gyms");
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Name).IsRequired().HasMaxLength(100);
                entity.Property(g => g.Address).IsRequired().HasMaxLength(150);

                entity.HasMany(g => g.Members)
                      .WithOne(m => m.Gym)
                      .HasForeignKey(m => m.GymId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Members");
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Name).IsRequired().HasMaxLength(50);
                entity.Property(m => m.Phone).HasMaxLength(20);
            });

            modelBuilder.Entity<Trainer>(entity =>
            {
                entity.ToTable("Trainers");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name).IsRequired().HasMaxLength(50);
                entity.Property(t => t.Specialization).HasMaxLength(50);
            });

            modelBuilder.Entity<Workout>(entity =>
            {
                entity.ToTable("Workouts");
                entity.HasKey(w => w.Id);
                entity.Property(w => w.Title).IsRequired().HasMaxLength(100);

                entity.HasOne(w => w.Member)
                      .WithMany(m => m.Workouts)
                      .HasForeignKey(w => w.MemberId);

                entity.HasOne(w => w.Trainer)
                      .WithMany(t => t.Workouts)
                      .HasForeignKey(w => w.TrainerId);
            });

            modelBuilder.Entity<Gym>().HasData(
                new Gym { Id = 1, Name = "Sport Life", Address = "вул. Хрещатик 1" },
                new Gym { Id = 2, Name = "FitStudio", Address = "вул. Свободи 10" }
            );

            modelBuilder.Entity<Trainer>().HasData(
                new Trainer { Id = 1, Name = "Андрій Коваль", Specialization = "Бодібілдинг" },
                new Trainer { Id = 2, Name = "Олена Франко", Specialization = "Йога" }
            );

            modelBuilder.Entity<Member>().HasData(
                new Member { Id = 1, Name = "Іван Шевченко", Phone = "0971112233", GymId = 1 },
                new Member { Id = 2, Name = "Марія Бондар", Phone = "0954445566", GymId = 2 }
            );

            modelBuilder.Entity<Workout>().HasData(
                new Workout { Id = 1, Title = "Силове тренування", Date = new DateTime(2026, 10, 1), MemberId = 1, TrainerId = 1 },
                new Workout { Id = 2, Title = "Персональна йога", Date = new DateTime(2026, 10, 2), MemberId = 2, TrainerId = 2 }
            );
        }
    }

    public class Gym
    {
        public Gym()
        {
            Members = new HashSet<Member>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }

    public class Member
    {
        public Member()
        {
            Workouts = new HashSet<Workout>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int GymId { get; set; }

        public virtual Gym Gym { get; set; }
        public virtual ICollection<Workout> Workouts { get; set; }
    }

    public class Trainer
    {
        public Trainer()
        {
            Workouts = new HashSet<Workout>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }

        public virtual ICollection<Workout> Workouts { get; set; }
    }

    public class Workout
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }

        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        public int TrainerId { get; set; }
        public virtual Trainer Trainer { get; set; }
    }
}
