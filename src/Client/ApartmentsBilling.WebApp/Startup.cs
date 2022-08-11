using ApartmentsBilling.WebApp.AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace ApartmentsBilling.WebApp
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
            services.AddControllersWithViews();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new GenericProfile());
            });
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            // .AddCookie(options =>
            // {
            //     options.Cookie.Name = "MySessionCookie";
            //     options.LoginPath = new PathString("/Login/Login");
            //     options.SlidingExpiration = true;
            //     options.Cookie.HttpOnly = true;
            //     options.AccessDeniedPath = new PathString("/Login/Login");
            //     options.Cookie.MaxAge = options.ExpireTimeSpan;
            //     options.SlidingExpiration = true;
            //     options.LogoutPath = new PathString("/Login/Logout");
            //     options.Cookie.SameSite = SameSiteMode.Strict;//This cookie cannot be used as a third-party cookie under any circumstances, without exception. For example, suppose b.com sets the following cookies:
            // });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
            {
                x.LoginPath = "/Login/Login";
                x.AccessDeniedPath = "/Error/UnAutHorized";
            });
            services.AddSession();
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = "/Login/Login";
            //    options.LogoutPath = "/Login/Logout";
            //    options.AccessDeniedPath = "/Login/Login";
            //    options.ExpireTimeSpan = TimeSpan.FromHours(1);
            //    options.SlidingExpiration = true;
            //    options.Cookie = new Microsoft.AspNetCore.Http.CookieBuilder()
            //    {
            //        HttpOnly = true,
            //        Name = "Sessin.Cookie",
            //        SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict
            //    };
            //});
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
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
