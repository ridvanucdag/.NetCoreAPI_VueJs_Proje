using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Follow:IEntity
    {
        //[ForeignKey(name:nameof(User))]
        public int? FollowedID { get; set; }

        [ForeignKey("UserID")]
        virtual public User? User { get; set; }

        [ForeignKey("FollowedID")]
         public User? Followed { get; set; }


    }
}
