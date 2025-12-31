using System.ComponentModel.DataAnnotations;

namespace QuizManagementSystem.Models
{
    public class QuizModel
    {
        public int QuizID { get; set; }

        [Required(ErrorMessage = "Please Enter QuizName!")]
        public string QuizName { get; set; }

        [Required(ErrorMessage = "Please Enter TotalQuestions!")]
        public string TotalQuestions { get; set; }

        [Required(ErrorMessage = "Please Enter QuizDate!")]
        public DateTime QuizDate { get; set; }

        //[Required(ErrorMessage = "Please Enter UserName!")]
        //public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter UserID!")]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "CreationDate")]
        public DateTime CreationDate { get; set; }
    }
    public class QuizDropDownModel
    {
        public int QuizID { get; set; }
        public string QuizName { get; set; }

    }
}