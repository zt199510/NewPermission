using IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZTDomain.IRepositories;
using ZTDomain.Model;
using ZTDomain.Models;
using ZTDomain.ModelsExtended;

namespace ZtApplication
{
    /// <summary>
    /// 用户管理服务
    /// </summary>
    public class UserAppService : IUserAppService
    {
        //用户管理仓储接口
        private readonly IUserRepository _userReporitory;
        private readonly IUserRefreshTokenRepository _RefreshTokenRepository;
        private readonly IMenuRepository _menuRepository;
        /// <summary>
        /// 构造函数 实现依赖注入
        /// </summary>
        /// <param name="userRepository">仓储对象</param>
        public UserAppService(IUserRepository userRepository, IUserRefreshTokenRepository RefreshTokenRepository, IMenuRepository menuRepository)
        {
            _userReporitory = userRepository;
            _RefreshTokenRepository = RefreshTokenRepository;
            _menuRepository = menuRepository;
        }

        public async Task<bool> Add(User  user)
        {
            var allMenus = _menuRepository.GetAllList(it => it.Type == 0).OrderBy(it => it.SerialNumber);

            return await _userReporitory.AddUser(user);
        }

        public async Task<object> RefreshToken(string id,string RefreshToken,string refreshToken)
        {
           var user= await _userReporitory.GetRefreshToken(id);
            if (user is null || user.UserRefreshTokens?.Count() <= 0)
              return null;
            if (!user.IsValidRefreshToken(RefreshToken))
             return null;
            UserRefreshToken refresh = user.UserRefreshTokens.Where(o => o.Token == RefreshToken).FirstOrDefault();
            if (!_RefreshTokenRepository.Delete(refresh))
                return null;
            user.CreateRefreshToken(refreshToken, user.UserName);
            if (!await _userReporitory.Save(user)) return null;
       
                IEnumerable<Claim> claims = new Claim[]
                   {
                         new Claim(JwtClaimTypes.Email,user.EMail),
                         new Claim(JwtClaimTypes.Name,user.UserName),
                         new Claim(JwtClaimTypes.Role,"admin"),
                   };

            return claims;
        }
   
        public async Task<User> CheckUser(string userName, string password)
        {
            return await _userReporitory.CheckUser(userName, password);
        }

        public async Task<bool> Save(User user)
        {
            return await _userReporitory.Save(user);
        }
    }
}
