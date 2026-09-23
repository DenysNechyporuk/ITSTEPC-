using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAnnotationHW
{
    [Table("Countries")]
    public class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
