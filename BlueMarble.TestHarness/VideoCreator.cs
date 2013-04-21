using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.IO;

namespace BlueMarble.TestHarness
{
    class VideoCreator
    {
        //BlueMarble.Data.MarbleDataBase dataBase;
        

        public void InitiateVideo(int rollNum, int frameRate)
        {
            FileStream stream = new FileStream("new.gif", FileMode.Create);

           

            Stream imageStream = new FileStream("images.jpg", FileMode.Open, FileAccess.Read, FileShare.Read);
            JpegBitmapDecoder decoder = new JpegBitmapDecoder(imageStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BitmapSource image = decoder.Frames[0];
            GifBitmapEncoder newGif = new GifBitmapEncoder();




            for (int x = 0; x < 60; x++)
            {
                newGif.Frames.Add(BitmapFrame.Create(image));
            }


            imageStream = new FileStream("images2.jpg", FileMode.Open, FileAccess.Read, FileShare.Read);
            decoder = new JpegBitmapDecoder(imageStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            image = decoder.Frames[0];

            for (int x = 0; x < 60; x++)
            {
                newGif.Frames.Add(BitmapFrame.Create(image));
            }

            imageStream = new FileStream("images3.jpg", FileMode.Open, FileAccess.Read, FileShare.Read);
            decoder = new JpegBitmapDecoder(imageStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            image = decoder.Frames[0];
            for (int x = 0; x < 60; x++)
            {
                newGif.Frames.Add(BitmapFrame.Create(image));
            }            newGif.Save(stream);
        }

         
    }
}
