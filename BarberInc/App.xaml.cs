global using Application = System.Windows.Application;
global using MessageBox = System.Windows.MessageBox;
global using UserControl = System.Windows.Controls.UserControl;
using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BarberInc.HostBuilders;
using Squirrel;
using EntityFramework;
using Microsoft.EntityFrameworkCore;

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
                .AddServices()
                .AddDbContext()
                .UseDefaultServiceProvider(options =>
                    options.ValidateScopes = false);
        }
        private async Task DbContextCreator()
        {
            using (DBContext dBContext = _host.Services.GetRequiredService<DBContextFactory>().CreateDbContext())
            {

                await dBContext.Database.MigrateAsync();

            }

        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            SquirrelAwareApp.HandleEvents(
                onInitialInstall: OnAppInstall,
                onAppUninstall: OnAppUninstall,
                onEveryRun: OnAppRun);
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
            await DbContextCreator();
            base.OnStartup(e);
        }
        private static void OnAppInstall(SemanticVersion version, IAppTools tools)
        {
            tools.CreateShortcutForThisExe(ShortcutLocation.StartMenu | ShortcutLocation.Desktop);
        }

        private static void OnAppUninstall(SemanticVersion version, IAppTools tools)
        {
            tools.RemoveShortcutForThisExe(ShortcutLocation.StartMenu | ShortcutLocation.Desktop);
        }

        private static void OnAppRun(SemanticVersion version, IAppTools tools, bool firstRun)
        {
            tools.SetProcessAppUserModelId();
            // show a welcome message when the app is first installed
            if (firstRun) MessageBox.Show("App Installed, Thank you for waiting!");
        }
    }
}


