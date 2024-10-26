namespace Anyone_for_Tennies.Models
{
    public class ScheduleCreateViewModel
    {
        public string EventName { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public string DayOfWeek { get; set; }
        public int? CoachID { get; set; }
        public int? AvailableSlots { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
    }

    public class ScheduleEditViewModel : ScheduleCreateViewModel
    {
        public int ScheduleId { get; set; }
    }
}
