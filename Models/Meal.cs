using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace healthicly.Models
{
    public class Meal
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        [Display(Name = "Brief Description")]
        public string BriefDescription { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public bool Vegan { get; set; }
        [Display(Name = "Contains Dairy")]
        public bool ContainsDairy { get; set; }
        [Display(Name = "Gluten Free")]
        public bool GlutenFree { get; set; }
        [Display(Name = "Contains Soy")]
        public bool ContainsSoy { get; set; }
        [Display(Name = "Contains Peanuts")]
        public bool ContainsPeanuts { get; set; }
        [Display(Name = "Is Approved")]
        public bool IsApproved { get; set; }
    }
}
