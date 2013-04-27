using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

using BlueMarble.Data;

using TweetSharp;
using Hammock;
using System.Configuration;
using System.Collections.Specialized;
using BlueMarble.Twitter.Properties;

namespace BlueMarble.Twitter
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		DispatcherTimer dispatcherTimer = new DispatcherTimer();
		TimeSpan TimerInterval = new TimeSpan(0, 0, 1); //one second
		TimeSpan currentTimeRemaining = new TimeSpan();

		private static string ConsumerKey = "9huEtq7jCHJEOnOfMkaeQ";
		private static string ConsumerSecret = "sDxWW2g4OIdn7ZRgNG74xRJftXvmZFu19go1VVUs";
		private static string AccessToken = "1368379986-cWjZ4GjsNy42xg4RlMQZBZr0DybG5qHy7gFsFqp";
		private static string AccessTokenSecret = "k3eSdjOJTMDc5jRM1RAeVqRwmTc024bcBDiwXrKjQ";
		private readonly string _bigMarbleUrl;

		//static string TwitPicKey = "ef2f0f75521caa4155581204b6df8753";

		public MainWindow()
		{
			InitializeComponent();
			hoursBetween.Text = "8";

			_bigMarbleUrl = Settings.Default.BigMarbleAPIUrl;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			ResetTimeRemaining();

			dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
			dispatcherTimer.Interval = TimerInterval;

			for (int i = 0; i < 10; i++)
			{
				upcomingLocations.Items.Add(RandomPlaces.GetRandomLocation());
			}

			dispatcherTimer.Start();
		}

		private void dispatcherTimer_Tick(object sender, EventArgs e)
		{
			DecrementTimer(TimerInterval);
		}

		private void ResetTimeRemaining()
		{
			int hours = Convert.ToInt32(hoursBetween.Text);
			currentTimeRemaining = new TimeSpan(hours, 0, 0);
		}

		private void DecrementTimer(TimeSpan decrement)
		{
			currentTimeRemaining -= decrement;

			timeLeft.Content = currentTimeRemaining.ToString(@"hh\:mm\:ss");

			if (currentTimeRemaining.Ticks < 0)
			{
				SendAutomatedTweet();
				ResetTimeRemaining();
			}
		}

		private void SendAutomatedTweet()
		{
			string nextLocation = upcomingLocations.Items[0].ToString();
			upcomingLocations.Items.RemoveAt(0);

			SendTweet(nextLocation);

			upcomingLocations.Items.Add(RandomPlaces.GetRandomLocation());
		}

		private void SendTweet(string location)
		{
			//Grab an Image
			ImageData imageToPost = null;
			var client = new HttpClient();
			client.BaseAddress = new Uri(_bigMarbleUrl);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			HttpResponseMessage response = client.GetAsync("api/image/?address=" + Uri.EscapeDataString(location)).Result;
			if (response.IsSuccessStatusCode)
			{
				var images = response.Content.ReadAsAsync<IEnumerable<ImageData>>().Result;

				ImageData[] imageArray = images.ToArray();
				Random newRandom = new Random();
				int imageID = newRandom.Next(imageArray.Count());

				imageToPost = imageArray[imageID];
			}

			//found an image
			if (imageToPost != null)
			{
				string tweetText = string.Format(RandomPlaces.GetRandomMessage(), location);
				tweetText += " " + imageToPost.Lowresurl;

				TwitterService service = new TwitterService(ConsumerKey, ConsumerSecret);
				service.AuthenticateWith(AccessToken, AccessTokenSecret);

				try
				{
					TwitterStatus status = service.SendTweet(new SendTweetOptions
						{
							Status = tweetText,
							Lat = imageToPost.Latitude,
							Long = imageToPost.Longitude,
							DisplayCoordinates = true
						});

					tweetHistory.Items.Add(string.Format("{0} {1}: {2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), status.Text));
				}
				catch (Exception ex)
				{
					tweetHistory.Items.Add("Exception:");
					tweetHistory.Items.Add(ex);
				}
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (adhocLocation.Text.Length <= 0)
			{
				return;
			}

			SendTweet(adhocLocation.Text);
			adhocLocation.Text = "";
		}

		private void ApplyHoursBetween_Click(object sender, RoutedEventArgs e)
		{
			ResetTimeRemaining();
		}

		private void ClearLogBtn_Click(object sender, RoutedEventArgs e)
		{
			tweetHistory.Items.Clear();
		}
	}
}
