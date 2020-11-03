﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZtApplication.MesnuAPP.Dtos;
using ZTDomain.IRepositories;
using ZTDomain.Model;

namespace ZtApplication.MesnuAPP
{
    public class MenuAppService : IMenuAppService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IUserRepository _userRepository;
    
        public MenuAppService(IMenuRepository menuRepository, IUserRepository userRepository)
        {
            _menuRepository = menuRepository;
            _userRepository = userRepository;
       

        }
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteBatch(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public MenuDto Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Menu> GetAllList()
        {
            var menu = _menuRepository.GetAllList().OrderBy(it => it.SerialNumber);
            //使用AutoMapper进行实体转换
            return menu.ToList() ;
        }

        public List<MenuDto> GetMenusByParent(Guid parentId, int startPage, int pageSize, out int rowCount)
        {
            throw new NotImplementedException();
        }

        public List<Menu> GetMenusByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public bool InsertOrUpdate(MenuDto dto)
        {
            throw new NotImplementedException();
        }
    }
}