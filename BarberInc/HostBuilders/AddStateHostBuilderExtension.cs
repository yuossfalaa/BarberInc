using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberInc.States.Authenticators;
using BarberInc.States.Navigators;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.DomainServices.UserServices;

namespace BarberInc.HostBuilders
{
    public static class AddStateHostBuilderExtension
    {
        public static IHostBuilder AddState(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<INavigator, Navigator>();
                services.AddSingleton<IAuthenticators, Authenticators>();




            });
            return host;
        }
    }
}
