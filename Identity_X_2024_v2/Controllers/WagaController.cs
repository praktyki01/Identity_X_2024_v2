using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Identity_X_2024_v2.Data;
using Identity_X_2024_v2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NuGet.Packaging.Signing;

namespace Identity_X_2024_v2.Controllers
{
    [Authorize]
    public class WagaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public WagaController(ApplicationDbContext context
            , UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Waga
        public async Task<IActionResult> Index()
        {
            // Ograniczenie wyświetlania danych do zalogowanego użytkownika
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var applicationDbContext = _context.Waga.Include(w => w.WagaUser).
                Where(w => w.WagaUserId == user.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Waga/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waga = await _context.Waga
                .Include(w => w.WagaUser)
                .FirstOrDefaultAsync(m => m.WagaId == id);
            if (waga == null)
            {
                return NotFound();
            }
            // Zablokowanie możliwości wyświetlenia danych innych niż zalogowanego użytkownika
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (waga.WagaUserId != user.Id)
                return RedirectToAction("Index");
            return View(waga);
        }

        // GET: Waga/Create
        public IActionResult Create()
        {
            ViewData["WagaUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            return View();
        }

        // POST: Waga/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WagaId,Data,Wartosc,WagaUserId")] Waga waga)
        {
            if (ModelState.IsValid)
            {
                // Sprawdzenie czy towrzymy zalogowanego użytkownika
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (waga.WagaUserId != user.Id)
                    return RedirectToAction("Index");
              
                _context.Add(waga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["WagaUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", waga.WagaUserId);
            return View(waga);
        }

        // GET: Waga/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waga = await _context.Waga.FindAsync(id);
            if (waga == null)
            {
                return NotFound();
            }
            // Sprawdzenie czy edytujemy zalogowanego użytkownika.
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (waga.WagaUserId != user.Id)
                return RedirectToAction("Index");
            
            //ViewData["WagaUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", waga.WagaUserId);
            return View(waga);
        }

        // POST: Waga/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WagaId,Data,Wartosc,WagaUserId")] Waga waga)
        {
            if (id != waga.WagaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Ograniczenie możliwości zapisania rekordu tylko dla zalogowanego użytkownika
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    if (waga.WagaUserId != user.Id)
                        return RedirectToAction("Index");
                    _context.Update(waga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WagaExists(waga.WagaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["WagaUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", waga.WagaUserId);
            return View(waga);
        }

        // GET: Waga/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waga = await _context.Waga
                .Include(w => w.WagaUser)
                .FirstOrDefaultAsync(m => m.WagaId == id);
            if (waga == null)
            {
                return NotFound();
            }
            // Zablokowanie możliwości usunięcia danych innego użytkownika
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (waga.WagaUserId != user.Id)
                return RedirectToAction("Index");
            return View(waga);
        }

        // POST: Waga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var waga = await _context.Waga.FindAsync(id);
            if (waga != null)
            {
                // Zablokowanie usunięcia danych innego użytkownika
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (waga.WagaUserId != user.Id)
                    return RedirectToAction("Index");
                _context.Waga.Remove(waga);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WagaExists(int id)
        {
            return _context.Waga.Any(e => e.WagaId == id);
        }
    }
}
