using System.ComponentModel.DataAnnotations;

namespace QuizManagementSystem.Models
{
    public class UserRegisterModel
    {
        public int UserID { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Mobile { get; set; }
    }
}
