using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Identity_X_2024_v2.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Display(Name = "Imię")]
        [Required(ErrorMessage ="Podanie imienia jest wymagane")]
        public string Imie { get; set; }
        [Required(ErrorMessage = "Podanie nazwiska jest wymagane")]
        public string Nazwisko { get; set; }
        public int? Wzrost { get; set; }
        [Display(Name = "Płeć")]
        public string? Plec { get; set; }
    }
}
