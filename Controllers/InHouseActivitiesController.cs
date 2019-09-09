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
    public class InHouseActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InHouseActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InHouseActivities
        public async Task<IActionResult> Index()
        {
            return View(await _context.InHouseActivities.ToListAsync());
        }

        // GET: InHouseActivities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inHouseActivity = await _context.InHouseActivities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inHouseActivity == null)
            {
                return NotFound();
            }

            return View(inHouseActivity);
        }

        // GET: InHouseActivities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InHouseActivities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DayAndTime,BriefDescription,IsApproved")] InHouseActivity inHouseActivity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inHouseActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inHouseActivity);
        }

        // GET: InHouseActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inHouseActivity = await _context.InHouseActivities.FindAsync(id);
            if (inHouseActivity == null)
            {
                return NotFound();
            }
            return View(inHouseActivity);
        }

        // POST: InHouseActivities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DayAndTime,BriefDescription,IsApproved")] InHouseActivity inHouseActivity)
        {
            if (id != inHouseActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inHouseActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InHouseActivityExists(inHouseActivity.Id))
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
            return View(inHouseActivity);
        }

        // GET: InHouseActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inHouseActivity = await _context.InHouseActivities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inHouseActivity == null)
            {
                return NotFound();
            }

            return View(inHouseActivity);
        }

        // POST: InHouseActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inHouseActivity = await _context.InHouseActivities.FindAsync(id);
            _context.InHouseActivities.Remove(inHouseActivity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InHouseActivityExists(int id)
        {
            return _context.InHouseActivities.Any(e => e.Id == id);
        }
    }
}
