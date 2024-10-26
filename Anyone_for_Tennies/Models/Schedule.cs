// Models/Schedule.cs
namespace Anyone_for_Tennies.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public string EventName { get; set; }
        public DateTime? Date { get; set; }  
        public TimeSpan? Time { get; set; } 
        public string? DayOfWeek { get; set; }  // Nullable string for DayOfWeek
        public int? CoachID { get; set; }  // Nullable CoachID
        public int? AvailableSlots { get; set; }
        public string? Location { get; set; }  // Nullable Location
        public string? Description { get; set; }  // Nullable Description
        public bool? Active { get; set; }
    }
}
