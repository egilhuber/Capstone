using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using healthicly.Data;
using healthicly.Models;
using healthicly.ViewModels;
using healthicly.ApiKeys;
using Microsoft.AspNetCore.Authorization;

namespace healthicly.Controllers
{
    public class OutingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private string MapToken = ApiKey.mapsKey;
        private readonly Object thisLock = new object();
        public string Token => MapToken;

        public OutingsController(ApplicationDbContext context)
        {
            _context = context;
        }





        public ActionResult AddComment(string type, int activityId, string commentData)
        {
            if (type.ToLower() == "outing")
            {
                Outing thisOuting;
                Employee thisEmployee;
                string thisEmail = User.Identity.Name;
                thisEmployee = _context.Employees.Where(e => e.Email == thisEmail).Single();
                Comment comment = new Comment();
                lock (thisLock)
                {
                    thisOuting = _context.Outings.Where(o => o.Id == activityId).Single();
                }
                lock (thisLock)
                {
                    thisEmployee = _context.Employees.Where(e => e.Id == thisEmployee.Id).Single();
                }
                comment.Outing = thisOuting;
                comment.OutingId = thisOuting.Id;
                comment.Employee = thisEmployee;
                comment.EmployeeId = thisEmployee.Id;
                comment.UserComment = commentData;
                comment.CommentDate = DateTime.Now;
                lock (thisLock)
                {
                    _context.Comments.Add(comment);
                    _context.SaveChanges();
                }
                return RedirectToAction("Details", "Outings", new { id = activityId });
            }
            return View();
        }





        // GET: Outings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Outings.ToListAsync());
        }

        // GET: Outings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var outing = await _context.Outings.Include(s => s.Client).FirstOrDefaultAsync(m => m.Id == id);
            var outing = await _context.Outings.FirstOrDefaultAsync(m => m.Id == id);
            var comments = await _context.Comments.Include(s=>s.Employee).Where(c => c.Outing.Id == outing.Id).ToListAsync();
            if (outing == null)
            {
                return NotFound();
            }
            ViewData.Add("Comments", comments);
            return View(outing);
        }

        // GET: Outings/Create
        public IActionResult Create()
        {
            OutingClientViewModel outingClientViewModel = new OutingClientViewModel(_context);
            return View(outingClientViewModel);
        }

        // POST: Outings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DayAndTime,Location,Group,Client,IsApproved")] Outing outing)
        {
            if (ModelState.IsValid)
            {
                //int clientId = outing.Client.Id;
                //outing.Client = _context.Clients.Where(c => c.Id == clientId).Single();
                _context.Add(outing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(outing);
        }

        // GET: Outings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outing = await _context.Outings.FindAsync(id);
            if (outing == null)
            {
                return NotFound();
            }
            return View(outing);
        }

        // POST: Outings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DayAndTime,Location,Group,IsApproved")] Outing outing)
        {
            if (id != outing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(outing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OutingExists(outing.Id))
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
            return View(outing);
        }

        // GET: Outings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outing = await _context.Outings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (outing == null)
            {
                return NotFound();
            }

            return View(outing);
        }

        // POST: Outings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var outing = await _context.Outings.FindAsync(id);
            _context.Outings.Remove(outing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OutingExists(int id)
        {
            return _context.Outings.Any(e => e.Id == id);
        }





    }
}
