using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Xamarin.Forms;

namespace MusicPlayer.Models
{
    public class AlbumCover
    {
        public int AlbumID { get; set; }
        public string CoverAddress { get; set; }

        private ImageSource _coverImage;
        public ImageSource CoverImage
        {
            get
            {
                try
                {
                    if (_coverImage == null)
                    {
                        var byteArray = new WebClient().DownloadData(App.HOST + CoverAddress);
                        _coverImage = ImageSource.FromStream(() => new MemoryStream(byteArray));
                    }
                    return _coverImage;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
