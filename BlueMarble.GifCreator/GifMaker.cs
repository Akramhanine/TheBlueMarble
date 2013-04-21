using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.IO;
using System.Net;

namespace BlueMarble.GifCreator
{
    public class GifMaker
    {
        public GifBitmapEncoder Create(IEnumerable<BlueMarble.Data.ImageData> listOFImages, int frameRate)
        {
            JpegBitmapDecoder decoder;
            BitmapSource convertToBmp;
            GifBitmapEncoder newGif = new GifBitmapEncoder();

            foreach (BlueMarble.Data.ImageData indImage in listOFImages)
            {
                var request = WebRequest.Create(indImage.Lowresurl);

                using (var response = request.GetResponse())
                using (var webstream = response.GetResponseStream())
                {
                    decoder = new JpegBitmapDecoder(webstream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    convertToBmp = decoder.Frames[0];

                    for (int x = 0; x < frameRate; x++)
                    {
                        newGif.Frames.Add(BitmapFrame.Create(convertToBmp));
                    }
                }

            }

            return newGif;

            /*FileStream stream = new FileStream("new.gif", FileMode.Create);



            //Stream imageStream = new FileStream("images.jpg", FileMode.Open, FileAccess.Read, FileShare.Read);


            JpegBitmapDecoder decoder = new JpegBitmapDecoder(imageStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BitmapSource image = decoder.Frames[0];
            GifBitmapEncoder newGif = new GifBitmapEncoder();


            var request = WebRequest.Create("http://www.gravatar.com/avatar/6810d91caff032b202c50701dd3af745?d=identicon&r=PG");

            using (var response = request.GetResponse())
            using (var webstream = response.GetResponseStream())
            {
                decoder = new JpegBitmapDecoder(webstream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                //imageStream = Bitmap.FromStream(webstream);
            }
            //decoder = new JpegBitmapDecoder(imageStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            image = decoder.Frames[0];
            for (int x = 0; x < 10; x++)
            {
                newGif.Frames.Add(BitmapFrame.Create(image));
            }

            newGif.Save(stream);*/
        }
    }
}
