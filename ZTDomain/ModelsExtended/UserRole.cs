using System;
using System.Collections.Generic;
using System.Text;
using ZTDomain.Model;

namespace ZTDomain.ModelsExtended
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }

    }
}
