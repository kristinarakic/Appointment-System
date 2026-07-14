using Microsoft.EntityFrameworkCore;
using SalonApp.Modules.Appointments.Domain;
using SalonApp.Modules.Clients.Domain;
using SalonApp.Modules.Notifications.Domain;
using SalonApp.Modules.Services.Domain;
using SalonApp.Modules.Staff.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SalonApp.Infrastructure.Persistence.Sqlite;

public class AppDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<StaffMember> StaffMembers { get; set; }
    public DbSet<WorkingSchedule> WorkingSchedules { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

}