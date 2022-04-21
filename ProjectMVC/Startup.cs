using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using ProjectMVC.Models;
using ProjectMVC.Services;
using ProjectMVC.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectMVC
{
    /// <summary>
    /// comment
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<ShopDBContext>(
               option => option.UseSqlServer(Configuration.GetConnectionString("EShopDBCon"))
               .LogTo(s=>Console.WriteLine(s)));

 


            services.AddAuthentication( options => {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;            
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options => {
                options.LoginPath = "/Account/Login";
                options.LogoutPath="/Account/Logout";
            }).AddGoogle(options => {
                IConfigurationSection googleAuthNSection = Configuration.GetSection("Authentication:Google");
                options.ClientId = "269971474734-mtf6t0r602l1usql3rk0dl6s98e8dcgf.apps.googleusercontent.com";
                options.ClientSecret = "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";          
            }).AddFacebook(facebookOptions => {
                facebookOptions.AppId = "497619958583488";
                facebookOptions.AppSecret = "4ca04ccee723bbfe3d729944b88deb30";
            });

            services.AddScoped<IImageHandler, ImageHandler>();
            services.AddScoped<IProductBaseRepo, ProductRepositoryy>();
            services.AddScoped<IBaseRepository<Category>, CategoryRepositary>();
            services.AddScoped<GenericRepository<Order>, OrderRepoService>();
            services.AddScoped<GenericRepository<OrderProduct>, OrderProductRepoService>();
            services.AddScoped<ICartRepository, CartRepoService>();

     
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ShopDBContext>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseStatusCodePagesWithRedirects("/Home/Error");
                app.UseExceptionHandler("/Home/Error");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Home/Error");
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();//=>Product/details
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => //exe 
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
