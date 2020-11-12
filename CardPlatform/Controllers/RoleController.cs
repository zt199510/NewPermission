using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardPlatform.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ZtApplication.RoleApp;
using ZtApplication.RoleApp.Dtos;

namespace CardPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
        [HttpPost]
        [Route("Edit")]
        public IActionResult EditorCreate(RoleDto dto)
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

        [HttpPost]
        [Route("GetAllPageList")]
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

        [HttpPost]
       [Route("DeleteMuti")] 
        public IActionResult DeleteMuti(string ids)
        {
            var Res = new ServiceResult();
            try
            {
                Res.IsSuccess();
                string[] idArray = ids.Split(',');
                List<Guid> delIds = new List<Guid>();
                foreach (string id in idArray)
                {
                    delIds.Add(Guid.Parse(id));
                }
                _service.DeleteBatch(delIds);
                return Ok(Res);
            }
            catch (Exception ex)
            {
                Res.IsFailed();
                return Ok(Res);
            }
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(Guid Id)
        {
            var Res = new ServiceResult();
            try
            { Res.IsSuccess();
                _service.Delete(Id);
                return Ok(Res);
            }
            catch (Exception)
            {
                Res.IsFailed();
                return Ok(Res);
                
            }
        }
        [HttpPost]
        [Route("GetId")]
        public IActionResult Get(Guid id)
        {
            var Res = new ServiceResultList<RoleDto>();
            var dto = _service.Get(id);
            Res.Data = dto;
            return Ok(Res);
            
        }



    }
}