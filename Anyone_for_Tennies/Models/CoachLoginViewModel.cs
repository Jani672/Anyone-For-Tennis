using System.ComponentModel.DataAnnotations;

namespace Anyone_for_Tennies.Models
{
    public class CoachLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
