using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ZTDomain.IRepositories;
using ZTDomain.Model;

namespace Entityframeworkcore.Repositories
{
    /// <summary>
    /// 用户管理仓储实现
    /// </summary>
    public class UserRepository : FonourRepositoryBase<User>, IUserRepository
    {
        public UserRepository(ZTDbContext dbcontext) : base(dbcontext)
        {

        }
        /// <summary>
        /// 检查用户是存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>存在返回用户实体，否则返回NULL</returns>
        public async  Task<User> CheckUser(string userName, string password)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(it => it.UserName == userName && it.Password == password);
        }
    }
}
