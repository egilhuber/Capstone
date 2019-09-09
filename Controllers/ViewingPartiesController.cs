using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using healthicly.Data;
using healthicly.Models;
using Microsoft.AspNetCore.Authorization;

namespace healthicly.Controllers
{
    public class ViewingPartiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ViewingPartiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ViewingParties
        public async Task<IActionResult> Index()
        {
            return View(await _context.ViewingParties.ToListAsync());
        }

        // GET: ViewingParties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewingParty = await _context.ViewingParties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viewingParty == null)
            {
                return NotFound();
            }

            return View(viewingParty);
        }

        // GET: ViewingParties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ViewingParties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ContentTitle,DayAndTime,IsApproved")] ViewingParty viewingParty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viewingParty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewingParty);
        }

        // GET: ViewingParties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewingParty = await _context.ViewingParties.FindAsync(id);
            if (viewingParty == null)
            {
                return NotFound();
            }
            return View(viewingParty);
        }

        // POST: ViewingParties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ContentTitle,DayAndTime,IsApproved")] ViewingParty viewingParty)
        {
            if (id != viewingParty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewingParty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViewingPartyExists(viewingParty.Id))
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
            return View(viewingParty);
        }

        // GET: ViewingParties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewingParty = await _context.ViewingParties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viewingParty == null)
            {
                return NotFound();
            }

            return View(viewingParty);
        }

        // POST: ViewingParties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var viewingParty = await _context.ViewingParties.FindAsync(id);
            _context.ViewingParties.Remove(viewingParty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViewingPartyExists(int id)
        {
            return _context.ViewingParties.Any(e => e.Id == id);
        }
    }
}
