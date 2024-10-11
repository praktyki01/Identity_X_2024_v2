using Identity_X_2024_v2.Data;
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
        //według dystansu
        public async Task<IActionResult> RankingNajTreningDysc(int id)
        {
            var najTrening=_context.Trening.Where(t=>t.SportId == id).
                OrderByDescending(t=>t.Dystans).Include(t=>t.TreningUser);
            return View(najTrening);
        }
    }
}
