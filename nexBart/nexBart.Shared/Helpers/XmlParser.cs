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

    class XmlParser
    {
        static List<Line> lines = new List<Line>();
        static List<string> usedDests = new List<string>();
        static List<string> usedColors = new List<string>();

        static string color, destName;
        static string[] times;
        static int counter = 0;

        public static async Task<List<Line>> Predictions(XDocument doc)
        {
            usedColors.Clear();
            usedDests.Clear();
            lines.Clear();

            XElement rootElement = doc.Element("root").Element("station");
            XElement destElement;
            XElement estimate;

            IEnumerable<XElement> estimateElements;
            IEnumerable<XElement> etdElements = rootElement.Elements("etd");

            for (int i = 0; i < etdElements.Count(); i++)
            {
                destElement = etdElements.ElementAt(i);
                destName = destElement.Element("destination").Value;

                estimateElements = destElement.Elements("estimate");
                times = new string[3];  
          
                for(int j=0; j<3; j++)
                {
                    estimate = estimateElements.ElementAt(j);
                    color = estimate.Element("color").Value;

                    if (estimate.Element("minutes").Value.Equals("Leaving"))
                        times[counter] = "Now";
                    else times[counter] = estimate.Element("minutes").Value;

                    if (!usedColors.Contains(color))
                    {
                        lines.Add(new Line(destName, color));
                        usedDests.Add(destName);
                        usedColors.Add(color);
                        SetDestination(estimate);
                    }
                    else
                    {
                        if (!usedDests.Contains(destName))
                        {
                            //lines.Add(new Line(dest, color));
                            usedDests.Add(destName);
                            //usedColors.Add(color);
                        }
                        SetDestination(estimate);
                    }
                }
            }
            return lines;
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

        public static async Task<Train[]> TrainDetails(XDocument doc)
        {
            return new Train[1];
        }

        private static void SetDestination(XElement e)
        {
            IEnumerable<Line> line = lines.Where(l => l.colorName.Equals(color));

            for (int k = 0; k < line.Count(); k++)
            {
                if (e.Element("direction").Value.Equals("South"))
                {
                    line.ElementAt(0).Destinations[1] = destName;
                    SetTimes(1, line);
                }
                else
                {
                    line.ElementAt(0).Destinations[0] = destName;
                    SetTimes(0, line);
                }
            }
        }

        private static void SetTimes(int id, IEnumerable<Line> _line)
        {
            string allTimes = "";

            for(int j=0; j<counter; j++)
            {
                if(j == 0) allTimes = times[0];
                else allTimes = String.Concat(allTimes, ", ", times[j]);
            }
            _line.ElementAt(0).Times[id] = allTimes;
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
