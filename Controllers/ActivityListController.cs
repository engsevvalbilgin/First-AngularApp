//using Newtonsoft.Json.Linq;
//using StajIlkProje.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.UI.WebControls;

//namespace StajIlkProje.Controllers
//{
//    public class ActivityListController : Controller
//    {
//        // GET: ActivityList
//        //public ActionResult Index()
//        //{
//        //    AppDBContext mydb = new AppDBContext();
//        //    var list = mydb.Activities.ToList();

//        //    return View();
//        //}
//        public ActionResult Index(StajIlkProje.Models.Client model)
//        {

//            return View(model);
//        }
//        public ActionResult AddActivity(StajIlkProje.Models.Client client)
//        {
//            return View(client);
//        }
//        [HttpPost]
//        public ActionResult UpdateActivity(StajIlkProje.Models.Activity activity)
//        {
//            AppDBContext mydb = new AppDBContext();
          

//            var existingActvity = mydb.Activities.Find(activity.ActivityId);
//            if (existingActvity != null)
//            {
//                // Güncelleme işlemleri
//                existingActvity.Subject = activity.Subject;
//                existingActvity.Notes = activity.Notes;
//                existingActvity.ProductName = activity.ProductName;
//                existingActvity.CommunicationChannel = activity.CommunicationChannel;
//                existingActvity.RecordDate = DateTime.Now;
//                existingActvity.State = activity.State;
//                existingActvity.Store = activity.Store;

//                // Diğer alanları güncelle
//                mydb.SaveChanges();
//            }
            


            
            
//            return View(mydb.Clients.Find(activity.ClientId));

//        }

    
//    [HttpPost]
//        public ActionResult AddActivity(StajIlkProje.Models.Activity activity)
//        {
//            AppDBContext mydb = new AppDBContext();
//            activity.RecordDate = DateTime.Now;
//            mydb.Activities.Add(activity);

            
            
//            var client = mydb.Clients.Find(activity.ClientId);
//            if (client != null)
//            {
//                if (client.ActivityList == null)
//                {
//                    client.ActivityList = new List<Activity>();
//                }
//                client.ActivityList.Add(activity);
//                mydb.SaveChanges();
//            }
//            return View( mydb.Clients.Find(activity.ClientId));

//        }

//    }

//    //public ActionResult DeleteActivity(StajIlkProje.Models.Activity activity)
//    //{
//    //    AppDBContext mydb = new AppDBContext();
//    //    StajIlkProje.Models.Activity db_activity= mydb.Activities.(activity.ActivityId);


    
//    //}
//    //    //    @using(Html.BeginForm("AddActivity", "ActivityList", FormMethod.Post, new { @class = "form-horizontal", role = "form" }))
//        //{

//        //    < th >
//        //        @Html.Hidden("ClientId", @Model.ClientId)
//        //        @Html.Hidden("Name", @Model.Name)
//        //        @Html.Hidden("Surname", @Model.Surname)
//        //        @Html.Hidden("email", @Model.email)
//        //        @Html.Hidden("password", @Model.password)
//        //        @Html.Hidden("isActive", @Model.isActive)
//        //        @Html.Hidden("PhoneNum", @Model.PhoneNum)

//        //        < input type = "submit" value = "Add New Activity" style = "color: white; font-size:14px; border-radius:10px; padding: 3px 3px 3px 3px; background-color: #F082AC; text-decoration: none;" />
//        //    </ th >
//        //
//    }