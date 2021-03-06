using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrowDataApp.Core.Dao;
using GrowDataApp.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

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
        private readonly IConfiguration _configuration;

        public GrowDataController(ILogger<GrowDataController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this._configuration = configuration;
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<GrowDataItem> Get()
        {
            _logger.LogInformation("Getting all records.");
            return GetDbContext().FindAll();
        }

        [HttpGet]
        public IEnumerable<GrowDataItem> GetInRange(DateTime from, DateTime to)
        {
            _logger.LogInformation("Getting records from {from} to {to}", from, to);
            return GetDbContext().FindInDateRange(from, to);
        }

        /// <summary>
        /// Saves new data item.
        /// </summary>
        /// <param name="dataItem">Data item to be saved.</param>
        // POST api/<GrowDataController>
        [HttpPost]
        [Authorize]
        public void Post([FromBody] GrowDataItem dataItem)
        {
            _logger.LogInformation("Saving new data time for {timestamp}", dataItem.Timestamp);
            try
            {
                GetDbContext().Save(dataItem);
            } catch (Exception ex)
            {
                _logger.LogError("Unexpected exception when saving new data item: {exception}", ex.Message);
                _logger.LogError("Stack trace: {stackTrace}", ex.StackTrace);
            }
        }

        private DbContext GetDbContext()
        {
            return HttpContext.RequestServices.GetService(typeof(DbContext)) as DbContext;
        }
    }
}
