using System.Linq;
using System.Threading.Tasks;
using CardPlatform.Common;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZTDomain;

namespace CardPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
     

        public DepartmentController()
        {
         
        }

        //[Route("Create")]
        //[HttpPost]
        //public async  Task<IActionResult> AddDepartment(Department mode)
        //{
        //    var result =new ServiceResult();
        //    result.IsFailed("部门编号已经存在");
        //    if (_UserDb.Departments.Any(rm => rm.Code == mode.Code)) return Ok(result);
        //    _UserDb.Departments.Add(mode);
        //    if(await _UserDb.SaveChangesAsync()>0)
        //    { result.IsSuccess("添加成功");return Ok(result); }
        //    result.IsFailed("未知问题");
        //    return Ok(result);
        //}
    }
}