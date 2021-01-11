using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrowDataApp.Model
{
    /// <summary>
    /// Class representing one data item received from the measure station.
    /// </summary>
    public class GrowDataItem
    {
        public DateTime Timestamp { get; set; }
        public float SoilTemperature { get; set; }
        public float AirTemperature { get; set; }
        public float SoilHumidity { get; set; }
        public float AirHumidity { get; set; }
    }
}
