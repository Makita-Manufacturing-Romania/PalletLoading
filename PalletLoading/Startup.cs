using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PalletLoading.Data;
using Rotativa.AspNetCore;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using PalletLoading.Helpers;
using Part_Center_Systems.Middleware;
using Microsoft.AspNetCore.Authentication.Negotiate;
using PalletLoading.Interfaces;
using PalletLoading.Services;

namespace PalletLoading
{
    public class Startup
    {
        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            Configuration = configuration;
            RotativaConfiguration.Setup(env);
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<PalletLoadingContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("PalletLoadingContext")));
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-GB");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-GB") };
                services.AddMvc();
            });


            services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
            .AddNegotiate();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Manager", policy => policy.Requirements.Add(new RoleRequirement(new string[] { "Manager" })));
                options.AddPolicy("Supervizor", policy => policy.Requirements.Add(new RoleRequirement(new string[] { "Manager", "Supervizor" })));
                options.AddPolicy("User", policy => policy.Requirements.Add(new RoleRequirement(new string[] { "Manager", "Supervizor", "User" })));
                options.AddPolicy("Guest", policy => policy.Requirements.Add(new RoleRequirement(new string[] { "Manager", "Supervizor", "User", "Guest" })));
            });

            services.AddScoped<IAuthorizationHandler, CustomAuthorization>();

            services.AddScoped<IBufferedFileUploadService, BufferedFileUploadLocalService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseStatusCodePagesWithReExecute("/Error/Index","?statusCode={0}");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

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
