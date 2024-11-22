//using StajIlkProje.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace StajIlkProje.Controllers
//{
//    public class ClientListController : Controller
//    {
//        // GET: ClientList
//        public ActionResult Index()
//        {
//            AppDBContext db = new AppDBContext();
//            var clients = db.Clients.ToList();
//            return View(clients);
//        }
//        public ActionResult DeleteClient(Client model)
//        {
//            var myDatabase = new AppDBContext();
//            var clientToDelete = myDatabase.Clients.Find(model.ClientId);
//            if (clientToDelete != null)
//            {
//                clientToDelete.isActive = false;



//                myDatabase.SaveChanges();
//            }



//            return RedirectToAction("");

//        }
//        public ActionResult UpdateClient(Client model)
//        {
//            var myDatabase = new AppDBContext();


//            var existingClient = myDatabase.Clients.Find(model.ClientId);
//            if (existingClient != null)
//            {
//                // Güncelleme işlemleri
//                existingClient.Name = model.Name;
//                existingClient.Surname = model.Surname;
//                existingClient.PhoneNum = model.PhoneNum;
//                existingClient.password = model.password;
//                existingClient.email = model.email;
//                existingClient.isActive = model.isActive;
//                existingClient.ActivityList = model.ActivityList;

//                // Diğer alanları güncelle
//                myDatabase.SaveChanges();
//            }


//            return RedirectToAction("Index");

//        }
        
//    }
//}