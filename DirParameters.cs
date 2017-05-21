using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace busy_city
{
    public class DirParameters
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Mode { get; set; }
        public string Units { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
		public string Avoid { get; set; }
    }
}
