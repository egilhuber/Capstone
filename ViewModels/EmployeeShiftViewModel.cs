using healthicly.Data;
using healthicly.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace healthicly.ViewModels
{
    public class EmployeeShiftViewModel
    {
        private readonly ApplicationDbContext _context;

        public Employee Employee { get; set; }
        public Shift Shift { get; set; }

        public SelectList ShiftList { get; set; }

        public EmployeeShiftViewModel(ApplicationDbContext context)
        {
            _context = context;

            List<Shift> ListOfShifts = _context.Shifts.Select(s => s).ToList();
            ShiftList = new SelectList(ListOfShifts, "Id", "Name");

        }
    }
}
