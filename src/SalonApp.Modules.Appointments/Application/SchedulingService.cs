using SalonApp.Modules.Appointments.Domain;
using SalonApp.SharedKernel;
using SalonApp.SharedKernel.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SalonApp.Modules.Appointments.Application
{
    public class SchedulingService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public SchedulingService(IAppointmentRepository appointmentRepository, IEventDispatcher eventDispatcher)
        {
            _appointmentRepository = appointmentRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<List<TimeSpan>> FindAvailableSlotsAsync(
            int staffMemberId,
            DateTime date,
            int durationInMinutes,
            TimeSpan workStartTime,
            TimeSpan workEndTime)
        {
            var existingAppointments = await _appointmentRepository.GetByStaffAndDateAsync(staffMemberId, date);

            var booked = existingAppointments
                .Where(a => a.Status == Domain.AppointmentStatus.Scheduled)
                .OrderBy(a => a.DateTime)
                .ToList();

            var availableSlots = new List<TimeSpan>();
            var slotDuration = TimeSpan.FromMinutes(durationInMinutes);
            var currentSlot = workStartTime;

            while (currentSlot + slotDuration <= workEndTime)
            {
                var slotStart = date.Date + currentSlot;
                var slotEnd = slotStart + slotDuration;

                var hasConflict = booked.Any(a => a.DateTime < slotEnd && a.EndTime > slotStart);

                if (!hasConflict)
                {
                    availableSlots.Add(currentSlot);
                }

                currentSlot = currentSlot.Add(TimeSpan.FromMinutes(30));

            }
            return availableSlots;

        }

        public async Task<Appointment> CreateAppointmentAsync(
            int clientId,
            int staffMemberId,
            DateTime appointmentDateTime,
            int serviceId,
            int durationInMinutes)
        {
            var appointment = new Appointment
            {
                ClientId = clientId,
                StaffMemberId = staffMemberId,
                DateTime = appointmentDateTime,
                ServiceId = serviceId,
                DurationInMinutes = durationInMinutes,
                Status = AppointmentStatus.Scheduled
            };

            await _appointmentRepository.AddAsync(appointment);
            await _appointmentRepository.SaveChangesAsync();

            var domainEvent = new AppointmentCreatedEvent(appointment.Id, clientId, staffMemberId, appointmentDateTime);

            await _eventDispatcher.DispatchAsync(domainEvent);

            return appointment;
        }

        public async Task CancelAppointmentAsync(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
                throw new Exception("Appointment not found");

            appointment.Cancel();
            await _appointmentRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByClientAsync(int clientId)
        {
            return await _appointmentRepository.GetByClientAsync(clientId);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByStaffAndDateAsync(int staffMemberId, DateTime date)
        {
            return await _appointmentRepository.GetByStaffAndDateAsync(staffMemberId, date);
        }
    }
}
