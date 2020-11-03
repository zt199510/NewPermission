using System;
using System.Collections.Generic;
using System.Text;
using ZTDomain.Model;

namespace ZTDomain.ModelsExtended
{
    public class RoleMenu
    {
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public Guid MenuId { get; set; }
        public Menu Menu { get; set; }
    }
}
