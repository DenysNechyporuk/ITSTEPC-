using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAnnotationHW
{
    public class ShopDb : DbContext
    {
        public ShopDb()
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

        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Worker> Workers { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
    }

    [Table("Shops")]
    public class Shop
    {
        public Shop()
        {
            Workers = new HashSet<Worker>();
            Products = new HashSet<Product>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(150)]
        public string Address { get; set; }
        public int CityId { get; set; }
        public int? ParkingArea { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        public virtual ICollection<Worker> Workers { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }

    [Table("Cities")]
    public class City
    {
        public City()
        {
            Shops = new HashSet<Shop>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
        public virtual ICollection<Shop> Shops { get; set; }
    }

    [Table("Countries")]
    public class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }

    [Table("Workers")]
    public class Worker
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(50)]
        public string Surname { get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int PositionId { get; set; }
        public int ShopId { get; set; }

        [ForeignKey("PositionId")]
        public virtual Position Position { get; set; }
        [ForeignKey("ShopId")]
        public virtual Shop Shop { get; set; }
    }

    [Table("Positions")]
    public class Position
    {
        public Position()
        {
            Workers = new HashSet<Worker>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Worker> Workers { get; set; }
    }

    [Table("Products")]
    public class Product
    {
        public Product()
        {
            Shops = new HashSet<Shop>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public float Discount { get; set; }
        public int? CategoryId { get; set; }
        public int Quantity { get; set; }
        public bool IsInStock { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public virtual ICollection<Shop> Shops { get; set; }
    }

    [Table("Categories")]
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
