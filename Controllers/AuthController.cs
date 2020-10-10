using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CardPlatform.Models;
using CardPlatform.MyDBModel;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CardPlatform.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly MyDbContext _UserDb;

        public AuthController(MyDbContext UserDb)
        {
            _UserDb = UserDb;
        }


        [HttpPost]
        public IActionResult Login(UserLogDTO user)
        {
            var loginModel = _UserDb.UserInfos.FirstOrDefault(m => m.UserName.Equals(user.UserName));
            if (loginModel != null)
            {
                try
                {

                    //定义发行人issuer
                    string iss = "JWTBearer.Auth";
                    //定义受众人audience
                    string aud = "api.auth";
                    IEnumerable<Claim> claims = new Claim[]
                    {
                         new Claim(JwtClaimTypes.Email,loginModel.Email),
                         new Claim(JwtClaimTypes.Name,loginModel.UserName),
                         new Claim(JwtClaimTypes.Role,"admin"),
                    };
                    var nbf = DateTime.UtcNow;

                    var Exp = DateTime.UtcNow.AddSeconds(1000);

                    string sign = "q2xiARx$4x3TKqBJ"; //SecurityKey 的长度必须 大于等于 16个字符
                    var secret = Encoding.UTF8.GetBytes(sign);
                    var key = new SymmetricSecurityKey(secret);
                    var signcreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var jwt = new JwtSecurityToken(issuer: iss, audience: aud, claims: claims, notBefore: nbf, expires: Exp, signingCredentials: signcreds);
                    var JwtHander = new JwtSecurityTokenHandler();
                    var token = JwtHander.WriteToken(jwt);

                    return Ok(new
                    {
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
                message = "账号秘密不正确",
                Error = true
            }); ;

        }
        [HttpPost]
        public IActionResult Registered(RegistModel model)
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
                var Isuse = _UserDb.UserInfos.FirstOrDefault(w=>w.UserName== model.Name);

                if (Isuse != null)
                    return Ok(new
                    {
                        message = "账号已存在",
                        Error = false
                    });

                var use = _UserDb.Add<UserInfo>(info);

                int sumok = _UserDb.SaveChanges();
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
    }
}