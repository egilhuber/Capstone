using healthicly.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace healthicly.Models
{
    public class ItemToBuy : ShoppingListItem
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Is Approved")]
        public bool IsApproved { get; set; }
    }
}
