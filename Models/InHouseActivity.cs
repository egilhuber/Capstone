using healthicly.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace healthicly.Models
{
    public class InHouseActivity : DashboardItem
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        [Display(Name = "Day and Time")]
        public DateTime DayAndTime { get; set; }
        [Display(Name = "Brief description of activity")]
        public string BriefDescription { get; set; }

        //public bool Group { get; set; }
        //[Display(Name = "Client")]
        //[ForeignKey("ClientId")]
        //public int ClientId { get; set; }
        //public Client Client { get; set; }
    }
}
