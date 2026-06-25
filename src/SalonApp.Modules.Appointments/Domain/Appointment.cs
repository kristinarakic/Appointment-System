using SalonApp.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonApp.Modules.Appointments.Domain
{
    public enum AppointmentStatus
    {
        Scheduled,
        Cancelled,
        Completed
    }
    public class Appointment : Entity
    {
        public DateTime DateTime { get; set; }
        public int ClientId { get; set; }
        public int StaffMemberId { get; set; }
        public int ServiceId { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
        public int DurationInMinutes { get; set; }

        public DateTime EndTime => DateTime.AddMinutes(DurationInMinutes);

        public void Cancel()
        {
            Status = AppointmentStatus.Cancelled;
        }
        public void Complete()
        {
            Status = AppointmentStatus.Completed;
        }
    }
}
