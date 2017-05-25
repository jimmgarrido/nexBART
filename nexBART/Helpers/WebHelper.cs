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
        public static async Task<List<Line>> GetPredictions(Station station)
        {
            string predictionsUrl = Requests.MakePredictionsUrl(station.Abbrv);
            var client = new HttpClient();
            var response = new HttpResponseMessage();
            var xmlDoc = new XDocument();

            //Make sure to pull from network not cache everytime
            client.DefaultRequestHeaders.IfModifiedSince = System.DateTime.Now;
            try
            {
                response = await client.GetAsync(new Uri(predictionsUrl));
                response.EnsureSuccessStatusCode();
                var reader = await response.Content.ReadAsStringAsync();
                xmlDoc = XDocument.Parse(reader);
            }
            catch(Exception)
            {
                //ErrorHandler.NetworkError("Error getting predictions. Check network connection and try again.");
            }

            client.Dispose();
            response.Dispose();

            return XmlParser.ParsePredictions(xmlDoc);
        }

        public static async Task<List<Alert>> GetAlerts()
        {
            string advisoryURL = Requests.MakeAdvsUrl();
            string elevURL = Requests.MakeElevUrl();

            var client = new HttpClient();
            var response = new HttpResponseMessage();
            XDocument advisoryXml = new XDocument();
            XDocument elevatorXml = new XDocument();
            string reader;

            //Make sure to pull from network not cache everytime
            client.DefaultRequestHeaders.IfModifiedSince = System.DateTime.Now;
            try
            {
                response = await client.GetAsync(new Uri(advisoryURL));
                response.EnsureSuccessStatusCode();
                reader = await response.Content.ReadAsStringAsync();
                advisoryXml = XDocument.Parse(reader);

                response = await client.GetAsync(new Uri(elevURL));
                response.EnsureSuccessStatusCode();
                reader = await response.Content.ReadAsStringAsync();
                elevatorXml = XDocument.Parse(reader);
            } 
            catch(Exception)
            {

            }

            client.Dispose();
            response.Dispose();
           
            return await Task.Run(() => XmlParser.ParseAlerts(advisoryXml, elevatorXml));
        }

        public static async Task<StationDetails> GetStationInfo(Station station)
        {
            string infoUrl = Requests.MakeInfoUrl(station.Abbrv);
            string accessUrl = Requests.MakeAccessUrl(station.Abbrv);

            var client = new HttpClient();
            var response = new HttpResponseMessage();
            XDocument infoXml = new XDocument();
            XDocument accessXml = new XDocument();
            string reader;

            //Make sure to pull from network not cache everytime
            client.DefaultRequestHeaders.IfModifiedSince = DateTime.Now;
            try
            {
                response = await client.GetAsync(new Uri(infoUrl));
                response.EnsureSuccessStatusCode();
                reader = await response.Content.ReadAsStringAsync();
                infoXml = XDocument.Parse(reader);

                response = await client.GetAsync(new Uri(accessUrl));
                response.EnsureSuccessStatusCode();
                reader = await response.Content.ReadAsStringAsync();
                accessXml = XDocument.Parse(reader);
            }
            catch (Exception)
            {

            }

            client.Dispose();
            response.Dispose();

            return await Task.Run(() => XmlParser.ParseInfo(infoXml, accessXml));
        }
    }
}
