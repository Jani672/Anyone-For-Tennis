using System.ComponentModel.DataAnnotations;

namespace assignment3.Models{

public class Member
{
    public int MemberID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Active { get; set; }
}

public class Schedule
{
    public int ScheduleID { get; set; }
    public string Name { get; set; }
    [Required]
    public DateTime Date { get; set; }  
    public string Location { get; set; }
    public string Description { get; set; }
    
    [Required]
    public int CoachID { get; set; } // Foreign key for Coach
    public Coach? Coach { get; set; } 

}

public class Coach
{
    public int CoachID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Biography { get; set; }
    public string Photo { get; set; }
    
}

}