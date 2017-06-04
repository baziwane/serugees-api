using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serugees.Api.Entities
{
    public class Loan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage="Amount is required")]
        public int Amount { get; set; }
        [Required(ErrorMessage="Duration is required")]
        public int Duration { get; set; }
        public DateTime DateRequested { get; set; }
        public bool IsActive { get; set; }
        // public int NumberOfPayments { get
        //     {
        //         return Payments.Count;
        //     }
        // } 
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        
        [ForeignKey("MemberId")]
        public Member Member { get; set; }
        public int MemberId { get; set; }
    }
}