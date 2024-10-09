using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Identity_X_2024_v2.Models
{
    public class Waga
    {
        public int WagaId { get; set; }
        public DateTime Data { get; set; }
        public float Wartosc { get; set; }

        public string WagaUserId { get; set; }
        public ApplicationUser? WagaUser { get; set; }
    }
}
