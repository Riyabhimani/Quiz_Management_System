using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using QuizManagementSystem.Models;

namespace QuizManagementSystem.Controllers
{
    public class PagesController : Controller
    {
        private IConfiguration configuration;

        public PagesController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [CheckAccess]
        public IActionResult Profile()
        {
            return View();
        }

        [CheckAccess]
        public IActionResult FAQ()
        {
            return View();
        }

        [CheckAccess]
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [CheckAccess]
        public IActionResult Error404()
        {
            return View();
        }

        [CheckAccess]
        public IActionResult Blank()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserLogin(UserLoginModel userLoginModel)
        {
            string ErrorMsg = string.Empty;

            if (string.IsNullOrEmpty(userLoginModel.UserName))
            {
                ErrorMsg += "User Name is Required";
            }

            if (string.IsNullOrEmpty(userLoginModel.Password))
            {
                ErrorMsg += "<br/>Password is Required";
            }

            if (ModelState.IsValid)
            {
                SqlConnection conn = new SqlConnection(this.configuration.GetConnectionString("ConnectionString"));
                conn.Open();

                SqlCommand objCmd = conn.CreateCommand();

                objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                objCmd.CommandText = "PR_MST_User_Login";
                objCmd.Parameters.AddWithValue("@UserName", userLoginModel.UserName);
                objCmd.Parameters.AddWithValue("@Password", userLoginModel.Password);

                SqlDataReader objSDR = objCmd.ExecuteReader();

                DataTable dtLogin = new DataTable();

                // Check if Data Reader has Rows or not
                // if row(s) does not exists that means either username or password or both are invalid.
                if (!objSDR.HasRows)
                {
                    TempData["ErrorMessage"] = "Invalid User Name or Password";
                    return RedirectToAction("Login", "Pages");
                }
                else
                {
                    dtLogin.Load(objSDR);

                    //Load the retrived data to session through HttpContext.
                    foreach (DataRow dr in dtLogin.Rows)
                    {
                        HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                        HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        //HttpContext.Session.SetString("Mobile", dr["Mobile"].ToString());
                        //HttpContext.Session.SetString("Email", dr["Email"].ToString());
                        HttpContext.Session.SetString("Password", dr["Password"].ToString());
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["ErrorMessage"] = ErrorMsg;
                return RedirectToAction("Login", "Pages");
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }
}
