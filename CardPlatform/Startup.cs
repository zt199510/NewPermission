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
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";//���������
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
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"֧��ƽ̨ V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthentication();
            //2.�ٿ�����Ȩ
            app.UseAuthorization();
            //ʹ�ÿ���
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
                Guid departmentId = Guid.NewGuid();

                    context.Departments.Add(
               new Department
               {
                   Id = departmentId,
                   Name = "Fonour�����ܲ�",
                   ParentId = Guid.Empty
               }
            );
                //����һ����������Ա�û�
                context.Users.Add(
                   new User
                   {
                       UserName = "admin",
                       Password = "123456", //
                       Name = "��������Ա",
                       DeptmentId = departmentId
                   }
              );


                //�����ĸ��������ܲ˵�
                context.Menus.AddRange(
                 new Menu
                 {
                     Name = "��֯��������",
                     Code = "Department",
                     SerialNumber = 0,
                     ParentId = Guid.Empty,
                     Icon = "fa fa-link"
                 },
                 new Menu
                 {
                     Name = "��ɫ����",
                     Code = "Role",
                     SerialNumber = 1,
                     ParentId = Guid.Empty,
                     Icon = "fa fa-link"
                 },
                 new Menu
                 {
                     Name = "�û�����",
                     Code = "User",
                     SerialNumber = 2,
                     ParentId = Guid.Empty,
                     Icon = "fa fa-link"
                 },
                 new Menu
                 {
                     Name = "���ܹ���",
                     Code = "Department",
                     SerialNumber = 3,
                     ParentId = Guid.Empty,
                     Icon = "fa fa-link"
                 }
              );
                context.SaveChanges();

            }
        }

    
    }
}
