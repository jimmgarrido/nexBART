using nexBart.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace nexBart.Helpers
{
    class XmlParser
    {
        static ObservableCollection<Line> lines = new ObservableCollection<Line>();
        static XElement destElement;
        static string color, dest;
        static IEnumerable<XElement> estimateElements;
        static string[] times = new string[5];
        static int counter = 0;

        public static async Task<ObservableCollection<Line>> Departures(XDocument _doc)
        {
            List<string> usedDests = new List<string>();
            List<string> usedColors = new List<string>();

            usedColors.Clear();
            usedDests.Clear();
            lines.Clear();

            XElement rootElement = _doc.Element("root").Element("station");

            IEnumerable<XElement> etdElements =
                from e in rootElement.Elements("etd")
                select e;

            

            for(int i=0; i<etdElements.Count(); i++)
            {
                destElement = etdElements.ElementAt(i);

                dest = destElement.Element("destination").Value;
                estimateElements = destElement.Elements("estimate");
                counter = 0;

                foreach (XElement est in estimateElements)
                {
                    color = est.Element("color").Value;
                    
                    if(est.Element("minutes").Value.Equals("Leaving"))
                        times[counter] = "Now";
                    else times[counter] = est.Element("minutes").Value;
                    counter++;

                    if(!usedColors.Contains(color))
                    {
                        lines.Add(new Line(dest, color));
                        usedDests.Add(dest);
                        usedColors.Add(color);
                        SetDestination(est);
                    } else
                    {
                        if (!usedDests.Contains(dest))
                        {
                            lines.Add(new Line(dest, color));
                            usedDests.Add(dest);
                            //usedColors.Add(color);
                        }
                        SetDestination(est);
                    }
                } 
            }
            return lines;
        }

        private static void SetDestination(XElement e)
        {
            IEnumerable<Line> line = lines.Where(l => l.colorName.Equals(color));

            for (int k = 0; k < line.Count(); k++)
            {
                if (e.Element("direction").Value.Equals("South"))
                {
                    line.ElementAt(0).Destinations[1] = dest;
                    SetTimes(1, ref line);
                }
                else
                {
                    line.ElementAt(0).Destinations[0] = dest;
                    SetTimes(0, ref line);
                }
            }
        }

        private static void SetTimes(int id, ref IEnumerable<Line> _line)
        {
            string allTimes = "";

            for(int j=0; j<counter; j++)
            {
                if(j == 0) allTimes = times[0];
                else allTimes = String.Concat(allTimes, ", ", times[j]);
            }
            _line.ElementAt(0).Times[id] = allTimes + " mins";
        }
    }
}
