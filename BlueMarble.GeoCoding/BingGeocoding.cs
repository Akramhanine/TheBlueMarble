using BlueMarble.Geocoding.DataTransferObjects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;


namespace BlueMarble.Geocoding
{
	internal class BingGeocoding
	{

		private const string BingUnformattedAddress = "http://dev.virtualearth.net/REST/v1/";
		private Uri BindUnformattedUri
		{
			get;
			set;
		}

		private string BingMapsKey
		{
			get;
			set;
		}

		public BingGeocoding(string bingKey)
		{
			BingMapsKey = bingKey;
			BindUnformattedUri = new Uri(BingUnformattedAddress);
		}

		public Location GeocodeFromString(string stringLocation)
		{
			Location location = null;
			try
			{
				HttpClient client = new HttpClient();
				client.BaseAddress = BindUnformattedUri;
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = client.GetAsync(constructQueryString(stringLocation)).Result;
				if (response.IsSuccessStatusCode)
				{
					// We can't use ReadAsAsync, because the resulting objects all have a generic type
					// of "Resource", rather than the "Location" type that it really is. See readJsonIntoAddress.
					//address = response.Content.ReadAsAsync<Response>().Result;
					location = readJsonIntoAddress(response.Content);
				}
			}
			catch (Exception ex)
			{
				
			}
			return location;
		}

		private string constructQueryString(string location)
		{
			return string.Format("Locations?q={0}&key={1}", location, BingMapsKey);
		}

		private Location readJsonIntoAddress(HttpContent content)
		{
			DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Response));
			Response bingResponse = jsonSerializer.ReadObject(content.ReadAsStreamAsync().Result) as Response;

			Location bingLocation = bingResponse.ResourceSets[0].Resources[0] as Location;

			return bingLocation;
		}
	}
}
