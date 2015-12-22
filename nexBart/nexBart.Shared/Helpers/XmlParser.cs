using nexBart.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace nexBart.Helpers
{
    /*
     * Where all the magic happens
    */

    public class XmlParser
    {
        public static List<Line> ParsePredictions(XDocument doc)
        {
            var stationLines = new List<Line>();

            var destination = "";
            var etdElements = doc.Element("root").Element("station").Elements("etd");

            for (int i=0;i<etdElements.Count(); i++)
            {
                var etd = etdElements.ElementAt(i);

                destination = etd.Element("destination").Value;
                var estimateElements = etd.Elements("estimate");

                for(int j=0; j<estimateElements.Count(); j++)
                {
                    var estimate = estimateElements.ElementAt(j);

                    var lineColor = estimate.Element("color").Value;
                    var estMins = estimate.Element("minutes").Value;
                    var length = estimate.Element("length").Value;
                    var bikeFlag = estimate.Element("bikeflag").Value;

                    var newTrain = new Train(estMins, length, bikeFlag);
                    /**If color does not exist in stationLines:
                    ** - Add new line
                    ** - Add new destination to new line depending on the train's directions
                    ** - Add new train to line
                    **/
                    if (!stationLines.Exists(l => l.colorName == lineColor))
                    {
                        var newLine = new Line(lineColor);

                        if (estimate.Element("direction").Value == "South")
                        {
                            newLine.Destinations[1] = new Destination(destination);
                            newLine.Destinations[1].Times = estMins;
                            newLine.Destinations[1].Trains.Add(newTrain);
                        }
                        else
                        {
                            newLine.Destinations[0] = new Destination(destination);
                            newLine.Destinations[0].Times = estMins;
                            newLine.Destinations[0].Trains.Add(newTrain);
                        }

                        stationLines.Add(newLine);
                    }
                    else
                    {
                        var line = stationLines.Find(l => l.colorName == lineColor);

                        if (estimate.Element("direction").Value == "South")
                        {
                            if(line.Destinations[1] == null) line.Destinations[1] = new Destination(destination);
                            if (line.Destinations[1].Trains.Count < 3)
                            {
                                if (line.Destinations[1].Times == null)
                                    line.Destinations[1].Times = estMins;
                                else
                                    line.Destinations[1].Times += ", " + estMins;

                                line.Destinations[1].Trains.Add(newTrain);
                            }
                        }
                        else
                        {
                            if (line.Destinations[0] == null) line.Destinations[0] = new Destination(destination);
                            if (line.Destinations[0].Trains.Count < 3)
                            {
                                if (line.Destinations[0].Times == null)
                                    line.Destinations[0].Times = estMins;
                                else
                                    line.Destinations[0].Times += ", " + estMins;

                                line.Destinations[0].Trains.Add(newTrain);
                            }
                        }
                    }
                }
            }

            return stationLines;
        }

        public static List<Alert> ParseAlerts(XDocument advDoc, XDocument elevDoc)
        {
            List<Alert> alerts = new List<Alert>();
            string advType, time, desc;
            DateTime fullTime;
            IEnumerable<XElement> bsaElements;

            //Get advisories
            bsaElements = advDoc.Element("root").Elements("bsa");
            foreach(XElement e in bsaElements)
            {
                desc = e.Element("description").Value;

                if (desc != "No delays reported.")
                {

                    advType = e.Element("type").Value;
                    advType = ToUpperFirstLetter(advType);

                    //Format time from given value to friendly AM/PM
                    time = e.Element("posted").Value;
                    time = time.Substring(0, time.Length - 4);

                    fullTime = DateTime.Parse(time);
                    time = fullTime.ToString("hh:mm tt");

                    alerts.Add(new Alert("Advisories", advType, desc, time));
                }
                else
                {
                    alerts.Add(new Alert("Advisories", "", desc + " Enjoy your ride!", ""));
                }
            }

            //Get elevator information
            bsaElements = elevDoc.Element("root").Elements("bsa");
            foreach (XElement e in bsaElements)
            {
                desc = e.Element("description").Value;

                if (desc != "Attention passengers: All elevators are in service. Thank You.")
                {
                    desc = desc.Substring(0, desc.Length - 12);

                    //Format time from given value to friendly AM/PM
                    time = e.Element("posted").Value;
                    time = time.Substring(0, time.Length - 4);

                    fullTime = DateTime.Parse(time);
                    time = fullTime.ToString("hh:mm tt");

                    alerts.Add(new Alert("Elevators", "", desc, time));
                }
                else
                {
                    alerts.Add(new Alert("Elevators", "", desc, ""));
                }
            }

            return alerts;
        }

        public static StationDetails ParseInfo(XDocument infoDoc, XDocument accessDoc)
        {
            var details = new StationDetails();
            var infoElement = infoDoc.Element("root").Element("stations").Element("station");
            var accessElement = accessDoc.Element("root").Element("stations").Element("station");

            var address = infoElement.Element("address").Value;
            var city = infoElement.Element("city").Value;
            string lockers;
            string bikes;
            string parking;

            if (accessElement.Attribute("locker_flag").Value == "1") lockers = "Available";
            else lockers = "Not Available";

            if (accessElement.Attribute("parking_flag").Value == "1") parking = "Available";
            else parking = "Not Available";

            if (accessElement.Attribute("bike_flag").Value == "1") bikes = "Bike Racks";
            else if (accessElement.Attribute("bike_station_flag").Value == "1") bikes = "Bike Station";
            else bikes = "Not Available";

            details.info = infoElement.Element("intro").Value;

            details.address = string.Format("{0} {1}, CA", address, city);
            details.lockers = lockers;
            details.parking = parking;
            details.bikes = bikes;

            return details;
        }

        private static string ToUpperFirstLetter(string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            source = source.ToLower();
            char[] letters = source.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);

            return new string(letters);
        }
    }
}
