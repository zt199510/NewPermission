using CardPlatform.Common;


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.OpenApi.Validations.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZTDomain;

namespace CardPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MenuController : ControllerBase
    {
      

        public MenuController()
        {
      
        }

        /// <summary>
        /// 新建页面
        /// </summary>
        /// <param name="menu"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("Create")]
        //public async Task<IActionResult> Create([Bind("Code,Name,ParentId,IndexCode,Url,MenuType,Icon,Remarks")] Menu menu)
        //{
        //    menu.ParentId= menu.ParentId == null ? Guid.Empty : menu.ParentId;
        //    var Result = new ServiceResult();
        //    Result.IsFailed("账号已经存在");
        //    var Isuse = await _UserDb.Menus.FirstOrDefaultAsync(w => w.Code == menu.Code);
        //    if (Isuse == null)
        //    {
        //        _UserDb.Add(menu);
        //        await _UserDb.SaveChangesAsync();
        //        Result.IsSuccess("注册成功");
        //        return Ok(Result);
        //    }
        //    return Ok(Result);
        //}


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


        /// <summary>
        /// 编辑保存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("Edit")]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Code,Name,ParentId,IndexCode,Url,MenuType,Icon,Remarks")] Menu menu)
        //{
        //    var Result = new ServiceResult();
        //    Result.IsFailed("失败");

        //    if (id == Guid.Empty||id==null)
        //    {
        //        return Ok(Result);
        //    }
        //    try
        //    {
        //        menu.Id = id;
        //        _UserDb.Update(menu);
        //        await _UserDb.SaveChangesAsync();
        //        Result.IsSuccess("成功");
        //        return Ok(Result);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        return Ok(Result); 
        //    }


        //}

        //[HttpPost]
        //[Route("Delete")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var Result = new ServiceResultList<Menu>();
        //    Result.IsFailed("没有该数据信息");
        //    if (id == null)
        //    {
        //        return Ok(Result);
        //    }
        //    var Isuse = await _UserDb.Menus.FirstOrDefaultAsync(w => w.Id == id);
        //    if (Isuse != null)
        //    {
        //         _UserDb.Menus.Remove(Isuse);
        //       int sum= await _UserDb.SaveChangesAsync();
        //        if (sum > 0)
        //            Result.IsSuccess("删除成功");
        //    }
        //    return Ok(Result);
                
        //}
    }

}
