using CardPlatform.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CardPlatform.Models
{
    public class UserInfo:Entity
    {
       
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string RegistTime { get; set; }
        public string LasLoginTime { get; set; }
        public bool Status { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid DeptmentId { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 所属部门实体
        /// </summary>
        public virtual Department.Department Department { get; set; }

        /// <summary>
        /// 角色集合
        /// </summary>
        public virtual ICollection<Role.Role> Roles { get; set; }

        private readonly List<UserRefreshToken> _userRefreshTokens = new List<UserRefreshToken>();
        public IEnumerable<UserRefreshToken> UserRefreshTokens => _userRefreshTokens;


        /// <summary>
        /// 验证刷新token是否存在或过期
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>

        public bool IsValidRefreshToken(string refreshToken)
        {
            return _userRefreshTokens.Any(d => d.Token.Equals(refreshToken) && d.Active);
        }

        /// <summary>
        /// 创建刷新Token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userId"></param>
        /// <param name="minutes"></param>

        public void CreateRefreshToken(string token, string userId, double minutes = 1)
        {
            _userRefreshTokens.Add(new UserRefreshToken() { Token = token, UserId = userId, Expires = DateTime.Now.AddDays(minutes) });
        }

        /// <summary>
        /// 移除刷新token
        /// </summary>
        /// <param name="refreshToken"></param>
        public void RemoveRefreshToken(string refreshToken)
        {
            _userRefreshTokens.Remove(_userRefreshTokens.FirstOrDefault(t => t.Token == refreshToken));
        }

    }
}
