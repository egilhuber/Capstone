using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using healthicly.Models;
using healthicly.ViewModels;
using healthicly.Data;

namespace healthicly.Controllers
{
    
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Standard") || User.Identity.Name != null)
            {
                var recUrl = Url.Content("~/Home/Dashboard");
                return Redirect(recUrl);
            }
            else
            {
                var recUrl = Url.Content("~/Home/SplashPage");
                return Redirect(recUrl);
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact page";
            return View();
        }

        public IActionResult Dashboard()
        {
            //return OnGetPartial();
            return View();
            
        }

        public IActionResult SplashPage()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult OnGetPartial() =>
            PartialView("_OutingPartial");
    }
}
