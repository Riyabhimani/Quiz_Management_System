using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QuizManagementSystem.Models;

namespace QuizManagementSystem.Controllers
{
    [CheckAccess]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private IConfiguration configuration;

        public HomeController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }


        public IActionResult Index()
        {
            //return View();
            DashboardModel model = new DashboardModel();
            string connectionString = configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Total Counts
                SqlCommand cmdUser = new SqlCommand("SELECT COUNT(*) FROM MST_User", conn);
                model.TotalUsers = (int)cmdUser.ExecuteScalar();

                SqlCommand cmdQuiz = new SqlCommand("SELECT COUNT(*) FROM MST_Quiz", conn);
                model.TotalQuizes = (int)cmdQuiz.ExecuteScalar();

                SqlCommand cmdQuestion = new SqlCommand("SELECT COUNT(*) FROM MST_Question", conn);
                model.TotalQuestions = (int)cmdQuestion.ExecuteScalar();

                // Recent Users
                model.RecentUsers = new List<User>();
                SqlCommand cmdRecentUsers = new SqlCommand("SELECT TOP 3 UserID, UserName FROM MST_User ORDER BY UserID DESC", conn);
                using (SqlDataReader reader = cmdRecentUsers.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.RecentUsers.Add(new User
                        {
                            UserID = reader.GetInt32(0),
                            UserName = reader.GetString(1)
                        });
                    }
                }

                // Recent Quizes
                model.RecentQuizes = new List<Quiz>();
                SqlCommand cmdRecentQuizes = new SqlCommand("SELECT TOP 3 QuizID, QuizName FROM MST_Quiz ORDER BY QuizID DESC", conn);
                using (SqlDataReader reader = cmdRecentQuizes.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.RecentQuizes.Add(new Quiz
                        {
                            QuizID = reader.GetInt32(0),
                            QuizName = reader.GetString(1)
                        });
                    }
                }

                // Recent Questions
                model.RecentQuestions = new List<Question>();
                SqlCommand cmdRecentCountries = new SqlCommand("SELECT TOP 3 QuestionID, QuestionText FROM MST_Question ORDER BY QuestionID DESC", conn);
                using (SqlDataReader reader = cmdRecentCountries.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.RecentQuestions.Add(new Question
                        {
                            QuestionID = reader.GetInt32(0),
                            QuestionText = reader.GetString(1)
                        });
                    }
                }
            }

            return View(model); 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
