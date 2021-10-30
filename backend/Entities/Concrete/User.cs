using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class User : IEntity
    {
        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string SurName { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Nick { get; set; }
        public string Phone { get; set; }
        [Column(TypeName = "varchar(300)")]
        public string Bio { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string Mail { get; set; }
        [Column(TypeName = "varchar(200)")]
        [JsonIgnore]
        public string Password { get ; set; }
        public bool IsDeleted { get; set; }

        [NotMapped]
        override public int? UserID { get; set; }
    }
}
