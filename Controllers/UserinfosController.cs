using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CardPlatform.Common;
using CardPlatform.Models;
using CardPlatform.Models.MenuMod;
using CardPlatform.MyDBModel;
using CardPlatform.ServiceEnd;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;


namespace CardPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
  
    public class UserinfosController : ControllerBase
    {
   
        private readonly ILogger<UserinfosController> _logger;
        private readonly CommonEven _CommonEven;
        private readonly MyDbContext _UserDb;
        private readonly NavMenuService _Navservice;

        public UserinfosController(ILogger<UserinfosController> logger,CommonEven commonEven, MyDbContext UserDb, NavMenuService Navservice)
        {
            _logger = logger;
            _CommonEven = commonEven;
            _UserDb = UserDb;
            _Navservice = Navservice;
        }

        [HttpGet]
      
        [Route("GetNavMenuItem")]
        [Authorize]
        public  IActionResult Get()
        {
            _Navservice.InitOrUpdate();
            var result = new ServiceResultList<List<NavMenu>>();
            result.IsSuccess();
            result.data=(List<NavMenu>) _Navservice.GetNavMenus();
            return new JsonResult(result);
        }
        [HttpPost]
        [Route("GetMenuItem")]
        [Authorize]
        public async Task<IActionResult> Index(MenuIndexQuery query)
        {
            var menus = _UserDb.Menus.AsNoTracking();
            if (!string.IsNullOrEmpty(query.QName))
            {
                menus = menus.Where(s => s.Name.Contains(query.QName.Trim()));
            }
            if (!string.IsNullOrEmpty(query.QId))
            {
                menus = menus.Where(s => s.Id.Contains(query.QId.Trim()));
            }
            if (!string.IsNullOrEmpty(query.QParentId))
            {
                menus = menus.Where(s => s.ParentId == query.QParentId.Trim());
            }
            if (query.QMenuType!=-1)
            {
                menus = menus.Where(s => s.MenuType == (MenuTypes)query.QMenuType);
            }
               
            return Ok(new  { Menus = await menus.ToListAsync(), Query = query });
          
        }


    }
}
