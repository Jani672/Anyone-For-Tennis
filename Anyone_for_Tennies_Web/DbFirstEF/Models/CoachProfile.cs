using System.ComponentModel.DataAnnotations.Schema;

namespace DbFirstEF.Models
{
    public class CoachProfile
    {
        public int CoachId { get; set; }
        public string Biography { get; set; }
    }

}
