using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CardPlatform.Common;
using CardPlatform.Models;

using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using ZtApplication;
using ZTDomain;
using ZTDomain.Model;


namespace CardPlatform.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class AuthController : ControllerBase
    {
     
        private readonly CommonEven _CommonEven;
        private IUserAppService _userAppService;
        public AuthController(CommonEven CommonEven, IUserAppService userAppService)
        {
            _userAppService = userAppService;
             _CommonEven = CommonEven;
        }
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(UserLogDTO user)
        {
            var result = new ServiceResult();

            var loginModel = _userAppService.CheckUser(user.UserName, user.Password);
            if (loginModel == null)
            {
                result.IsFailed("账号或密码错误");
                return Ok(result);
            }

            IEnumerable<Claim> claims = new Claim[]
            {
                   new Claim(JwtClaimTypes.Id,loginModel.Id.ToString()),
                   new Claim(JwtClaimTypes.Name,loginModel.UserName),
                   new Claim(JwtClaimTypes.Role,"admin")
            };
            //生成允许访问的JWT
            var token = _CommonEven.GenerateAccessToken(claims);
            //生成允许刷新JWT的Token
            var refreshToken = _CommonEven.GenerateRefreshToken();
            //更新登入时间
            loginModel.LastLoginTime = DateTime.Now;
                return Ok(new
                {
                    RefreshToken = refreshToken,
                    access_token = token,
                    token_type = "Bearer",
                    resetTime = 3000,
                    message = "登入成功",
                    Error = false
                });
            
            #region 废弃的

            #endregion
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Registered(RegistModel model)
        {
            var Res = new ServiceResult();
            //SysUser sysUser = new SysUser()
            //{
            //    UserEmail = model.Email,
            //    CreateTime = DateTime.Now,
            //    UserName = model.Name,
            //    UserPwd = model.Password,
            //    UserId = Guid.NewGuid().ToString(),
            //    CreateUserId = Guid.Empty.ToString()
            //};

            //bool IsRegistered = _UserDb.Set<SysUser>().Any(o => sysUser.Equals(o.UserBirthday) || sysUser.UserName == sysUser.UserName);
            //if (IsRegistered)
            //{
            //    Res.IsFailed("用户名或者邮箱已经存在");
            //    return Ok(Res);
            //}
            //_UserDb.SysUser.Add(sysUser);
            //int IsSuccess = await _UserDb.SaveChangesAsync();
            //if (IsSuccess > 0)
            //    Res.IsSuccess("注册成功");
            //else
            //    Res.IsFailed("注册失败");


            return Ok(Res);

        }


    }
}