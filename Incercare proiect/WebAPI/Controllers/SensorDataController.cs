using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI;
using WebAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace SimulatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulatorController : ControllerBase
    {
        // Adaugă în interiorul clasei SimulatorController
        private static List<double> humidityData = new List<double>();

        [HttpPost]
        public ActionResult Post([FromBody] JObject data)
        {
            double humidity = data["Humidity"].Value<double>();
            humidityData.Add(humidity);

            return Ok();
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(humidityData);
        }

    }
}