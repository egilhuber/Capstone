using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace healthicly.Models
{
    public class WishListItem
    {
        [Key] 
        public int Id { get; set; }

        public string Name { get; set; }
        [Display(Name = "Reason Needed")]
        public string Reason { get; set; }
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public Employee UserEmail { get; set; }
    }
}
