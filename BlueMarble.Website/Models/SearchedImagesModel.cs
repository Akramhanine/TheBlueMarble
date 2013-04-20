using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlueMarble.Data;

namespace BlueMarble.Website.Models
{
    public class SearchedImagesModel
    {
        /// <summary>
        /// The collection of imagedata entity objects
        /// </summary>
        public IList<ImageData> Images
        {
            get;
            private set;
        }

        /// <summary>
        /// The count of loaded image data objects
        /// </summary>
        public int ImageCount
        {
            get
            {
                return Images.Count();
            }
        }

        public SearchedImagesModel(IList<ImageData> Images)
        {
            this.Images = Images;
        }
    }
}