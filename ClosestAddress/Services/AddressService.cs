using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ClosestAddress.Services
{
    public class AddressService : IAddressService
    {
        public IEnumerable<KeyValuePair<string, double>> GetClosetAddresses(Dictionary<string, List<double>> list, double latitude, double longitude)
        {
            Dictionary<string, double> addressDistancesDic = new Dictionary<string, double>();

            try
            {
                foreach (var addressCoordinates in list)
                {
                    var addressDistance = Helpers.Helper.GetDistanceBetweenPoints(addressCoordinates.Value[0], addressCoordinates.Value[1], latitude, longitude);
                    addressDistancesDic.Add(addressCoordinates.Key, addressDistance);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name);
                Console.WriteLine("");
                Console.WriteLine(ex.Message);
            }

            var sortedDictionary =addressDistancesDic.OrderBy(x=>x.Value).Take(5);

            return sortedDictionary;
        }

        public Dictionary<string, List<double>> GetCSVAddressCoordinate()
        {
            var coordinates = new Dictionary<string, List<double>>();

            try
            {
                string appdatafolder = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "App_Data");
                var csv = Helpers.Helper.ReadCSV(appdatafolder);
                coordinates = Helpers.Helper.GetLatLongByAddress(csv);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.GetType().Name);
                Console.WriteLine("");
                Console.WriteLine(ex.Message);
            }
            return coordinates;
        }
    }
}