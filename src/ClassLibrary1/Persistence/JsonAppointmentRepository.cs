using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalonApp.Modules.Appointments.Application;
using SalonApp.Modules.Appointments.Domain;

namespace SalonApp.Infratructure.Persistence
{
    public class JsonAppointmentRepository : JsonRepository<Appointment>, IAppointmentRepository
    {
        public JsonAppointmentRepository(string dataDirectory) : base(dataDirectory)
        {
        }

        public async Task<IEnumerable<Appointment>> GetByStaffAndDateAsync(int staffMemberId, DateTime date)
        {
            var all = await GetAllAsync();
            return all.Where(a => a.StaffMemberId == staffMemberId && a.DateTime.Date == date.Date);
        }

        public async Task<IEnumerable<Appointment>> GetByClientAsync(int clientId)
        {
            var all = await GetAllAsync();
            return all.Where(a => a.ClientId == clientId);
        }
    }
}
