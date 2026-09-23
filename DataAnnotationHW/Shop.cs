using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAnnotationHW
{
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

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Address { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }

        public int? ParkingArea { get; set; }

        public ICollection<Worker> Workers { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
