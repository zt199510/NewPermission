using System;
using System.Threading.Tasks;
using ZTDomain.IRepositories;
using ZTDomain.Model;

namespace ZtApplication
{
    /// <summary>
    /// 用户管理服务
    /// </summary>
    public class UserAppService : IUserAppService
    {
        //用户管理仓储接口
        private readonly IUserRepository _userReporitory;

        /// <summary>
        /// 构造函数 实现依赖注入
        /// </summary>
        /// <param name="userRepository">仓储对象</param>
        public UserAppService(IUserRepository userRepository)
        {
            _userReporitory = userRepository;
        }

        public async Task<User> CheckUser(string userName, string password)
        {
            return await _userReporitory.CheckUser(userName, password);
        }
    }
}
