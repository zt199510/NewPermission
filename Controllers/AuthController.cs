using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CardPlatform.Common;
using CardPlatform.Models;
using CardPlatform.MyDBModel;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CardPlatform.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly MyDbContext _UserDb;
        private readonly CommonEven _CommonEven;

        public AuthController(MyDbContext UserDb, CommonEven CommonEven)
        {
            _UserDb = UserDb;
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
            var loginModel = _UserDb.UserInfos.Include(d => d.UserRefreshTokens).FirstOrDefault(m => m.UserName.Equals(user.UserName));
            if (loginModel != null)
            {
                try
                {
                    IEnumerable<Claim> claims = new Claim[]
                      {
                         new Claim(JwtClaimTypes.Email,loginModel.Email),
                         new Claim(JwtClaimTypes.Name,loginModel.UserName),
                         new Claim(JwtClaimTypes.Role,"admin"),
                      };
                    //生成允许访问的JWT
                    var token = _CommonEven.GenerateAccessToken(claims);
                    //生成允许刷新JWT的Token
                    var refreshToken = _CommonEven.GenerateRefreshToken();
                    //更新登入时间
                    loginModel.LasLoginTime = DateTime.Now.ToString();
                  
                    //存入数据库
                    loginModel.CreateRefreshToken(refreshToken, user.UserName);
                    int resultsub = await _UserDb.SaveChangesAsync();
                    if (resultsub > 0)
                        return Ok(new
                        {
                            RefreshToken = refreshToken,
                            access_token = token,
                            token_type = "Bearer",
                            message = "登入成功",
                            Error = false
                        }); 
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return Ok(new
            {
                message = "账号密码不正确",
                Error = true
            }); ;

        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Registered(RegistModel model)
        {
            if (ModelState.IsValid)
            {

                UserInfo info = new UserInfo()
                {
                    Email = model.Email,
                    UserName = model.Name,
                    Password = model.Password,
                    RegistTime = DateTime.Now.ToString(),

                };
                var Isuse = await _UserDb.UserInfos.FirstOrDefaultAsync(w => w.UserName == model.Name);

                if (Isuse != null)
                    return Ok(new
                    {
                        message = "账号已存在",
                        Error = false
                    });

                var use = _UserDb.Add<UserInfo>(info);

                int sumok = await _UserDb.SaveChangesAsync();
                if (sumok > 0)
                {
                    return Ok(new
                    {
                        message = "注册成功",
                        Error = true
                    });
                }
            }
            return Ok(new
            {
                message = "注册失败",
                Error = false
            });


        }
        /// <summary>
        /// 利用刷新令牌(也叫长Token)刷新访问Token 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] Request request)
        {
            //将访问令牌解密 并且返回Claims实体
            var principal = _CommonEven.GetPrincipalFromAccessToken(request.AccessToken);

            if (principal is null)
                return Ok(false);

            var id = principal.Claims.First(c => c.Type == JwtClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(id))
                return Ok(false);

            var user = await _UserDb.UserInfos.Include(d => d.UserRefreshTokens)
              .FirstOrDefaultAsync(d => d.UserName == id);

            if (user is null || user.UserRefreshTokens?.Count() <= 0)
                return Ok(false);

            if (!user.IsValidRefreshToken(request.RefreshToken))
                return Ok(false);
          
            _UserDb.UserRefreshToken.Remove(user.UserRefreshTokens.First(d=>d.Token== request.RefreshToken));

           var refreshToken = _CommonEven.GenerateRefreshToken();
  
            user.CreateRefreshToken(refreshToken, user.UserName);

            try
            {
                await _UserDb.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            IEnumerable<Claim> claims = new Claim[]
                   {
                         new Claim(JwtClaimTypes.Email,user.Email),
                         new Claim(JwtClaimTypes.Name,user.UserName),
                         new Claim(JwtClaimTypes.Role,"admin"),
                   };
            return Ok(new
            {
                AccessToken = _CommonEven.GenerateAccessToken(claims),
                RefreshToken = refreshToken
            });
        }

    }
}