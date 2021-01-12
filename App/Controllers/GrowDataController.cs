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

        // GET: api/<GrowDataController>
        [HttpGet]
        public IEnumerable<GrowDataItem> Get()
        {
            _logger.LogInformation("Getting all records.");
            return GetDbContext().FindAll();
        }

        /// <summary>
        /// TODO: used for debugging purposes, delete when not needed anymore.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/token")]
        public string Token()
        {
            JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])), SecurityAlgorithms.HmacSha256)
                    );

            return new JwtSecurityTokenHandler().WriteToken(token);
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
            GetDbContext().Save(dataItem);
        }

        private DbContext GetDbContext()
        {
            return HttpContext.RequestServices.GetService(typeof(DbContext)) as DbContext;
        }
    }
}
