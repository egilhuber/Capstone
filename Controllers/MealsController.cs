﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using healthicly.Data;
using healthicly.Models;
using healthicly.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using IronPdf.Forms;
using Syncfusion.Pdf.Graphics;
using System.Drawing;

namespace healthicly.Controllers
{
    public class MealsController : Controller
    {
        private readonly ApplicationDbContext _context;


        public MealsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Menu()
        {
            List<Meal> sunday = GenerateMenu();
            List<Meal> monday = GenerateMenu();
            List<Meal> tuesday = GenerateMenu();
            List<Meal> wednesday = GenerateMenu();
            List<Meal> thursday = GenerateMenu();
            List<Meal> friday = GenerateMenu();
            List<Meal> saturday = GenerateMenu();
            ViewData["Sunday"] = sunday;
            ViewData["Monday"] = monday;
            ViewData["Tuesday"] = tuesday;
            ViewData["Wednesday"] = wednesday;
            ViewData["Thursday"] = thursday;
            ViewData["Friday"] = friday;
            ViewData["Saturday"] = saturday;
            return View();
        }

        public List<Meal> GenerateMenu()
        {
            List<Meal> allMeals = _context.Meals.Select(m => m).ToList();
            List<Meal> allBreakfastMeals = _context.Meals.Include(m => m.Category).Where(m => m.Category.Name == "Breakfast").ToList();
            List<Meal> allLunchMeals = _context.Meals.Include(m => m.Category).Where(m => m.Category.Name == "Lunch").ToList();
            List<Meal> allDinnerMeals = _context.Meals.Include(m => m.Category).Where(m => m.Category.Name == "Dinner").ToList();
            List<Meal> allSnackMeals = _context.Meals.Include(m => m.Category).Where(m => m.Category.Name == "Snack").ToList();

            List<Meal> approvedBreakfastMeals = new List<Meal>();
            List<Meal> approvedLunchMeals = new List<Meal>();
            List<Meal> approvedDinnerMeals = new List<Meal>();
            List<Meal> approvedSnackMeals = new List<Meal>();

            foreach(Meal m in allBreakfastMeals)
            {
                if(m.IsApproved == true)
                {
                    approvedBreakfastMeals.Add(m);
                }
            }

            foreach (Meal m in allLunchMeals)
            {
                if (m.IsApproved == true)
                {
                    approvedLunchMeals.Add(m);
                }
            }

            foreach (Meal m in allDinnerMeals)
            {
                if (m.IsApproved == true)
                {
                    approvedDinnerMeals.Add(m);
                }
            }

            foreach (Meal m in allSnackMeals)
            {
                if (m.IsApproved == true)
                {
                    approvedSnackMeals.Add(m);
                }
            }

            int breakfastCount = approvedBreakfastMeals.Count();
            int lunchCount = approvedLunchMeals.Count();
            int dinnerCount = approvedDinnerMeals.Count();
            int snackCount = approvedSnackMeals.Count();

            int chosenBreakfast = RandomNumber(breakfastCount);
            int chosenLunch = RandomNumber(lunchCount);
            int chosenDinner = RandomNumber(dinnerCount);
            int chosenSnack = RandomNumber(snackCount);

            List<Meal> generatedMenu = new List<Meal>();
            Meal chosenBreakfastItem = approvedBreakfastMeals[chosenBreakfast];
            Meal chosenLunchItem = approvedLunchMeals[chosenLunch];
            Meal chosenDinnerItem = approvedDinnerMeals[chosenDinner];
            Meal chosenSnackItem = approvedSnackMeals[chosenSnack];
            generatedMenu.Add(chosenBreakfastItem);
            generatedMenu.Add(chosenLunchItem);
            generatedMenu.Add(chosenDinnerItem);
            generatedMenu.Add(chosenSnackItem);

            return generatedMenu;
        }

        public int RandomNumber(int max)
        {
            Random random = new Random();
            return random.Next(1, max);
        }

        // GET: Meals
        public async Task<IActionResult> Index()
        {
            return View(await _context.Meals.Include(s => s.Category).ToListAsync());
        }

        public IActionResult ApprovedMeals()
        {
            DateTime today = DateTime.Today;
            List<Meal> approvedMeals = _context.Meals.Include(m => m.Category).Where(m => m.IsApproved == true).ToList();
            ViewData["ApprovedMeal"] = approvedMeals;
            return View();
        }

        // GET: Meals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals
                .FirstOrDefaultAsync(m => m.Id == id);
            meal.Category = await _context.Categories.Where(m => m.Id == meal.CategoryId).SingleOrDefaultAsync();
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // GET: Meals/Create
        public IActionResult Create()
        {
            MealCreateViewModel mealCreateViewModel = new MealCreateViewModel(_context);
            return View(mealCreateViewModel);
        }

        // POST: Meals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BriefDescription,Category,CategoryId,Vegan,ContainsDairy,GlutenFree,ContainsSoy,ContainsPeanuts,IsApproved")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                int category = meal.Category.Id;
                meal.Category = _context.Categories.Where(c => c.Id == category).Single();
                meal.CategoryId = meal.Category.Id;
                _context.Add(meal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meal);
        }

        // GET: Meals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals.Include(m => m.Category).FirstOrDefaultAsync(m => m.Id == id);
            meal.Category = await _context.Categories.Where(c => c.Id == meal.CategoryId).SingleOrDefaultAsync();
            if (meal == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", meal.CategoryId);
            return View(meal);
        }

        // POST: Meals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BriefDescription,Category,CategoryId,Vegan,ContainsDairy,GlutenFree,ContainsSoy,ContainsPeanuts,IsApproved")] Meal meal)
        {
            if (id != meal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    meal.Category = await _context.Categories.Where(c => c.Id == meal.CategoryId).SingleOrDefaultAsync();
                    _context.Update(meal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealExists(meal.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", meal.CategoryId);
            return View(meal);
        }

        // GET: Meals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealExists(int id)
        {
            return _context.Meals.Any(e => e.Id == id);
        }

    }
}
