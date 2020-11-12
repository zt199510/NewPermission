using CardPlatform.Common;
using CardPlatform.Model;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.JSInterop.Infrastructure;
using Microsoft.OpenApi.Validations.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZtApplication.Common;
using ZtApplication.MesnuAPP;
using ZtApplication.MesnuAPP.Dtos;
using ZTDomain;

namespace CardPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MenuController : ControllerBase
    {
        //  private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMenuAppService _menuAppService;
        private readonly CommonEven _commonEven;

        public MenuController(IMenuAppService menuAppService, CommonEven commonEven)
        {
            _menuAppService = menuAppService;
            _commonEven = commonEven;
            //   _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        [Route("GetNavigation")]
        public IActionResult GetNavigation()
        {
            var headers = HttpContext.Request.Headers["Authorization"].ToString();

            var Res = new ServiceResultList<List<MenuDto>>();
            Res.IsFailed();
            if (string.IsNullOrEmpty(headers)) return Ok(Res);
            headers = headers.Replace("Bearer ", "");
            var principal = _commonEven.GetPrincipalFromAccessToken(headers);
            var id = principal.Claims.First(c => c.Type == JwtClaimTypes.Id)?.Value;

            Res.Data = _menuAppService.GetMenusByUser(Guid.Parse(id));


            return Ok(Res);


        }

        /// <summary>
        /// 获取功能树JSON数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMenuTree")]
        public IActionResult GetMenuTreeData()
        {
            var Res = new ServiceResultList<List<MenuDto>>();
            try
            {
                Res.IsSuccess();
                Res.Data = _menuAppService.GetAllList();

            }
            catch (Exception)
            {
                Res.IsFailed();

            }

            return Ok(Res);
        }

        /// <summary>
        /// 新建页面
        /// </summary>
        /// <param name="menu"></param>
        ///// <returns></returns>
        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit(MenuDto dto)
        {
            var Res = new ServiceResult();
            if (_menuAppService.InsertOrUpdate(dto))
                Res.IsSuccess("成功");
            else
                Res.IsFailed("失败");

            return Ok(Res);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(Guid id)
        {
            var Res = new ServiceResult();
            try
            {
                Res.IsSuccess();
                _menuAppService.Delete(id);
                return Ok(Res);
            }
            catch (Exception ex)
            {
                Res.IsFailed();
                return Ok(Res);
            }

        }
    }

}
