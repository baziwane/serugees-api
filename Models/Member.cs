using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serugees.Api.Models
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set;}

        [Required(ErrorMessage="FirstName is required")]
        [MaxLength(50)]
        public string FirstName { get; set;}

        [Required(ErrorMessage="LastName is required")]
        [MaxLength(50)]
        public string LastName {get; set;}

        [Required(ErrorMessage="TelephoneNumber is required")]
        [MaxLength(25)]       
        public string TelephoneNumber { get; set;}

        public string DateRegistered { get;set; }
        
        public bool Active { get;set; }
        // public int NumberOfLoans { get
        //     {
        //         return Loans.Count;
        //     }
        // } 
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}