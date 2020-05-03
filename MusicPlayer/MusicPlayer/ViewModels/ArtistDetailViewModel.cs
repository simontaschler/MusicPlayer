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
        private List<Song> Songs;

        private Command _playCommand;
        public Command PlayCommand => _playCommand ?? (_playCommand = new Command(Play));

        public ArtistDetailViewModel(Artist artist)
        {
            Artist = artist ?? throw new ArgumentException();
            Name = Artist.Name;
            ArtistImage = Artist.ArtistImage ?? DependencyHelper.Container.ResolveNamed<ImageSource>("defaultArtist");
        }

        private void Play(object songObject)
        {
            var playlist = DependencyService.Resolve<List<Song>>();
            playlist.Clear();
            playlist.AddRange(Songs);
            var loadPlaylist = DependencyHelper.Container.ResolveNamed<Command>("loadPlaylist");
            loadPlaylist.Execute(songObject);
            var shell = DependencyService.Resolve<AppShell>();
            shell.GoToAsync("//tabbar/player/playingpage");
        }

        public async Task<List<Song>> LoadSongs() 
        {
            var api = DependencyService.Resolve<IMusicPlayerAPI>();
            var songs = await api.GetArtistSongs(Artist.ArtistID);
            var albumCovers = await api.GetAlbumCovers(Artist.ArtistID);

            foreach (var song in songs) 
            {
                var cover = albumCovers.Where(q => q.AlbumID == song.AlbumID).FirstOrDefault()?.CoverImage;
                song.ArtistNames = await api.GetSongArtistNames(song.SongID);
                song.Cover = cover;
            }

            Songs = songs;
            return Songs;
        }

        public async Task<List<Album>> LoadAlbums() 
        {
            var api = DependencyService.Resolve<IMusicPlayerAPI>();
            var albums = await api.GetArtistAlbums(Artist.ArtistID);
            return albums;
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
