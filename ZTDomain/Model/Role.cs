using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZTDomain.Model
{
    public class Role : Entity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public Guid CreateUserId { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Remarks { get; set; }

        [NotMapped]
        public virtual User CreateUser { get; set; }
        [NotMapped]
        public virtual ICollection<User> Users { get; set; }
        [NotMapped]
        public virtual ICollection<Menu> Menus { get; set; }
    }
}
