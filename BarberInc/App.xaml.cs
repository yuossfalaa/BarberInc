global using Application = System.Windows.Application;
global using MessageBox = System.Windows.MessageBox;
global using UserControl = System.Windows.Controls.UserControl;
using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BarberInc.HostBuilders;

namespace BarberInc
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        public App()
        {
 
            try
            {
                _host = CreateHostBuilder().Build();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host
                .CreateDefaultBuilder(args)
                .AddViewModels()
                .AddViews()
                .AddState()
                .UseDefaultServiceProvider(options =>
                    options.ValidateScopes = false);
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}


