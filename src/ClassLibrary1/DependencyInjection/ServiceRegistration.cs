using Microsoft.Extensions.DependencyInjection;
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
            services.AddSingleton<IRepository<Appointment>>(new JsonRepository<Appointment>(dataDirectory));
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
    }
}
