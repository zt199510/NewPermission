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

            var loginModel = await _userAppService.CheckUser(user.UserName, user.Password);
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
            loginModel.CreateRefreshToken(token, loginModel.UserName);
            result.IsFailed("发生未知错误");
            if (!await _userAppService.Save(loginModel)) throw new System.Exception("Throw Exception");

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
            if (!model.Password.Equals(model.CPassword))
            {
                Res.IsFailed("密码不一样,请检查");
                return Ok(Res);
            }
            var loginModel = await _userAppService.CheckUser(model.Name, model.Password);
            if (loginModel != null)
            {
                Res.IsFailed("用户名或者邮箱已经存在");
                return Ok(Res);
            }
            bool IsOK = await _userAppService.Add(new ZTDomain.Model.User() { EMail = model.Email, UserName = model.Name, Password = model.Password, DeptmentId = Guid.Empty });
            if (IsOK)
                Res.IsSuccess("注册成功");
            else
                Res.IsFailed("注册失败");

            return Ok(Res);

        }
        /// <summary>
        /// 利用刷新令牌(也叫长Token)刷新访问Token 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] Request request)
        {

            //将访问令牌解密 并且返回Claims实体
            var principal = _CommonEven.GetPrincipalFromAccessToken(request.AccessToken);

            if (principal is null)
                return Ok(false);
            var id = principal.Claims.First(c => c.Type == JwtClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(id))
                return Ok(false);
            if (await _userAppService.RefreshToken(id, request.RefreshToken))
            {
               

            }



            //if (user is null || user.UserRefreshTokens?.Count() <= 0)
            //    return Ok(false);

            //if (!user.IsValidRefreshToken(request.RefreshToken))
            //    return Ok(false);

            //_UserDb.UserRefreshToken.Remove(user.UserRefreshTokens.First(d => d.Token == request.RefreshToken));

            //var refreshToken = _CommonEven.GenerateRefreshToken();

            //user.CreateRefreshToken(refreshToken, user.UserName);

            //try
            //{
            //    await _UserDb.SaveChangesAsync();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            //IEnumerable<Claim> claims = new Claim[]
            //       {
            //             new Claim(JwtClaimTypes.Email,user.Email),
            //             new Claim(JwtClaimTypes.Name,user.UserName),
            //             new Claim(JwtClaimTypes.Role,"admin"),
            //       };
            return Ok(new
            {
                AccessToken = "123"
                //AccessToken = _CommonEven.GenerateAccessToken(claims),
                //RefreshToken = refreshToken
            });
        }

    }
}