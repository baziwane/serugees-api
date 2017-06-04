using System;
namespace Serugees.Api.Models
{
    public class LoanWithoutPaymentsDto
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int Duration { get; set; }
        public DateTime DateRequested { get; set; }
        public bool IsActive { get; set; }
    }
}