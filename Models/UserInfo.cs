using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CardPlatform.Models
{
    public class UserInfo
    {

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public string id { get; set; }
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }

        public string RegistTime { get; set; }

        public string LasLoginTime { get; set; }

        public bool Status { get; set; }
    }
}
