using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardPlatform.Common;
using CardPlatform.MyDBModel;
using CardPlatform.ServiceEnd;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;

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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "V1",
                    Title = "֧��ƽ̨�ӿ�",
                    Description = $"һ��֧��ƽ̨"
                });

              var OpenApiSecurityScheme = new OpenApiSecurityScheme
                 {
                  Description = "JWT��֤��Ȩ��ʹ��ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�\"",
                  Name = "Authorization",  //jwt Ĭ�ϲ�������
                  In = ParameterLocation.Header,  //jwtĬ�ϴ��Authorization��Ϣ��λ�ã�����ͷ��
                  Type = SecuritySchemeType.ApiKey
              };

                c.AddSecurityDefinition("oauth2", OpenApiSecurityScheme);
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
             services.AddDbContext<MyDbContext>(config=> {

                 config.UseSqlite(connectionString);
             });

            services.AddControllers().AddNewtonsoftJson(options=> {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }); ;
            services.AddAuthorization(options=> {
                options.AddPolicy("admin",option=>option.RequireRole("admin"));
            });
            var Issurer = "JWTBearer.Auth";  //������
            var Audience = "api.auth";       //������
            var secretCredentials = "q2xiARx$4x3TKqBJ";   //��Կ

            services.AddAuthentication(options=> {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options=>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Issurer,
                    ValidateAudience = true,
                    ValidAudience = Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretCredentials)),
                    ValidateLifetime = true, //��֤��������
                    RequireExpirationTime = true, //����ʱ��
                    ClockSkew=TimeSpan.FromSeconds(4)
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))//token �������ͷ������ֶ�
                        {
                            context.Response.Headers.Add("TokenExpired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            ///�������п������
            services.AddCors(options=> {
                options.AddPolicy(MyAllowSpecificOrigins,builder=> {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            services.AddScoped<CommonEven>().
                AddScoped<NavMenuService>(); 
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
            app.UseSwaggerUI(c=> {
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
        }
    }
}
