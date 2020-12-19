using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Models
{
    public class Member
    {
        [Key]
        [Column(TypeName = "varchar(50)")]

        public string UserName { get; set; } //nvarchar  có dấu unocode 
        [Column(TypeName = "varchar(255)")]
        public string Password { get; set; }
        public int Role { get; set; }
    }
}
