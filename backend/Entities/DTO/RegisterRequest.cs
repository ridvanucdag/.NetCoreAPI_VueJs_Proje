using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class RegisterRequest
    {

        public string Name { get; set; }
        public string SurName { get; set; }
        public string Nick { get; set; }
        public string Phone { get; set; }
        public string Bio { get; set; }
        public string Mail { get; set; }
        public string Password { get ; set ; }


    }
}
