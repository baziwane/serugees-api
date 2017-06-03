using System;
using System.ComponentModel.DataAnnotations;

namespace Serugees.Api.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OutstandingBalance { get; set; }
        [Required(ErrorMessage="AmountPaid is required")]
        public int AmoutPaid { get; set; }
        public DateTime DateDeposited { get; set; }
        public DateTime NextInstallmentDueDate { get; set; }
        public int MinimumPaymentDueAtNextInstallment { get; set; }

    }
}