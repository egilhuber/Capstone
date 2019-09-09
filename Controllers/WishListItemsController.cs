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
    public class WishListItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WishListItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WishListItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.WishListItems.Include(w => w.UserEmail);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: WishListItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishListItem = await _context.WishListItems
                .Include(w => w.UserEmail)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wishListItem == null)
            {
                return NotFound();
            }

            return View(wishListItem);
        }

        // GET: WishListItems/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            return View();
        }

        // POST: WishListItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Reason,EmployeeId,IsApproved")] WishListItem wishListItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wishListItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", wishListItem.EmployeeId);
            return View(wishListItem);
        }

        // GET: WishListItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishListItem = await _context.WishListItems.FindAsync(id);
            if (wishListItem == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", wishListItem.EmployeeId);
            return View(wishListItem);
        }

        // POST: WishListItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Reason,EmployeeId,IsApproved")] WishListItem wishListItem)
        {
            if (id != wishListItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wishListItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WishListItemExists(wishListItem.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", wishListItem.EmployeeId);
            return View(wishListItem);
        }

        // GET: WishListItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishListItem = await _context.WishListItems
                .Include(w => w.UserEmail)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wishListItem == null)
            {
                return NotFound();
            }

            return View(wishListItem);
        }

        // POST: WishListItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wishListItem = await _context.WishListItems.FindAsync(id);
            _context.WishListItems.Remove(wishListItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WishListItemExists(int id)
        {
            return _context.WishListItems.Any(e => e.Id == id);
        }
    }
}
