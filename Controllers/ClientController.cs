using OfficeOpenXml;
using StajIlkProje.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security;
namespace StajIlkProje.Controllers
{
    public class ClientController : Controller
    {
        AppDBContext myDatabase = new AppDBContext();
        
        #region Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
        #endregion

        #region ShowViews
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Activities()
        {
            return View("Activities");
        }

        public ActionResult ClientList(StajIlkProje.Models.Client client)
        {
            var clients = myDatabase.Clients.ToList();
            return View("ClientList", clients);
        }
        public ActionResult ShowActiveClientsList() {

            List<Client> activeClients = myDatabase.Clients.Where(c => c.isActive).ToList();
            TempData["from"] = "ShowActiveClientsList";
            return View("ClientList", activeClients);
        }
        public ActionResult ShowClientList()
        {
            
            List<Client> liste= myDatabase.Clients.ToList();
            TempData["from"] = "ShowClientList";
            return View("ClientList",liste);
        }
        public ActionResult ShowSearchedClientList(string word) {
            string pattern = $@"\b\w*{Regex.Escape(word)}\w*\b";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            
            List<Client> searchedClients = myDatabase.Clients.ToList().
                Where(e => (e.email != null && regex.IsMatch(e.email))|| (e.Name != null && regex.IsMatch(e.Name))|| (e.Surname != null && regex.IsMatch(e.Surname))|| (e.PhoneNum != null && regex.IsMatch(e.PhoneNum))
                ).ToList();
            TempData["from"] = "ShowSearchedClientList";
            return View("ClientList", searchedClients);
        }
        public ActionResult ShowSearchedClientListForOpen(string word)
        {
            string pattern = $@"\b\w*{Regex.Escape(word)}\w*\b";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            List<Client> searchedClients = myDatabase.Clients.Where(c=>c.isActive==true).ToList().
                Where(e => (e.email != null && regex.IsMatch(e.email)) || (e.Name != null && regex.IsMatch(e.Name)) || (e.Surname != null && regex.IsMatch(e.Surname)) || (e.PhoneNum != null && regex.IsMatch(e.PhoneNum))
                ).ToList();
            TempData["from"] = "ShowSearchedClientList";
            return View("ClientList", searchedClients);
        }
        public ActionResult ShowUpdateClientfromClientList(Client model)
        {
            TempData["from"] = "ShowUpdateClientfromClientList";
            return View("UpdateClient", model);

        }
        public ActionResult ShowUpdateClientfromOpenClientList(Client model)
        {
            TempData["from"] = "ShowUpdateClientfromOpenClientList";
            return View("UpdateClient", model);

        }
        public ActionResult ShowAddClient()
        {

            return View("AddClient");
        }
        #endregion
        
        #region AddClient
        public ActionResult AddClient(Client client)
        {
            //Console.WriteLine(ModelState.IsValid);
            if (ModelState.IsValid)
            {
                try
                {
                    myDatabase.Clients.Add(client);
                    myDatabase.SaveChanges();
                    TempData["from"] = "ShowClientList";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "An error occurred while adding the client.";

                }
            }
            return View("ClientList", myDatabase.Clients.ToList());


        }
        [HttpPost]
        
        public ActionResult AddNewClient(Client client)
        {
            //Console.WriteLine(client.Name);
            if (ModelState.IsValid)
            {
                try
                {
                    myDatabase.Clients.Add(client);
                    myDatabase.SaveChanges();
                    TempData["from"] = "ShowClientList";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "An error occurred while adding the client.";

                }
            }
            return View("ClientList", myDatabase.Clients.ToList());

             
            
            

        }
        #endregion

