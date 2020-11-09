using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CardPlatform.Common;
using Entityframeworkcore;
using Entityframeworkcore.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;
using ZtApplication;
using ZtApplication.Common;
using ZtApplication.MesnuAPP;
using ZTDomain;
using ZTDomain.IRepositories;
using ZTDomain.Model;

namespace CardPlatform
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";//名字随便起
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ZTDbContext>(config =>
            {
                config.UseSqlite(connectionString);
            });

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            services.AddAuthDefault();

            services.AddCorsExtensions(MyAllowSpecificOrigins);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuAppService, MenuAppService>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
            services.AddScoped<CommonEven>();
            services.AddAutoMapper(typeof(FonourMapper).Assembly);
            services.AddSwagger();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"支付平台 V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthentication();
            //2.再开启授权
            app.UseAuthorization();
            //使用跨域
            app.UseCors(MyAllowSpecificOrigins);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            InitIdentityServerDataBase(app);
        }

        private void InitIdentityServerDataBase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<ZTDbContext>();
                  if (context.Users.Any()) return;
                //    Guid departmentId = Guid.NewGuid();

                //        context.Departments.Add(
                //   new Department
                //   {
                //       Id = departmentId,
                //       Name = "Fonour集团总部",
                //       ParentId = Guid.Empty
                //   }
                //);
                //    //增加一个超级管理员用户
                //    context.Users.Add(
                //       new User
                //       {
                //           UserName = "admin",
                //           Password = "123456", //
                //           Name = "超级管理员",
                //           DeptmentId = departmentId
                //       }
                //  );




                //    //增加四个基本功能菜单
                //    context.Menus.AddRange(
                //     new Menu
                //     {
                //         Name = "组织机构管理",
                //         Code = "Department",
                //         SerialNumber = 0,
                //         ParentId = Guid.Empty,
                //         Icon = "fa fa-link"
                //     },
                //     new Menu
                //     {
                //         Name = "角色管理",
                //         Code = "Role",
                //         SerialNumber = 1,
                //         ParentId = Guid.Empty,
                //         Icon = "fa fa-link"
                //     },
                //     new Menu
                //     {
                //         Name = "用户管理",
                //         Code = "User",
                //         SerialNumber = 2,
                //         ParentId = Guid.Empty,
                //         Icon = "fa fa-link"
                //     },
                //     new Menu
                //     {
                //         Name = "功能管理",
                //         Code = "Department",
                //         SerialNumber = 3,
                //         ParentId = Guid.Empty,
                //         Icon = "fa fa-link"
                //     }
                //  );

                //var role = context.Roles.Add(rout);
                //foreach (var item in context.Menus)
                //{
                //    context.RoleMenus.Add(new ZTDomain.ModelsExtended.RoleMenu
                //    {
                //        Menu = item,
                //        MenuId = item.Id,
                //        Role = rout,
                //        RoleId= rout.Id
                //    });
                //}
                var rout = new Role
                {
                    Code = "9527",
                    Remarks = "9527",
                    Name = "9527"
                };
            User user=    context.Users.Where(o => o.UserName.Equals("string")).FirstOrDefault();
                foreach (var item in context.Roles)
                {
                    context.UserRoles.Add(new ZTDomain.ModelsExtended.UserRole { 
                        Role=rout,
                        RoleId=rout.Id,
                        User= user,
                        UserId= user.Id

                    });
                }

                context.SaveChanges();

            }
        }


    }
}
