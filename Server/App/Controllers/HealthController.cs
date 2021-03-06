using GrowDataApp.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrowDataApp.App.Controllers
{
    /// <summary>
    /// Controller used for health checks.
    /// </summary>
    [Route("/health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public HealthCheck Health()
        {
            _logger.LogInformation("Health check OK.");
            return new HealthCheck();
        }
    }
}
