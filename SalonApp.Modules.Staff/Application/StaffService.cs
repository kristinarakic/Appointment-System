using SalonApp.Modules.Staff.Domain;
using SalonApp.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonApp.Modules.Staff.Application
{
    public class StaffService
    {
        private readonly IRepository<StaffMember> _staffRepository;
        private readonly IRepository<WorkingSchedule> _workingScheduleRepository;

        public StaffService(IRepository<StaffMember> staffRepository, IRepository<WorkingSchedule> workingScheduleRepository)
        {
            _staffRepository = staffRepository;
            _workingScheduleRepository = workingScheduleRepository;
        }

        public async Task<IEnumerable<StaffMember>> GetAllStaffMembersAsync()
        {
            return await _staffRepository.GetAllAsync();
        }

        public async Task AddStaffMemberAsync(StaffMember staffMember)
        {
            await _staffRepository.AddAsync(staffMember);
            await _staffRepository.SaveChangesAsync();
        }

        public async Task AddScheduleAsync(WorkingSchedule schedule)
        {
            await _workingScheduleRepository.AddAsync(schedule);
            await _workingScheduleRepository.SaveChangesAsync();
        }
        public async Task UpdateStaffMemberAsync(StaffMember staffMember)
        {
            _staffRepository.Update(staffMember);
            await _staffRepository.SaveChangesAsync();
        }

        public async Task DeleteStaffMemberAsync(int id)
        {
            var staff = await _staffRepository.GetByIdAsync(id);
            if (staff == null)
                throw new InvalidOperationException("Worker not found.");

            _staffRepository.Remove(staff);
            await _staffRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<WorkingSchedule>> GetSchedulesByStaffMemberIdAsync(int staffMemberId)
        {
            var allSchedules = await _workingScheduleRepository.GetAllAsync();
            return allSchedules.Where(s => s.StaffMemberId == staffMemberId);
        }
    }
}