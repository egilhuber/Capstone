using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace healthicly.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Position")]
        public string Position { get; set; }
        [Display(Name = "Shift")]
        [ForeignKey("ShiftId")]
        public int ShiftId { get; set; }
        public Shift Shift { get; set; }
        [Display(Name = "Assigned Client")]
        [ForeignKey("ClientId")]
        public int ClientId { get; set; }
        [Display(Name = "Assigned Client")]
        public Client AssignedClient { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
