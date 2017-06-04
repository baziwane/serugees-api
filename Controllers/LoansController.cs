using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
        private ISerugeesRepository _repository;

        public LoansController (ILogger<LoansController> logger, IMailService mail,
            ISerugeesRepository repository)
        {
            _logger = logger;
            _mailService = mail;
            _repository = repository;
        }

        [HttpGet("{memberId}/loans")]
        public IActionResult GetLoans(int memberId)
        {
            try
            {
                if(!_repository.MemberExists(memberId))
                {
                    _logger.LogInformation($"Member with id {memberId} was not found when accessing loan.");
                    return NotFound();
                }
                var loansForMember = _repository.GetAllLoansForMember(memberId);
                var results = Mapper.Map<IEnumerable<LoanWithoutPaymentsDto>>(loansForMember);
                return Ok(results);
            }
            catch (System.Exception ex)
            {         
                _logger.LogCritical($"Exception while retrieving member with id {memberId}.", ex);
                return StatusCode(500, "A Fatal error occurred while processing your request.");
            }
        }

        [HttpGet("{memberId}/loans/{id}", Name = "GetLoan")]
        public IActionResult GetLoan(int memberId, int id, bool includePayments = false)
        {
            if(!_repository.MemberExists(memberId))
            {
                return NotFound();
            }
            var loan = _repository.GetLoanForMember(memberId, id, includePayments);
            if(loan == null)
            {
                return NotFound();
            }
            if(includePayments)
            {
                var loanResult = Mapper.Map<LoanDto>(loan);
                return Ok(loanResult);
            }
            var loanToReturn = Mapper.Map<LoanWithoutPaymentsDto>(loan);

            return Ok(loanToReturn);
        }

        // [HttpPost("{memberId}/loans")]
        // public IActionResult AddLoan(int memberId, [FromBody]LoanDto loan)
        // {
        //     throw NotImplementedException();
        //     // if (loan == null)
        //     // {
        //     //     return BadRequest();
        //     // }

        //     // if(!ModelState.IsValid)
        //     // {
        //     //     return BadRequest(ModelState);
        //     // }

        //     // var member = MembersDataStore.Current.Members.FirstOrDefault(m => m.Id == memberId);
        //     // if(member == null)
        //     // {
        //     //     return NotFound();
        //     // }

        //     // var maxLoanId = MembersDataStore.Current.Members
        //     //                     .SelectMany(m => m.Loans).Max(l => l.Id);
        //     // var finalLoan = new LoanDto()
        //     // {
        //     //       Id = ++maxLoanId,
        //     //       Amount = 6000000,
        //     //       DateRequested = DateTime.Today,
        //     //       Duration = 2,
        //     //       IsActive = true
        //     // };

        //     // member.Loans.Add(finalLoan);
        //     // _mailService.Send($"New Loan Request Added {loan.Amount}", $"A new loan has been added by {memberId}");
        //     // return CreatedAtRoute("GetLoan", new {
        //     //     memberId = memberId, id = finalLoan.Id
        //     // });
        // }
    }
}