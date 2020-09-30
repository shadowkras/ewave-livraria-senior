using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BibliotecaVirtual.Data;
using BibliotecaVirtual.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BibliotecaVirtual
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
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Fastest);
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = new[]
                {
                    // Default
                    "text/plain",
                    "text/css",
                    "application/javascript",
                    "text/html",
                    "application/xml",
                    "text/xml",
                    "application/json",
                    "text/json",
                    "image/png",
                    "image/x-icon",
                    "application/font-woff",
                    "font/woff2",
                };
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            #region Default Identity, UI, Framework

            services.AddIdentity<IdentityUser, IdentityRole>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = true;
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequiredLength = 8;
            }).AddDefaultUI()
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders()
              .AddErrorDescriber<PortugueseIdentityErrorDescriber>();

            services.AddRazorPages();

            #endregion

            #region Cultura

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { new CultureInfo("pt-BR") };
                var culture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR");

                #region Default Culture

                options.DefaultRequestCulture = culture;
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                #endregion
            });

            #endregion

            #region Response Cache

            services.AddResponseCaching(options =>
            {
                options.UseCaseSensitivePaths = false;
            });

            #endregion

            #region Cookie Policy

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            #endregion

            #region Mvc

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            #endregion

            #region Services

            //services.AddTransient<IAuthorService, AuthorService>();
            //services.AddTransient<IPublisherService, PublisherService>();
            //services.AddTransient<ICategoryService, CategoryService>();
            //services.AddTransient<IBookService, BookService>();
            //services.AddTransient<IAuthorRepository, AuthorRepository>();
            //services.AddTransient<IPublisherRepository, PublisherRepository>();
            //services.AddTransient<ICategoryRepository, CategoryRepository>();
            //services.AddTransient<IBookRepository, BookRepository>();
            //services.AddTransient<IBookCategoryRepository, BookCategoryRepository>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                              ApplicationDbContext dbContext)
        {
            #region Compressão

            app.UseResponseCompression();

            #endregion

            #region Erros

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            #endregion

            #region Https

            app.UseHttpsRedirection();

            #endregion

            #region Static Files

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context =>
                {
                    // Manter cache estático por 1 ano, arquivos versionados são atualizados automaticamente.
                    if (!string.IsNullOrEmpty(context.Context.Request.Query["v"]))
                    {
                        context.Context.Response.Headers.Add("cache-control", new[] { "public,max-age=31536000,immutable,vary-by-header=host;version" });
                        context.Context.Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") });
                    }
                },
            });

            #endregion

            #region Response Cache

            app.UseResponseCaching();

            #endregion

            #region Cookies

            app.UseCookiePolicy();

            #endregion

            #region Autenticação

            app.UseAuthentication();

            #endregion

            #region Rotas

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(name: "areaRoute",
                        template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });

            #endregion

            #region Migrations

            dbContext.Database.Migrate();

            #endregion
        }
    }
}
