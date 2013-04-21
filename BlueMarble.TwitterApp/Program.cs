using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

using TweetSharp;
using Hammock;

namespace BlueMarble.TwitterApp
{
    class Program
    {
        static string ConsumerKey = "9huEtq7jCHJEOnOfMkaeQ";
        static string ConsumerSecret = "sDxWW2g4OIdn7ZRgNG74xRJftXvmZFu19go1VVUs";
        static string AccessToken = "1368379986-cWjZ4GjsNy42xg4RlMQZBZr0DybG5qHy7gFsFqp";
        static string AccessTokenSecret = "k3eSdjOJTMDc5jRM1RAeVqRwmTc024bcBDiwXrKjQ";

        static string TwitPicKey = "ef2f0f75521caa4155581204b6df8753";

        static TwitterService service;

        static void Main(string[] args)
        {
            service = new TwitterService(ConsumerKey, ConsumerSecret);
            service.AuthenticateWith(AccessToken, AccessTokenSecret);

            //TestTwitterPost();
            //TestTwitPic();
            TestBothTwitterAndTwitPic();
        }

        static void TestTwitterPost()
        {
            TwitterStatus status = service.SendTweet(new SendTweetOptions { Status = "This is a test of the API" });
        }

        static string TestTwitPic()
        {
            // Prepare an OAuth Echo request to TwitPic
            RestRequest request = service.PrepareEchoRequest(); 
            request.Path = "uploadAndPost.xml";
            request.AddFile("media", "cat-wearing-overalls", "cat-wearing-overalls.jpg", "image/jpeg");
            request.AddField("key", TwitPicKey);
            request.AddField("message", "Test of twit pic!");

            // Post photo to TwitPic with Hammock
            RestClient client = new RestClient { Authority = "http://api.twitpic.com/", VersionPath = "2"};
            RestResponse response = client.Request(request);

            int urlStart = response.Content.IndexOf("<url>");
            int urlEnd = response.Content.IndexOf("</url>");

            string url = response.Content.Substring(urlStart + 5, urlEnd - (urlStart + 5));

            return url;
        }

        static void TestBothTwitterAndTwitPic()
        {
            string twitPicUrl = TestTwitPic();

            string tweet = string.Format("This is a test of the API with a URL and geocoding {0}", twitPicUrl);

            TwitterStatus status = service.SendTweet(new SendTweetOptions 
            { 
                Status = tweet,
                Lat = 41.4994,
                Long = 81.6956
            });

            Console.ReadLine();
        }
    }
}
