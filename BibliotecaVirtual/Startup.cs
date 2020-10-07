using System;
using System.Globalization;
using BibliotecaVirtual.Application.Helpers;
using BibliotecaVirtual.Application.Interfaces;
using BibliotecaVirtual.Application.Services;
using BibliotecaVirtual.Data;
using BibliotecaVirtual.Data.Interfaces;
using BibliotecaVirtual.Data.Repositories;
using BibliotecaVirtual.Data.UnitOfWork;
using BibliotecaVirtual.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace BibliotecaVirtual
{
    public class Startup
    {
        public Startup(IConfiguration configuration,
                       IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region Obtendo informações do settings

            services.Configure<Settings>(Configuration.GetSection(nameof(Settings)));

            #endregion

            #region Compressão

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

            #endregion

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddEntityFrameworkNpgsql();

            var connectionString = new Npgsql.NpgsqlConnectionStringBuilder(Configuration.GetConnectionString("DefaultConnection"))
            {
                // Connecting to a local proxy that does not support ssl.
                SslMode = Npgsql.SslMode.Disable
            }.ToString();

            services.AddDbContext<IdentityFrameworkDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            }).AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            //services.BuildServiceProvider();

            #region Default Identity, UI, Framework

            services.AddIdentity<IdentityUser, IdentityRole>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = false;
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequiredLength = 8;
            }).AddDefaultUI()
              .AddEntityFrameworkStores<IdentityFrameworkDbContext>()
              .AddDefaultTokenProviders()
              .AddErrorDescriber<PortugueseIdentityErrorDescriber>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

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

            services.Configure<MvcOptions>(options =>
            {
                #region Cache Profiles

                options.CacheProfiles.Add("UserYearCache", new CacheProfile()
                {
                    Location = ResponseCacheLocation.Client,
                    VaryByHeader = "User-Agent,Accept-Encoding,Version",
                    VaryByQueryKeys = new string[] { "*" },
                    Duration = 86400 * 365, //Um ano
                });

                options.CacheProfiles.Add("UserDayCache", new CacheProfile()
                {
                    Location = ResponseCacheLocation.Client,
                    VaryByHeader = "User-Agent,Accept-Encoding,Version",
                    VaryByQueryKeys = new string[] { "*" },
                    Duration = 86400 * 1, //Um dia
                });

                options.CacheProfiles.Add("UserHourCache", new CacheProfile()
                {
                    Location = ResponseCacheLocation.Client,
                    VaryByHeader = "User-Agent,Accept-Encoding,Version",
                    VaryByQueryKeys = new string[] { "*" },
                    Duration = 60 * 60, //Uma hora
                });

                options.CacheProfiles.Add("UserMinuteCache", new CacheProfile()
                {
                    Location = ResponseCacheLocation.Client,
                    VaryByHeader = "User-Agent,Accept-Encoding,Version",
                    VaryByQueryKeys = new string[] { "*" },
                    Duration = 60, //Um minuto
                });

                #endregion
            });

            #endregion

            #region Cookie Policy

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            #endregion

            #region Injeção de dependencia

            services.AddOptions();

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

            services.AddTransient<IApplicationUnitOfWork, ApplicationUnitOfWork>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IPublisherService, PublisherService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserPermissionService, UserPermissionService>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IPublisherRepository, PublisherRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IBookCategoryRepository, BookCategoryRepository>();
            services.AddTransient<IEmailSender, EmailSender>();
            

            #endregion

            #region Resolução de IoC

            //var container = BootStrapper.Container;
            //container.Configure(config =>
            //{
            //    config.AddRegistry(new BootstrapperApplication());
            //    config.Populate(services);
            //});
            ////StructureMapDependencyResolver.ContainerAcesso = () => container;
            ////container.Populate(services);

            //var serviceProvider = container.GetInstance<IServiceProvider>();

            #endregion

            #region Google Cloud Keys

            if (Environment.IsProduction() == true)
            {
                services.AddDataProtection()
                        .PersistKeysToGoogleCloudStorage(
                            "my-bucket", "DataProtectionKeys.xml")
                        .ProtectKeysWithGoogleKms(
                            "projects/e-topic-291313/locations/global/keyRings/BibliotecaVirtualKey/cryptoKeys/BibliotecaVirtualKey");
            }

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              IOptions<Settings> settings)
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
                        context.Context.Response.Headers.Add("Version", new[] { settings.Value.AplicacaoVersao });
                    }
                },
            });

            #endregion

            #region Response Cache

            app.UseResponseCaching();

            #endregion

            #region Versão

            //app.Use(async (context, next) =>
            //{
            //    context.Response.Headers.Append("Version", settings.Value.AplicacaoVersao);
            //    await next();
            //});

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
                routes.MapRoute(name: "areaRoute",
                        template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            #endregion

            #region Atualizar o banco de dados

            UpdateDatabase(app);

            #endregion
        }

        /// <summary>
        /// Atualiza o banco de dados executando os migrations dos contextos da aplicação e identity.
        /// </summary>
        /// <param name="app"></param>
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                                                             .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<IdentityFrameworkDbContext>())
                {
                    try
                    {
                        context.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debugger.Break();
                        //TODO Logar e tratar
                    }
                }

                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    try
                    {
                        context.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debugger.Break();
                        //TODO Logar e tratar
                    }
                }
            }
        }
    }
}


