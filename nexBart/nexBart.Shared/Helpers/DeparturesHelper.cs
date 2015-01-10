using nexBart.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace nexBart.Helpers
{
    public class DeparturesHelper
    {
        public static async Task<ObservableCollection<Line>> GetDepartures(StationData _station)
        {
            string requestURL = MakeRequestURL(_station.Abbrv);
            var client = new HttpClient();
            var response = new HttpResponseMessage();
            XDocument xmlDoc = new XDocument();
            string reader;

            //Make sure to pull from network not cache everytime
            client.DefaultRequestHeaders.IfModifiedSince = System.DateTime.Now;
            try
            {
                response = await client.GetAsync(new Uri(requestURL));
                response.EnsureSuccessStatusCode();
                reader = await response.Content.ReadAsStringAsync();
                xmlDoc = XDocument.Parse(reader);
            }
            catch(Exception)
            {
                //ErrorHandler.NetworkError("Error getting predictions. Check network connection and try again.");
            }

            return await XmlParser.Departures(xmlDoc);
        }

        private static string MakeRequestURL(string a)
        {
            string url = Requests.Urls["departures"];
            
            return String.Concat(url, a, "&key=", Requests.key);
            //return url;
        }
    }
}
