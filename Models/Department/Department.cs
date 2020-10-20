using CardPlatform.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CardPlatform.Models.Department
{
    public class Department:Entity
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 部门负责人
        /// </summary>
        public string Manager { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 父级部门ID
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 包含用户
        /// </summary>
        [NotMapped]
        public virtual ICollection<UserInfo> Users { get; set; }

        /// <summary>
        /// 创建人信息
        /// </summary>
        public virtual UserInfo CreateUser { get; set; }

    }
}
