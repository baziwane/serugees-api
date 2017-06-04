using System;
using System.Collections.Generic;

namespace Serugees.Api.Models
{
    public class LoanDto
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int Duration { get; set; }
        public DateTime DateRequested { get; set; }
        public bool IsActive { get; set; }
        public ICollection<PaymentDto> Payments { get; set; } = new List<PaymentDto>();
    }
}