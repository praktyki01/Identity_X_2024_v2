﻿using System;
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

namespace Identity_X_2024_v2.Controllers
{
    [Authorize]
    public class TreningController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TreningController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Trening
        public async Task<IActionResult> Index()
        {
            // Ograniczenie wyświetlania danych do zalogowanego użytkownika
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var applicationDbContext = _context.Trening
                .Include(t => t.Sport).Include(t => t.TreningUser).Where(u=>u.TreningUserId==user.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Trening/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trening = await _context.Trening
                .Include(t => t.Sport)
                .Include(t => t.TreningUser)
                .FirstOrDefaultAsync(m => m.TreningId == id);
            if (trening == null)
            {
                return NotFound();
            }
            // Zablokowanie możliwości wyświetlenia danych innych niż zalogowanego użytkownika
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (trening.TreningUserId != user.Id)
                return RedirectToAction("Index");
            return View(trening);
        }

        // GET: Trening/Create
        public IActionResult Create()
        {
            ViewData["SportId"] = new SelectList(_context.Sport, "SportId", "Nazwa");
            ViewData["TreningUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            return View();
        }

        // POST: Trening/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TreningId,SportId,Dystans,Data,Czas,TreningUserId")] Trening trening)
        {
            if (ModelState.IsValid)
            {
                // Sprawdzenie czy towrzymy zalogowanego użytkownika
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (trening.TreningUserId!= user.Id)
                    return RedirectToAction("Index");

                _context.Add(trening);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SportId"] = new SelectList(_context.Sport, "SportId", "Nazwa", trening.SportId);
            //ViewData["TreningUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", trening.TreningUserId);
            return View(trening);
        }

        // GET: Trening/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trening = await _context.Trening.FindAsync(id);
            if (trening == null)
            {
                return NotFound();
            }

            // Sprawdzenie czy edytujemy zalogowanego użytkownika.
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (trening.TreningUserId!= user.Id)
                return RedirectToAction("Index");

            ViewData["SportId"] = new SelectList(_context.Sport, "SportId", "Nazwa", trening.SportId);
            //ViewData["TreningUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", trening.TreningUserId);
            return View(trening);
        }

        // POST: Trening/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TreningId,SportId,Dystans,Data,Czas,TreningUserId")] Trening trening)
        {
            if (id != trening.TreningId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Ograniczenie możliwości zapisania rekordu tylko dla zalogowanego użytkownika
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    if (trening.TreningUserId != user.Id)
                        return RedirectToAction("Index");
                    _context.Update(trening);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreningExists(trening.TreningId))
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
            ViewData["SportId"] = new SelectList(_context.Sport, "SportId", "Nazwa", trening.SportId);
            //ViewData["TreningUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", trening.TreningUserId);
            return View(trening);
        }

        // GET: Trening/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trening = await _context.Trening
                .Include(t => t.Sport)
                .Include(t => t.TreningUser)
                .FirstOrDefaultAsync(m => m.TreningId == id);
            if (trening == null)
            {
                return NotFound();
            }

            // Zablokowanie możliwości usunięcia danych innego użytkownika
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (trening.TreningUserId != user.Id)
                return RedirectToAction("Index");

            return View(trening);
        }

        // POST: Trening/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trening = await _context.Trening.FindAsync(id);
            if (trening != null)
            {
                _context.Trening.Remove(trening);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreningExists(int id)
        {
            return _context.Trening.Any(e => e.TreningId == id);
        }
    }
}
