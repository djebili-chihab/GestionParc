using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionNonconformite_NCARouiba.Models;
using GestionNonconformite_NCARouiba.Models.Entities;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace GestionParck.Controllers
{
    public class DemandeCarburantsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        public DemandeCarburantsController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: DemandeCarburants
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.demandeCarburants.Include(d => d.Conducteur).Include(d => d.car);
            return View(await appDbContext.ToListAsync());
        }

        // GET: DemandeCarburants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demandeCarburant = await _context.demandeCarburants
                .Include(d => d.Conducteur)
                .Include(d => d.car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (demandeCarburant == null)
            {
                return NotFound();
            }

            return View(demandeCarburant);
        }

        // GET: DemandeCarburants/Create
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

        // POST: DemandeCarburants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,CarId,Date,NouveauKM,PreuveKM,Commentaire")] DemandeCarburant demandeCarburant,IFormFile PreuveKM)
        {
            if (ModelState.IsValid)

            {
                if (PreuveKM != null)
                {
                    var filename = ContentDispositionHeaderValue.Parse(PreuveKM.ContentDisposition).FileName.Trim('"');
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", PreuveKM.FileName);
                    using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                    {
                        await PreuveKM.CopyToAsync(stream);
                    }
                    demandeCarburant.PreuveKM = filename;
                }
                _context.Add(demandeCarburant);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "DemandeCarburants", new { @Id = demandeCarburant.Id }); 
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", demandeCarburant.UserId);
            ViewData["CarId"] = new SelectList(_context.cars, "Id", "Id", demandeCarburant.CarId);
            return View(demandeCarburant);
        }

        // GET: DemandeCarburants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demandeCarburant = await _context.demandeCarburants.FindAsync(id);
            if (demandeCarburant == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", demandeCarburant.UserId);
            ViewData["CarId"] = new SelectList(_context.cars, "Id", "Id", demandeCarburant.CarId);
            return View(demandeCarburant);
        }

        // POST: DemandeCarburants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CarId,Date,NouveauKM,PreuveKM,Commentaire")] DemandeCarburant demandeCarburant)
        {
            if (id != demandeCarburant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(demandeCarburant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DemandeCarburantExists(demandeCarburant.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", demandeCarburant.UserId);
            ViewData["CarId"] = new SelectList(_context.cars, "Id", "Id", demandeCarburant.CarId);
            return View(demandeCarburant);
        }

        // GET: DemandeCarburants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demandeCarburant = await _context.demandeCarburants
                .Include(d => d.Conducteur)
                .Include(d => d.car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (demandeCarburant == null)
            {
                return NotFound();
            }

            return View(demandeCarburant);
        }

        // POST: DemandeCarburants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var demandeCarburant = await _context.demandeCarburants.FindAsync(id);
            _context.demandeCarburants.Remove(demandeCarburant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DemandeCarburantExists(int id)
        {
            return _context.demandeCarburants.Any(e => e.Id == id);
        }
    }
}
