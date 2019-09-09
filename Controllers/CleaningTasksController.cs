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

namespace healthicly.Controllers
{
    public class CleaningTasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CleaningTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CleaningTasks
        public async Task<IActionResult> Index()
        {
            return View(await _context.CleaningTasks.Include(s => s.AssignedEmployee).ToListAsync());
        }

        public IActionResult PersonalizedTasks()
        {
            return View();
        }

        // GET: CleaningTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cleaningTask = await _context.CleaningTasks.FirstOrDefaultAsync(m => m.Id == id);
            cleaningTask.AssignedEmployee = await _context.Employees.Where(e => e.Id == cleaningTask.EmployeeId).SingleOrDefaultAsync();
            if (cleaningTask == null)
            {
                return NotFound();
            }

            return View(cleaningTask);
        }

        // GET: CleaningTasks/Create
        public IActionResult Create()
        {
            CleaningEmployeesViewModel cleaningEmployeesViewModel = new CleaningEmployeesViewModel(_context);
            return View(cleaningEmployeesViewModel);
        }

        // POST: CleaningTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BriefDescription,AssignedEmployee")] CleaningTask cleaningTask)
        {
            if (ModelState.IsValid)
            {
                int employee = cleaningTask.AssignedEmployee.Id;
                cleaningTask.AssignedEmployee = _context.Employees.Where(e => e.Id == employee).Single();
                _context.Add(cleaningTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cleaningTask);
        }

        // GET: CleaningTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cleaningTask = await _context.CleaningTasks.FindAsync(id);
            if (cleaningTask == null)
            {
                return NotFound();
            }
            return View(cleaningTask);
        }

        // POST: CleaningTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BriefDescription,AssignedEmployee")] CleaningTask cleaningTask)
        {
            if (id != cleaningTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cleaningTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CleaningTaskExists(cleaningTask.Id))
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
            return View(cleaningTask);
        }

        // GET: CleaningTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cleaningTask = await _context.CleaningTasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cleaningTask == null)
            {
                return NotFound();
            }

            return View(cleaningTask);
        }

        // POST: CleaningTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cleaningTask = await _context.CleaningTasks.FindAsync(id);
            _context.CleaningTasks.Remove(cleaningTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CleaningTaskExists(int id)
        {
            return _context.CleaningTasks.Any(e => e.Id == id);
        }
    }
}
