﻿using CardPlatform.Models;
using CardPlatform.Models.MenuMod;
using CardPlatform.MyDBModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPlatform.ServiceEnd
{
    public class NavMenuService
    {
        private readonly MyDbContext _UserDb;
        public NavMenuService(MyDbContext UserDb)
        {
            _UserDb = UserDb;
        }
        private static IList<NavMenu> NavMenus { get; set; }

        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <returns></returns>
        public IList<NavMenu> GetNavMenus()
        {
            if (NavMenus == null)
                InitOrUpdate();

            return NavMenus;
        }

        /// <summary>
        /// 生成导航菜单
        /// </summary>
        /// <returns></returns>
        public void InitOrUpdate()
        {
            NavMenus = new List<NavMenu>();

            var rootMenus = _UserDb.Menus
                .Where(s => string.IsNullOrEmpty(s.ParentId))
                .AsNoTracking()
                .OrderBy(s => s.IndexCode)
                .ToList();

            foreach (var rootMenu in rootMenus)
            {
                NavMenus.Add(GetOneNavMenu(rootMenu));
            }
        }
        /// <summary>
        /// 根据给定的Menu，生成对应的导航菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public NavMenu GetOneNavMenu(Menu menu)
        {
            //构建菜单项
            var navMenu = new NavMenu
            {
                Id = menu.Id,
                Name = menu.Name,
                MenuType = menu.MenuType.Value,
                Url = menu.Url,
                Icon = menu.Icon
            };

            //构建子菜单
            var subMenus = _UserDb.Menus
                .Where(s => s.ParentId == menu.Id)
                .AsNoTracking()
                .OrderBy(s => s.IndexCode)
                .ToList();

            foreach (var subMenu in subMenus)
            {
                navMenu.SubNavMenus.Add(GetOneNavMenu(subMenu));
            }

            return navMenu;
        }

    }
}
