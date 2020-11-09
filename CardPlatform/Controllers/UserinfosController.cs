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
using ZtApplication.Common;
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

            return Ok(result);
        }
        [HttpPost]
        [Route("GetMenuItem")]
        [Authorize]
        public async Task<IActionResult> SelectMenuItem()
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
