using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Abstract
{
    public class IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int ID { get; set; }
        public DateTime At { get; set; } = DateTime.Now;
        virtual public int? UserID { get; set; }
    }
}
