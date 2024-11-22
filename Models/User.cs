using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StajIlkProje.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        [Index(IsUnique = true)]
        public string email { get; set; }
        public string password { get; set; }
        public bool isActive { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{2}[\s.-]?\d{2}$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNum { get; set; }
        public bool isAdmin { get; set; }=false;
        public User()
        {
            isActive = true;
        }
    }
}