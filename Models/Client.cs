using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StajIlkProje.Models
{
    public class Client
    {
        

        [Key] 
        public int ClientId { get; set; }
        public bool isActive { get; set; } 
        public string Name { get; set; }
        public string Surname { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{2}[\s.-]?\d{2}$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNum { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(255)]
        [Index(IsUnique =true)]
        

        public string email { get; set; }
        //[Required]
        
        //public string password { get; set; }
        public List<Activity> ActivityList { get; set; }
        public Client() {
            isActive = true;
            this.ActivityList = new List<Activity>();
        }
        public Client(string Name, string Surname,string PhoneNum) { 
            this.isActive = true;
            this.Name = Name;
            this.Surname = Surname;
            this.PhoneNum = PhoneNum;
            this.ActivityList = new List<Activity>();




        }


    }

}