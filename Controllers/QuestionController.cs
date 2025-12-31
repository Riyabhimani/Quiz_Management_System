using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using QuizManagementSystem.Models;
using OfficeOpenXml;

namespace QuizManagementSystem.Controllers
{
    [CheckAccess]
    public class QuestionController : Controller
    {
        private IConfiguration configuration;

        public QuestionController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public IActionResult QuestionAdd(int? QuestionID)
        {
            UserDropDown();
            QuestionLevelDropDown();
            if (QuestionID == null)
            {
                var m = new QuestionModel
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
            command.CommandText = "PR_MST_Question_SelectByID";
            command.Parameters.AddWithValue("@QuestionID", QuestionID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            QuestionModel model = new QuestionModel();

            foreach (DataRow dataRow in table.Rows)
            {
                model.QuestionText = dataRow["QuestionText"].ToString();
                model.QuestionLevelID = Convert.ToInt32(dataRow["QuestionLevelID"]);
                model.OptionA = dataRow["OptionA"].ToString();
                model.OptionB = dataRow["OptionB"].ToString();
                model.OptionC = dataRow["OptionC"].ToString();
                model.OptionD = dataRow["OptionD"].ToString();
                model.CorrectOption = dataRow["CorrectOption"].ToString();
                model.QuestionMarks = dataRow["QuestionMarks"].ToString();
                model.UserID = Convert.ToInt32(dataRow["UserID"]);
            }
            return View(model);
        }

        
        public IActionResult QuestionList()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_Question_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }

        public IActionResult QuestionDelete(int QuestionID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_MST_Question_Delete";
                    command.Parameters.Add("@QuestionID", SqlDbType.Int).Value = QuestionID;

                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "Question deleted successfully.";
                return RedirectToAction("QuestionList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the Question: " + ex.Message;
                return RedirectToAction("QuestionList");
            }
        }

        public IActionResult Save(QuestionModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;

                if (model.QuestionID == 0)
                {
                    command.CommandText = "PR_MST_Question_Insert";
                    TempData["SuccessMessage"] = "Question Added successfully!";
                }
                else
                {
                    command.CommandText = "PR_MST_Question_Update";
                    TempData["SuccessMessage"] = "Data Updated successfully!";
                    command.Parameters.Add("@QuestionID", SqlDbType.Int).Value = model.QuestionID;
                }

                command.Parameters.Add("@QuestionText", SqlDbType.VarChar).Value = model.QuestionText;
                command.Parameters.Add("@QuestionLevelID", SqlDbType.Int).Value = model.QuestionLevelID;
                command.Parameters.Add("@OptionA", SqlDbType.VarChar).Value = model.OptionA;
                command.Parameters.Add("@OptionB", SqlDbType.VarChar).Value = model.OptionB;
                command.Parameters.Add("@OptionC", SqlDbType.VarChar).Value = model.OptionC;
                command.Parameters.Add("@OptionD", SqlDbType.VarChar).Value = model.OptionD;
                command.Parameters.Add("@CorrectOption", SqlDbType.VarChar).Value = model.CorrectOption;
                command.Parameters.Add("@QuestionMarks", SqlDbType.VarChar).Value = model.QuestionMarks;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID;
                command.ExecuteNonQuery();
                return RedirectToAction("QuestionList");
            }
            UserDropDown();
            QuestionLevelDropDown();
            return View("QuestionAdd", model);
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

        public void QuestionLevelDropDown()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command2 = connection.CreateCommand();
            command2.CommandType = System.Data.CommandType.StoredProcedure;
            command2.CommandText = "PR_MST_QuestionLevel_DropdownForQuestionLevel";
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable dataTable2 = new DataTable();
            dataTable2.Load(reader2);
            List<QuestionLevelDropDownModel> questionlevelList = new List<QuestionLevelDropDownModel>();
            foreach (DataRow data in dataTable2.Rows)
            {
                QuestionLevelDropDownModel model = new QuestionLevelDropDownModel();
                model.QuestionLevelID = Convert.ToInt32(data["QuestionLevelID"]);
                model.QuestionLevel = data["QuestionLevel"].ToString();
                questionlevelList.Add(model);
            }
            ViewBag.questionlevelList = questionlevelList;
        }

        public IActionResult ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_MST_Question_SelectAll";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(sqlDataReader);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("DataSheet");

                // Add headers
                worksheet.Cells[1, 1].Value = "QuestionID";
                worksheet.Cells[1, 2].Value = "QuestionText";
                worksheet.Cells[1, 3].Value = "QuestionLevel";
                worksheet.Cells[1, 4].Value = "OptionA";
                worksheet.Cells[1, 5].Value = "OptionB";
                worksheet.Cells[1, 6].Value = "OptionC";
                worksheet.Cells[1, 7].Value = "OptionD";
                worksheet.Cells[1, 8].Value = "CorrectOption";
                worksheet.Cells[1, 9].Value = "QuestionMarks";
                worksheet.Cells[1, 10].Value = "IsActive";
                worksheet.Cells[1, 11].Value = "UserName";
                worksheet.Cells[1, 12].Value = "Created";
                worksheet.Cells[1, 13].Value = "Modified";

                // Add data
                int row = 2;
                foreach (DataRow item in data.Rows)
                {
                    worksheet.Cells[row, 1].Value = item["QuestionID"];
                    worksheet.Cells[row, 2].Value = item["QuestionText"];
                    worksheet.Cells[row, 3].Value = item["QuestionLevel"];
                    worksheet.Cells[row, 4].Value = item["OptionA"];
                    worksheet.Cells[row, 5].Value = item["OptionB"];
                    worksheet.Cells[row, 6].Value = item["OptionC"];
                    worksheet.Cells[row, 7].Value = item["OptionD"];
                    worksheet.Cells[row, 8].Value = item["CorrectOption"];
                    worksheet.Cells[row, 9].Value = item["QuestionMarks"];
                    worksheet.Cells[row, 10].Value = item["IsActive"];
                    worksheet.Cells[row, 11].Value = item["UserName"];
                    worksheet.Cells[row, 12].Value = item["Created"];
                    worksheet.Cells[row, 13].Value = item["Modified"];
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
