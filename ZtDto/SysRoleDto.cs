using System;
using System.Collections.Generic;
using System.Text;

namespace ZtDto
{
    public class SysRoleDto
    {
        /// <summary>
        /// 角色id
        /// </summary>
        /// 
        public string RoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
    }
}
