using Microsoft.Owin.Security;
using StajIlkProje.Models;
using System.Linq;
using System.Web.Mvc;
namespace StajIlkProje.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ShowLogIn()
        {
            return View("Index");
        }
        public static bool yetki = false;
        public ActionResult Login(string returnUrl)
        {
            var properties = new AuthenticationProperties { RedirectUri = returnUrl };
            ///object value = HttpContext.GetOwinContext().Authentication.Challenge(properties, GoogleOAuth2AuthenticationDefaults.AuthenticationType);
            return new HttpUnauthorizedResult();
        }
        [HttpPost]
        public ActionResult LogIn(string email, string password) {
            var myDatabase = new AppDBContext();
            if (email == "" || password == "") {
                TempData["Error"] = "Fill in all Fields!";
            }
            var existingUser = myDatabase.Users.FirstOrDefault(client => client.email == email && client.password == password);
             var wrong_pass = myDatabase.Users.FirstOrDefault(client => client.email == email && client.password != password);
           var wrong_pass_2 = myDatabase.Users.Where(client => client.email == email);
            if (existingUser != null)
            {
                TempData["Success"] = "Successful Login!";
                Session["IsAdmin"] = existingUser.isAdmin;
                yetki= existingUser.isAdmin;

                return RedirectToAction("Index", "Dashboard");
            }
           
            else if (wrong_pass != null)
            {
                TempData["Error"] = "Unsuccessful Login! Plz Check your password";
            }
            
            else if (wrong_pass_2 == null)
            {
                TempData["Error"] = "This user is not registered!";
            }
            yetki = false;
            return RedirectToAction("Index","Home");
        }
    }
}
