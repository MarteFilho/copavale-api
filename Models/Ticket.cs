using System.ComponentModel.DataAnnotations;

namespace CopaVale.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required]
        public User User { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Problem { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        public string DateOpen { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        public string Awnser { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        public string Status { get; set; }
    }
}
