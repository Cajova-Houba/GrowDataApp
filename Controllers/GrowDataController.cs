using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowDataApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrowDataApp.Controllers
{
    /// <summary>
    /// Controller for accessing the grow data.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GrowDataController : ControllerBase
    {
        private readonly ILogger<GrowDataController> _logger;

        public GrowDataController(ILogger<GrowDataController> logger)
        {
            _logger = logger;
        }

        // GET: api/<GrowDataController>
        [HttpGet]
        public IEnumerable<GrowDataItem> Get()
        {
            _logger.LogInformation("Getting all records.");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new GrowDataItem
            {
                Timestamp = DateTime.Now.AddDays(index),
                SoilTemperature = (float)(24.0f + rng.NextDouble() * 5f),
                SoilHumidity = (float)(24.0f + rng.NextDouble() * 5f),
                AirTemperature = (float)(20.0f + rng.NextDouble() * 5f),
                AirHumidity = (float)(20.0f + rng.NextDouble() * 5f)
            })
            .ToArray(); 
        }

        // GET api/<GrowDataController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        // POST api/<GrowDataController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GrowDataController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GrowDataController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
