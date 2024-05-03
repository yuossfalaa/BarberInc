using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BarberInc.Models;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace BarberInc.HostBuilders
{
    public static class AddDbContextHostBuilderExtension
    {
        private static Assembly assembly;

        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {

            host.ConfigureServices((context, services) =>
            {
                Action<DbContextOptionsBuilder> configerdbContext;


                configerdbContext = o => o.UseSqlServer(GetConnectionString(),
             x =>
             {
                 x.EnableRetryOnFailure(maxRetryCount: 9999, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                 x.CommandTimeout(300);
             });



                services.AddSingleton(new DBContextFactory(configerdbContext));
                services.AddDbContext<DBContext>(configerdbContext);
            });


            return host;
        }

        private static string GetConnectionString()
        {
            App_Settings Settings = new App_Settings();
            return Settings.appSettings.ConnectionString;
        }
    }
}

