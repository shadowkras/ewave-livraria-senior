using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BibliotecaVirtual
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var webHost = Host.CreateDefaultBuilder(args)
                                .ConfigureWebHostDefaults(webBuilder =>
                                {
                                    webBuilder.ConfigureAppConfiguration(AddDbConfiguration);
                                    webBuilder.UseStartup<Startup>();
                                });
            return webHost;
        }

        #region Configuração do banco de dados (database.json)

        /// <summary>
        /// Método de leitura do arquivo de configuração do banco de dados.
        /// </summary>
        /// <param name="context">Contexto do WebHostBuilder (Program.cs)</param>
        /// <param name="builder">Interface do IConfigurationBuilder.</param>
        private static void AddDbConfiguration(WebHostBuilderContext context, IConfigurationBuilder builder)
        {
            var configuration = builder.Build();
            builder.AddJsonFile("database.json", optional: false, reloadOnChange: true);
        }

        #endregion
    }
}
