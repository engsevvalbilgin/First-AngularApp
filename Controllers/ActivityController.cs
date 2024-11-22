using OfficeOpenXml;
using StajIlkProje.Helpers;
using StajIlkProje.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;

// veya
namespace StajIlkProje.Controllers
{
    public class ActivityController : Controller
    {
        AppDBContext db = new AppDBContext();
        public ActionResult Index()
        {

            return View();
        }
        #region ShowAllActivities

        public ActionResult ShowAllActivities()
        {
            List<StajIlkProje.Models.Activity> openActivities = db.Activities.ToList();
            TempData["From"] = "ShowAllActivities";
            return View("OpenActivitiesList", openActivities);
        }
        public ActionResult ShowActivityListForClient(StajIlkProje.Models.Client client)
        {
            TempData["From"] = "ShowActivityListForClient";
            var item = db.Activities.Where(a => a.ClientId == client.ClientId).ToList();
            TempData["client_id"] = db.Activities.ToList()[0].ClientId;

            return View("OpenActivitiesList", item);



        }
        public ActionResult ShowOpenActivitiesList()
        {
            TempData["From"] = "ShowOpenActivitiesList";

            List<StajIlkProje.Models.Activity> openActivities = db.Activities.Where(c => c.State).ToList();
            return View("OpenActivitiesList", openActivities);
        }
        #endregion

        #region ShowSearchedActivities
        public ActionResult ShowSearchedActivitiesListInAll(string key)
        {
            var activities = db.Activities.ToList();
            //ViewBag.DropDownOptions = options;

            string pattern = $@"\b\w*{Regex.Escape(key)}\w*\b";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var results = activities;

            results = activities
             .Where(e => (e.CommunicationChannel != null && regex.IsMatch(e.CommunicationChannel)) ||
                     (e.Subject != null && regex.IsMatch(e.Subject)) ||
                     (e.Notes != null && regex.IsMatch(e.Notes)) ||
                     (e.Store != null && regex.IsMatch(e.Store)))
         .ToList();
            TempData["From"] = "ShowAllActivities";
            return View("OpenActivitiesList", results);
        }
        public ActionResult ShowSearchedActivitiesListInClient(string key, int ClientId)
        {
            var activities = db.Activities.Where(a => a.ClientId == ClientId).ToList();

            string pattern = $@"\b\w*{Regex.Escape(key)}\w*\b";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var results = activities;

            results = activities
             .Where(e => (e.CommunicationChannel != null && regex.IsMatch(e.CommunicationChannel)) ||
                     (e.Subject != null && regex.IsMatch(e.Subject)) ||
                     (e.Notes != null && regex.IsMatch(e.Notes)) ||
                     (e.Store != null && regex.IsMatch(e.Store)))
         .ToList();
            TempData["From"] = "ShowActivityListForClient";
            TempData["client_id"] = db.Activities.ToList()[0].ClientId;

            return View("OpenActivitiesList", results);
        }
        public ActionResult ShowSearchedActivitiesListInOpenActivities(string key)
        {
            var activities = db.Activities.Where(a => a.State == true).ToList();
            //ViewBag.DropDownOptions = options;

            string pattern = $@"\b\w*{Regex.Escape(key)}\w*\b";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var results = activities;

            results = activities
             .Where(e => (e.CommunicationChannel != null && regex.IsMatch(e.CommunicationChannel)) ||
                     (e.Subject != null && regex.IsMatch(e.Subject)) ||
                     (e.Notes != null && regex.IsMatch(e.Notes)) ||
                     (e.Store != null && regex.IsMatch(e.Store)))
         .ToList();
            TempData["From"] = "ShowOpenActivitiesList";
            return View("OpenActivitiesList", results);
        }
        #endregion

        #region ShowViews
        [HttpPost]
        public ActionResult ShowAddActivity(int ClientId)
        {
            Activity activity = new Activity();
            activity.ClientId = ClientId;
            Client c = db.Clients.Find(ClientId);
            return View("AddActivity", c);

        }
        public ActionResult ShowUpdateActivity(StajIlkProje.Models.Activity activity)
        {


            var existingActvity = db.Activities.Find(activity.ActivityId);
            if (existingActvity != null)
            {
                existingActvity.Subject = activity.Subject;
                existingActvity.Notes = activity.Notes;
                existingActvity.ProductAmount = activity.ProductAmount;
                existingActvity.CommunicationChannel = activity.CommunicationChannel;
                existingActvity.RecordDate = DateTime.Now;
                existingActvity.State = activity.State;
                existingActvity.Store = activity.Store;
                db.SaveChanges();
            }
            return View("UpdateActivity", existingActvity);

        }
        #endregion
        

