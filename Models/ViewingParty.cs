using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace healthicly.Models
{
    public class ViewingParty
    {
        [Key]
        public int Id { get; set; }

        public string ContentTitle { get; set; }

        public DateTime DayAndTime { get; set; }
    }
}
