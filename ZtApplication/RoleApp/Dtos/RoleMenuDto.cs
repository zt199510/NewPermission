using System;
using System.Collections.Generic;
using System.Text;
using ZtApplication.MesnuAPP.Dtos;

namespace ZtApplication.RoleApp.Dtos
{
    public class RoleMenuDto
    {
        public Guid RoleId { get; set; }
        public RoleDto Role { get; set; }

        public Guid MenuId { get; set; }
        public MenuDto Menu { get; set; }
    }
}
