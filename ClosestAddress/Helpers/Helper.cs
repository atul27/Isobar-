using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace ClosestAddress.Helpers
{
    public class Helper
    {
        public static List<string> ReadCSV(string path)
        {
            List<string> list = new List<string>();
            try
            {
                using (var reader = new StreamReader(path + "\\address list australia.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine().TrimEnd(',');
                        list.Add(line);
                    }
                }
            }
            catch(FileNotFoundException fex)
            {
                Console.WriteLine("\nAddress list csv file not found");
                Console.WriteLine("Message :{0} ", fex.Message);
            }
            catch(IOException ioe)
            {
                Console.WriteLine("\nException Caught while reading the Address CSV file ");
                Console.WriteLine("Message :{0} ", ioe.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.GetType().Name);
                Console.WriteLine("");
                Console.WriteLine(ex.Message);
            }
          
            return list;
        }

        public static Dictionary<string, List<double>> GetLatLongByAddress(List<string> addresses)
        {

            Dictionary<string, List<double>> dictionary = new Dictionary<string, List<double>>();
            double lng, lat;
            foreach (var address in addresses)
            {
                try
                {
                    var url = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key=AIzaSyC-lBvL-c_VGFUeKStpXYUWsmJgprWw-BM&libraries=places&address={0}&sensor=true_or_false", address);

                    var req = (HttpWebRequest)WebRequest.Create(url);

                    var res = (HttpWebResponse)req.GetResponse();

                    using (var streamreader = new StreamReader(res.GetResponseStream()))
                    {
                        XmlTextReader xmlReader = new XmlTextReader(streamreader);
                        bool latread = false;
                        bool longread = false;
                        List<double> coordinates = new List<double>();

                        while (xmlReader.Read())
                        {
                            xmlReader.MoveToElement();
                            switch (xmlReader.Name)
                            {
                                case "lat":

                                    if (!latread)
                                    {
                                        xmlReader.Read();
                                        lat = double.Parse(xmlReader.Value);
                                        latread = true;
                                        coordinates.Add(lat);
                                    }
                                    break;
                                case "lng":
                                    if (!longread)
                                    {
                                        xmlReader.Read();
                                        lng = double.Parse(xmlReader.Value);
                                        longread = true;
                                        coordinates.Add(lng);
                                    }

                                    break;
                            }
                        }
                        dictionary.Add(address, coordinates);
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught while making the Google Address servicd api call!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
                catch(IOException ioe)
                {
                    Console.WriteLine("\nException Caught while reading the API response using stream reader!");
                    Console.WriteLine("Message :{0} ", ioe.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetType().Name);
                    Console.WriteLine("");
                    Console.WriteLine(ex.Message);
                }
            }
            return dictionary;
        }

        public static double GetDistanceBetweenPoints(double lat1, double lon1, double lat2, double lon2)
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;
                dist = dist * 1.609344;
                return (dist);
            }
        }
        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }
        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}