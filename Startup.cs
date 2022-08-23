using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCreate.Data;
using MyCreate.model;
using MyCreate;
using System.Reflection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Hazirweb
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
            //services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

            //services.AddMvc()
            //    .AddViewLocalization()
            //    .AddDataAnnotationsLocalization(option =>
            //    {
            //        var type = typeof(SharedResource);
            //        var assemblyName = new AssemblyName(type.GetTypeInfo
            //            ().Assembly.FullName);
            //        var factory = services.BuildServiceProvider
            //        ().GetService<IStringLocalizerFactory>();
            //        var localizer = factory.Create("SharedResource", assemblyName.Name);
            //        option.DataAnnotationLocalizerProvider = (t, f) => localizer;


            //    });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<NewsDb>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("NewsDb")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<IdentityOptions>(
                x =>
                {

                    x.Password.RequireNonAlphanumeric = false;
                    x.Password.RequireDigit = true;
                    x.Password.RequireLowercase = false;
                    x.Password.RequireUppercase = false;
                    x.Password.RequiredLength = 3;
                });

            services.Configure<RequestLocalizationOptions>(
              opt =>
              {
                  var supportedCulteres = new List<CultureInfo>
                  {
                        new CultureInfo("en-US"),
                new CultureInfo("fr-FR"),
                new CultureInfo("de-DE")

                  };
                  opt.DefaultRequestCulture = new RequestCulture("en");
                  opt.SupportedCultures = supportedCulteres;
                  opt.SupportedUICultures = supportedCulteres;

              });
            //services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("fr-FR"),
                new CultureInfo("de-DE")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures

            });


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();  

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
            name: "areas",
            template: "{area:exists}/{controller=default}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
