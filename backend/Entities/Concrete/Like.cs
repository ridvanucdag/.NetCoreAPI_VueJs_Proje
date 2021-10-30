using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Like : IEntity
    {
        public int TweetID { get; set; }

        [ForeignKey("UserID")]
        virtual public User? User { get; set; }


    }
}
