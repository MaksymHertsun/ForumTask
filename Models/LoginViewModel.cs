using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class LoginViewModel : User
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
