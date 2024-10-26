using Anyone_for_Tennies.Data;
using Anyone_for_Tennies.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Anyone_for_Tennies.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly AppDbContext _context;

        public ScheduleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Schedule> CreateAsync(ScheduleCreateViewModel model)
        {
            var schedule = new Schedule
            {
                EventName = model.EventName,
                Date = model.Date,
                Time = model.Time,
                DayOfWeek = model.DayOfWeek,
                CoachID = model.CoachID,
                AvailableSlots = model.AvailableSlots,
                Location = model.Location,
                Description = model.Description,
                Active = model.Active
            };
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task<Schedule> GetByIdAsync(int id)
        {
            return await _context.Schedules.FindAsync(id);
        }

        public async Task<Schedule> EditAsync(int id, ScheduleEditViewModel model)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null) return null;

            schedule.EventName = model.EventName;
            schedule.Date = model.Date;
            schedule.Time = model.Time;
            schedule.DayOfWeek = model.DayOfWeek;
            schedule.CoachID = model.CoachID;
            schedule.AvailableSlots = model.AvailableSlots;
            schedule.Location = model.Location;
            schedule.Description = model.Description;
            schedule.Active = model.Active;

            _context.Schedules.Update(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null) return false;

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