        #region DeleteClient
        public ActionResult DeleteClientfromOpenClients(Client model)
        {
            var myDatabase = new AppDBContext();
            var clientToDelete = myDatabase.Clients.Find(model.ClientId);
            if (clientToDelete != null)
            {
                clientToDelete.isActive = false;
                myDatabase.SaveChanges();
            }
            TempData["from"] = "ShowActiveClientsList";
            List<StajIlkProje.Models.Client> open_clients = myDatabase.Clients.Where(c => c.isActive==true).ToList();
            return RedirectToAction("ClientList", open_clients);

        }
        public ActionResult DeleteClientfromAllClients(Client model)
        {
            var myDatabase = new AppDBContext();
            var clientToDelete = myDatabase.Clients.Find(model.ClientId);
            if (clientToDelete != null)
            {
                clientToDelete.isActive = false;
                myDatabase.SaveChanges();
            }
            TempData["from"] = "ShowClientList";
            

            return RedirectToAction("ClientList", myDatabase.Clients.ToList());

        }
        #endregion

        #region UpdateClient
        public ActionResult UpdateClientfromClientList(Client model)
        {
            var client=myDatabase.Clients.Find(model.ClientId);
            if (client != null) { 
                    client.isActive = model.isActive;
                client.email = model.email;
                //client.password = model.password;
                client.Name = model.Name;
                client.Surname = model.Surname;
                client.PhoneNum = model.PhoneNum;
                
                myDatabase.SaveChanges();
            }
            TempData["from"] = "UpdateClientfromClientList";
            return View("ClientList", myDatabase.Clients.ToList());

        }
        public ActionResult UpdateClientfromOpenClientList(Client model)
        {
            var client = myDatabase.Clients.Find(model.ClientId);
            if (client != null)
            {
                client.isActive = model.isActive;
                client.email = model.email;
                //client.password = model.password;
                client.Name = model.Name;
                client.Surname = model.Surname;
                client.PhoneNum = model.PhoneNum;

                myDatabase.SaveChanges();
            }
            TempData["from"] = "UpdateClientfromOpenClientList";
            return View("ClientList", myDatabase.Clients.Where(c=>c.isActive==true).ToList());

        }
        #endregion

        #region Excel
        public ActionResult exportDataToExcel(Collection<StajIlkProje.Models.Client> clients) {


            
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Activities");

                    // Başlık satırı
                    worksheet.Cells[1, 1].Value = "Client ID";
                    worksheet.Cells[1, 2].Value = "Name";
                    worksheet.Cells[1, 3].Value = "Surname";
                    worksheet.Cells[1, 4].Value = "PhoneNum";
                    worksheet.Cells[1, 5].Value = "email";
                    //worksheet.Cells[1, 6].Value = "password";
                    worksheet.Cells[1, 6].Value = "State";
                    

                    // Verileri ekle
                    for (int i = 0; i < clients.Count; i++)
                    {
                        var activity = clients[i];
                        worksheet.Cells[i + 2, 1].Value = activity.ClientId;
                        worksheet.Cells[i + 2, 2].Value = activity.Name;
                        worksheet.Cells[i + 2, 3].Value = activity.Surname;
                        worksheet.Cells[i + 2, 4].Value = activity.PhoneNum;
                        worksheet.Cells[i + 2, 5].Value = activity.email;
                        //worksheet.Cells[i + 2, 6].Value = activity.password;
                    if (activity.isActive) { worksheet.Cells[i + 2, 7].Value = "Active"; }
                    else { worksheet.Cells[i + 2, 6].Value = "Active"; }
                        
                    }

                    // Excel dosyasını hafıza akışına kaydet
                    using (var stream = new MemoryStream())
                    {
                        package.SaveAs(stream);
                        var content = stream.ToArray();

                        // Excel dosyasını indirme olarak döndür
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Clients.xlsx");
                    }
                }


            }

        #endregion

        #region GoBack
        public ActionResult GoBacktoOpenClients()
        {
           List<StajIlkProje.Models.Client> c= myDatabase.Clients.Where(lc=>lc.isActive==true).ToList();
            TempData["from"] = "ShowActiveClientsList";
            return View("ClientList",c);
        }
        public ActionResult GoBacktoAllClients()
        {
            List<StajIlkProje.Models.Client> c = myDatabase.Clients.ToList();
            TempData["from"] = "ShowClientList";
            return View("ClientList", c);
        }
        #endregion
    }
}
        
        
       
        

       
