using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SalonApp.Modules.Appointments.Application;
using SalonApp.Modules.Appointments.Domain;
using SalonApp.SharedKernel;

namespace SalonApp.Tests
{
    public class SchedulingServiceTests
    {
        private readonly Mock<IAppointmentRepository> _mockRepo;
        private readonly Mock<IEventDispatcher> _mockDispatcher;
        private readonly SchedulingService _service;

        public SchedulingServiceTests()
        {
            _mockRepo = new Mock<IAppointmentRepository>();
            _mockDispatcher = new Mock<IEventDispatcher>();
            _service = new SchedulingService(_mockRepo.Object, _mockDispatcher.Object);
        }

        [Fact]
        public async Task FindAvailableSlots_NoBookings_ReturnsAllSlots()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByStaffAndDateAsync(1, It.IsAny<DateTime>()))
                .ReturnsAsync(new List<Appointment>());

            // Act
            var slots = await _service.FindAvailableSlotsAsync(
                staffMemberId: 1,
                date: DateTime.Today,
                durationInMinutes: 30,
                workStartTime: new TimeSpan(9, 0, 0),
                workEndTime: new TimeSpan(12, 0, 0));

            // Assert
            Assert.Equal(6, slots.Count); // 9:00, 9:30, 10:00, 10:30, 11:00, 11:30
        }

        [Fact]
        public async Task FindAvailableSlots_WithBooking_ExcludesOccupiedSlot()
        {
            // Arrange
            var existing = new List<Appointment>
        {
            new Appointment
            {
                DateTime = DateTime.Today.AddHours(10),
                DurationInMinutes = 30,
                StaffMemberId = 1,
                Status = AppointmentStatus.Scheduled
            }
        };

            _mockRepo.Setup(r => r.GetByStaffAndDateAsync(1, It.IsAny<DateTime>()))
                .ReturnsAsync(existing);

            // Act
            var slots = await _service.FindAvailableSlotsAsync(
                staffMemberId: 1,
                date: DateTime.Today,
                durationInMinutes: 30,
                workStartTime: new TimeSpan(9, 0, 0),
                workEndTime: new TimeSpan(12, 0, 0));

            // Assert
            Assert.DoesNotContain(new TimeSpan(10, 0, 0), slots);
            Assert.Contains(new TimeSpan(9, 0, 0), slots);
            Assert.Contains(new TimeSpan(10, 30, 0), slots);
        }

        [Fact]
        public async Task CreateAppointment_ShouldDispatchEvent()
        {
            // Arrange
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Appointment>()))
                .Returns(Task.CompletedTask);
            _mockRepo.Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);
            _mockDispatcher.Setup(d => d.DispatchAsync(It.IsAny<IDomainEvent>()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.CreateAppointmentAsync(1, 1, DateTime.Now, 1, 30);

            // Assert
            _mockDispatcher.Verify(d => d.DispatchAsync(It.IsAny<IDomainEvent>()), Times.Once);
        }
    }
}
