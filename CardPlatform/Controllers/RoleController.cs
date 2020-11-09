using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardPlatform.Common;
using Microsoft.AspNetCore.Mvc;
using ZtApplication.RoleApp;
using ZtApplication.RoleApp.Dtos;

namespace CardPlatform.Controllers
{
    public class RoleController : ControllerBase
    {

        private readonly IRoleAppService _service;
        public RoleController(IRoleAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 新增或编辑功能
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public IActionResult Edit(RoleDto dto)
        {
            var Res = new ServiceResult();
            Res.IsFailed();
            if (!ModelState.IsValid)
            { 
                return Ok(Res);
            }
            if (dto.Id == Guid.Empty)
                dto.CreateTime = DateTime.Now;
           
            if (_service.InsertOrUpdate(dto))
            {
                Res.IsSuccess();
                return Ok(Res);
            }
            return Ok(Res);
        }

        public IActionResult GetAllPageList(int startPage, int pageSize)
        {
            int rowCount = 0;
            var result = _service.GetAllPageList(startPage, pageSize, out rowCount);
            return Ok(new
            {
                rowCount = rowCount,
                pageCount = Math.Ceiling(Convert.ToDecimal(rowCount) / pageSize),
                rows = result,
            });
        }



    }
}