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
        [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
        public JsonResult[] Result { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
    }
}
