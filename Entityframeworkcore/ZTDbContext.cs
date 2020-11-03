using Microsoft.EntityFrameworkCore;
using System;
using ZTDomain;
using ZTDomain.Model;
using ZTDomain.ModelsExtended;

namespace Entityframeworkcore
{
    public class ZTDbContext:DbContext
    {
        public ZTDbContext()
        {

        }
        public ZTDbContext(DbContextOptions<ZTDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=UserInfo.db;");
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //UserRole关联配置
            modelBuilder.Entity<UserRole>()
              .HasKey(ur => new { ur.UserId, ur.RoleId });
            //RoleMenu关联配置
            modelBuilder.Entity<RoleMenu>()
         .HasKey(rm => new { rm.RoleId, rm.MenuId });

        }


    }
}
