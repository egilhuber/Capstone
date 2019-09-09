using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace healthicly.Controllers
{
    public class SmsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}