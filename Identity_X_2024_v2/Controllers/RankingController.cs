using Identity_X_2024_v2.Data;
using Identity_X_2024_v2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity_X_2024_v2.Controllers
{
    public class RankingController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RankingController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> RankingNajTreningDyscIndex()
        {
            return View(await _context.Sport.ToListAsync());
        }
        public async Task<IActionResult> RankingNajTreningDyscCzasIndex()
        {
            return View(await _context.Sport.ToListAsync());
        }
        //według dystansu
        public async Task<IActionResult> RankingNajTreningDysc(int id)
        {
            var najTrening=_context.Trening.Where(t=>t.SportId == id).
                OrderByDescending(t=>t.Dystans).Include(t=>t.TreningUser);
            return View(najTrening);
        }
        //według czasu
        public async Task<IActionResult> RankingNajTreningDyscCzas(int id)
        {
            var najTrening = _context.Trening.Where(t => t.SportId == id).
                OrderByDescending(t => t.Czas).Include(t => t.TreningUser);
            return View(najTrening);
        }
        public async Task<IActionResult> SumaUzytkownikDyscyplinaIndex()
        {
            return View(await _context.Sport.ToListAsync());
        }
        public async Task<IActionResult> SumaUzytkownikDyscyplina(int id)
        {
            var results = from trening in _context.Trening
                          where trening.SportId==id
                          group trening by trening.TreningUserId into g
                          select new 
                          {
                              Uzytkownik = g.First().TreningUser.Nazwisko,
                              DystansSuma= g.Sum(d=>d.Dystans),
                          };
            ViewBag.Wyniki = results;
            return View();
        }
    }
}
