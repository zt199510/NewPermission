using CardPlatform.Common;
using CardPlatform.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.JSInterop.Infrastructure;
using Microsoft.OpenApi.Validations.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        private readonly IMenuAppService _menuAppService;
        public MenuController(IMenuAppService menuAppService)
        {
            _menuAppService = menuAppService;
        }

        /// <summary>
        /// 获取功能树JSON数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMenuTree")]
        public IActionResult GetMenuTreeData()
        {
            var menus = _menuAppService.GetAllList();
            return Ok(menus);
        }



        /// <summary>
        /// 新建页面
        /// </summary>
        /// <param name="menu"></param>
        ///// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public  IActionResult Edit(MenuDto dto)
        {
            var Res = new ServiceResult();
            if (_menuAppService.InsertOrUpdate(dto))
                Res.IsSuccess("成功");
            else
                Res.IsFailed("失败");

            return Ok(Res);
        }



        /// <summary>
        /// 详情页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("Details")]
        //public async Task<IActionResult> Details(Guid id)
        //{
        //    var Result = new ServiceResultList<Menu>();
        //    Result.IsFailed("查询失败");
        //    if (id == null)
        //    {
        //        return Ok(Result);
        //    }

        //    var menu = await _UserDb.Menus
        //    .SingleOrDefaultAsync(m => m.Id == id);
        //    if (menu == null)
        //    {
        //        return Ok(Result);
        //    }
        //    Result.IsSuccess();
        //    Result.data = menu;
        //    return Ok(Result);
        //}

        [HttpPost]
        [Route("Delete")]
        public  IActionResult Delete(Guid id)
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
