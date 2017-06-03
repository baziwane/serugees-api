using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Serugees.Api.Models;
using Microsoft.Extensions.Logging;

namespace Serugees.Api.Controllers
{
    [Route("api/members")]
    public class LoansController : Controller
    {
        private ILogger<LoansController> _logger;
        
        [HttpGet("{memberId}/loans")]
        public IActionResult GetLoans(int memberId)
        {
            var member = MembersDataStore.Current.Members.FirstOrDefault(m => m.MemberId == memberId);
            if(member == null)
            {
                return NotFound();
            }
            return Ok(member.Loans);
        }

        [HttpGet("{memberId}/loans/{id}", Name = "GetLoan")]
        public IActionResult GetLoan(int memberId, int id)
        {
              var member = MembersDataStore.Current.Members.FirstOrDefault(m => m.MemberId == memberId);
              if(member == null)
              {
                  return NotFound();
              }
              var loan = member.Loans.FirstOrDefault(l => l.LoanId == id);
              if(loan == null)
              {
                  return NotFound();
              }
              return Ok(loan);
        }

        [HttpPost("{memberId}/loans")]
        public IActionResult AddLoan(int memberId, [FromBody]Loan loan)
        {
            if (loan == null)
            {
                return BadRequest();
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var member = MembersDataStore.Current.Members.FirstOrDefault(m => m.MemberId == memberId);
            if(member == null)
            {
                return NotFound();
            }

            var maxLoanId = MembersDataStore.Current.Members
                                .SelectMany(m => m.Loans).Max(l => l.LoanId);
            var finalLoan = new Loan()
            {
                  LoanId = ++maxLoanId,
                  Amount = 6000000,
                  DateRequested = DateTime.Today,
                  Duration = 2,
                  IsActive = true
            };

            member.Loans.Add(finalLoan);
            return CreatedAtRoute("GetLoan", new {
                memberId = memberId, id = finalLoan.LoanId
            });
        }
    }
}