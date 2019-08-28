using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace healthicly.Models
{
    public class Shift
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Shift")]
        public int Name { get; set; }

    }
}
