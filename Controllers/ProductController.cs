using OfficeOpenXml;
using StajIlkProje.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace StajIlkProje.Controllers
{
    public class ProductController : Controller
    {
        AppDBContext db=new AppDBContext();
        // GET: Urun
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowProductList()
        {
        List<StajIlkProje.Models.Product> products=db.Products.Where(c=>c.isActive==true).ToList();

            return View("ProductList",products);
        }
        public ActionResult SearchInProducts(string text)
        {
            string pattern = $@"\b\w*{Regex.Escape(text)}\w*\b";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var allUsers = db.Products.Where(c => c.isActive == true).ToList();

            var results = allUsers
             .Where(e => (e.kategory != null && regex.IsMatch(e.kategory)) ||
                     (e.subkategory != null && regex.IsMatch(e.subkategory)) 
                     
                     )
         .ToList();

            return View("ProductList", results);
        }
        public ActionResult AddProduct(StajIlkProje.Models.Product product) { 
        db.Products.Add(product);
            db.SaveChanges();
            List<StajIlkProje.Models.Product> products = db.Products.Where(c => c.isActive == true).ToList();

            return View("ProductList", products);
        }
        public ActionResult UpdateProduct(StajIlkProje.Models.Product product) {
            
            StajIlkProje.Models.Product existing_product = db.Products.Find(product.ProductId);
            if (existing_product != null) { 
            existing_product.number = product.number;
                existing_product.carat=product.carat;
                existing_product.gram=product.gram;
                existing_product.RecordDate=product.RecordDate;
                existing_product.kategory=product.kategory;
                existing_product.subkategory=product.subkategory;
                db.SaveChanges() ;
            }
            List<StajIlkProje.Models.Product> products = db.Products.Where(c => c.isActive == true).ToList();
            return View("ProductList", products);
        }
        public ActionResult DeleteProduct(int productId) {
            db.Products.Find(productId).isActive = false;
            db.SaveChanges();
            List<StajIlkProje.Models.Product> products = db.Products.Where(c => c.isActive == true).ToList();
            return View("ProductList", products);
            
        }
        //public ActionResult exportDataToExcel(Collection<StajIlkProje.Models.Product> products)
        //{



        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //    using (var package = new ExcelPackage())
        //    {
        //        var worksheet = package.Workbook.Worksheets.Add("Activities");

        //        // Başlık satırı
        //        worksheet.Cells[1, 1].Value = "Client ID";
        //        worksheet.Cells[1, 2].Value = "Name";
        //        worksheet.Cells[1, 3].Value = "Surname";
        //        worksheet.Cells[1, 4].Value = "PhoneNum";
        //        worksheet.Cells[1, 5].Value = "email";
        //        //worksheet.Cells[1, 6].Value = "password";
        //        worksheet.Cells[1, 6].Value = "State";


        //        // Verileri ekle
        //        for (int i = 0; i < clients.Count; i++)
        //        {
        //            var activity = clients[i];
        //            worksheet.Cells[i + 2, 1].Value = activity.ClientId;
        //            worksheet.Cells[i + 2, 2].Value = activity.Name;
        //            worksheet.Cells[i + 2, 3].Value = activity.Surname;
        //            worksheet.Cells[i + 2, 4].Value = activity.PhoneNum;
        //            worksheet.Cells[i + 2, 5].Value = activity.email;
        //            //worksheet.Cells[i + 2, 6].Value = activity.password;
        //            if (activity.isActive) { worksheet.Cells[i + 2, 7].Value = "Active"; }
        //            else { worksheet.Cells[i + 2, 6].Value = "Active"; }

        //        }

        //        // Excel dosyasını hafıza akışına kaydet
        //        using (var stream = new MemoryStream())
        //        {
        //            package.SaveAs(stream);
        //            var content = stream.ToArray();

        //            // Excel dosyasını indirme olarak döndür
        //            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Ürünler.xlsx");
        //        }
        //    }


        //}
    }
}