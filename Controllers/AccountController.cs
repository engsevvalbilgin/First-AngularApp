using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

public class AccountController : Controller
{
    
    

    // Login işlemi başlatır
    [HttpPost]
    public ActionResult Login(string returnUrl)
    {
        // Ensure returnUrl is not null
        if (string.IsNullOrEmpty(returnUrl))
        {
            returnUrl = Url.Action("Index", "Home"); 
        }

        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl })
        };

        HttpContext.GetOwinContext().Authentication.Challenge(properties, "Google");
        return new HttpUnauthorizedResult(); 
    }


    // Google'dan geri döndükten sonra işlemleri yapar
    public ActionResult ExternalLoginCallback(string returnUrl)
    {
        var result = HttpContext.GetOwinContext().Authentication.AuthenticateAsync(DefaultAuthenticationTypes.ExternalCookie).Result;
        
        if (result?.Identity == null)
        {
            TempData["Success"] = "Giriş Başarılı!";
            return RedirectToAction("Index","Dashboard");
        }
        return Redirect(returnUrl ?? "/");
    }
}
