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
        private IMailService _mailService;
        private ISerugeesRepository _repository;
        public MembersController(ISerugeesRepository repository,
        IMailService mail)
        {
            _repository = repository;
            _mailService = mail;
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
        public IActionResult UpdateMember(int id, [FromBody]CreateMemberDto member)
        {
            if (member == null)
            {
                return BadRequest();
            }
             if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!_repository.MemberExists(id))
            {
                return NotFound();
            }

            var memberToUpdate = _repository.GetMember(id, false);
            
            if(memberToUpdate == null)
            {
                return NotFound();
            }

            Mapper.Map(member, memberToUpdate);

            if(!_repository.Save())
            {
                return StatusCode(500, "A Fatal error occurred while performing this operation.");
            } 

            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id)
        {
            if(!_repository.MemberExists(id)){
                return NotFound();
            }

            var member = _repository.GetMember(id, false);
            if(member == null){
                return NotFound();
            }
            _repository.DeleteMember(member);

            if(!_repository.Save()){
                return StatusCode(500, "Sorry, an error occurred while processing your request.");
            }

            _mailService.Send($"{member.LastName} {member.FirstName} has been deleted.", $"A member has been deleted.");

            return NoContent();
        }
    }
}