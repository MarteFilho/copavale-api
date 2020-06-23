using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public string Password { get; set; }


        [Required]
        [MinLength(10)]
        [MaxLength(50)]
        public string FaceitURL { get; set; }

    }
}
