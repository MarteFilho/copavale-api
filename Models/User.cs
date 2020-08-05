using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopaVale.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Nickname { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Phone { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Password { get; set; }

        public string FaceitURL { get; set; }

        public string Role { get; set; }

        public string Function { get; set; }

        public List<Ticket> Ticket { get; set; }
        public string TeamName { get; set; }

    }
}
