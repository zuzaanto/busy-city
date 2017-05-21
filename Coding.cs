using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace busy_city
{
    public class Coding
    {
        public enum Result
        {
            OK,
            ZERO_RESULTS,
            OVER_QUERY_LIMIT,
            REQUEST_DENIED,
            INVALID_REQUEST,
            UNKNOWN_ERROR

        }
        const string GGeoCodeJsonServiceUrl = "https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&mode={2}&avoid={3}&units={4}&key={5}";
        public string Key { get; set; }
        public Result DirResult { get; set; }

        public DirResult DirInfo(DirParameters dirpar)
        {
            if (string.IsNullOrEmpty(Key))
            {
                throw new MissingFieldException("Your Google API Key is missing");
            }

            using (var client = new WebClient())
            {
                var sO = new StringBuilder();
                var sD = new StringBuilder();
                var sM = new StringBuilder();
                var sA = new StringBuilder();
                var sU = new StringBuilder();
                string formatAddress = string.Format(GGeoCodeJsonServiceUrl, sO.Append(Uri.EscapeUriString(dirpar.Origin)), sD.Append(Uri.EscapeUriString(dirpar.Destination)), sM.Append(Uri.EscapeUriString(dirpar.Mode)), sA.Append(Uri.EscapeUriString(dirpar.Avoid)), sU.Append(Uri.EscapeUriString(dirpar.Units)), Key);
                var result = client.DownloadString(formatAddress);
                var O = JsonConvert.DeserializeObject<DirResult>(result);
                SetGeoResultFlag(O.status);
                return O;
            }
        }
        private string EncodeAddress(DirParameters dirpar)
        {
            var sb = new StringBuilder();


            if (!string.IsNullOrEmpty(dirpar.Origin))
                sb.Append(Uri.EscapeUriString(" " + dirpar.Origin));

            //if (!string.IsNullOrEmpty(dirpar.Destination))
            //    sb.Append(Uri.EscapeUriString(" " + dirpar.Destination));

            //if (!string.IsNullOrEmpty(dirpar.Mode))
            //    sb.Append(Uri.EscapeUriString(" " + dirpar.Mode));

            //if (!string.IsNullOrEmpty(dirpar.Avoid))
            //    sb.Append(Uri.EscapeUriString(" " + dirpar.Avoid));

            //if (!string.IsNullOrEmpty(dirpar.Units))
            //    sb.Append(Uri.EscapeUriString(" " + dirpar.Units));


            return sb.ToString();
        }

        private void SetGeoResultFlag(string status)
        {
            switch (status)
            {
                case "OK":
                    DirResult = Result.OK;
                    break;
                case "ZERO_RESULTS":
                    DirResult = Result.ZERO_RESULTS;
                    break;
                case "OVER_QUERY_LIMIT":
                    DirResult = Result.OVER_QUERY_LIMIT;
                    break;
                case "REQUEST_DENIED":
                    DirResult = Result.REQUEST_DENIED;
                    break;
                case "INVALID_REQUEST":
                    DirResult = Result.INVALID_REQUEST;
                    break;
                case "UNKNOWN_ERROR":
                    DirResult = Result.UNKNOWN_ERROR;
                    break;
            }
        }
    }
}
