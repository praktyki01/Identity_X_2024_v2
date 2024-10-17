using Identity_X_2024_v2.Data;
using Identity_X_2024_v2.Models;
using Identity_X_2024_v2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity_X_2024_v2.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var usersList = await _userManager.Users.ToListAsync();
            var users = (from u in usersList
                        select new UserViewModel
                        {
                            Imie=u.Imie,
                            Nazwisko=u.Nazwisko,
                            ListaWag=_context.Waga.Where(w=>w.WagaUserId==u.Id).ToList(),
                            ListaTreningow=_context.Trening.Where(t=>t.TreningUserId==u.Id).ToList()
                        }).FirstOrDefault();
            return View(users);
        }
    }
}
