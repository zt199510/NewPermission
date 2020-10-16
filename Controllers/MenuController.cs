using CardPlatform.Common;
using CardPlatform.Models;
using CardPlatform.MyDBModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly MyDbContext _UserDb;

        public MenuController(MyDbContext UserDb)
        {
            _UserDb = UserDb;
        }

        /// <summary>
        /// 新建页面
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("Id,Name,ParentId,IndexCode,Url,MenuType,Icon,Remarks")] Menu menu)
        {
            var Result = new ServiceResult();
            Result.IsFailed("账号已经存在");
            var Isuse = await _UserDb.Menus.FirstOrDefaultAsync(w => w.Id == menu.Id);
            if (Isuse == null)
            {
                _UserDb.Add(menu);
                await _UserDb.SaveChangesAsync();
                Result.IsSuccess("注册成功");
                return Ok(Result);
            }
            return Ok(Result);
        }


        /// <summary>
        /// 详情页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Details")]
        public async Task<IActionResult> Details(string id)
        {
            var Result = new ServiceResultList<Menu>();
            Result.IsFailed("查询失败");
            if (id == null)
            {
                return Ok(Result);
            }

            var menu = await _UserDb.Menus
            .SingleOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return Ok(Result);
            }
            Result.IsSuccess();
            Result.data = menu;



            return Ok(Result);


        }


        /// <summary>
        /// 编辑保存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,ParentId,IndexCode,Url,MenuType,Icon,Remarks")] Menu menu)
        {
            var Result = new ServiceResult();
            Result.IsFailed("失败");

            if (id != menu.Id)
            {
                return Ok(Result);
            }
            try
            {
                _UserDb.Update(menu);
                await _UserDb.SaveChangesAsync();
                Result.IsSuccess("成功");
                return Ok(Result);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Ok(Result);
            }


        }

    }

}
