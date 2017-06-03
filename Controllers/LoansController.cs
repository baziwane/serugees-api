using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Serugees.Api.Models;
using Microsoft.Extensions.Logging;
using Serugees.Api.Services;

namespace Serugees.Api.Controllers
{
    [Route("api/members")]
    public class LoansController : Controller
    {
        private ILogger<LoansController> _logger;
        private IMailService _mailService;

        public LoansController (ILogger<LoansController> logger, IMailService mail)
        {
            _logger = logger;
            _mailService = mail;
        }

        [HttpGet("{memberId}/loans")]
        public IActionResult GetLoans(int memberId)
        {
            var member = MembersDataStore.Current.Members.FirstOrDefault(m => m.Id == memberId);
            if(member == null)
            {
                _logger.LogInformation($"No loan for member with Id { memberId } was found");
                return NotFound();
            }
            return Ok(member.Loans);
        }

        [HttpGet("{memberId}/loans/{id}", Name = "GetLoan")]
        public IActionResult GetLoan(int memberId, int id)
        {
                var member = MembersDataStore.Current.Members.FirstOrDefault(m => m.Id == memberId);
                if(member == null)
                {
                    return NotFound();
                }
                var loan = member.Loans.FirstOrDefault(l => l.Id == id);
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

            var member = MembersDataStore.Current.Members.FirstOrDefault(m => m.Id == memberId);
            if(member == null)
            {
                return NotFound();
            }

            var maxLoanId = MembersDataStore.Current.Members
                                .SelectMany(m => m.Loans).Max(l => l.Id);
            var finalLoan = new Loan()
            {
                  Id = ++maxLoanId,
                  Amount = 6000000,
                  DateRequested = DateTime.Today,
                  Duration = 2,
                  IsActive = true
            };

            member.Loans.Add(finalLoan);
            _mailService.Send($"New Loan Request Added {loan.Amount}", $"A new loan has been added by {memberId}");
            return CreatedAtRoute("GetLoan", new {
                memberId = memberId, id = finalLoan.Id
            });
        }
    }
}