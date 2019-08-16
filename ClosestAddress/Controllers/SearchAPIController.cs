using ClosestAddress.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClosestAddress.Controllers
{
    public class SearchAPIController : ApiController
    {

        private readonly IAddressService _addressService;
        public SearchAPIController() : this(new AddressService())
        {
        }

        public SearchAPIController(IAddressService addressService)
        {
            this._addressService = addressService;
        }


        [HttpGet]
        public IHttpActionResult GetSearchResult(double latitude, double longitude)
        {
            SearchAPIController apiConroller = new SearchAPIController(new AddressService());
          
            var closeAddress = _addressService.GetClosetAddresses(_addressService.GetCSVAddressCoordinate(), latitude,longitude);
            
            if(closeAddress==null)
            {
                return NotFound();
            }
           
            var json = JsonConvert.SerializeObject(closeAddress);

            return Ok(json);
            
        }
    }
}
