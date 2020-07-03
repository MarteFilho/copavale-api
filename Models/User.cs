using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CopaVale.Models
{
    public class User
    {
        public int Id { get; set; }

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
        [MaxLength(30)]
        public string Password { get; set; }


        [Required]
        [MinLength(10)]
        [MaxLength(50)]
        public string FaceitURL { get; set; }

        [MinLength(5)]
        [MaxLength(10)]
        public string Role { get; set; }

        [MinLength(5)]
        [MaxLength(20)]
        [Required]
        public string Function { get; set; }

        public List<Ticket> Tickets { get; set; }


    }
}
