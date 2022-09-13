using System.ComponentModel.DataAnnotations;

namespace PhilipsHIS.Models
{
    public class LoginModel
    {
        [Key]
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
