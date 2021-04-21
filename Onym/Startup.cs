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

            //Extension service
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
            //Routing config
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