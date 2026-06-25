using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonApp.SharedKernel.Events
{
    public class AppointmentCreatedEvent : IDomainEvent
    {
        public int AppointmentId { get; }
        public int ClientId { get; }
        public int StaffMemberId { get; }
        public DateTime AppointmentDateTime { get; }
        public DateTime OccurredOn { get; }
    
        public AppointmentCreatedEvent(int appointmentId, int clientId, int staffMemberId, DateTime appointmentDateTime)
        {
            AppointmentId = appointmentId;
            ClientId = clientId;
            StaffMemberId = staffMemberId;
            AppointmentDateTime = appointmentDateTime;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
