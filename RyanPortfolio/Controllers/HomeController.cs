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
        private static string skillFile = AppDomain.CurrentDomain.BaseDirectory + @"\skills.json";

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSkillCategories()
        {
            return Json(System.IO.File.ReadAllText(skillFile), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetChildSkills(int parent)
        {
            IEnumerable<dynamic> jsonObject = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(System.IO.File.ReadAllText(skillFile));

            jsonObject = jsonObject.Where(x => x.id == parent).First().skills;

            return Json(JsonConvert.SerializeObject(jsonObject), JsonRequestBehavior.AllowGet);
        }
    }
}