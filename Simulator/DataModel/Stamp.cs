using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Stamp
    {
        public DateTime Timestamp { get; set; }
        public bool Sensor1 { get; set; }
        public bool Sensor2 { get; set; }
        public bool Sensor3 { get; set; }
        public bool Sensor4 { get; set; }
        public double WaterLevel { get; set; }
    }

}
