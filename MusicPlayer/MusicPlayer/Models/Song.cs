using MusicPlayer.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MusicPlayer.Models
{
    public class Song
    {
        public int SongID { get; set; }
        public string Title { get; set; }
        public string FileAddress { get; set; }
        public int AlbumID { get; set; }
        public int Position { get; set; }

        public List<string> ArtistNames { get; set; }
        public async Task<List<string>> GetArtistNames() 
        {
            var api = DependencyService.Resolve<IMusicPlayerAPI>();
            ArtistNames = await api.GetSongArtistNames(SongID);
            return ArtistNames;
        }

        public ImageSource Cover { get; set; }
        public async Task<ImageSource> GetCover() 
        {
            var api = DependencyService.Resolve<IMusicPlayerAPI>();
            var coverAddress = await api.GetAlbumCover(AlbumID);
            var byteArray = new WebClient().DownloadData(App.HOST + coverAddress);
            Cover = ImageSource.FromStream(() => new MemoryStream(byteArray));
            return Cover;
        }

        public Stream Audio 
        {
            get 
            {
                try
                {
                    var byteArray = new WebClient().DownloadData(App.HOST + FileAddress);
                    return new MemoryStream(byteArray);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Song song)
                return song.SongID == SongID;
            return false;
        }
    }
}
