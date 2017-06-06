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

         [HttpPost("{memberId}/loans/{loanId}/payments")]
        public IActionResult AddPayment(int memberId, int loanId, [FromBody]CreatePaymentDto payment)
        {
            
            if(!_repository.MemberExists(memberId))
            {
                return NotFound();
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var finalPayment = Mapper.Map<Entities.Payment>(payment);

            _repository.AddLoanPaymentForMember(memberId, loanId, finalPayment);

            if(!_repository.Save())
            {
                return StatusCode(500, "A Fatal error occurred while performing this operation.");
            } 

            var createdPaymentToReturn = Mapper.Map<Models.PaymentDto>(finalPayment); 
            // send an email about newly added loan
            _mailService.Send($"New Payment of {payment.AmoutPaid} for loan {loanId} added.", $"A new loan has been added by {memberId}");
            
            return CreatedAtRoute("GetPayment", new 
            { memberId = memberId, loanId = loanId, id = createdPaymentToReturn.Id }, createdPaymentToReturn);
        }

    }
}