using SalonApp.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonApp.Modules.Staff.Domain
{
    public class StaffMember : Entity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty; 

        public string FullName => $"{FirstName} {LastName}";

        public List<WorkingSchedule> WorkingSchedules { get; set; } = new ();
    }
}
