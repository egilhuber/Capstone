﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using healthicly.Data;
using healthicly.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using healthicly.ApiKeys;

namespace healthicly.Controllers
{
    public class AlertsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlertsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alerts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Alerts.Include(a => a.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Alerts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alert = await _context.Alerts
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alert == null)
            {
                return NotFound();
            }

            return View(alert);
        }

        // GET: Alerts/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            return View();
        }

        // POST: Alerts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Message,Date,EmployeeId")] Alert alert)
        {
            var thisUserName = User.Identity.Name;
            var thisEmployee = _context.Employees.FirstOrDefault(e => e.Email == thisUserName);
            List<Employee> poolEmployees = _context.Employees.Where(e => e.ShiftId == 4).ToList();
            DateTime currentTime = DateTime.Now;
            var thisShift = thisEmployee.ShiftId;
            List<Employee> sendToTheseEmployees = _context.Employees.Where(e => e.ShiftId == thisShift).ToList();
            foreach (Employee e in poolEmployees)
            {
                sendToTheseEmployees.Add(e);
            }
            List<string> phoneNumbers = new List<string>();
            foreach(Employee e in sendToTheseEmployees)
            {
                phoneNumbers.Add(e.PhoneNumber);
            }


            if (ModelState.IsValid)
            {
                alert.Employee = thisEmployee;
                alert.EmployeeId = thisEmployee.Id;
                alert.Date = currentTime;
                _context.Add(alert);
                await _context.SaveChangesAsync();

                foreach (string s in phoneNumbers)
                {
                    SendAlertText(s, alert.Title);
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", alert.EmployeeId);
            
            return View(alert);
        }

        // GET: Alerts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alert = await _context.Alerts.FindAsync(id);
            if (alert == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", alert.EmployeeId);
            return View(alert);
        }

        // POST: Alerts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Message,Date,EmployeeId")] Alert alert)
        {
            if (id != alert.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlertExists(alert.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", alert.EmployeeId);
            return View(alert);
        }

        // GET: Alerts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alert = await _context.Alerts
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alert == null)
            {
                return NotFound();
            }

            return View(alert);
        }

        // POST: Alerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alert = await _context.Alerts.FindAsync(id);
            _context.Alerts.Remove(alert);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlertExists(int id)
        {
            return _context.Alerts.Any(e => e.Id == id);
        }

        public void SendAlertText(string sendHere, string sendMessage)
        {
            const string accountSid = ApiKey.accountSid;
            const string authToken = ApiKey.authToken;

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: sendMessage,
                from: new Twilio.Types.PhoneNumber("+12624193587"),
                to: new Twilio.Types.PhoneNumber(sendHere)
            );

            Console.WriteLine(message.Sid);
        }
    }
}
