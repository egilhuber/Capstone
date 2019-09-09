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
            //InHouseActivities
            List<string> inHouseActivityNames = _context.InHouseActivities.Select(c => c.Name).ToList();
            List<InHouseActivity> todayActivities = _context.InHouseActivities.Where(a => a.DayAndTime.Date == today).ToList();
            List<string> todayActivitiesNames = new List<string>();
            foreach (InHouseActivity a in todayActivities)
            {
                if(a.IsApproved == true)
                {
                    todayActivitiesNames.Add(a.Name);
                }
            }
            ViewData["InHouseActivity"] = todayActivitiesNames;

            //CleaningTasks
            List<string> cleaningTaskNames = _context.CleaningTasks.Select(c => c.Name).ToList();
            List<CleaningTask> myTasks = _context.CleaningTasks.Where(c => c.EmployeeId == thisEmployee.Id).ToList();
            List<string> myTasksNames = new List<string>();
            foreach (CleaningTask c in myTasks)
            {
                if(c.TaskComplete == false)
                {
                    myTasksNames.Add(c.Name);
                }
            }
            ViewData["CleaningTask"] = myTasksNames;

            //Clients
            List<string> clientNames = _context.Clients.Select(c => c.PrefFirstName).ToList();
            List<Client> myClients = _context.Clients.Where(c => c.Id == thisEmployee.ClientId).ToList();
            List<string> myClientsNames = new List<string>();
            foreach(Client c in myClients)
            {
                if(c.CaresComplete == false)
                {
                    myClientsNames.Add(c.PrefFirstName);
                }
            }
            ViewData["Client"] = myClientsNames;

            //Employees
            List<string> employeeNames = _context.Employees.Select(c => c.FirstName).ToList();
            List<Employee> thisShiftEmployees = _context.Employees.Where(e => e.ShiftId == thisEmployee.ShiftId).ToList();
            List<string> thisShiftEmployeeNames = new List<string>();
            foreach (Employee e in thisShiftEmployees)
            {
                thisShiftEmployeeNames.Add(e.FirstName);
            }
            ViewData["Employee"] = thisShiftEmployeeNames;

            //ViewingParties
            List<string> viewingPartyNames = _context.ViewingParties.Select(c => c.ContentTitle).ToList();
            List<ViewingParty> todayViewingParties = _context.ViewingParties.Where(v => v.DayAndTime.Date == today).ToList();
            List<string> todayViewingPartyNames = new List<string>();
            foreach(ViewingParty v in todayViewingParties)
            {
                if(v.IsApproved == true)
                {
                    todayViewingPartyNames.Add(v.ContentTitle);
                }
            }
            ViewData["ViewingParty"] = todayViewingPartyNames;

            //Outings
            List<string> outingNames = _context.Outings.Select(c => c.Name).ToList();
            List<Outing> todayOutings = _context.Outings.Where(o => o.DayAndTime.Date == today).ToList();
            List<string> todayOutingNames = new List<string>();
            foreach(Outing o in todayOutings)
            {
                if(o.IsApproved == true)
                {
                    todayOutingNames.Add(o.Name);
                }
            }
            ViewData["Outing"] = todayOutingNames;


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
