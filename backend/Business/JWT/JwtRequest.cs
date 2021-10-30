using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.JWT
{
    public class JwtRequest
    {
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Password { get; set; }
    
    }
}
