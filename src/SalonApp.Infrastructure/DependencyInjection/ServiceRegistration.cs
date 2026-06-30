using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalonApp.Infrastructure.Persistence.Sqlite;
using SalonApp.Infratructure.Events;
using SalonApp.Infratructure.Persistence;
using SalonApp.Modules.Appointments.Application;
using SalonApp.Modules.Appointments.Domain;
using SalonApp.Modules.Clients.Application;
using SalonApp.Modules.Clients.Domain;
using SalonApp.Modules.Notifications.Application;
using SalonApp.Modules.Notifications.Domain;
using SalonApp.Modules.Services.Application;
using SalonApp.Modules.Services.Domain;
using SalonApp.Modules.Staff.Application;
using SalonApp.Modules.Staff.Domain;
using SalonApp.SharedKernel;
using SalonApp.SharedKernel.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonApp.Infratructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddSalonServices(this IServiceCollection services, string dataDirectory)
        {
            // Repositories
            services.AddSingleton<IRepository<Client>>(new JsonRepository<Client>(dataDirectory));
            services.AddSingleton<IRepository<Service>>(new JsonRepository<Service>(dataDirectory));
            services.AddSingleton<IRepository<StaffMember>>(new JsonRepository<StaffMember>(dataDirectory));
            services.AddSingleton<IRepository<WorkingSchedule>>(new JsonRepository<WorkingSchedule>(dataDirectory));
            services.AddSingleton<IAppointmentRepository>(new JsonAppointmentRepository(dataDirectory));
            services.AddSingleton<IRepository<Notification>>(new JsonRepository<Notification>(dataDirectory));

            // Application services
            services.AddTransient<ClientService>();
            services.AddTransient<ServiceManager>();
            services.AddTransient<StaffService>();
            services.AddTransient<SchedulingService>();

            // Event infrastructure
            services.AddSingleton<IEventDispatcher, EventDispatcher>();
            services.AddTransient<IDomainEventHandler<AppointmentCreatedEvent>, AppointmentCreatedHandler>();

            return services;
        }

        public static IServiceCollection AddSalonServicesWithSqlite(this IServiceCollection services, string dbPath)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

            services.AddScoped<IRepository<Client>, SqliteRepository<Client>>();
            services.AddScoped<IRepository<Service>, SqliteRepository<Service>>();
            services.AddScoped<IRepository<StaffMember>, SqliteRepository<StaffMember>>();
            services.AddScoped<IRepository<WorkingSchedule>, SqliteRepository<WorkingSchedule>>();
            services.AddScoped<IAppointmentRepository, SqliteAppointmentRepository>();
            services.AddScoped<IRepository<Notification>, SqliteRepository<Notification>>();

            services.AddTransient<ClientService>();
            services.AddTransient<ServiceManager>();
            services.AddTransient<StaffService>();
            services.AddTransient<SchedulingService>();

            services.AddSingleton<IEventDispatcher>(sp => { return new EventDispatcher(sp); });
            services.AddTransient<IDomainEventHandler<AppointmentCreatedEvent>, AppointmentCreatedHandler>();

            return services;
        }
    }
}
