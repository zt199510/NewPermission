using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ZTDomain.IRepositories;
using ZTDomain.Model;
using ZTDomain.Models;
using ZTDomain.ModelsExtended;

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

        public async Task<User> GetRefreshToken(string id)
        {

            return await _dbContext.Set<User>().Include(c => c.UserRefreshTokens).FirstOrDefaultAsync(c => c.UserName == id);
        }


        public async Task<bool> AddUser(User user)
        {
             _dbContext.Set<User>().Add(user);
    
            return await _dbContext.SaveChangesAsync()>0?true:false;
        }

        public async Task<bool> Save(User user)
        {
            Update(user);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }
        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        public User GetWithRoles(Guid id)
        {
            var user = _dbContext.Set<User>().FirstOrDefault(it => it.Id == id);
            if (user != null)
            {
                List<UserRole> userRoles= _dbContext.Set<UserRole>().Where(it => it.UserId == id).ToList();
                userRoles.ForEach((c)=> {
                    user.Roles.Add(c.Role);
                });
               
            }
            return user;
        }

    }
}
