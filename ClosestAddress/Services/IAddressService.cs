using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClosestAddress.Services
{
    public interface IAddressService
    {
        Dictionary<string, List<double>> GetCSVAddressCoordinate();

        IEnumerable<KeyValuePair<string, double>> GetClosetAddresses(Dictionary<string, List<double>> list, double latitude, double longitude);

    }
}