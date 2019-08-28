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
        public string UserEmail { get; set; }
        [ForeignKey("OutingId")]
        public int? OutingId { get; set; }
        [ForeignKey("ViewingPartyId")]
        public int? ViewingPartyId { get; set; }
        [ForeignKey("InHouseActivityId")]
        public int? InHouseActivityId { get; set; }
         
    }
}
