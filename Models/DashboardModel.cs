namespace QuizManagementSystem.Models
{
    public class DashboardModel
    {
        public int TotalUsers { get; set; }
        public int TotalQuizes { get; set; }
        public int TotalQuestions { get; set; }

        public List<User> RecentUsers { get; set; }
        public List<Quiz> RecentQuizes { get; set; }
        public List<Question> RecentQuestions { get; set; }
    }

    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
    }

    public class Quiz
    {
        public int QuizID { get; set; }
        public string QuizName { get; set; }
    }

    public class Question
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
    }
}