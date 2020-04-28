using Autofac;
using MusicPlayer.Helpers;
using MusicPlayer.Models;
using MusicPlayer.Services;
using MusicPlayer.Views.ContentViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MusicPlayer.ViewModels
{
    public class AlbumDetailViewModel : BaseViewModel
    {
        private readonly Album Album;
        private List<Song> Songs;

        private Command _songTappedCommand;
        public Command SongTappedCommand => _songTappedCommand ?? (_songTappedCommand = new Command(SongTapped));

        public AlbumDetailViewModel(Album album)
        {
            Album = album ?? throw new ArgumentException();
            Cover = Album.CoverImage ?? DependencyHelper.Container.ResolveNamed<ImageSource>("defaultCover");
            AlbumTitle = Album.Title;
            Year = Album.ReleaseYear;
        }

        private void SongTapped(object songObject) 
        {
            var playlist = DependencyService.Resolve<List<Song>>();
            playlist.Clear();
            playlist.AddRange(Songs);
            var loadPlaylist = DependencyHelper.Container.ResolveNamed<Command>("loadPlaylist");
            loadPlaylist.Execute(songObject);
            var shell = DependencyService.Resolve<AppShell>();
            shell.GoToAsync("//tabbar/player/playingpage");
        }

        public async Task<List<Artist>> LoadArtists() 
        {
            var api = DependencyService.Resolve<IMusicPlayerAPI>();
            var artists = await api.GetAlbumArtists(Album.AlbumID);
            return artists;
        }

        public async Task<List<Song>> LoadSongs() 
        {
            var api = DependencyService.Resolve<IMusicPlayerAPI>();
            var songs = await api.GetAlbumSongs(Album.AlbumID);

            foreach (var song in songs) 
            {
                song.ArtistNames = await api.GetSongArtistNames(song.SongID);
                song.Cover = Cover;
            }

            Songs = songs;
            return Songs;
        }

        private ImageSource _cover;
        public ImageSource Cover 
        {
            get => _cover;
            set => SetProperty(ref _cover, value);
        }

        private string _albumTitle;
        public string AlbumTitle 
        {
            get => _albumTitle;
            set => SetProperty(ref _albumTitle, value);
        }

        public int _year;
        public int Year 
        {
            get => _year;
            set => SetProperty(ref _year, value);
        }
    }
}
