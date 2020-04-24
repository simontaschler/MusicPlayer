using Autofac;
using MoreLinq;
using MusicPlayer.Helpers;
using MusicPlayer.Models;
using MusicPlayer.Services;
using MusicPlayer.Views.ContentViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MusicPlayer.ViewModels
{
    public class ArtistDetailViewModel : BaseViewModel
    {
        private readonly Artist Artist;

        public ArtistDetailViewModel(Artist artist)
        {
            Artist = artist ?? throw new ArgumentException();
            Name = Artist.Name;
            ArtistImage = Artist.ArtistImage ?? DependencyHelper.Container.ResolveNamed<ImageSource>("defaultArtist");
        }

        public async Task<List<SongContentView>> LoadSongs() 
        {
            var api = DependencyService.Resolve<IMusicPlayerAPI>();
            var songs = await api.GetArtistSongs(Artist.ArtistID);
            var albumCovers = await api.GetAlbumCovers(Artist.ArtistID);

            var contentViews = new List<SongContentView>();
            foreach (var song in songs) 
            {
                var cover = albumCovers.Where(q => q.AlbumID == song.AlbumID).FirstOrDefault()?.CoverImage;
                var artists = await api.GetSongArtistNames(song.SongID);
                var contentView = new SongContentView(song, artists, cover);
                contentViews.Add(contentView);
            }

            return contentViews;
        }

        public async Task<List<AlbumContentView>> LoadAlbums() 
        {
            var api = DependencyService.Resolve<IMusicPlayerAPI>();
            var albums = await api.GetArtistAlbums(Artist.ArtistID);

            var contentViews = new List<AlbumContentView>();
            foreach (var album in albums) 
            {
                var contentView = new AlbumContentView(album);
                contentViews.Add(contentView);
            }

            return contentViews;
        }

        private ImageSource _artistImage;
        public ImageSource ArtistImage 
        {
            get => _artistImage;
            set => SetProperty(ref _artistImage, value);
        }

        private string _name;
        public string Name 
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
    }
}
