using StajIlkProje.Models;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace StajIlkProje.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        //[RequireHttps]
        public ActionResult Index()
        {
            return View("Index");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}