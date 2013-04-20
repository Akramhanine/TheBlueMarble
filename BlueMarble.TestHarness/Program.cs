using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoCoding;

namespace BlueMarble.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            // Testing geocoding utilities
            Address address = GeocodingUtils.MicrosoftGeocodeAddress("28500 Clemens Road Westlake, OH 44145");
            Console.WriteLine("Found latitude and longtiude for {0}", address.FormattedAddress);
            Console.WriteLine("Latitude, Longitude:  {0}, {1}", address.Coordinates.Latitude, address.Coordinates.Longitude);

            Console.ReadLine();
        }
    }
}