        #region ActivityOps
        public ActionResult AddActivity(StajIlkProje.Models.Activity activity)
        {
            activity.RecordDate = DateTime.Now;
            bool result = CheckState(activity.ProductId, activity.ProductAmount);
            db.Activities.Add(activity);
            db.Clients.Find(activity.ClientId).ActivityList.Add(activity);
            db.SaveChanges();
            
            if (result)
            {
                //UpdateStock(activity.ProductId, activity.ProductAmount);
                TempData["SuccessMessage"] = "Ürün satımı başarılı!";
                var client = db.Clients.Find(activity.ClientId);
                if (client != null)
                {
                    if (client.ActivityList == null)
                    {
                        client.ActivityList = new List<StajIlkProje.Models.Activity>();

                    }
                    client.ActivityList.Add(activity);
                    db.SaveChanges();
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Yeterli Stok Yok!";
            }
            return View("OpenActivitiesList", db.Activities.Where(a => a.ClientId == activity.ClientId).ToList());

        }
        private bool CheckState(int product_id, int miktar)
        {
            Product product = db.Products.Find(product_id);

            if (product != null)
            {
                int stok = product.number;
                int result = stok - miktar;
                if (result < 0)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        //private void UpdateStock(int product_id, int miktar)
        //{
        //    StajIlkProje.Models.Product product = db.Products.Find(product_id);
        //    product.number = product.number - miktar;
        //    db.SaveChanges();
        //}
        //[HttpPost]
        //public ActionResult UpdateActivity(StajIlkProje.Models.Activity activity)
        //{


        //    var existingActvity = db.Activities.Find(activity.ActivityId);
        //    if (existingActvity != null)
        //    {
        //        // Güncelleme işlemleri
        //        existingActvity.Subject = activity.Subject;
        //        existingActvity.Notes = activity.Notes;
        //        existingActvity.ProductAmount = activity.ProductAmount;
        //        existingActvity.CommunicationChannel = activity.CommunicationChannel;
        //        existingActvity.RecordDate = DateTime.Now;
        //        existingActvity.State = activity.State;
        //        existingActvity.Store = activity.Store;

        //        // Diğer alanları güncelle
        //        db.SaveChanges();
        //    }



        //    var item = db.Activities.Where(c => c.ClientId == activity.ClientId);

        //    return View("OpenActivitiesList", item);

        //}
        [HttpPost]

        #endregion

        #region ChangeState
        public ActionResult fromClientListChangeState(int ActivityId)
        {

            StajIlkProje.Models.Activity db_activity = db.Activities.Find(ActivityId);
            db_activity.State = !db_activity.State;
            db.SaveChanges();
            Session.Clear();
            TempData["From"] = "ShowActivityListForClient";
            TempData["client_id"] = db.Activities.ToList()[0].ClientId;
            List <StajIlkProje.Models.Activity> acts = db.Activities.Where(a => a.ClientId == db_activity.ClientId).ToList();
            return View("OpenActivitiesList", db.Activities.Where(a => a.ClientId == db_activity.ClientId).ToList());
        }
        [HttpPost]
        public ActionResult fromActivityListChangeState(int ActivityId)



        {

            StajIlkProje.Models.Activity db_activity = db.Activities.Find(ActivityId);
            db_activity.State = !db_activity.State;
            db.SaveChanges();
            Session.Clear();
            TempData["From"] = "ShowAllActivities";
            return View("OpenActivitiesList", db.Activities.ToList());
        }
        [HttpPost]
        public ActionResult fromOpenActivitiesChangeState(int ActivityId)



        {
            StajIlkProje.Models.Activity db_activity = db.Activities.Find(ActivityId);
            db_activity.State = !db_activity.State;
            db.SaveChanges();
            Session.Clear();
            TempData["From"] = "ShowOpenActivitiesList";
            return View("OpenActivitiesList", db.Activities.Where(a => a.State == true).ToList());
        }
        #endregion
        #region EditActivity
        public ActionResult fromAllEditActivity(StajIlkProje.Models.Activity activity)
        {
            StajIlkProje.Models.Activity existing_activity = db.Activities.Find(activity.ActivityId);
            List<StajIlkProje.Models.Activity> all_activities = db.Activities.ToList();
            return View("OpenActivitiesList", all_activities);
        }
        public ActionResult fromActivityListEditActivity(StajIlkProje.Models.Activity activity)
        {
            StajIlkProje.Models.Activity existing_activity = db.Activities.Find(activity.ActivityId);
            TempData["client_id"] = db.Activities.ToList()[0].ClientId;

            List<StajIlkProje.Models.Activity> all_activities = db.Activities.Where(a => a.ClientId == activity.ClientId).ToList();
            return View("OpenActivitiesList", all_activities);
        }
        public ActionResult fromOpenActivitiesEditActivity(StajIlkProje.Models.Activity activity)
        {
            StajIlkProje.Models.Activity existing_activity = db.Activities.Find(activity.ActivityId);
            List<StajIlkProje.Models.Activity> all_activities = db.Activities.Where(a => a.State == true).ToList();
            return View("OpenActivitiesList", all_activities);
        }
        #endregion
        public ActionResult exportDataToExcel(Collection<StajIlkProje.Models.Activity> activityList)

        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Activities");

                // Başlık satırı
                worksheet.Cells[1, 1].Value = "Client ID";
                worksheet.Cells[1, 2].Value = "Activity ID";
                worksheet.Cells[1, 3].Value = "Product ID";
                worksheet.Cells[1, 4].Value = "Communication Channel";
                worksheet.Cells[1, 5].Value = "Subject";
                worksheet.Cells[1, 6].Value = "Notes";
                worksheet.Cells[1, 7].Value = "Product Amount";
                worksheet.Cells[1, 8].Value = "Store";
                worksheet.Cells[1, 9].Value = "Record Date";
                worksheet.Cells[1, 10].Value = "State";

                // Verileri ekle
                for (int i = 0; i < activityList.Count; i++)
                {
                    var activity = activityList[i];
                    worksheet.Cells[i + 2, 1].Value = activity.ClientId;
                    worksheet.Cells[i + 2, 2].Value = activity.ActivityId;
                    worksheet.Cells[i + 2, 3].Value = activity.ProductId;
                    worksheet.Cells[i + 2, 4].Value = activity.CommunicationChannel;
                    worksheet.Cells[i + 2, 5].Value = activity.Subject;
                    worksheet.Cells[i + 2, 6].Value = activity.Notes;
                    worksheet.Cells[i + 2, 7].Value = activity.ProductAmount;
                    worksheet.Cells[i + 2, 8].Value = activity.Store;
                    worksheet.Cells[i + 2, 9].Value = activity.RecordDate.ToString();
                    if (activity.State) { worksheet.Cells[i + 2, 10].Value = "OPEN"; }
                    else { worksheet.Cells[i + 2, 10].Value = "CLOSED"; }
                }

                // Excel dosyasını hafıza akışına kaydet
                using (var stream = new MemoryStream())
                {
                    package.SaveAs(stream);
                    var content = stream.ToArray();

                    // Excel dosyasını indirme olarak döndür
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Activities.xlsx");
                }
            }


        }


        ///private readonly string _connectionString = "\"Data Source=ŞEVVAL;Initial Catalog=Proje;Integrated Security=True;\"";

        
        
        public async Task<ActionResult> SendEmailAsync(int productId, int requestedAmount)
        {
            string body = $@"
            <p>Product ID: {productId}</p>
            <p>Requested Amount: {requestedAmount}</p>
            <p>This is a test email sent from ASP.NET MVC using MailKit.</p>";

            await EmailHelper.SendEmailAsync(
                "eng.sevval.bilgin@gmail.com",
                "Test Email",
                body
            );
            TempData["SuccessMessage"] = "Tedarikçiye mail yollandı!";
            List<StajIlkProje.Models.Activity> activities = db.Activities.Where(a=>a.State).ToList();
            return View("ActivityList",activities);

        }
       


    }
}







