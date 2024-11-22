using StajIlkProje.Helpers;
using StajIlkProje.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace StajIlkProje.Controllers
{
    public class UserController : Controller
    {
        AppDBContext db = new AppDBContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowUserList()
        {
            List<StajIlkProje.Models.User> all_users = db.Users.ToList();
            return View("UserList", all_users);
        }
        public ActionResult ShowEditUser(StajIlkProje.Models.User user)
        {
            StajIlkProje.Models.User find_user = db.Users.Find(user.UserId);
            return View("EditUser", find_user);
        }
        public ActionResult EditUser(StajIlkProje.Models.User user) {
            StajIlkProje.Models.User find_user = db.Users.Find(user.UserId);
            PasswordScoreModel model = new PasswordScoreModel();

            PasswordHelper passwordHelper = new PasswordHelper(model);

            PasswordScoreModel result = passwordHelper.CheckStrength(user.password);

            if (result.IsValid)
            {
                find_user.Name = user.Name;
                find_user.Surname = user.Surname;
                find_user.PhoneNum = user.PhoneNum;
                find_user.password = user.password;
                find_user.isActive = user.isActive;
                db.SaveChanges();
            }
            

            return RedirectToAction("GoBackToUserList", "User");
        }
        public ActionResult SearchInUsers(string text) {
            string pattern = $@"\b\w*{Regex.Escape(text)}\w*\b";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var allUsers = db.Users.ToList();

            var results = allUsers
             .Where(e => (e.Name != null && regex.IsMatch(e.Name)) ||
                     (e.Surname != null && regex.IsMatch(e.Surname)) ||
                     (e.email != null && regex.IsMatch(e.email)) ||
                     (e.PhoneNum != null && regex.IsMatch(e.PhoneNum)) 
                     )
         .ToList();
           
            return View("UserList",results);
        }
        public ActionResult GoBackToUserList()
        {
            List<StajIlkProje.Models.User> users = db.Users.ToList();
            return View("UserList", users);
        }
        public ActionResult ChangeUserState(StajIlkProje.Models.User user)
        {
            StajIlkProje.Models.User find_user = db.Users.Find(user.UserId);
            if (find_user != null)
            {
                find_user.isActive = !find_user.isActive;
                db.SaveChanges();
            }
            List<StajIlkProje.Models.User> users = db.Users.ToList();
           return View("UserList", users);
        }
        public ActionResult ShowAddUser() { 
        return View("AddUser"); 
        }
        [HttpPost]
        public ActionResult AddUser(StajIlkProje.Models.User user) {
            PasswordScoreModel model = new PasswordScoreModel();

            PasswordHelper passwordHelper = new PasswordHelper(model);

            PasswordScoreModel result = passwordHelper.CheckStrength(user.password);
            
            if (result.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            var all_users = db.Users.ToList();
            
                return View("UserList", all_users);
            
           }
    }
}