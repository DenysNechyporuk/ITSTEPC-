using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAnnotationHW
{
    [Table("Positions")]
    public class Position
    {
        public Position()
        {
            Workers = new HashSet<Worker>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Worker> Workers { get; set; }
    }
}
