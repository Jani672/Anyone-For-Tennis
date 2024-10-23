using assignment3.Models;
using Microsoft.EntityFrameworkCore;

namespace assignment3.Service{


public class AdminService
{
    private readonly ApplicationDbContext _context;

    public AdminService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CreateSchedule(string name, DateTime date, string location, int coachId, string description)
    {
        var schedule = new Schedule
        {
            Name = name,
            Date = date,
            Location = location,
            Description = description,
            CoachID = coachId
        };
        _context.Schedules.Add(schedule);
        _context.SaveChanges();
    }

    public void MatchCoachToSchedule(int scheduleId, int coachId)
    {
        var schedule = _context.Schedules.Find(scheduleId);
        if (schedule != null)
        {
            schedule.CoachID = coachId; // Update the coach ID
            _context.SaveChanges();
        }
    }

    public List<Member> ViewMembers()
    {
        return _context.Members.ToList();
    }

    public List<Coach> GetCoaches()
    {
        return _context.Coaches.ToList();
    }

    public Coach GetCoachById(int coachId)
    {
        return _context.Coaches
            .FirstOrDefault(c => c.CoachID == coachId);
    }

    public List<Schedule> GetSchedules()
    {
        return _context.Schedules.Include(s => s.Coach).ToList();
    }

    public List<Schedule> GetSchedulesByCoach(int coachId)
    {
        return _context.Schedules
            .Where(s => s.CoachID == coachId)
            .ToList();
    }

    public Schedule GetScheduleById(int id)
    {
        return _context.Schedules.FirstOrDefault(s => s.ScheduleID == id);
    }

    public bool UpdateSchedule(int id, Schedule updatedSchedule)
    {
        var existingSchedule = _context.Schedules.FirstOrDefault(s => s.ScheduleID == id);
        if (existingSchedule != null)
        {
            existingSchedule.Name = updatedSchedule.Name;
            existingSchedule.Date = updatedSchedule.Date;
            existingSchedule.Location = updatedSchedule.Location;
            existingSchedule.Description = updatedSchedule.Description;
            existingSchedule.CoachID = updatedSchedule.CoachID;

            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool DeleteSchedule(int id)
    {
        var schedule = _context.Schedules.FirstOrDefault(s => s.ScheduleID == id);
        if (schedule != null)
        {
            _context.Schedules.Remove(schedule);
            _context.SaveChanges();
            return true;
        }
        return false;
    }



}
}