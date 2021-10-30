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
    public class Tweet : IEntity
    {

        [Column(TypeName = "varchar(300)")]
        public string Content { get; set; }
        public int? LikeCount { get; set; } = 0;
        public bool IsDeleted { get; set; }
        [ForeignKey("UserID")]
        public User? User { get; set; }
        public ICollection<Like>? Likes { get; set; }

    }
}
