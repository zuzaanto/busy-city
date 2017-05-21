using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace busy_city
{

    public class Waypoints
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string[] Types { get; set; }

    }
    public class routes
    {
        [JsonProperty("legs")]
        public DirectionLeg[] Legs { get; set; }

        


    }
    public class Steps
    {
        [JsonProperty("distance")]
        public ValueText Distance { get; set; }

        [JsonProperty("start_location")]
        public Location StartLocation { get; set; }

        [JsonProperty("end_location")]
        public Location EndLocation { get; set; }
    }

    public class DirectionLeg
    {
        [JsonProperty("steps")]
        public Steps[] Steps { get; set; }

        [JsonProperty("distance")]
        public ValueText Distance { get; set; }

        [JsonProperty("start_location")]
        public Location StartLocation { get; set; }

        [JsonProperty("end_location")]
        public Location EndLocation { get; set; }
    }
    public class ValueText
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        public override string ToString()
        {
            return String.Format("{0} ({1})", Text, Value);
        }
    }
        public class Location
    {
        [JsonProperty("lat", NullValueHandling = NullValueHandling.Ignore)]
        public double Lat { get; set; }

        [JsonProperty("lng", NullValueHandling = NullValueHandling.Ignore)]
        public double Long { get; set; }
    }
}

