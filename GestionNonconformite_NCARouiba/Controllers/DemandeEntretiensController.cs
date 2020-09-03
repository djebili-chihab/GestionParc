using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionNonconformite_NCARouiba.Models;
using GestionNonconformite_NCARouiba.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace GestionParck.Controllers
{
    public class DemandeEntretiensController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        public DemandeEntretiensController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: DemandeEntretiens
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.demandeEntretiens.Include(d => d.Conducteur).Include(d => d.car);
            return View(await appDbContext.ToListAsync());
        }

        // GET: DemandeEntretiens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demandeEntretien = await _context.demandeEntretiens
                .Include(d => d.Conducteur)
                .Include(d => d.car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (demandeEntretien == null)
            {
                return NotFound();
            }

            return View(demandeEntretien);
        }

        // GET: DemandeEntretiens/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = _userManager.Users.ToList<User>().Select(u => new SelectListItem()
            {
                Value = u.Id,
                Text = u.Nom + " " + u.Prenom
            });
            ViewData["CarId"] = new SelectList(_context.cars, "Id", "Matricule");
            return View();
        }

        // POST: DemandeEntretiens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,CarId,Date,Urgence,Commentaire")] DemandeEntretien demandeEntretien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(demandeEntretien);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "DemandeEntretiens", new { @Id = demandeEntretien.Id });
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", demandeEntretien.UserId);
            ViewData["CarId"] = new SelectList(_context.cars, "Id", "Id", demandeEntretien.CarId);
            return View(demandeEntretien);
        }

        // GET: DemandeEntretiens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demandeEntretien = await _context.demandeEntretiens.FindAsync(id);
            if (demandeEntretien == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", demandeEntretien.UserId);
            ViewData["CarId"] = new SelectList(_context.cars, "Id", "Id", demandeEntretien.CarId);
            return View(demandeEntretien);
        }

        // POST: DemandeEntretiens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CarId,Date,Urgence,Commentaire")] DemandeEntretien demandeEntretien)
        {
            if (id != demandeEntretien.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(demandeEntretien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DemandeEntretienExists(demandeEntretien.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", demandeEntretien.UserId);
            ViewData["CarId"] = new SelectList(_context.cars, "Id", "Id", demandeEntretien.CarId);
            return View(demandeEntretien);
        }

        // GET: DemandeEntretiens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demandeEntretien = await _context.demandeEntretiens
                .Include(d => d.Conducteur)
                .Include(d => d.car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (demandeEntretien == null)
            {
                return NotFound();
            }

            return View(demandeEntretien);
        }

        // POST: DemandeEntretiens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var demandeEntretien = await _context.demandeEntretiens.FindAsync(id);
            _context.demandeEntretiens.Remove(demandeEntretien);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DemandeEntretienExists(int id)
        {
            return _context.demandeEntretiens.Any(e => e.Id == id);
        }
    }
}
