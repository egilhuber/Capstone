using healthicly.Data;
using healthicly.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace healthicly.ViewModels
{
    public class CleaningEmployeesViewModel
    {
        private readonly ApplicationDbContext _context;
        public Employee Employee { get; set; }
        public CleaningTask CleaningTask { get; set; }
        public SelectList EmployeeList { get; set; }
        public CleaningEmployeesViewModel(ApplicationDbContext context)
        {
            _context = context;
            List<Employee> ListOfEmployees = _context.Employees.Select(e => e).ToList();
            EmployeeList = new SelectList(ListOfEmployees, "Id", "FirstName");
        }
    }
}
