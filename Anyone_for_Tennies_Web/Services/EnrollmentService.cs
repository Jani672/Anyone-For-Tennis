// Services/EnrollmentService.cs
using Anyone_for_Tennies.Data;
using Anyone_for_Tennies.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anyone_for_Tennies.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly AppDbContext _context;

        public EnrollmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Enrollment> CreateAsync(EnrollmentCreateViewModel model)
        {
            var enrollment = new Enrollment
            {
                MemberId = model.MemberId,
                ScheduleId = model.ScheduleId,
                EnrollmentDate = DateTime.Now,
                Active = true
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return enrollment;
        }

        public async Task<IEnumerable<Enrollment>> GetByMemberIdAsync(int memberId)
        {
            var enrollments = await _context.Enrollments
                                            .Include(e => e.Schedule)
                                            .Include(e => e.Member)
                                            .Where(e => e.MemberId == memberId)
                                            .ToListAsync();

            foreach (var enrollment in enrollments)
            {
                enrollment.Schedule!.Date = enrollment.Schedule.Date ?? DateTime.MinValue;
                enrollment.Schedule.Time = enrollment.Schedule.Time ?? TimeSpan.Zero;
                enrollment.Schedule.Description ??= "No Description";
                enrollment.Schedule.DayOfWeek ??= "Unknown";
                enrollment.Schedule.Location ??= "Not specified";

            }

            return enrollments;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null) return false;

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
