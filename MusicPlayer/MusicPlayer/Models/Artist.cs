using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Xamarin.Forms;

namespace MusicPlayer.Models
{
    public class Artist
    {
        public int ArtistID { get; set; }
        public string Name { get; set; }
        public string PictureAddress { get; set; }

        private ImageSource _artistImage;
        public ImageSource ArtistImage
        {
            get
            {
                try
                {
                    if (_artistImage == null)
                    {
                        var byteArray = new WebClient().DownloadData(App.HOST + PictureAddress);
                        _artistImage = ImageSource.FromStream(() => new MemoryStream(byteArray));
                    }
                    return _artistImage;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
