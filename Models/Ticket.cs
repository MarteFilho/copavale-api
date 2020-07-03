using System.ComponentModel.DataAnnotations;

namespace CopaVale.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

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
