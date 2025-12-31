using System.ComponentModel.DataAnnotations;

namespace QuizManagementSystem.Models
{
    public class QuizWizeQuestionsModel
    {
        [Required]
        public int QuizWiseQuestionsID { get; set; }

        [Required(ErrorMessage = "Please Enter QuizName!")]
        public string QuizName { get; set; }

        [Required(ErrorMessage = "Please Enter QuizID!")]
        public int QuizID { get; set; }

        [Required(ErrorMessage = "Please Enter QuestionID!")]
        public int QuestionID { get; set; }

        [Required(ErrorMessage = "Please Enter UserID!")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please Enter QuestionText!")]
        public string QuestionText { get; set; }

        [Required(ErrorMessage = "Please Enter UserName!")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "CreationDate")]
        public DateTime CreationDate { get; set; }

    }
    public class QuizWiseQuestionDropDownModel
    {
        public int QuizWiseQuestionID { get; set; }
        public string QuizName { get; set; }

        public string QuestionText { get; set; }

        public string UserID { get; set; }

    }
}