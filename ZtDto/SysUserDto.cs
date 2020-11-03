using System;
using System.Collections.Generic;
using System.Text;

namespace ZtDto
{
    public class SysUserDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserNikeName { get; set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public int? UserSex { get; set; }
        /// <summary>
        /// 用户出生日期
        /// </summary>
        public DateTime? UserBirthday { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// 用户QQ
        /// </summary>
        public string UserQq { get; set; }
        /// <summary>
        /// 用户微信
        /// </summary>
        public string UserWx { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserAvatar { get; set; }
        /// <summary>
        /// 用户手机
        /// </summary>
        public string UserPhone { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public int UserStatus { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { set; get; }
        /// <summary>
        /// 所属用户组
        /// </summary>
        public string UserGroupId { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
    }
}
