using Microsoft.Extensions.DependencyInjection;
using SalonApp.Infrastructure.Persistence.Sqlite;
using SalonApp.Infratructure.DependencyInjection;
using SalonApp.WPF.ViewModels;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace SalonApp.WPF
{

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

            // Za SQLite:
            // services.AddSalonServicesWithSqlite(Path.Combine(dataDirectory, "salon.db"));

            //ViewModels
            services.AddTransient<ClientsViewModel>();
            services.AddTransient<ServicesViewModel>();
            services.AddTransient<MainWindow>();
            services.AddTransient<StaffViewModel>();
            services.AddTransient<AppointmentsViewModel>();

            _serviceProvider = services.BuildServiceProvider();

            using (var scope = _serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
            }

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
