using healthicly.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace healthicly.Models
{
    public class Client : DashboardItem
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Preferred First Name")]
        public string PrefFirstName { get; set; }
        [Display(Name = "Last Initial")]
        public string LastInitial { get; set; }

        public string Email { get; set; }

        [Display(Name = "Cares Complete")]
        public bool CaresComplete { get; set; }
    }
}
