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
    public class PredictionsHelper
    {
        public static async Task<ObservableCollection<Line>> GetPredictions(StationData station)
        {
            string requestURL = MakeRequestURL(station.Abbrv);
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

            return XmlParser.Predictions(xmlDoc);
        }

        private static string MakeRequestURL(string abbrv)
        {
            string url = Requests.Urls["departures"]; 
            return String.Concat(url, abbrv, "&key=", Requests.Key);
        }
    }
}
