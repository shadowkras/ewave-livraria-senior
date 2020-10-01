using BibliotecaVirtual.Application.Helpers;
using BibliotecaVirtual.Application.Interfaces;
using BibliotecaVirtual.Application.Services;
using BibliotecaVirtual.Data.Extensions;
using BibliotecaVirtual.Data.Interfaces;
using BibliotecaVirtual.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaVirtual.DependencyInjection
{
    public class BootStrapper
    {
        #region Instância

        private static IContainer _iContainer;

        public static IContainer Container => _iContainer ?? (_iContainer = Registrar());

        #endregion

        #region Registrar - Injenção de Dependência

        private static IContainer Registrar()
        {
            var container = new Container(p =>
            {
                //p.For<IHttpContextAccessor>().Use<HttpContextAccessor>();
                p.AddRegistry<Application.IoC.BootstrapperApplication>();
                p.For<IOptionsFactory<Settings>>().Use<OptionsFactory<Settings>>();
                //p.For<IOptionsFactory<EmailSettings>>().Use<OptionsFactory<EmailSettings>>();
                p.For<IOptionsFactory<PasswordHasherOptions>>().Use<OptionsFactory<PasswordHasherOptions>>();
                p.For<IOptionsFactory<IdentityOptions>>().Use<OptionsFactory<IdentityOptions>>();
                //p.For<IUserStore<IdentityUser>>().Use<UserStore<IdentityUser>>();
                p.For<IOptions<IdentityOptions>>().Use<OptionsManager<IdentityOptions>>();
                p.For<IOptions<Settings>>().Use<OptionsManager<Settings>>();
                //p.For<IOptions<EmailSettings>>().Use<OptionsManager<EmailSettings>>();
                p.For<IPasswordHasher<IdentityUser>>().Use<PasswordHasher<IdentityUser>>();
                p.For<IOptions<PasswordHasherOptions>>().Use<OptionsManager<PasswordHasherOptions>>();
                p.For<ILookupNormalizer>().Use<UpperInvariantLookupNormalizer>();
                p.For<IServiceProvider>().Use<StructureMapServiceProvider>();
                p.For<ILogger<IdentityUser>>().Use<Logger<IdentityUser>>();
                p.For<ILogger<UserManager<IdentityUser>>>().Use<Logger<UserManager<IdentityUser>>>();
                p.For<ILoggerFactory>().Use<LoggerFactory>();
                p.For<IHostEnvironment>().Use<HostingEnvironment>();
                p.For<IHostEnvironment>().Use<HostingEnvironment>();
            });

            #region Validação ambiente de programação

            if (Debugger.IsAttached == true)
            {
                try
                {
                    container.AssertConfigurationIsValid();
                    Debug.WriteLine(container.WhatDidIScan());
                }
                catch (StructureMapConfigurationException ex)
                {
                    Console.WriteLine(ex);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.GetMessages());
                    throw ex;
                }
            }

            #endregion

            return container;
        }

        #endregion
    }
}
