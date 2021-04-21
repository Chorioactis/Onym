using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Onym.Data;
using Onym.Models;

namespace Onym
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddConfiguration(configuration);
            Configuration = configurationBuilder.Build();
        }

        private IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Database and identity context
            services.AddDbContext<OnymDbContext<User>>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseNpgsql(Configuration.GetConnectionString("OnymDb"));
            });
            services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<OnymDbContext<User>>()
                .AddDefaultTokenProviders();
            //Model-View-Controller
            services.AddMvc();
            /*
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = new PathString("/sign_in");
            });
            services.AddAuthentication().AddCookie("user_identity");
            services.AddDistributedMemoryCache();
            services.AddSession();
            */

            //Extension services
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

            /*app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseCookiePolicy();*/

            //Routing config
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "feed",
                    "{controller=Feed}/{action=Index}");
                endpoints.MapControllerRoute(
                    "profile",
                    "{controller=User}/{action=Index}/{'@' + login?}");
            });
        }
    }
}