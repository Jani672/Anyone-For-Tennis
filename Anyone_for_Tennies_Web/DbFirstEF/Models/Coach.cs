namespace DbFirstEF.Models
{
    public class Coach
    {
        public int CoachId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; } // Make sure this is string as well
    }
}
