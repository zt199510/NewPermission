
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        public MenuAppService(IMenuRepository menuRepository, IUserRepository userRepository, IMapper mapper,IRoleRepository roleRepository)
        {
            _menuRepository = menuRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;

        }
        public void Delete(Guid id)
        {
            _menuRepository.Delete(id);
        }

        public void DeleteBatch(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public MenuDto Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<MenuDto> GetAllList()
        {
            var menus = _menuRepository.GetAllList().OrderBy(it => it.SerialNumber);
            //使用AutoMapper进行实体转换
            return _mapper.Map<List<MenuDto>>(menus);
        }

        public List<MenuDto> GetMenusByParent(Guid parentId, int startPage, int pageSize, out int rowCount)
        {
            var menus = _menuRepository.LoadPageList(startPage, pageSize, out rowCount, it => it.ParentId == parentId, it => it.SerialNumber);
            return _mapper.Map<List<MenuDto>>(menus);
        }

        public List<MenuDto> GetMenusByUser(Guid userId)
        {
            List<MenuDto> result = new List<MenuDto>();
            var allMenus = _menuRepository.GetAllList(it => it.Type == 0).OrderBy(it => it.SerialNumber);
            if (userId == Guid.Empty) //超级管理员
                return _mapper.Map<List<MenuDto>>(allMenus);
            var user = _userRepository.GetWithRoles(userId);
            if (user == null)
                return result;
            var userRoles = user.Roles;
            List<Guid> menuIds = new List<Guid>();
            foreach (var role in userRoles)
            {
                menuIds = menuIds.Union(_roleRepository.GetAllMenuListByRole(role.Id)).ToList();
            }
            allMenus = allMenus.Where(it => menuIds.Contains(it.Id)).OrderBy(it => it.SerialNumber);
            return _mapper.Map<List<MenuDto>>(allMenus);
        }

        public bool InsertOrUpdate(MenuDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
