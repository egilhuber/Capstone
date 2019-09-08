using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using healthicly.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using healthicly.Data;

namespace healthicly.ViewComponents
{
    public class PriorityListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public PriorityListViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }
        private Task<List<Outing>> GetItemsAsync()
        {
            return _context.Outings.Select(o => o).ToListAsync();
        }
    }
}
