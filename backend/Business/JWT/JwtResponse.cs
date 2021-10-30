using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.JWT
{
    public class JwtResponse
    { 
        public string Token { get; set; }
        public User User { get; set; }
    }
}
