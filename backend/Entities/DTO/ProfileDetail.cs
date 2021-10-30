using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class ProfileDetail
    {

        public User User { get; set; }
        public Tweet Tweet { get; set; }
        public ICollection<Like>? Like { get; set; }
    }
}
