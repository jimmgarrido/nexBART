using System;
using System.Collections.Generic;
using System.Text;

namespace nexBart.Helpers
{
    public class Requests
    {
        public static string key = "Q9VI-UXQY-I9GQ-DT35";

        public static Dictionary<string, string> Urls = new Dictionary<string, string>()
        {
            {"departures", "http://api.bart.gov/api/etd.aspx?cmd=etd&orig=" }
        };
    }
}
