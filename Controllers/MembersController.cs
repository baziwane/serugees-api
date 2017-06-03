using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Serugees.Api.Controllers
{
    [Route("/api/members")]
    public class MembersController : Controller
    {
        [HttpGet()]
        public IActionResult GetMembers()
        {      
            // if(member.FirstName == member.LastName) { modelState.AddModelError("FirstName", "First name can't be same as last name")}      
            return Ok(MembersDataStore.Current.Members);
        }

        [HttpGet("{id}")]
        public IActionResult GetMember(int id)
        {
            var memberToReturn = MembersDataStore.Current.Members.FirstOrDefault(m => m.Id == id);
            if(memberToReturn == null){
                return NotFound();
            }
            return Ok(memberToReturn);
        }
    }
}