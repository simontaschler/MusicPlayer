using System;
using System.Collections.Generic;
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
    }
}
