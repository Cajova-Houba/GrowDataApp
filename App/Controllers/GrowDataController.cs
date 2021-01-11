using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowDataApp.Core.Dao;
using GrowDataApp.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrowDataApp.App.Controllers
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
            return GetDbContext().FindAll();
        }

        // GET api/<GrowDataController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Saves new data item.
        /// </summary>
        /// <param name="dataItem">Data item to be saved.</param>
        // POST api/<GrowDataController>
        [HttpPost]
        public void Post([FromBody] GrowDataItem dataItem)
        {
            GetDbContext().Save(dataItem);
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

        private DbContext GetDbContext()
        {
            return HttpContext.RequestServices.GetService(typeof(DbContext)) as DbContext;
        }
    }
}
