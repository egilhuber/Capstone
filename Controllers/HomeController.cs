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
            DateTime today = DateTime.Today;
            var thisUserName = User.Identity.Name;
            var thisEmployee = _context.Employees.FirstOrDefault(e => e.Email == thisUserName);

            List<string> inHouseActivityNames = _context.InHouseActivities.Select(c => c.Name).ToList();
            ViewData["InHouseActivity"] = inHouseActivityNames;

            List<string> cleaningTaskNames = _context.CleaningTasks.Select(c => c.Name).ToList();
            ViewData["CleaningTask"] = cleaningTaskNames;

            List<string> clientNames = _context.Clients.Select(c => c.PrefFirstName).ToList();
            ViewData["Client"] = clientNames;

            List<string> employeeNames = _context.Employees.Select(c => c.FirstName).ToList();
            ViewData["Employee"] = employeeNames;

            List<string> viewingPartyNames = _context.ViewingParties.Select(c => c.ContentTitle).ToList();
            ViewData["ViewingParty"] = viewingPartyNames;

            List<string> outingNames = _context.Outings.Select(c => c.Name).ToList();
            ViewData["Outing"] = outingNames;


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
