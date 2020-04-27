using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MusicPlayer.Models
{
    public class Song
    {
        public int SongID { get; set; }
        public string Title { get; set; }
        public string FileAddress { get; set; }
        public int AlbumID { get; set; }
        public int Position { get; set; }

        private Stream _audio;
        public Stream Audio 
        {
            get 
            {
                try
                {
                    if (_audio == null)
                    {
                        var byteArray = new WebClient().DownloadData(App.HOST + FileAddress);
                        _audio = new MemoryStream(byteArray);
                    }
                    return _audio;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
