using SalonApp.Modules.Notifications.Domain;
using SalonApp.SharedKernel;
using SalonApp.SharedKernel.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonApp.Modules.Notifications.Application
{
    public class AppointmentCreatedHandler : IDomainEventHandler<AppointmentCreatedEvent>
    {
        private readonly IRepository<Notification> _repository;
        
        public AppointmentCreatedHandler(IRepository<Notification> repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(AppointmentCreatedEvent domainEvent)
        {
            var notification = new Notification
            {
                Type = "AppointmentCreated",
                Message = $"Appointment #{domainEvent.AppointmentId} created for {domainEvent.AppointmentDateTime:dd.MM.yyyy HH:mm}",
                CreatedAt = domainEvent.OccurredOn
            };
            await _repository.AddAsync(notification);
            await _repository.SaveChangesAsync();
        }
    }
}
