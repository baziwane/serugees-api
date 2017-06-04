using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Serugees.Api.Models;
using Serugees.Api.Services;

namespace Serugees.Api.Controllers
{
    [Route("/api/members")]
    public class MembersController : Controller
    {
        private ISerugeesRepository _repository;
        public MembersController(ISerugeesRepository repository)
        {
            _repository = repository;
        }
        [HttpGet()]
        public IActionResult GetMembers()
        {      
            // if(member.FirstName == member.LastName) { modelState.AddModelError("FirstName", "First name can't be same as last name")} 
            //return Ok(MembersDataStore.Current.Members);
            var memberEntities = _repository.GetAllMembers();
            var results = new List<Member>();
            foreach (var member in memberEntities)
            {
                results.Add(new Member
                {
                    Id = member.Id,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    TelephoneNumber = member.TelephoneNumber,
                    DateRegistered = member.DateRegistered,
                    Active = member.Active,
                });
            }    
            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetMember(int id, bool includeLoans = false)
        {
            // var memberToReturn = MembersDataStore.Current.Members.FirstOrDefault(m => m.Id == id);
            // if(memberToReturn == null){
            //     return NotFound();
            // }
            // return Ok(memberToReturn);
            var member = _repository.GetMember(id, includeLoans);
            if(member == null)
            {
                return NotFound();
            }
            if(includeLoans)
            {
                var memberResult = new Member()
                {
                    Id = member.Id,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    TelephoneNumber = member.TelephoneNumber,
                    DateRegistered = member.DateRegistered,
                    Active = member.Active
                };

                foreach (var loan in member.Loans)
                {
                    memberResult.Loans.Add(
                        new Loan()
                        {
                            Id = loan.Id,
                            Duration = loan.Duration,
                            DateRequested = loan.DateRequested,
                            Amount = loan.Amount,
                            IsActive = loan.IsActive
                        });
                }

                return Ok(memberResult);
            }

            var memberToReturn = new Member()
            {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                TelephoneNumber = member.TelephoneNumber,
                DateRegistered = member.DateRegistered,
                Active = member.Active
            };

            return Ok(memberToReturn);

        }
    }
}