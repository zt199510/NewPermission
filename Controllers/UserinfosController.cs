using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CardPlatform.Common;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
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

        public UserinfosController(ILogger<UserinfosController> logger,CommonEven commonEven)
        {
            _logger = logger;
            _CommonEven = commonEven;
        }

        [HttpGet]
        [Authorize(Roles ="admin")]
        [Route("GetSelectItem")]
        public async Task<IActionResult> Get()
        {
            var token = await HttpContext.GetTokenAsync("Bearer", "access_token");
            var principal = _CommonEven.GetPrincipalFromAccessToken(token);
            if (principal is null)
                return Ok(false);

            var id = principal.Claims.First(c => c.Type == JwtClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(id))
                return Ok(false);


            return Ok(id);
        }

       
    }
}
