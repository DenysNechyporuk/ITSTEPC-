using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreIntroHW
{
    [Table("Flights")]
    public class Flight
    {
        public Flight()
        {
            Clients = new HashSet<Client>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string FlightNumber { get; set; }

        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string DeparturePlace { get; set; }

        [Required]
        [MaxLength(100)]
        public string ArrivalPlace { get; set; }

        public int AirplaneId { get; set; }

        [ForeignKey("AirplaneId")]
        public Airplane Airplane { get; set; }

        public ICollection<Client> Clients { get; set; }
    }
}
