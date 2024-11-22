using System;

namespace StajIlkProje.Models
{
    public class PasswordScoreModel
    {
        public bool IsValid { get; set; }
        public PasswordScore Score { get; set; }
        public string Context { get; set; } = string.Empty;
    }

    public enum PasswordScore
    {
        Blank,
        VeryWeak,
       
        Strong
    }
}
