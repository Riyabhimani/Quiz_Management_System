using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using QuizManagementSystem.Models;
using OfficeOpenXml;

namespace QuizManagementSystem.Controllers
{
    [CheckAccess]
    public class QuestionLevelController : Controller
    {
        private IConfiguration configuration;

        public QuestionLevelController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public IActionResult QuestionLevelAdd(int? QuestionLevelID)
        {
            UserDropDown();
            //QuestionLevelDropDown();
            if (QuestionLevelID == null)
            {
                var m = new QuestionLevelModel
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
            command.CommandText = "PR_MST_QuestionLevel_SelectByID";
            command.Parameters.AddWithValue("@QuestionLevelID", QuestionLevelID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            QuestionLevelModel model = new QuestionLevelModel();

            foreach (DataRow dataRow in table.Rows)
            {
                //model.QuestionLevelID = Convert.ToInt32(dataRow["QuestionLevelID"]);
                model.QuestionLevel = dataRow["QuestionLevel"].ToString();
                model.UserID = Convert.ToInt32(dataRow["UserID"]);
            }
            return View(model);
        }

        public IActionResult QuestionLevelList()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("PR_MST_QuestionLevel_SelectAll", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        return View(table);
                    }
                }
            }
        }

        
        public IActionResult QuestionLevelDelete(int QuestionLevelID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_MST_QuestionLevel_Delete";
                    command.Parameters.Add("@QuestionLevelID", SqlDbType.Int).Value = QuestionLevelID;

                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "QuestionLevel deleted successfully.";
                return RedirectToAction("QuestionLevelList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the QuestionLevel: " + ex.Message;
                return RedirectToAction("QuestionLevelList");
            }
        }

        //public IActionResult Save(QuestionLevelModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string connectionString = this.configuration.GetConnectionString("ConnectionString");
        //        SqlConnection connection = new SqlConnection(connectionString);
        //        connection.Open();
        //        SqlCommand command = connection.CreateCommand();
        //        command.CommandType = CommandType.StoredProcedure;

        //        if (model.QuestionLevelID == 0)
        //        {
        //            command.CommandText = "PR_MST_QuestionLevel_Insert";
        //            TempData["SuccessMessage"] = "QuestionLevel Added successfully!";
        //        }
        //        else
        //        {
        //            command.CommandText = "PR_MST_QuestionLevel_Update";
        //            TempData["SuccessMessage"] = "Data Updated successfully!";
        //            command.Parameters.Add("@QuestionLevelID", SqlDbType.Int).Value = model.QuestionLevelID;
        //        }

        //        command.Parameters.Add("@QuestionLevel", SqlDbType.VarChar).Value = model.QuestionLevel;
        //        command.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID;
        //        command.ExecuteNonQuery();
        //        return RedirectToAction("QuestionLevelList");
        //    }
        //    UserDropDown();
        //    //QuestionLevelDropDown();
        //    return View("QuestionLevelAdd", model);
        //}

        public IActionResult Save(QuestionLevelModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string connectionString = configuration.GetConnectionString("ConnectionString");
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            if (model.QuestionLevelID == 0)
                            {
                                command.CommandText = "PR_MST_QuestionLevel_Insert";
                                TempData["SuccessMessage"] = "Question Level added successfully!";
                                command.Parameters.AddWithValue("@QuestionLevel", model.QuestionLevel);
                            }
                            else
                            {
                                command.CommandText = "PR_MST_QuestionLevel_Update";
                                TempData["SuccessMessage"] = "Data updated successfully!";
                                command.Parameters.AddWithValue("@QuestionLevelID", model.QuestionLevelID);
                            }
                            //command.Parameters.AddWithValue("@QuestionLevel", model.QuestionLevel);
                            command.Parameters.AddWithValue("@UserID", model.UserID);
                            command.ExecuteNonQuery();
                            return RedirectToAction("QuestionLevelList");
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Error: " + ex.Message;
                }
            }

            UserDropDown();
            return View("QuestionLevelAdd", model);
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
            sqlCommand.CommandText = "PR_MST_QuestionLevel_SelectAll";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(sqlDataReader);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("DataSheet");

                // Add headers
                worksheet.Cells[1, 1].Value = "QuestionLevelID";
                worksheet.Cells[1, 2].Value = "QuestionLevel";
                worksheet.Cells[1, 3].Value = "UserName";
                worksheet.Cells[1, 4].Value = "Created";
                worksheet.Cells[1, 5].Value = "Modified";

                // Add data
                int row = 2;
                foreach (DataRow item in data.Rows)
                {
                    worksheet.Cells[row, 1].Value = item["QuestionLevelID"];
                    worksheet.Cells[row, 2].Value = item["QuestionLevel"];
                    worksheet.Cells[row, 3].Value = item["UserName"];
                    worksheet.Cells[row, 4].Value = item["Created"];
                    worksheet.Cells[row, 5].Value = item["Modified"];
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
