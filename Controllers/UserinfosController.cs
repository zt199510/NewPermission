using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CardPlatform.Common;
using CardPlatform.MyDBModel;
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
    [Authorize]
    public class UserinfosController : ControllerBase
    {
   
        private readonly ILogger<UserinfosController> _logger;
        private readonly CommonEven _CommonEven;
        private readonly MyDbContext _UserDb;
        public UserinfosController(ILogger<UserinfosController> logger,CommonEven commonEven, MyDbContext UserDb)
        {
            _logger = logger;
            _CommonEven = commonEven;
            _UserDb = UserDb;
        }

        [HttpGet]
      
        [Route("GetSelectItem")]
        public async Task<IActionResult> Get()
        {


            return Ok(false);
        }

       
    }
}
