using System.ComponentModel.DataAnnotations;

namespace QuizManagementSystem.Models
{
    public class QuestionLevelModel
    {
        public int QuestionLevelID { get; set; }

        [Required(ErrorMessage = "Please Enter QuestionLevel!")]
        public string QuestionLevel { get; set; }

        //[Required(ErrorMessage = "Pleaes Enter UserName!")]
        //public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter UserName!")]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "CreationDate")]
        public DateTime CreationDate { get; set; }
    }

    public class QuestionLevelDropDownModel
    {
        public int QuestionLevelID { get; set; }
        public string QuestionLevel { get; set; }
    }

}