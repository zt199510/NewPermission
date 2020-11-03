using System;
using System.Collections.Generic;
using System.Text;
using ZTDomain.IRepositories;
using ZTDomain.Model;

namespace Entityframeworkcore.Repositories
{
    public class MenuRepository : FonourRepositoryBase<Menu>, IMenuRepository
    {
        public MenuRepository(ZTDbContext dbcontext) : base(dbcontext)
        {

        }
    }
}
