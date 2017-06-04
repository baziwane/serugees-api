using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Serugees.Api.Models;
using Serugees.Api;
using Serugees.Api.Services;

namespace Serugees.Api.Controllers
{
    [Route("api/members")]
    public class PaymentsController : Controller
    {
        private ILogger<PaymentsController> _logger;
        private IMailService _mailService;
        private ISerugeesRepository _repository;

        public PaymentsController (ILogger<PaymentsController> logger, IMailService mail,
            ISerugeesRepository repository)
        {
            _logger = logger;
            _mailService = mail;
            _repository = repository;
        }

        [HttpGet("{memberId}/loans/{loanId}/payments")]
        public IActionResult GetPayments(int memberId, int loanId)
        {
             if(!_repository.MemberExists(memberId))
            {
                _logger.LogInformation($"Member with id {memberId} was not found when accessing payment.");
                return NotFound();
            }

            var loanPayments = _repository.GetAllPaymentsForLoanForMember(memberId, loanId);
            var results = Mapper.Map<IEnumerable<PaymentDto>>(loanPayments);
            return Ok(results);  
        }

        [HttpGet("{memberId}/loans/{loanId}/payments/{id}", Name = "GetPayment")]
        public IActionResult GetPayment(int memberId, int loanId, int id)
        {
            
            if(!_repository.MemberExists(memberId))
            {
                return NotFound();
            }

            var payment = _repository.GetPaymentForLoanForMember(memberId, loanId,id);
            if(payment == null)
            {
                return NotFound();
            }

            var paymentToReturn = Mapper.Map<PaymentDto>(payment);
            return Ok(paymentToReturn);
        }

        // [HttpPost("{memberId}/loans/{loanId}/payments")]
        // public IActionResult AddPayment(int memberId, int loanId, [FromBody]PaymentDto payment)
        // {
        //     if (payment == null)
        //     {
        //         return BadRequest();
        //     }

        //     if(!ModelState.IsValid)
        //     {
        //         return BadRequest();
        //     }

        //     var member = MembersDataStore.Current.Members.FirstOrDefault(m => m.Id == memberId);
        //     if(member == null)
        //     {
        //         return NotFound();
        //     }

        //     var loan = member.Loans.FirstOrDefault(l => l.Id == loanId);
        //     if(loan == null)
        //     {
        //         return NotFound();
        //     }

        //     var maxPaymentId = member.Loans.SelectMany(p => p.Payments).Max(paid => paid.Id);
        //     var finalPayment = new PaymentDto()
        //     {
        //           Id = ++maxPaymentId,
        //           AmoutPaid = 500000,
        //           OutstandingBalance = 0,
        //           NextInstallmentDueDate = DateTime.Now,
        //           MinimumPaymentDueAtNextInstallment = 0,
        //           DateDeposited = DateTime.Now
        //     };

        //       loan.Payments.Add(finalPayment);
        //       return CreatedAtRoute("GetPayment", new {
        //           loandId = loanId, id = finalPayment.Id
        //       });
        // }

    }
}