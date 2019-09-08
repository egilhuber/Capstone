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
        [Display(Name = "BriefDescription")]
        public string BriefDescription { get; set; }
        [Display(Name = "Assigned Employee")]
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public Employee AssignedEmployee { get; set; }
    }
}
