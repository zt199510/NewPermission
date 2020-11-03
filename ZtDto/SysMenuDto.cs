using System;
using System.Collections.Generic;
using System.Text;

namespace ZtDto
{
    public class SysMenuDto
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        public string MenuId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        public string MenuIcon { get; set; }
        /// <summary>
        /// 菜单地址
        /// </summary>
        public string MenuUrl { get; set; }
        /// <summary>
        /// 父级菜单
        /// </summary>
        public string ParentMenuId { get; set; }
        /// <summary>
        /// 菜单排序号
        /// </summary>
        public Int32 MenuSort { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public List<SysMenuDto> Children { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { set; get; }
    }
}
