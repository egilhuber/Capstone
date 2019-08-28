using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace healthicly.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Comment")]
        public string UserComment { get; set; }

        [Display(Name = "Date")]
        public DateTime CommentDate { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee UserEmail { get; set; }
        [ForeignKey("OutingId")]
        public Outing Outing { get; set; }
        [ForeignKey("ViewingPartyId")]
        public ViewingParty ViewingParty { get; set; }
        [ForeignKey("InHouseActivityId")]
        public InHouseActivity InHouseActivity { get; set; }
         
    }
}
