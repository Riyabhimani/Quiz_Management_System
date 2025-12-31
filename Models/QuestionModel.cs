using System.ComponentModel.DataAnnotations;

namespace QuizManagementSystem.Models
{
    public class QuestionModel
    {
        public int QuestionID { get; set; }

        [Required(ErrorMessage = "Please Enter QuestionText!")]
        public string QuestionText { get; set; }

        //[Required(ErrorMessage = "Please Enter Question Level!")]
        //public string QuestionLevel { get; set; }

        [Required(ErrorMessage = "Please Enter Question LevelID!")]
        public int QuestionLevelID { get; set; }

        [Required(ErrorMessage = "Please Enter OptionA!")]
        public string OptionA { get; set; }

        [Required(ErrorMessage = "Please Enter OptionB!")]
        public string OptionB { get; set; }

        [Required(ErrorMessage = "Please Enter OptionC!")]
        public string OptionC { get; set; }

        [Required(ErrorMessage = "Please Enter OptionD!")]
        public string OptionD { get; set; }

        [Required(ErrorMessage = "Please Enter CurrectOption!")]
        public string CorrectOption { get; set; }

        [Required(ErrorMessage = "Please Enter QuestionMarks!")]
        public string QuestionMarks { get; set; }

        //[Required(ErrorMessage = "Please Enter UserName!")]
        //public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter UserID!")]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "CreationDate")]
        public DateTime CreationDate { get; set; }
    }

    public class QuestionDropDownModel
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
    }

}