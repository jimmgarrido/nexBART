using System;
using System.Collections.Generic;
using System.Text;

namespace nexBart.Helpers
{
    public class Requests
    {
        private static readonly string key = "Q9VI-UXQY-I9GQ-DT35";

        private static Dictionary<string, string> Urls = new Dictionary<string, string>()
        {
            {"departures", "http://api.bart.gov/api/etd.aspx?cmd=etd&orig=" },
            {"advisories", "http://api.bart.gov/api/bsa.aspx?cmd=bsa"},
            {"elevators", "http://api.bart.gov/api/bsa.aspx?cmd=elev"},
            {"station-info", "http://api.bart.gov/api/stn.aspx?cmd=stninfo&orig="},
            {"station-access", "http://api.bart.gov/api/stn.aspx?cmd=stnaccess&orig="}
        };

        public static string MakePredictionsUrl(string station)
        {
            string url = Urls["departures"];
            return String.Concat(url, station, "&key=", key);
        }

        public static string MakeInfoUrl(string station)
        {
            string url = Urls["station-info"];
            return string.Format("{0}{1}&key={2}", url, station, key);
        }

        public static string MakeAccessUrl(string station)
        {
            string url = Urls["station-access"];
            return string.Format("{0}{1}&key={2}", url, station, key);
        }

        public static string MakeAdvsUrl()
        {
            string url = Urls["advisories"];
            return String.Concat(url + "&key=", key);
        }

        public static string MakeElevUrl()
        {
            string url = Urls["elevators"];
            return String.Concat(url + "&key=", key);
        }

        //public static string MakeDetails(string station)
        //{
        //    string url = URLs[""]
        //}
    }
}
