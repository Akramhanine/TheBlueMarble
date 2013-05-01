using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueMarble.Geocoding;
using BlueMarble.Geocoding.DataTransferObjects;

namespace BlueMarble
{
	/// <summary>
	/// Geocoding utils will convert an address to longitude and latitude
	/// </summary>
	public static class GeocodingUtils
	{
		/// <summary>
		/// Calculates the minimum and maximum latitude and longitude points surrounding the given address.
		/// </summary>
		/// <param name="Address">The address to resolve into latitude/longitude</param>
		/// <param name="range">The range, in miles, that reflects the </param>
		/// <returns>CoordinateRange with address residing in the center of the points.</returns>
		public static CoordinateRange BingGeocodeAddress(string Address, double range)
		{
			CoordinateRange coordRange = null;
			BingGeocoding geoCoder = new BingGeocoding("AphMNwBsMsPWN6Ss2xvurl_19C7iztqYWFZGkKUET0gc2kO6c81bXCSQoY9pPDv9");
			
			Location thisLocation = geoCoder.GeocodeFromString(Address);
			if (thisLocation != null)
			{
				coordRange = GeocodingUtils.GetLongLatRangeFromCenter(thisLocation.Point, range);
			}

			return coordRange;
		}

		/// <summary>
		/// Return a range of long/lat points based on a given distance.
		/// </summary>
		/// <param name="CenterLocation">The center location.</param>
		/// <param name="Range">The range of miles from center location.</param>
		/// <returns>Returns a set of coordinates that create a bounding box around the center location based on Range</returns>
		private static CoordinateRange GetLongLatRangeFromCenter(Point centerPoint, double range)
		{
			double pointRange = range / 68; //68 miles is the equivalent of one latitude and longitude point
			CoordinateRange coordRange = new CoordinateRange();

			coordRange.LatitudeMin = centerPoint.Coordinates[0] - pointRange;
			coordRange.LatitudeMax = centerPoint.Coordinates[0] + pointRange;
			coordRange.LongitudeMin = centerPoint.Coordinates[1] - pointRange;
			coordRange.LongitudeMax = centerPoint.Coordinates[1] + pointRange;

			return coordRange;
		}
	}

}
