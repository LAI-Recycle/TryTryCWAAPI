using System.Globalization;
using System.Web.Mvc;

namespace _20240300TryCWAAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult ChangeLanguage(string lang)
        {
            Session["CurrentCulture"] = new CultureInfo(lang);
            return Json(true);
        }
    }
}