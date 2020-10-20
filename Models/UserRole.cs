using CardPlatform.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPlatform.Models
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public UserInfo User { get; set; }

        public Guid RoleId { get; set; }
        public Role.Role Role { get; set; }
    }

    public class RoleMenu
    {
        public Guid RoleId { get; set; }
        public Role.Role Role { get; set; }

        public Guid MenuId { get; set; }
        public Menu Menu { get; set; }
    }

  
}
