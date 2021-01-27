using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.DTOs
{
    public class ManageSignUpDTO
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Password { get; set; }
    }

    public class ManageLogInDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Password { get; set; }
    }
}
