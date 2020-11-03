using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ZTDomain.Model;

namespace ZTDomain.IRepositories
{/// <summary>
 /// 用户管理仓储接口
 /// </summary>
    public interface  IUserRepository: IRepository<User>
    {

        /// <summary>
        /// 检查用户是存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>存在返回用户实体，否则返回NULL</returns>
        public  Task<User>  CheckUser(string userName, string password);
    }
}
