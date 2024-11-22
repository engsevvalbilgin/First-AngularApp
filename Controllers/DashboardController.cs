using StajIlkProje.Models;
using System.Linq;
using System.Web.Mvc;

namespace StajIlkProje.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            
            AppDBContext db = new AppDBContext();
            int clientNumber = db.Clients.Count(c => c.isActive);
            int actionNumber = db.Activities.Count(c => c.State);
            ViewBag.ClientNumber = clientNumber;
            ViewBag.ActivityNumber = actionNumber;
            TempData["isAdmin"] = TempData["IsAdmin"];
            return View();
        }
        
    }
}