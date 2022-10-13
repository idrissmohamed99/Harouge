
using AutoMapper;
using FilterAttributeHarougeAPI.Common;
using FluentValidation.AspNetCore;
using Infra;
using Infra.Domain;
using Infra.Repository;
using Infra.Services.IAuthUser;
using Infra.Services.IService;
using Infra.Services.IService.IUserManagmentServices;
using Infra.Utili;
using Infra.Utili.ConfigrationModels;
using Infra.Utili.Filters;
using Infra.ValidationServices.IAuthUserValidation;
using Infra.ValidationServices.IUserManagmentServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using HarougeAPI.AuthClimas;
using Repository.Repository;
using Services.Services.AuthUser;
using Services.Services.UserManagmentServices;
using Services.ValidationServices;
using Services.ValidationServices.AuthUserValidation;
using Services.ValidationServices.UserManagmentValidationServices;
using System.Text;
using UnitOfWork.Work;

namespace HarougeAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime.Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            DI(services);

            services.AddOptions();
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c => c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "My API", Version = "V1" }));
            services.AddCors(opt => opt.AddPolicy("CorsPolicy",
              builder =>
              {
                  builder.WithOrigins("*")
                      .AllowAnyHeader().
                      AllowAnyOrigin()
                      .AllowAnyMethod();
              }));

            IConfigurationSection appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettingsConfig>(appSettingsSection);
            AppSettingsConfig appSettings = appSettingsSection.Get<AppSettingsConfig>();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret));

            services.AddAuthentication("OAuth").AddJwtBearer("OAuth", config =>
            {
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = HelperUtili.LifetimeValidator,
                    IssuerSigningKey = securityKey,
                    SaveSigninToken = true,
                };

                config.EventsType = typeof(EventHandlerUtili);
                config.SaveToken = true;
            });

            services.AddControllers(config =>
            {
                config.Filters.Add<ExceptionHandlerUtili>();
                config.Filters.Add<ParametorModelFilter>();
            }
            ).AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssemblyContaining<Startup>();
            });



        }

        //  This method gets called by the runtime.Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1"));
        }

        private void DI(IServiceCollection services)
        {

            string con = @"Server=.;Database=HarougeDb;user id=sa;password=AS@n0601";



           services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork<HarougeDbContext>));
           services.AddDbContext<HarougeDbContext>(options => options.UseSqlServer(con));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<EventHandlerUtili>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.TryAddSingleton<HelperUtili>();
            services.TryAddSingleton<AuthUser>();
            services.TryAddSingleton<CheckUserIsAdminSytem>();

            services.AddScoped<IUserAuth, UserAuth>();

            services.AddScoped<IUserAuthValidationServices, UserAuthValidationServices>();


            services.AddScoped<IPermisstionServices, PermisstionServices>();
            services.AddScoped<IPermisstionValidationServices, PermisstionValidationServices>();

            services.AddScoped<IModuleServices, ModuleServices>();

            services.AddScoped<IRoleValidationServices, RoleValidationServices>();
            services.AddScoped<IRoleServices, RoleServices>();

            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IUserValidationServices, UserValidationServices>();



        }

    }
}


