using Identity_X_2024_v2.Models;
using System.ComponentModel.DataAnnotations;

namespace Identity_X_2024_v2.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Podanie imienia jest wymagane")]
        public string Imie { get; set; }
        [Required(ErrorMessage = "Podanie nazwiska jest wymagane")]
        public string Nazwisko { get; set; }
        public int? Wzrost { get; set; }
        [Display(Name = "Płeć")]
        public string? Plec { get; set; }

        public List<Trening> ListaTreningow=new List<Trening>();
        public List<Waga> ListaWag = new List<Waga>();
    }
}
