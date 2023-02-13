using HomeWork_29_.Services;
using HomeWork_29_.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Windows;
using HomeWork_29_.Data;

namespace HomeWork_29_
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Window ActiveWindow => Application.Current.Windows
            .OfType<Window>()
            .FirstOrDefault(w=>w.IsActive);

        public static Window FocusedWindow => Application.Current.Windows
            .OfType<Window>()
            .FirstOrDefault(w => w.IsFocused);

        public  static bool IsDesignTime { get; set; } = true;

        public static Window ActivedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);

      
        public static Window CurrentWindow => FocusedWindow ?? ActiveWindow;

        private static IHost __Host;

       
        public static IHost Host => __Host ??= Program
            .CreateHostBuilder(Environment.GetCommandLineArgs())
            .ConfigureAppConfiguration(cfg => cfg.AddJsonFile("appsettings.json", true, true))
            .ConfigureServices((host, services) => services
                .AddViews()
                .AddServices()
                .AddDatabase(host.Configuration.GetSection("Database"))
            )
            .Build();

        public static IServiceProvider Services => Host.Services;

        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesignTime = false;

            var host = Host;

            using (var scope = Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<DBInitializer>().InitialazeAsync().Wait();
            }

            base.OnStartup(e);
            await host.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            using var host = Host;
            await host.StopAsync();
        }

        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
            .AddViews()
            .AddServices()
            .AddDatabase(host.Configuration.GetSection("Database"))
        ;
        
    }
}
