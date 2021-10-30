using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class SearchDetail
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public User User { get; set; }
        public Tweet Tweet { get; set; }

    }
}
