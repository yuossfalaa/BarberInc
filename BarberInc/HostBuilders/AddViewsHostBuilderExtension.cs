using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberInc.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BarberInc.HostBuilders
{ 
        static class AddViewsHostBuilderExtension
        {
            public static IHostBuilder AddViews(this IHostBuilder host)
            {
                host.ConfigureServices(services =>
                {
                    services.AddSingleton<MainWindow>();
                });
                return host;
            }
        }
}
