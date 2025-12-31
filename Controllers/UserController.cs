using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using QuizManagementSystem.Models;

namespace QuizManagementSystem.Controllers
{
    [CheckAccess]
    public class UserController : Controller
    {
        private IConfiguration configuration;

        public UserController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public IActionResult UserAdd(int? UserID)
        {
            if (UserID == null)
            {
                var m = new UserModel
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
            command.CommandText = "PR_MST_User_SelectByID";
            command.Parameters.AddWithValue("@UserID", UserID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            UserModel model = new UserModel();

            foreach (DataRow dataRow in table.Rows)
            {
                model.UserName = dataRow["UserName"].ToString();
                model.Password = dataRow["Password"].ToString();
                model.Email = dataRow["Email"].ToString();
                model.Mobile = dataRow["Mobile"].ToString();
                model.IsActive = Convert.ToInt32(dataRow["IsActive"]);
                model.IsAdmin = Convert.ToInt32(dataRow["IsAdmin"]);

            }
            return View(model);
        }

        public IActionResult UserList()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_User_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }


        public IActionResult UserDelete(int UserID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_MST_User_Delete";
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;


                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "User deleted successfully.";
                return RedirectToAction("UserList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the User: " + ex.Message;
                return RedirectToAction("UserList");
            }
        }

        public IActionResult Save(UserModel model)
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (model.UserID == 0)
                        {
                            command.CommandText = "PR_MST_User_Insert";
                        }
                        else
                        {
                            command.CommandText = "PR_MST_User_Update";
                            command.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID;
                        }

                        command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = model.UserName;
                        command.Parameters.Add("@Password", SqlDbType.VarChar).Value = model.Password;
                        command.Parameters.Add("@Email", SqlDbType.VarChar).Value = model.Email;
                        command.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = model.Mobile;
                        command.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = model.IsActive;
                        command.Parameters.Add("@IsAdmin", SqlDbType.VarChar).Value = model.IsAdmin;

                        command.ExecuteNonQuery();

                        //SqlConnection conn = new SqlConnection(this.configuration.GetConnectionString("ConnectionString"));
                        //conn.Open();

                        //SqlCommand objCmd = conn.CreateCommand();

                        //objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        //objCmd.CommandText = "PR_MST_User_Login";
                        //objCmd.Parameters.AddWithValue("@UserName", model.UserName);
                        //objCmd.Parameters.AddWithValue("@Password", model.Password);

                        //SqlDataReader objSDR = objCmd.ExecuteReader();

                        //DataTable dtLogin = new DataTable();

                        //// Check if Data Reader has Rows or not
                        //// if row(s) does not exists that means either username or password or both are invalid.
                        //if (!objSDR.HasRows)
                        //{
                        //    TempData["ErrorMessage"] = "Invalid User Name or Password";
                        //    return RedirectToAction("Login", "Pages");
                        //}
                        //else
                        //{
                        //    dtLogin.Load(objSDR);

                        //    //Load the retrived data to session through HttpContext.
                        //    foreach (DataRow dr in dtLogin.Rows)
                        //    {
                        //        HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                        //        HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        //        //HttpContext.Session.SetString("Mobile", dr["Mobile"].ToString());
                        //        //HttpContext.Session.SetString("Email", dr["Email"].ToString());
                        //        HttpContext.Session.SetString("Password", dr["Password"].ToString());
                        //    }
                        //    return RedirectToAction("Index", "Home");
                        //}
                    }

                }
                //}

            }
            return View("UserAdd", model);
        }

        public IActionResult UserRegister(UserRegisterModel userRegisterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string connectionString = this.configuration.GetConnectionString("ConnectionString");
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "PR_MST_User_Register";
                    sqlCommand.Parameters.Add("@Username", SqlDbType.VarChar).Value = userRegisterModel.UserName;
                    sqlCommand.Parameters.Add("@Password", SqlDbType.VarChar).Value = userRegisterModel.Password;
                    sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = userRegisterModel.Email;
                    sqlCommand.ExecuteNonQuery();
                    return RedirectToAction("Login", "User");
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Register");
            }
            return RedirectToAction("Register");
        }

        public IActionResult Register()
        {
            return View();
        }



    }

}

