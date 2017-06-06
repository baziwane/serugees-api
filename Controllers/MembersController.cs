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

        [HttpGet("{id}", Name = "GetMember")]
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

        [HttpPost()]
        public IActionResult AddMember([FromBody]CreateMemberDto member)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var memberToAdd = Mapper.Map<Entities.Member>(member);

            if(!_repository.AddMember(memberToAdd))
            {
                return StatusCode(500, "A Fatal error occurred while performing this operation.");
            }

            var createdMemberToReturn = Mapper.Map<Models.MemberWithoutLoansDto>(memberToAdd); 
            
            return CreatedAtRoute("GetMember", new 
            { id = createdMemberToReturn.Id }, createdMemberToReturn);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateMember(int id, [FromBody]MemberDto member)
        {
            throw new NotImplementedException();
        }

         [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id)
        {
            throw new NotImplementedException();
        }
    }
}