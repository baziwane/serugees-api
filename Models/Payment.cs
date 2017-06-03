using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serugees.Api.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OutstandingBalance { get; set; }
        [Required(ErrorMessage="AmountPaid is required")]
        public int AmoutPaid { get; set; }
        public DateTime DateDeposited { get; set; }
        public DateTime NextInstallmentDueDate { get; set; }
        public int MinimumPaymentDueAtNextInstallment { get; set; }

        [ForeignKey("LoanId")]
        public Loan Loan { get; set; }
        public int LoanId { get; set; }

    }
}