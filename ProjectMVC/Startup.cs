using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectMVC.Models;
using ProjectMVC.Services;
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
               option => option.UseSqlServer(Configuration.GetConnectionString("EShopDBCon")));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ShopDBContext>();

            services.AddAuthentication( options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                })
        .AddGoogle(options =>
        {
            IConfigurationSection googleAuthNSection =
                  Configuration.GetSection("Authentication:Google");
            options.ClientId = "269971474734-mtf6t0r602l1usql3rk0dl6s98e8dcgf.apps.googleusercontent.com";
            options.ClientSecret = "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";
           
        });
            services.AddScoped<IProductRepository, ProductRepoService>();
            services.AddScoped<ICategoryRepository, CatgoryRepoService>();

            //services.AddScopyd<GenericRepository<Product>,ProductRepositoryu>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
