using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
            var memberEntities = _repository.GetAllMembers();
            var results = Mapper.Map<IEnumerable<MemberWithoutLoansDto>>(memberEntities);
            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetMember(int id, bool includeLoans = false)
        {
            var member = _repository.GetMember(id, includeLoans);
            if(member == null)
            {
                return NotFound();
            }
            if(includeLoans)
            {
                var memberResult = Mapper.Map<MemberDto>(member);
                return Ok(memberResult);
            }
            var memberToReturn = Mapper.Map<MemberWithoutLoansDto>(member);
            return Ok(memberToReturn);

        }
    }
}