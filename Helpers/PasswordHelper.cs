using StajIlkProje.Models;
using System;
using System.Text.RegularExpressions;

namespace StajIlkProje.Helpers
{
    public class PasswordHelper
    {
        private readonly PasswordScoreModel _model;
        private int MinimumLengthPassword = 8;
        private int MaximumLengthPassword = 12;
        private int MinimumNumericChars = 3;
        private int MinimumLowerCaseChars = 1;
        private int MinimumUpperCaseChars = 1;
        private int MinimumSpecialChars = 1;
        private string AllSpecialChars = "~!@#$%^&*()_+{}:\"<>?";

        // Constructor ile modeli al
        public PasswordHelper(PasswordScoreModel model)
        {
            _model = model;
        }

        public PasswordScoreModel CheckStrength(string password)
        {
            // Modeli kullanarak şifreyi kontrol et
            _model.IsValid = false;
            int score = 0;

            try
            {
                int countLower = Regex.Matches(password, @"\p{Ll}").Count;
                int countUpper = Regex.Matches(password, @"\p{Lu}").Count;
                int countSpecial = Regex.Matches(password, "[~!@#$%^&*()_+{}:\"<>?]").Count;
                int countNumber = Regex.Matches(password, "[0-9]").Count;

                if (password.Length < MinimumLengthPassword)
                {
                    _model.Score = PasswordScore.Blank;
                    _model.Context += $" <br/> Girilen Şifre Uzunluğu Minimum {MinimumLengthPassword} Karakter olmak zorunda !";
                    _model.IsValid = false;
                    return _model;
                }

                if (password.Length > MaximumLengthPassword)
                {
                    _model.Score = PasswordScore.VeryWeak;
                    _model.Context += $"Girilen Şifre Uzunluğu Minimum {MaximumLengthPassword} Karakterden az olmak zorunda !";
                    _model.IsValid = false;
                    return _model;
                }

                if (countNumber >= MinimumNumericChars)
                    score++;
                else
                {
                    _model.Score = PasswordScore.VeryWeak;
                    _model.Context += $" <br/> Girilen Şifre en az {MinimumNumericChars} Rakam İçermeli !";
                    _model.IsValid = false;
                }

                if (countLower >= MinimumLowerCaseChars)
                    score++;
                else
                {
                    _model.Score = PasswordScore.VeryWeak;
                    _model.Context += $" <br/> Girilen Şifre en az {MinimumLowerCaseChars} küçük harf içermesi gerekmektedir !";
                    _model.IsValid = false;
                }

                if (countUpper >= MinimumUpperCaseChars)
                    score++;
                else
                {
                    _model.Score = PasswordScore.VeryWeak;
                    _model.Context += $" <br/> Girilen Şifre en az {MinimumUpperCaseChars} Büyük harf içermesi gerekmektedir !";
                    _model.IsValid = false;
                }

                if (countSpecial >= MinimumSpecialChars)
                    score++;
                else
                {
                    _model.Score = PasswordScore.VeryWeak;
                    _model.Context += $" <br/> Girilen Şifre en az {MinimumSpecialChars} adet {AllSpecialChars} karakterlerden birini içermesi gerekmektedir !";
                    _model.IsValid = false;
                }
            }
            catch (Exception e)
            {
                score = (int)PasswordScore.Blank;
                _model.Context = e.InnerException != null ? e.InnerException.Message : e.Message;
                _model.IsValid = false;
            }

            _model.Score = (PasswordScore)score;
            _model.IsValid = score == 4;

            return _model;
        }
    }
}
