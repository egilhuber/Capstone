using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using healthicly.Data;
using healthicly.Models;

namespace healthicly.Controllers
{
    public class ItemToBuysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemToBuysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItemToBuys
        public async Task<IActionResult> Index()
        {
            return View(await _context.ItemToBuys.ToListAsync());
        }

        // GET: ItemToBuys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemToBuy = await _context.ItemToBuys
                .FirstOrDefaultAsync(m => m.id == id);
            if (itemToBuy == null)
            {
                return NotFound();
            }

            return View(itemToBuy);
        }

        // GET: ItemToBuys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItemToBuys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,IsApproved,Id")] ItemToBuy itemToBuy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemToBuy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemToBuy);
        }

        // GET: ItemToBuys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemToBuy = await _context.ItemToBuys.FindAsync(id);
            if (itemToBuy == null)
            {
                return NotFound();
            }
            return View(itemToBuy);
        }

        // POST: ItemToBuys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,IsApproved,Id")] ItemToBuy itemToBuy)
        {
            if (id != itemToBuy.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemToBuy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemToBuyExists(itemToBuy.id))
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
            return View(itemToBuy);
        }

        // GET: ItemToBuys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemToBuy = await _context.ItemToBuys
                .FirstOrDefaultAsync(m => m.id == id);
            if (itemToBuy == null)
            {
                return NotFound();
            }

            return View(itemToBuy);
        }

        // POST: ItemToBuys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemToBuy = await _context.ItemToBuys.FindAsync(id);
            _context.ItemToBuys.Remove(itemToBuy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemToBuyExists(int id)
        {
            return _context.ItemToBuys.Any(e => e.id == id);
        }
    }
}
