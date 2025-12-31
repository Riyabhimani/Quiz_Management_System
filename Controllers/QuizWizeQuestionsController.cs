using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using QuizManagementSystem.Models;
using OfficeOpenXml;

namespace QuizManagementSystem.Controllers
{
    [CheckAccess]
    public class QuizWizeQuestionsController : Controller
    {
        private IConfiguration configuration;

        public QuizWizeQuestionsController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        
        public IActionResult QuizWizeQuestionsAdd(int? QuizWiseQuestionsID)
        {
            UserDropDown();
            QuizDropDown();
            QuestionDropDown();
            if (QuizWiseQuestionsID == null)
            {
                var m = new QuizWizeQuestionsModel
                {
                    CreationDate = DateTime.Now
                };
                return View(m);
            }

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_QuizWiseQuestions_SelectByID";
            command.Parameters.AddWithValue("@QuizWiseQuestionsID", QuizWiseQuestionsID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            QuizWizeQuestionsModel model = new QuizWizeQuestionsModel();

            foreach (DataRow dataRow in table.Rows)
            {
                model.QuizID = Convert.ToInt32(dataRow["QuizID"]);
                model.QuestionID = Convert.ToInt32(dataRow["QuestionID"]);
                model.UserID = Convert.ToInt32(dataRow["UserID"]);
            }
            return View(model);
        }

        public IActionResult Save(QuizWizeQuestionsModel model)
        {
            UserDropDown();
            QuizDropDown();
            QuestionDropDown();
            if (!ModelState.IsValid)
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;

                if (model.QuizWiseQuestionsID == 0)
                {
                    command.CommandText = "PR_MST_QuizWiseQuestions_Insert";
                    TempData["SuccessMessage"] = "QuizWiseQuestions Added successfully!";
                }
                else
                {
                    command.CommandText = "PR_MST_QuizWiseQuestions_Update";
                    TempData["SuccessMessage"] = "Data Updated successfully!";
                    command.Parameters.Add("@QuizWiseQuestionsID", SqlDbType.Int).Value = model.QuizWiseQuestionsID;
                }

                command.Parameters.Add("@QuizID", SqlDbType.Int).Value = model.QuizID;
                command.Parameters.Add("@QuestionID", SqlDbType.Int).Value = model.QuestionID;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID;
                command.ExecuteNonQuery();
                return RedirectToAction("QuizWizeQuestionsList");
            }
            UserDropDown();
            return View("QuizWizeQuestionsAdd", model);
        }

       
        public IActionResult QuizWizeQuestionsList()
        {
            DataTable table = new DataTable();
            string connectionString = configuration.GetConnectionString("ConnectionString");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("PR_MST_QuizWiseQuestions_SelectAll", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                table.Load(reader);
                                Console.WriteLine("Rows fetched: " + table.Rows.Count);
                            }
                            else
                            {
                                Console.WriteLine("No rows fetched from DB.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
                TempData["ErrorMessage"] = "Database error: " + ex.Message;
            }

            return View(table);
        }


        //[HttpPost]
        //public IActionResult QuizWizeQuestionsDelete(int QuizWiseQuestionsID)
        //{
        //    try
        //    {
        //        if (QuizWiseQuestionsID <= 0)
        //        {
        //            TempData["ErrorMessage"] = "Invalid ID";
        //            return RedirectToAction("QuizWizeQuestionsList");
        //        }

        //        string connectionString = configuration.GetConnectionString("ConnectionString");
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            using (SqlCommand command = new SqlCommand("PR_MST_QuizWiseQuestions_Delete", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.Add("@QuizWiseQuestionsID", SqlDbType.Int).Value = QuizWiseQuestionsID;
        //                int rowsAffected = command.ExecuteNonQuery();

        //                if (rowsAffected == 0)
        //                    TempData["ErrorMessage"] = "No record found to delete.";
        //            }
        //        }

        //        TempData["SuccessMessage"] = "QuizWizeQuestion deleted successfully.";
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
        //    }
        //    return RedirectToAction("QuizWizeQuestionsList");
        //}

        public IActionResult QuizWizeQuestionsDelete(int QuizWiseQuestionsID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_MST_QuizWiseQuestions_Delete";
                    command.Parameters.Add("@QuizWiseQuestionsID", SqlDbType.Int).Value = QuizWiseQuestionsID;

                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "QuizWiseQuestion deleted successfully.";
                return RedirectToAction("QuizWizeQuestionsList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the QuizWiseQuestion: " + ex.Message;
                return RedirectToAction("QuizWizeQuestionsList");
            }
        }


        public void UserDropDown()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command2 = connection.CreateCommand();
            command2.CommandType = System.Data.CommandType.StoredProcedure;
            command2.CommandText = "PR_MST_User_DropdownForUser";
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable dataTable2 = new DataTable();
            dataTable2.Load(reader2);
            List<UserDropDownModel> userList = new List<UserDropDownModel>();
            foreach (DataRow data in dataTable2.Rows)
            {
                UserDropDownModel model = new UserDropDownModel();
                model.UserID = Convert.ToInt32(data["UserID"]);
                model.UserName = data["UserName"].ToString();
                userList.Add(model);
            }
            ViewBag.userList = userList;
        }

        public void QuizDropDown()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command2 = connection.CreateCommand();
            command2.CommandType = System.Data.CommandType.StoredProcedure;
            command2.CommandText = "PR_MST_Quiz_DropdownForQuiz";
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable dataTable2 = new DataTable();
            dataTable2.Load(reader2);
            List<QuizDropDownModel> quizList = new List<QuizDropDownModel>();
            foreach (DataRow data in dataTable2.Rows)
            {
                QuizDropDownModel model = new QuizDropDownModel();
                model.QuizID = Convert.ToInt32(data["QuizID"]);
                model.QuizName = data["QuizName"].ToString();
                quizList.Add(model);
            }
            ViewBag.quizList = quizList;
        }

        
        public void QuestionDropDown()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command2 = connection.CreateCommand();
            command2.CommandType = System.Data.CommandType.StoredProcedure;
            command2.CommandText = "PR_MST_question_DropdownForQuestion";
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable dataTable2 = new DataTable();
            dataTable2.Load(reader2);
            List<QuestionDropDownModel> questionList = new List<QuestionDropDownModel>();
            foreach (DataRow data in dataTable2.Rows)
            {
                QuestionDropDownModel model = new QuestionDropDownModel();
                model.QuestionID = Convert.ToInt32(data["QuestionID"]);
                model.QuestionText = data["QuestionText"].ToString();
                questionList.Add(model);
            }
            ViewBag.questionList = questionList;
        }

