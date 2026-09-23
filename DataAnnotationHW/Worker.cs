using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAnnotationHW
{
    [Table("Workers")]
    public class Worker
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        public decimal Salary { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public int PositionId { get; set; }

        [ForeignKey("PositionId")]
        public Position Position { get; set; }

        public int ShopId { get; set; }

        [ForeignKey("ShopId")]
        public Shop Shop { get; set; }
    }
}
