using healthicly.Data;
using healthicly.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace healthicly.ViewModels
{
    public class MealCreateViewModel
    {
        private readonly ApplicationDbContext _context;

        public Meal Meal { get; set; }
        public Category Category { get; set; }
        public SelectList CategoryList { get; set; }

        public MealCreateViewModel(ApplicationDbContext context)
        {
            _context = context;

            List<Category> ListOfCategories = _context.Categories.Select(c => c).ToList();
            CategoryList = new SelectList(ListOfCategories, "Id", "Name");
        }
    }
}
