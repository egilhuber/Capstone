using healthicly.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace healthicly.Models
{
    public class CleaningTask : DashboardItem
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        [Display(Name = "Brief Description")]
        public string BriefDescription { get; set; }
        [Display(Name = "Assigned Employee")]
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        [Display(Name = "Assigned Employee")]
        public Employee AssignedEmployee { get; set; }
        [Display(Name = "Task Complete")]
        public bool TaskComplete { get; set; }
    }
}
