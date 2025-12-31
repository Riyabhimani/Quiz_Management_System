using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QuizManagementSystem.Models;
using OfficeOpenXml;

namespace QuizManagementSystem.Controllers
{
    [CheckAccess]
    public class QuizController : Controller
    {
        private IConfiguration configuration;

        public QuizController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public IActionResult QuizAdd(int? QuizID)
        {
            UserDropDown();
            if (QuizID == null)
            {
                var m = new QuizModel
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
            command.CommandText = "PR_MST_Quiz_SelectByID";
            command.Parameters.AddWithValue("@QuizID", QuizID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            QuizModel model = new QuizModel();

            foreach (DataRow dataRow in table.Rows)
            {
                model.QuizName = dataRow["QuizName"].ToString();
                model.TotalQuestions = dataRow["TotalQuestions"].ToString();
                model.QuizDate = Convert.ToDateTime(dataRow["QuizDate"]);
                model.UserID = Convert.ToInt32(dataRow["UserID"]);
            }
            return View(model);
        }

        public IActionResult QuizList()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_Quiz_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }

        public IActionResult QuizDelete(int QuizID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_MST_Quiz_Delete";
                    command.Parameters.Add("@QuizID", SqlDbType.Int).Value = QuizID;


                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "Quiz deleted successfully.";
                return RedirectToAction("QuizList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the Quiz: " + ex.Message;
                return RedirectToAction("QuizList");
            }
        }

        public IActionResult Save(QuizModel model)
        {
            UserDropDown();
            if (ModelState.IsValid)
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;

                if (model.QuizID == 0)
                {
                    command.CommandText = "PR_MST_Quiz_Insert";
                    TempData["SuccessMessage"] = "Quiz Added successfully!";
                }
                else
                {
                    command.CommandText = "PR_MST_Quiz_Update";
                    TempData["SuccessMessage"] = "Data Updated successfully!";
                    command.Parameters.Add("@QuizID", SqlDbType.Int).Value = model.QuizID;
                }

                command.Parameters.Add("@QuizName", SqlDbType.VarChar).Value = model.QuizName;
                command.Parameters.Add("@TotalQuestions", SqlDbType.Int).Value = model.TotalQuestions;
                command.Parameters.Add("@QuizDate", SqlDbType.DateTime).Value = model.QuizDate;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID;
                command.ExecuteNonQuery();
                return RedirectToAction("QuizList");
            }
            UserDropDown();
            return View("QuizAdd", model);
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

        
        public IActionResult QuizSearch(IFormCollection fc)
        {
            UserDropDown();

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "PR_MST_Quiz_Search";
            //command.Parameters.Add("@UserID", SqlDbType.Int).Value = CommonVariable.UserID();
            command.Parameters.Add("@QuizName", SqlDbType.VarChar).Value = fc["QuizName"].ToString();
            command.Parameters.Add("@MinQuestions", SqlDbType.Int).Value = Convert.ToInt32(fc["@MinQuestions"]);
            command.Parameters.Add("@MaxQuestions", SqlDbType.Int).Value = Convert.ToInt32(fc["@MaxQuestions"]);
            command.Parameters.Add("@FromQuizDate", SqlDbType.Date).Value = fc["FromQuizDate"].ToString();
            command.Parameters.Add("@ToQuizDate", SqlDbType.Date).Value = fc["ToQuizDate"].ToString();

            SqlDataReader reader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            return View("QuizList", dataTable);
        }

        public IActionResult ExportToExcel()    
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_MST_Quiz_SelectAll";
            //sqlCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = CommonVariable.UserID();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(sqlDataReader);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("DataSheet");

                // Add headers
                worksheet.Cells[1, 1].Value = "QuizID";
                worksheet.Cells[1, 2].Value = "QuizName";
                worksheet.Cells[1, 3].Value = "TotalQuestions";
                worksheet.Cells[1, 4].Value = "QuizDate";
                worksheet.Cells[1, 5].Value = "UserName";
                worksheet.Cells[1, 6].Value = "Created";
                worksheet.Cells[1, 7].Value = "Modified";

                // Add data
                int row = 2;
                foreach (DataRow item in data.Rows)
                {
                    worksheet.Cells[row, 1].Value = item["QuizID"];
                    worksheet.Cells[row, 2].Value = item["QuizName"];
                    worksheet.Cells[row, 3].Value = item["TotalQuestions"];
                    worksheet.Cells[row, 4].Value = item["QuizDate"];
                    worksheet.Cells[row, 5].Value = item["UserName"];
                    worksheet.Cells[row, 6].Value = item["Created"];
                    worksheet.Cells[row, 7].Value = item["Modified"];
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
