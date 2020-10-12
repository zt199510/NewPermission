using CardPlatform.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPlatform.MyDBModel
{
    public class MyDbContext:DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> dbContextOptions):base(dbContextOptions)
        {

        }

        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<UserRefreshToken>  UserRefreshToken { get; set; }
        public DbSet<PermissionModels>  PermissionModels { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
