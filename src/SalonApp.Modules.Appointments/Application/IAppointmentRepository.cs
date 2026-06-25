using SalonApp.SharedKernel;
using SalonApp.Modules.Appointments.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonApp.Modules.Appointments.Application
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetByStaffAndDateAsync(int staffMemberId, DateTime date);
        Task<IEnumerable<Appointment>> GetByClientAsync(int clientId);
    }
}
