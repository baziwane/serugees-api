using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Serugees.Api.Models;

namespace Serugees.Api.Controllers
{
    [Route("api/members")]
    public class PaymentsController : Controller
    {
        [HttpGet("{memberId}/loans/{loanId}/payments")]
        public IActionResult GetPayments(int memberId, int loanId)
        {
              var member = MembersDataStore.Current.Members.FirstOrDefault(m => m.Id == memberId);
              if(member == null)
              {
                  return NotFound();
              }
              var loan = member.Loans.FirstOrDefault(l => l.Id == loanId);
              if(loan == null)
              {
                  return NotFound();
              }
              return Ok(loan.Payments);
        }

        [HttpGet("{memberId}/loans/{loanId}/payments/{id}", Name = "GetPayment")]
        public IActionResult GetPayment(int memberId, int loanId, int id)
        {
              var member = MembersDataStore.Current.Members.FirstOrDefault(m => m.Id == memberId);
              if(member == null)
              {
                  return NotFound();
              }
              
              var loan = member.Loans.FirstOrDefault(l => l.Id == loanId);
              if(loan == null)
              {
                  return NotFound();
              }
              
              var payment = loan.Payments.FirstOrDefault(p => p.Id == id);
              if(payment == null)
              {
                  return NotFound();
              }
              return Ok(payment);
        }

        [HttpPost("{memberId}/loans/{loanId}/payments")]
        public IActionResult AddPayment(int memberId, int loanId, [FromBody]Payment payment)
        {
            if (payment == null)
            {
                return BadRequest();
            }

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var member = MembersDataStore.Current.Members.FirstOrDefault(m => m.Id == memberId);
            if(member == null)
            {
                return NotFound();
            }

            var loan = member.Loans.FirstOrDefault(l => l.Id == loanId);
            if(loan == null)
            {
                return NotFound();
            }

            var maxPaymentId = member.Loans.SelectMany(p => p.Payments).Max(paid => paid.Id);
            var finalPayment = new Payment()
            {
                  Id = ++maxPaymentId,
                  AmoutPaid = 500000,
                  OutstandingBalance = 0,
                  NextInstallmentDueDate = DateTime.Now,
                  MinimumPaymentDueAtNextInstallment = 0,
                  DateDeposited = DateTime.Now
            };

              loan.Payments.Add(finalPayment);
              return CreatedAtRoute("GetPayment", new {
                  loandId = loanId, id = finalPayment.Id
              });
        }

    }
}