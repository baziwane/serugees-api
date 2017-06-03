using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Serugees.Api.Models;
using Microsoft.Extensions.Logging;
using Serugees.Api.Services;

namespace Serugees.Api.Controllers
{
    public class DummyController : Controller
    {
        private SerugeesContext _ctx;
        public DummyController(SerugeesContext ctx)
        {
            _ctx = ctx;
        }
        [HttpGet]
        [Route("api/testdatabase")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}