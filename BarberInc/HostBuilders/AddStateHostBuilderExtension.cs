using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberInc.States.Navigators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BarberInc.HostBuilders
{
    public static class AddStateHostBuilderExtension
    {
        public static IHostBuilder AddState(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<INavigator, Navigator>();
 

            });
            return host;
        }
    }
}
