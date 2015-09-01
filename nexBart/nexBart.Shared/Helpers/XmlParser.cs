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
                                line.Destinations[1].Times += ", " + estMins;
                                line.Destinations[1].Trains.Add(newTrain);
                            }
                        }
                        else
                        {
                            if (line.Destinations[0] == null) line.Destinations[0] = new Destination(destination);
                            if (line.Destinations[0].Trains.Count < 3)
                            {
                                line.Destinations[0].Times += ", " + estMins;
                                line.Destinations[0].Trains.Add(newTrain);
                            }
                        }
                    }
                }
            }

            return stationLines;

            //for (int i = 0; i < etdElements.Count(); i++)
            //{
            //    destElement = etdElements.ElementAt(i);
            //    destName = destElement.Element("destination").Value;

            //    estimateElements = destElement.Elements("estimate");
            //    times = new string[3];  

            //    for(int j=0; j<3; j++)
            //    {
            //        estimate = estimateElements.ElementAt(j);
            //        color = estimate.Element("color").Value;

            //        if (estimate.Element("minutes").Value.Equals("Leaving"))
            //            times[counter] = "Now";
            //        else times[counter] = estimate.Element("minutes").Value;

            //        if (!usedColors.Contains(color))
            //        {
            //            lines.Add(new Line(destName, color));
            //            usedDests.Add(destName);
            //            usedColors.Add(color);
            //            SetDestination(estimate);
            //        }
            //        else
            //        {
            //            if (!usedDests.Contains(destName))
            //            {
            //                //lines.Add(new Line(dest, color));
            //                usedDests.Add(destName);
            //                //usedColors.Add(color);
            //            }
            //            SetDestination(estimate);
            //        }
            //    }
            //}
            //return lines;
        }

        public static async Task<List<Alert>> Alerts(XDocument advDoc, XDocument elevDoc)
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
                    alerts.Add(new Alert("Advisories", "", desc, ""));
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


        //private static void SetDestination(XElement e)
        //{
        //    IEnumerable<Line> line = lines.Where(l => l.colorName.Equals(color));

        //    for (int k = 0; k < line.Count(); k++)
        //    {
        //        if (e.Element("direction").Value.Equals("South"))
        //        {
        //            //line.ElementAt(0).Destinations[1] = destName;
        //            SetTimes(1, line);
        //        }
        //        else
        //        {
        //            //line.ElementAt(0).Destinations[0] = destName;
        //            SetTimes(0, line);
        //        }
        //    }
        //}

        //private static void SetTimes(int id, IEnumerable<Line> _line)
        //{
        //    string allTimes = "";

        //    for(int j=0; j<counter; j++)
        //    {
        //        if(j == 0) allTimes = times[0];
        //        else allTimes = String.Concat(allTimes, ", ", times[j]);
        //    }
        //    //_line.ElementAt(0).Times[id] = allTimes;
        //}

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
