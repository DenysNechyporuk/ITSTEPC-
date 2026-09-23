using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAnnotationHW
{
    [Table("Cities")]
    public class City
    {
        public City()
        {
            Shops = new HashSet<Shop>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        public ICollection<Shop> Shops { get; set; }
    }
}
