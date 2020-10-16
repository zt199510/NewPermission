using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CardPlatform.Models.MenuMod
{
    /// <summary>
    /// 导航菜单项
    /// </summary>
   
    public class NavMenu
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public MenuTypes MenuType { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool IsOpen { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        
        public List<NavMenu> SubNavMenus = new List<NavMenu>();
    }

    /// <summary>
    /// 左侧导航菜单视图模型
    /// </summary>
    public class NavMenuVM
    {
        public IEnumerable<NavMenu> NavMenus { get; set; }

        public string[] MenuidsOpen { get; set; }
    }
}
