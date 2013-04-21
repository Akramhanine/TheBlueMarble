using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.IO;
using System.Net;
using BlueMarble.Data;

namespace BlueMarble.GifCreator
{
    public class GifMaker
    {

        //This method takes in a list of imagedata found in the database, creates a new gifbitmapencoder and puts the images into the newly formed gif.
        public GifBitmapEncoder Create(IEnumerable<ImageData> listOFImages, int frameRate = 2)
        {
            JpegBitmapDecoder decoder;
            BitmapSource convertToBmp;
            GifBitmapEncoder newGif = new GifBitmapEncoder();


            //Run through the list of ImageData from the database
            foreach (BlueMarble.Data.ImageData indImage in listOFImages)
            {
                //Get the image based off of the url found on the data of the image
                var request = WebRequest.Create(indImage.Lowresurl);

                using (var response = request.GetResponse())
                using (var webstream = response.GetResponseStream())
                {
                    decoder = new JpegBitmapDecoder(webstream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    convertToBmp = decoder.Frames[0];

                    //The amount of times you store the image in the in the gif, the slower it appears to transition, default for this speed is 2
                    for (int x = 0; x < frameRate; x++)
                    {
                        newGif.Frames.Add(BitmapFrame.Create(convertToBmp));
                    }
                }

            }

            return newGif;
        }
    }
}
