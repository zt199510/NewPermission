using CardPlatform.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CardPlatform.Models.Role
{
    public class Role:Entity
    {

        public string Code { get; set; }

        public string Name { get; set; }

        public Guid CreateUserId { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Remarks { get; set; }

        public virtual UserInfo CreateUser { get; set; }
        [NotMapped]
        public virtual ICollection<UserInfo> Users { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
    }
}
