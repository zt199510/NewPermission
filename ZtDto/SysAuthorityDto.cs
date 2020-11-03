using System;
using System.Collections.Generic;
using System.Text;

namespace ZtDto
{
    /// <summary>
    /// 权限表Dto
    /// </summary>
    public class SysAuthorityDto
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        public string AuthorityId { get; set; }
        /// <summary>
        /// 权限类型
        /// </summary>
        public int AuthorityType { get; set; }
        /// <summary>
        /// 菜单
        /// </summary>
        public SysMenuDto sysMenuDto { set; get; }

    }
}
