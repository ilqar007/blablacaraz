using BlaBlaCarAz.BLL.DomainModel.Entities;
using BlaBlaCarAz.BLL.DomainModel.IRepositories;
using BlaBlaCarAz.BLL.HelperClasses;
using BlaBlaCarAz.BLL.ServiceLayer.Services;
using BlaBlaCarAz.BLL.ServiceLayer.Services.Interfaces;
using BlaBlaCarAz.DAL.DataContext;
using BlaBlaCarAz.DAL.Repositories;
using BlaBlaCarAz.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Globalization;
using static BlaBlaCarAz.BLL.DomainModel.Entities.IdentityModels;

namespace BlaBlaCarAz.UI
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

            services.AddDbContext<BlaBlaCarAzContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            //services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<BlaBlaCarAzContext>();

            services.AddIdentity<AppUser, Role>(options => options.SignIn.RequireConfirmedAccount = true)
                                .AddEntityFrameworkStores<BlaBlaCarAzContext>().
                                AddDefaultUI().
                                AddDefaultTokenProviders().
                                AddErrorDescriber<MultilanguageIdentityErrorDescriber>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "BlaBlacaraz";
            });
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });
            services.Configure<EmailSettings>(Configuration.GetSection(EmailSettings.EmailSetting));
            services.Configure<GooglePlacesSettings>(Configuration.GetSection(GooglePlacesSettings.GooglePlacesSetting));

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllersWithViews().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization().AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));
                });
            services.AddRazorPages();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                new CultureInfo("en"),
                new CultureInfo("az")
            };

                // State what the default culture for your application is. This will be used if no specific culture
                // can be determined for a given request.
                options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");

                // You must explicitly state which cultures your application supports.
                // These are the cultures the app supports for formatting numbers, dates, etc.
                options.SupportedCultures = supportedCultures;

                // These are the cultures the app supports for UI strings, i.e. we have localized resources for.
                options.SupportedUICultures = supportedCultures;
            });

            // The Tempdata provider cookie is not essential. Make it essential
            // so Tempdata is functional when tracking is disabled.
            services.Configure<CookieTempDataProviderOptions>(options => {
                options.Cookie.IsEssential = true;
            });

            services.AddScoped(typeof(IRepo<>), typeof(RepoBase<>));
            services.AddScoped(typeof(IService<>), typeof(ServiceBase<>));
            services.AddTransient<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

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
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
