using System.ComponentModel.DataAnnotations;
using System.Data;

namespace QuizManagementSystem.Models
{
    public class UserModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please Enter UserName!")]
        public string UserName { get; set; }

        [Required]
        //[RegularExpression(@"^(?=.[A-Z])(?=.\d)[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Password must be at least 6 characters, include one uppercase letter, and one number")]
        public string Password { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email format!")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits")]
        public string Mobile { get; set; }

        [Required]
        public int? IsActive { get; set; }

        [Required]
        public int? IsAdmin { get; set; }

        [Required]
        [Display(Name = "CreationDate")]
        public DateTime CreationDate { get; set; }
    }
    public class UserDropDownModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
    }

}