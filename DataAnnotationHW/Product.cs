using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAnnotationHW
{
    [Table("Products")]
    public class Product
    {
        public Product()
        {
            Shops = new HashSet<Shop>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public float Discount { get; set; }

        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public int Quantity { get; set; }

        public bool IsInStock { get; set; }

        public ICollection<Shop> Shops { get; set; }
    }
}
