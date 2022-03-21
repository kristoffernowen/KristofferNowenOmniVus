using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewOmniVus.Data;
using NewOmniVus.Models.Profiles;

namespace KristofferNowen_OmniVus.Controllers
{
    public class AppUserProfileEntitiesController : Controller
    {
        private readonly SecondDbContext _context;

        public AppUserProfileEntitiesController(SecondDbContext context)
        {
            _context = context;
        }

        // GET: AppUserProfileEntities
        public async Task<IActionResult> Index()
        {
            var secondDbContext = _context.Profiles.Include(a => a.Address);
            return View(await secondDbContext.ToListAsync());
        }

        // GET: AppUserProfileEntities/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUserProfileEntity = await _context.Profiles
                .Include(a => a.Address)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUserProfileEntity == null)
            {
                return NotFound();
            }

            return View(appUserProfileEntity);
        }

        // GET: AppUserProfileEntities/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "AddressLine");
            return View();
        }

        // POST: AppUserProfileEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,UserEmail,AddressId")] AppUserProfileEntity appUserProfileEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appUserProfileEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "AddressLine", appUserProfileEntity.AddressId);
            return View(appUserProfileEntity);
        }

        // GET: AppUserProfileEntities/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUserProfileEntity = await _context.Profiles.FindAsync(id);
            if (appUserProfileEntity == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "AddressLine", appUserProfileEntity.AddressId);
            return View(appUserProfileEntity);
        }

        // POST: AppUserProfileEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,UserEmail,AddressId")] AppUserProfileEntity appUserProfileEntity)
        {
            if (id != appUserProfileEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appUserProfileEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserProfileEntityExists(appUserProfileEntity.Id))
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
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "AddressLine", appUserProfileEntity.AddressId);
            return View(appUserProfileEntity);
        }

        // GET: AppUserProfileEntities/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUserProfileEntity = await _context.Profiles
                .Include(a => a.Address)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUserProfileEntity == null)
            {
                return NotFound();
            }

            return View(appUserProfileEntity);
        }

        // POST: AppUserProfileEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var appUserProfileEntity = await _context.Profiles.FindAsync(id);
            _context.Profiles.Remove(appUserProfileEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserProfileEntityExists(string id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }
    }
}
