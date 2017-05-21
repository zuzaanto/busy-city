using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace busy_city
{
    public class DirResult
    {
        [JsonProperty("geocoded_waypoints", NullValueHandling = NullValueHandling.Ignore)]
        public Waypoints[] Waypoint { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string status { get; set; }

        [JsonProperty("routes", NullValueHandling = NullValueHandling.Ignore)]
        public routes[] routes { get; set; }
    }
}
