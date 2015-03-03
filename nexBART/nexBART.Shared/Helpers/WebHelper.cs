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
    public class WebHelper
    {
        public static async Task<List<Line>> GetPredictions(StationData station)
        {
            string predictionsURL = Requests.MakePredictionsURL(station.Abbrv);
            var client = new HttpClient();
            var response = new HttpResponseMessage();
            XDocument xmlDoc = new XDocument();
            string reader;

            //Make sure to pull from network not cache everytime
            client.DefaultRequestHeaders.IfModifiedSince = System.DateTime.Now;
            try
            {
                response = await client.GetAsync(new Uri(predictionsURL));
                response.EnsureSuccessStatusCode();
                reader = await response.Content.ReadAsStringAsync();
                xmlDoc = XDocument.Parse(reader);
            }
            catch(Exception)
            {
                //ErrorHandler.NetworkError("Error getting predictions. Check network connection and try again.");
            }

            List<Line> lines = await XmlParser.Predictions(xmlDoc);
            return lines;
        }

        public static async Task GetAlerts()
        {
            string advisoryURL = Requests.MakeAdvsURL();
            string elevURL = Requests.MakeElevURL();
        }
    }
}
