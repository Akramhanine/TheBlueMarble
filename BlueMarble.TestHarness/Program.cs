using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoCoding;
using BlueMarble.Data;
using System.Data.Entity;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BlueMarble.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer<MarbleDataBase>(new DropCreateDatabaseAlways<MarbleDataBase>());
            // Testing geocoding utilities
            //Address address = GeocodingUtils.MicrosoftGeocodeAddress("28500 Clemens Road Westlake, OH 44145");
            //Console.WriteLine("Found latitude and longtiude for {0}", address.FormattedAddress);
            //Console.WriteLine("Latitude, Longitude:  {0}, {1}", address.Coordinates.Latitude, address.Coordinates.Longitude);

            /*MarbleDataBase db = new MarbleDataBase();
            db.Dataset.Add(new Dataset() { Description = "aljdsfjlaksdf" });
            db.SaveChanges();*/

            /*Dataset set = db.Dataset.Find(1);
            if (set != null)
            {
                Console.Write("Description is: " + set.Description);
            }*/

            FileStream stream = new FileStream("new.gif", FileMode.Create);
            List<BlueMarble.Data.ImageData> listOfImages = new List<BlueMarble.Data.ImageData>();

            BlueMarble.Data.ImageData newPic1 = new BlueMarble.Data.ImageData();
            BlueMarble.Data.ImageData newPic2 = new BlueMarble.Data.ImageData();
            BlueMarble.Data.ImageData newPic3 = new BlueMarble.Data.ImageData();
            BlueMarble.Data.ImageData newPic4 = new BlueMarble.Data.ImageData();
            BlueMarble.Data.ImageData newPic5 = new BlueMarble.Data.ImageData();
            BlueMarble.Data.ImageData newPic6 = new BlueMarble.Data.ImageData();
            BlueMarble.Data.ImageData newPic7 = new BlueMarble.Data.ImageData();
            BlueMarble.Data.ImageData newPic8 = new BlueMarble.Data.ImageData();
            BlueMarble.Data.ImageData newPic9 = new BlueMarble.Data.ImageData();
            BlueMarble.Data.ImageData newPic10 = new BlueMarble.Data.ImageData();
            BlueMarble.Data.ImageData newPic12 = new BlueMarble.Data.ImageData();
            BlueMarble.Data.ImageData newPic13 = new BlueMarble.Data.ImageData();
            BlueMarble.Data.ImageData newPic14 = new BlueMarble.Data.ImageData();
            BlueMarble.Data.ImageData newPic15 = new BlueMarble.Data.ImageData();
            BlueMarble.Data.ImageData newPic16 = new BlueMarble.Data.ImageData();
            BlueMarble.Data.ImageData newPic17 = new BlueMarble.Data.ImageData();
            BlueMarble.Data.ImageData newPic18 = new BlueMarble.Data.ImageData();
            BlueMarble.Data.ImageData newPic19 = new BlueMarble.Data.ImageData();
            BlueMarble.Data.ImageData newPic20 = new BlueMarble.Data.ImageData();
            BlueMarble.Data.ImageData newPic11 = new BlueMarble.Data.ImageData();

            newPic1.Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1000.JPG";
            newPic2.Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1001.JPG";
            newPic3.Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1002.JPG";
            newPic4.Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1003.JPG";
            newPic5.Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1004.JPG";
            newPic6.Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1005.JPG";
            newPic7.Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1006.JPG";
            newPic8.Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1007.JPG";
            newPic9.Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1008.JPG";
            newPic10.Lowresurl ="http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1009.JPG";
            newPic11.Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1010.JPG";
            newPic12.Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1011.JPG";
            newPic13.Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1012.JPG";
            newPic14.Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1013.JPG";
            newPic15.Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1014.JPG";
            newPic16.Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1015.JPG";
            newPic17.Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1016.JPG";
            newPic18.Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1017.JPG";
            newPic19.Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1018.JPG";
            newPic20.Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-1019.JPG";


            listOfImages.Insert(0, newPic1);
            listOfImages.Insert(0, newPic2);
            listOfImages.Insert(0, newPic3);
            listOfImages.Insert(0, newPic4);
            listOfImages.Insert(0, newPic5);
            listOfImages.Insert(0, newPic6);
            listOfImages.Insert(0, newPic7);
            listOfImages.Insert(0, newPic8);
            listOfImages.Insert(0, newPic9);
            listOfImages.Insert(0, newPic10);
            listOfImages.Insert(0, newPic11);
            listOfImages.Insert(0, newPic12);
            listOfImages.Insert(0, newPic13);
            listOfImages.Insert(0, newPic14);
            listOfImages.Insert(0, newPic15);
            listOfImages.Insert(0, newPic16);
            listOfImages.Insert(0, newPic17);
            listOfImages.Insert(0, newPic18);
            listOfImages.Insert(0, newPic19);
            listOfImages.Insert(0, newPic20);


            GifCreator.GifMaker bleh = new GifCreator.GifMaker();

            GifBitmapEncoder newGif = bleh.Create(listOfImages);

            newGif.Save(stream);
  
            
        }
    }
}
