using CardPlatform.Models;
using CardPlatform.Models.Department;
using CardPlatform.Models.Role;
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

        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu>  Menus { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserRole>()
          .HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Entity<RoleMenu>()
         .HasKey(rm => new { rm.RoleId, rm.MenuId });

    


            base.OnModelCreating(builder);
        }
    }
}
