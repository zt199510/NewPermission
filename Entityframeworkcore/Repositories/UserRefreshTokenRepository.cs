using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZTDomain.IRepositories;
using ZTDomain.Models;

namespace Entityframeworkcore.Repositories
{
    public class UserRefreshTokenRepository : FonourRepositoryBase<UserRefreshToken>, IUserRefreshTokenRepository
    {

        public UserRefreshTokenRepository(ZTDbContext dbcontext) : base(dbcontext)
        {

        }


    



    }
}
