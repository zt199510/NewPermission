using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using CardPlatform.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZTDomain.Model;


namespace CardPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
  
    public class UserinfosController : ControllerBase
    {
   
        private readonly ILogger<UserinfosController> _logger;
        private readonly CommonEven _CommonEven;
  
        public UserinfosController(ILogger<UserinfosController> logger,CommonEven commonEven)
        {
            _logger = logger;
            _CommonEven = commonEven;
      
          
        }

        [HttpGet]
      
        [Route("GetNavMenuItem")]
        [Authorize]
        public  IActionResult Get()
        {
            var result = new ServiceResult();
            //result.IsFailed();
            //var token = new JwtSecurityToken(HttpContext.GetTokenAsync("Bearer", "access_token").Result);
            //string email = token.Claims.FirstOrDefault(t => t.Type == "email").Value;
            //SysUser user = _UserDb.SysUser.Where(o => o.UserEmail.Equals(email)).FirstOrDefault();
            //if (user == null) return Ok(result);

            //user.sysRoles = (from sur in _UserDb.SysUrRelated
            //                 join sr in _UserDb.SysRole
            //                 on sur.RoleId equals sr.RoleId
            //                 where sur.UserId.Equals(user.UserId)
            //                 select new SysRole
            //                 {
            //                     RoleId = sr.RoleId,
            //                     RoleName = sr.RoleName,
            //                     CreateUserId = sr.CreateUserId,
            //                     DeleteSign = sr.DeleteSign,
            //                     CreateTime = sr.CreateTime,
            //                     DeleteTime = sr.DeleteTime,
            //                     EditTime = sr.EditTime,
            //                     Note = sr.Note
            //                 }
            //                   ).ToList();


       
            result.IsSuccess();
         
            return Ok(result);
        }
        [HttpPost]
        [Route("GetMenuItem")]
        [Authorize]
        public async Task<IActionResult> SelectMenuItem(string query)
        {
            //var menus = _UserDb.Menus.AsNoTracking();
            //if (!string.IsNullOrEmpty(query.QName))
            //{
            //    menus = menus.Where(s => s.Name.Contains(query.QName.Trim()));
            //}
            //if (!string.IsNullOrEmpty(query.QId))
            //{
            //    menus = menus.Where(s => s.Code==query.QId);
            //}
            //if (query.QParentId!=null)
            //{
            //    menus = menus.Where(s => s.ParentId == query.QParentId);
            //}
            //if (query.QMenuType!=-1)
            //{
            //    menus = menus.Where(s => s.Type == query.QMenuType);
            //}

            //return Ok(new  { Menus = await menus.ToListAsync(), Query = query });
            return Ok(false);

        }


    }
}
