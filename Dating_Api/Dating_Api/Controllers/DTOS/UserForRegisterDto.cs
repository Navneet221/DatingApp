using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dating_Api.Controllers.DTOS
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8,MinimumLength =4,ErrorMessage ="Please specify Password between 4 and 8 characters")]
        public string Password { get; set; }
    }
}
