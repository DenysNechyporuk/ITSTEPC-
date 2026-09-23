using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreIntroHW
{
    [Table("Airplanes")]
    public class Airplane
    {
        public Airplane()
        {
            Flights = new HashSet<Flight>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        public int MaxPassengers { get; set; }

        [MaxLength(50)]
        public string Country { get; set; }

        public ICollection<Flight> Flights { get; set; }
    }
}
