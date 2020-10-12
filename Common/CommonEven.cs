using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CardPlatform.Common
{
    public class CommonEven
    {
        /// <summary>
        /// 解析访问令牌
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ClaimsPrincipal GetPrincipalFromAccessToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                string sign = "q2xiARx$4x3TKqBJ";
                return handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(sign)),
                    ValidateLifetime = false
                }, out SecurityToken validatedToken);
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 生成刷新Token
        /// </summary>
        /// <returns></returns>
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        /// <summary>
        /// 生成访问令牌
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            string signingKey = "q2xiARx$4x3TKqBJ";//SecurityKey 的长度必须 大于等于 16个字符
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            //定义发行人issuer
            string iss = "JWTBearer.Auth";
            //定义受众人audience
            string aud = "api.auth";

            var token = new JwtSecurityToken(
                issuer: iss,
                audience: aud,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
