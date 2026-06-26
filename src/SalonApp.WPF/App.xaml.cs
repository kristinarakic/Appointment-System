using Microsoft.Extensions.DependencyInjection;
using SalonApp.Infratructure.DependencyInjection;
using SalonApp.WPF.ViewModels;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace SalonApp.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            var dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            Directory.CreateDirectory(dataDirectory);

            services.AddSalonServices(dataDirectory);

            //ViewModels
            services.AddTransient<ClientsViewModel>();
            services.AddTransient<ServicesViewModel>();
            services.AddTransient<MainWindow>();

            _serviceProvider = services.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
