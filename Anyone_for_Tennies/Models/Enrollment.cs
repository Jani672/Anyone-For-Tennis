// Models/Enrollment.cs
namespace Anyone_for_Tennies.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int MemberId { get; set; }  // Foreign Key to Members
        public int ScheduleId { get; set; }  // Foreign Key to Schedules
        public DateTime? EnrollmentDate { get; set; } = DateTime.Now;
        public bool? Active { get; set; }

        // Navigation Properties
        public Member? Member { get; set; }
        public Schedule? Schedule { get; set; }
    }
}
