using Microsoft.EntityFrameworkCore;
using SalonApp.Modules.Appointments.Application;
using SalonApp.Modules.Appointments.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonApp.Infrastructure.Persistence.Sqlite;

public class SqliteAppointmentRepository : SqliteRepository<Appointment>, IAppointmentRepository
{
    public SqliteAppointmentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Appointment>> GetByStaffAndDateAsync(int staffMemberId, DateTime date)
    {
        return await _dbSet
            .Where(a => a.StaffMemberId == staffMemberId && a.DateTime.Date == date.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetByClientAsync(int clientId)
    {
        return await _dbSet
            .Where(a => a.ClientId == clientId)
            .ToListAsync();
    }
}
