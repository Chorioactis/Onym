using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using Onym.Data;
using Onym.Models;
using Onym.Services;

namespace Onym
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            
            var configurationBuilder = new ConfigurationBuilder()
                .AddConfiguration(configuration);
            Configuration = configurationBuilder.Build();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            //Database and identity context
            services.AddDbContext<OnymDbContext<User>>();
            services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<OnymDbContext<User>>()
                .AddDefaultTokenProviders();
            //Model-View-Controller
            services.AddControllersWithViews();
            services.AddRazorPages();

            //Identity configuration
            services.Configure<IdentityOptions>(options =>
            {
                //Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                //Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(0);
                options.Lockout.MaxFailedAccessAttempts = 0;
                options.Lockout.AllowedForNewUsers = false;

                //User settings
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                //Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(7);

                options.LoginPath = "/sign_in";
                options.AccessDeniedPath = "/access_denied";
                options.SlidingExpiration = true;
            });
            //Sessions configuration
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
            });;
            
            //Extension service
            services.AddEmailService();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            //Exceptions and status code pages
            if (env.IsDevelopment())    
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStatusCodePagesWithReExecute("/error", "?code={0}");
            //Static files config
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Add("Cache-Control", "public,max-age=2419200");
                }
            });
            //Router
            app.UseRouting();
            //Authorization evaluate
            app.UseAuthentication();
            app.UseAuthorization();
            //Sessions evaluate
            app.UseSession();
            //Routing config
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "feed",
                    "{controller=Feed}/{action=Index}");
                endpoints.MapControllerRoute(
                    "userProfile",
                    "{controller=User}/{action=Index}/{'@' + userName?}");
                endpoints.MapControllerRoute(
                    name: "userSettings",
                    pattern: "{controller=User}/{action=Settings}");
                endpoints.MapRazorPages();
            });
        }
    }
}