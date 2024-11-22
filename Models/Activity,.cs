using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StajIlkProje.Models
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public string CommunicationChannel { get; set; }
        public string Subject { get; set; }
        public string Notes { get; set; }
        public int ProductAmount { get; set; }
        public string Store { get; set; }
        public DateTime RecordDate { get; set; }
        public bool State { get; set; }
        //public int orderState {  get; set; }
        public Activity()
        {
            State = true;
        }
        [NotMapped]

        public string OpenDuration { get; set; }
    }
}