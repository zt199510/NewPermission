using System;

namespace ZtDto
{
    public class SysUserGroupDto
    {
        /// <summary>
        /// 用户组Id
        /// </summary>
        public string UserGroupId { get; set; }
        /// <summary>
        /// 用户组名称
        /// </summary>
        public string UserGroupName { get; set; }
        /// <summary>
        /// 父用户组Id
        /// </summary>
        public string ParentUserGroupId { get; set; }
        /// <summary>
        /// 父用户组名称
        /// </summary>
        public string ParentUserGroupName { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
    }
}
