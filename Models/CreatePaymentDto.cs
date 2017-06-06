using System;

namespace Serugees.Api.Models
{
    public class CreatePaymentDto
    {
        public int OutstandingBalance { get; set; }
        public int AmoutPaid { get; set; }
        public DateTime DateDeposited { get; set; }
        public DateTime NextInstallmentDueDate { get; set; }
        public int MinimumPaymentDueAtNextInstallment { get; set; }
    }
}