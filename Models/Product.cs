using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace StajIlkProje.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public int number { get; set; }
        public int carat { get; set; }
        public int gram { get; set; }
        public DateTime RecordDate { get; set; }= DateTime.Now;
        public string kategory { get; set; }
        public bool isActive { get; set; } = true;
        public string subkategory { get; set; }
        //public  enum Kategori
        //{
        //    Yuzuk, Bileklik, Kolye, Gram, Ceyrek, Yarım, Tam, Resad, Ata, Bilezik, Kupe, Saat, Zincir
        //}

       

    }
}