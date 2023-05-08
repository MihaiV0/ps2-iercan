using System;
using System.Collections.Generic;
using System.Linq;
using DataModel;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulatorController : ControllerBase
    {
        private static List<Stamp> stamps = new List<Stamp>();

        // GET api/simulator
        [HttpGet]
        public IEnumerable<Stamp> Get()
        {
            return stamps;
        }

        // POST api/simulator
        [HttpPost]
        public IActionResult Post([FromBody] Stamp stamp)
        {
            if (stamp == null)
            {
                return BadRequest();
            }

            stamps.Add(stamp);

            return Ok();
        }
    }
}
