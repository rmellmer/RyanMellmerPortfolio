using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RyanPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["SQLDatabase"].ConnectionString;
        private static string findSkillsString = "SELECT id, Name, Description, ParentID, Level, Project, Details, Link FROM Skills WHERE ParentID = @ParentID";
        private static string findLevelString = "SELECT id, Name, Description, ParentID, Level FROM Skills WHERE Level = @Level";

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSkillCategories()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(findSkillsString, connection);
                command.Parameters.AddWithValue("@ParentID", 0);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                connection.Close();
                return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetChildSkills(int parent)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(findSkillsString, connection);
                command.Parameters.AddWithValue("@ParentID", parent);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                connection.Close();
                return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            }
        }
    }
}