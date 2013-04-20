using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoCoding;
using GeoCoding.Google;
using GeoCoding.Microsoft;

namespace BlueMarble
{
    /// <summary>
    /// Geocoding utils will convert an address to longitude and latitude
    /// </summary>
    public static class GeocodingUtils
    {
        static IGeoCoder geoCoder;

        public static Address GoogleGeocodeAddress(string Address)
        {
            try
            {
                geoCoder = new GoogleGeoCoder();
                return geoCoder.GeoCode(Address).First();
            }
            catch
            {
                return null;
            }
        }

        public static Address MicrosoftGeocodeAddress(string Address)
        {
            try
            {
                geoCoder = new BingMapsGeoCoder("AphMNwBsMsPWN6Ss2xvurl_19C7iztqYWFZGkKUET0gc2kO6c81bXCSQoY9pPDv9");
                return geoCoder.GeoCode(Address).First();
            }
            catch
            {
                return null;
            }
        }
    }

}
