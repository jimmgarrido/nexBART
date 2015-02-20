using System;
using System.Collections.Generic;
using System.Text;

namespace nexBart.Helpers
{
    public class Requests
    {
        private static string _key = "Q9VI-UXQY-I9GQ-DT35";

        public static string Key
        {
            get
            {
                return _key;
            }
        }
        public static Dictionary<string, string> Urls = new Dictionary<string, string>()
        {
            {"departures", "http://api.bart.gov/api/etd.aspx?cmd=etd&orig=" }
        };
    }
}
