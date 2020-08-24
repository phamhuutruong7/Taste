using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Taste.DataAccess;
using Taste.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Identity.UI.Services;
using Taste.Utility;
using Stripe;
using Microsoft.AspNetCore.Authentication.Facebook;

namespace Taste
{
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddScoped<IUnitOfWork, DataAccess.Data.Repository.UnitOfWork>(); //add UnitOfWork to project.

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));

            //add MVC
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddAuthentication().AddFacebook(facebookOptions => {
                facebookOptions.AppId = "3143731685746545";
                facebookOptions.AppSecret = "2947dd4d373457f579badb7539d4c694";
            });

            services.AddAuthentication().AddMicrosoftAccount(options => { 
                options.ClientId = "c9dd6fe0-1630-43e0-bf71-8b8bfbea73ce";
                options.ClientSecret = "DkH~v84r.Y7UhRg31D79__~M4qRVZky-CN";
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            //app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            /*app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
            */
            app.UseMvc();
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];
        }
    }
}
