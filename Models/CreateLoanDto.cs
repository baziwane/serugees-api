using System;

namespace Serugees.Api.Models
{
    public class CreateLoanDto
    {
        public int Amount { get; set; }
        public int Duration { get; set; }
        public DateTime DateRequested { get; set; }
        public bool IsActive { get; set; }
    }
}