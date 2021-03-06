using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrowDataApp.Core.Model
{
    /// <summary>
    /// Small entity returned as a result of health check.
    /// </summary>
    public class HealthCheck
    {
        public string Status { get; private set; }

        public HealthCheck()
        {
            Status = "OK";
        }
    }
}
