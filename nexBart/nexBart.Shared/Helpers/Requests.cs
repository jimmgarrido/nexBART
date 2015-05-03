using System;
using System.Collections.Generic;
using System.Text;

namespace nexBart.Helpers
{
    public class Requests
    {
        private static readonly string key = "Q9VI-UXQY-I9GQ-DT35";

        private static Dictionary<string, string> URLs = new Dictionary<string, string>()
        {
            {"departures", "http://api.bart.gov/api/etd.aspx?cmd=etd&orig=" },
            {"advisories", "http://api.bart.gov/api/bsa.aspx?cmd=bsa"},
            {"elevators", "http://api.bart.gov/api/bsa.aspx?cmd=elev"},
            {"station-info", "http://api.bart.gov/api/stn.aspx?cmd=stninfo&orig="},
            {"station-access", "http://api.bart.gov/api/stn.aspx?cmd=stnaccess&orig="}
        };

        public static string MakePredictionsURL(string station)
        {
            string url = URLs["departures"];
            return String.Concat(url, station, "&key=", key);
        }

        public static string MakeAdvsURL()
        {
            string url = URLs["advisories"];
            return String.Concat(url + "&key=", key);
        }

        public static string MakeElevURL()
        {
            string url = URLs["elevators"];
            return String.Concat(url + "&key=", key);
        }

        //public static string MakeDetails(string station)
        //{
        //    string url = URLs[""]
        //}
    }
}
