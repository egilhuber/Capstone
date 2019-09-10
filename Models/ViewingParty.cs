using healthicly.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace healthicly.Models
{
    public class ViewingParty : DashboardItem
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Content Title")]
        public string ContentTitle { get; set; }
        [Display(Name = "Day and time")]
        public DateTime DayAndTime { get; set; }
        [Display(Name = "Is Approved")]
        public bool IsApproved { get; set; }
    }
}
