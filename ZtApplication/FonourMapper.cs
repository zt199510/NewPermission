
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using ZtApplication.MesnuAPP.Dtos;
using ZtApplication.RoleApp.Dtos;
using ZTDomain.Model;

namespace ZtApplication
{
    /// <summary>
    /// Enity与Dto映射
    /// </summary>
    public    class FonourMapper: AutoMapper.Profile
    {
        public FonourMapper()
        {
          
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<MenuDto, Menu>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<RoleDto, Role>().ReverseMap();
        }
     
    }
}
