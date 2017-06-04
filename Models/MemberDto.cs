using System;
using System.Collections.Generic;

namespace Serugees.Api.Models
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string FirstName { get; set;}
        public string LastName {get; set;}    
        public string TelephoneNumber { get; set;}
        public string DateRegistered { get;set; }     
        public bool Active { get;set; }
        public ICollection<LoanWithoutPaymentsDto> Loans { get; set; } = new List<LoanWithoutPaymentsDto>();
    }
}