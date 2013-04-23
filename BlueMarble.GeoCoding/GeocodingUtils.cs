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

        /// <summary>
        /// Return a geocoded address from Google.
        /// </summary>
        /// <param name="Address">The address to geocode.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Return a geocoded address from Bing.
        /// </summary>
        /// <param name="Address">The address to geocode.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Return a range of long/lat points based on a given distance.
        /// </summary>
        /// <param name="CenterLocation">The center location.</param>
        /// <param name="Range">The range of miles from center location.</param>
        /// <returns>Returns a set of coordinates that create a bounding box around the center location based on Range</returns>
        public static CoordinateRange GetLongLatRangeByDistance(Location CenterLocation, double Range)
        {
            double pointRange = Range / 68; //68 miles is the equivalent of one latitude and longitude point
			CoordinateRange coordRange = new CoordinateRange();

			coordRange.LatitudeMin = CenterLocation.Latitude - pointRange;
			coordRange.LatitudeMax = CenterLocation.Latitude + pointRange;
			coordRange.LongitudeMin = CenterLocation.Longitude - pointRange;
			coordRange.LongitudeMax = CenterLocation.Longitude + pointRange;

			return coordRange;
        }
    }

}
