using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberInc.Factories;
using BarberInc.States.Navigators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BarberInc.HostBuilders
{
    public static class AddViewModelsHostBuilderExtension
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {

                #region ViewModels
                services.AddTransient<MainViewModel>();
                services.AddTransient<HomeViewModel>();
                services.AddTransient<LoginViewModel>();
                services.AddTransient<SignUpViewModel>();
                services.AddTransient<ReservationViewModel>();
                services.AddTransient<AdminDashViewModel>();
                services.AddTransient<AdminReservationsPanalViewModel>();
                services.AddTransient<AdminUserPanalViewModel>();

                #endregion

                #region CreateViewModel
                services.AddSingleton<CreateViewModel<HomeViewModel>>(services => () => services.GetRequiredService<HomeViewModel>());
                services.AddSingleton<CreateViewModel<LoginViewModel>>(services => () => services.GetRequiredService<LoginViewModel>());
                services.AddSingleton<CreateViewModel<SignUpViewModel>>(services => () => services.GetRequiredService<SignUpViewModel>());
                services.AddSingleton<CreateViewModel<ReservationViewModel>>(services => () => services.GetRequiredService<ReservationViewModel>());
                services.AddSingleton<CreateViewModel<AdminDashViewModel>>(services => () => services.GetRequiredService<AdminDashViewModel>());

                #endregion

                #region ViewModelDelegateRenavigator
                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
                services.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<SignUpViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<ReservationViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<AdminDashViewModel>>();

                #endregion

            });
            return host;
        }
    }
}