        public IActionResult QuizWiseQuestionsSearch(int? QuizID)
        {
            QuizDropDown(); // Ensure the dropdown has values

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_MST_QuizWiseQuestions_SelectByQuizID";

                if (QuizID.HasValue && QuizID > 0)
                {
                    command.Parameters.Add("@QuizID", SqlDbType.Int).Value = QuizID.Value;
                }

                SqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                return View("QuizWiseQuestionsList", dataTable);
            }
        }

        public IActionResult ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_MST_QuizWiseQuestions_SelectAll";
            //sqlCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = CommonVariable.UserID();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(sqlDataReader);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("DataSheet");

                // Add headers
                worksheet.Cells[1, 1].Value = "QuizWiseQuestionsID";
                worksheet.Cells[1, 2].Value = "QuizName";
                worksheet.Cells[1, 3].Value = "QuestionText";
                worksheet.Cells[1, 4].Value = "UserName";
                worksheet.Cells[1, 5].Value = "Created";
                worksheet.Cells[1, 6].Value = "Modified";

                // Add data
                int row = 2;
                foreach (DataRow item in data.Rows)
                {
                    worksheet.Cells[row, 1].Value = item["QuizWiseQuestionsID"];
                    worksheet.Cells[row, 2].Value = item["QuizName"];
                    worksheet.Cells[row, 3].Value = item["QuestionText"];
                    worksheet.Cells[row, 4].Value = item["UserName"];
                    worksheet.Cells[row, 5].Value = item["Created"];
                    worksheet.Cells[row, 6].Value = item["Modified"];
                    row++;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"Data-{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }

    }
}
