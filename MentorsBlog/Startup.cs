using System;
using System.IO;
using System.Reflection;
using System.Text;
using MentorsBlog.Application.Domain;
using MentorsBlog.Application.Domain.Interfaces;
using MentorsBlog.Application.Service;
using MentorsBlog.Application.Service.Interfaces;
using MentorsBlog.Core.Common;
using MentorsBlog.Core.Common.Extensions;
using MentorsBlog.Core.Common.Interfaces;
using MentorsBlog.Core.Common.Models;
using MentorsBlog.Core.DataAccess;
using MentorsBlog.Core.DataAccess.Intefaces;
using MentorsBlog.Core.Web.Auth;
using MentorsBlog.Core.Web.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace MentorsBlog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            CommonRegistration(services);
            DatabaseRegistration(services);
            ServicesRegistration(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsLocal())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MentorsBlog v1"));
            }

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            
            app.UseExceptionHandlerMiddleware();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void CommonRegistration(IServiceCollection services)
        {
            services.AddSingleton<ISettings, Settings>();
            var settings = services.BuildServiceProvider().GetService<ISettings>() 
                           ?? throw new NullReferenceException("Settings resolve failed");

            var tokens = settings.GetSection<AppSettings>(nameof(AppSettings)).Tokens;
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MentorsBlog", 
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Denis Chervinskiy",
                        Email = string.Empty,
                        Url = new Uri("https://vk.com/bluesy_fluesy"),
                    },
                });
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference 
                            { 
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer" 
                            }
                        },
                        new string[] {}
                    }
                });
                
                c.OperationFilter<AuthOperationAttribute>();
            });
            
            services.AddHttpContextAccessor()
                .AddCors()
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = tokens.Authenticate.Issuer,
                        ValidateAudience = true,
                        ValidAudience = tokens.Authenticate.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokens.Authenticate.Secret)),
                        ValidateIssuerSigningKey = true
                    };
                });
        }
        
        private void DatabaseRegistration(IServiceCollection services)
        {
            var settings = services.BuildServiceProvider().GetService<ISettings>() 
                           ?? throw new NullReferenceException("Settings resolve failed");
            var connectionStrings = settings.GetSection<AppSettings>(nameof(AppSettings)).ConnectionStrings;
            
            services
                .AddDbContext<IDataContext, DataContext>(options =>
                    options.UseNpgsql(connectionStrings.DbConnection));
        }
        
        private void ServicesRegistration(IServiceCollection services)
        {
            services
                .AddSingleton<JwtTokenGenerator>()
                .AddSingleton<Authenticator>();
            
            services
                .AddTransient<IPostService, PostService>()
                .AddTransient<ICommentService, CommentService>()
                .AddTransient<IUserService, UserService>();
            
            services
                .AddScoped<IPostDomain, PostDomain>()
                .AddScoped<ICommentDomain, CommentDomain>()
                .AddScoped<IUserDomain, UserDomain>();
        }
    }
}
