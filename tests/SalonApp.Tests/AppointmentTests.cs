using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalonApp.Modules.Appointments.Domain;

namespace SalonApp.Tests
{
    public class AppointmentTests
    {
        [Fact]
        public void Cancel_ShouldChangeStatusToCancelled()
        {
            // Arrange
            var appointment = new Appointment
            {
                ClientId = 1,
                StaffMemberId = 1,
                ServiceId = 1,
                DateTime = DateTime.Now,
                DurationInMinutes = 30,
                Status = AppointmentStatus.Scheduled
            };

            // Act
            appointment.Cancel();

            // Assert
            Assert.Equal(AppointmentStatus.Cancelled, appointment.Status);
        }

        [Fact]
        public void Complete_ShouldChangeStatusToCompleted()
        {
            // Arrange
            var appointment = new Appointment
            {
                ClientId = 1,
                StaffMemberId = 1,
                ServiceId = 1,
                DateTime = DateTime.Now,
                DurationInMinutes = 30
            };

            // Act
            appointment.Complete();

            // Assert
            Assert.Equal(AppointmentStatus.Completed, appointment.Status);
        }

        [Fact]
        public void EndTime_ShouldBeStartPlusDuration()
        {
            // Arrange
            var start = new DateTime(2026, 7, 1, 10, 0, 0);
            var appointment = new Appointment
            {
                DateTime = start,
                DurationInMinutes = 45
            };

            // Act
            var endTime = appointment.EndTime;

            // Assert
            Assert.Equal(new DateTime(2026, 7, 1, 10, 45, 0), endTime);
        }
    }
}
