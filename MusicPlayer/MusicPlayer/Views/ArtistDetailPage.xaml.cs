using MusicPlayer.Models;
using MusicPlayer.ViewModels;
using MusicPlayer.Views.ContentViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPlayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArtistDetailPage : ContentPage
    {
        private bool Loaded = false;

        public ArtistDetailPage(Artist artist)
        {
            InitializeComponent();
            BindingContext = new ArtistDetailViewModel(artist);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Loaded)
                return;
            InsertAlbums();
            InsertSongs();
            Loaded = true;
        }

        private void AlbumTapped(object sender, AlbumEventArgs e) => 
            Navigation.PushAsync(new AlbumDetailPage(e.Album));

        private void Song_Tapped(object sender, SongEventArgs e) =>
            ((ArtistDetailViewModel) BindingContext).SongTappedCommand.Execute(e.Song);

        private async void InsertAlbums()
        {
            AlbumContainer.Children.Clear();
            var albums = await ((ArtistDetailViewModel)BindingContext).LoadAlbums();

            if (albums.Count == 0)
                return;

            AlbumsHeader.IsVisible = true;
            var firstAlbumView = new AlbumContentView(albums.FirstOrDefault());
            firstAlbumView.Tapped += AlbumTapped;
            AlbumContainer.Children.Add(firstAlbumView);

            foreach (var album in albums.Skip(1))
            {
                var albumView = new AlbumContentView(album);
                albumView.Tapped += AlbumTapped;
                var separator = new BoxView
                {
                    HeightRequest = 1,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.Gray
                };
                AlbumContainer.Children.Add(separator);
                AlbumContainer.Children.Add(albumView);
            }
        }

        private async void InsertSongs()
        {
            SongContainer.Children.Clear();
            var songs = await ((ArtistDetailViewModel)BindingContext).LoadSongs();

            if (songs.Count == 0)
                return;

            var firstSongView = new SongContentView(songs.FirstOrDefault());
            firstSongView.Tapped += Song_Tapped;
            SongContainer.Children.Add(firstSongView);

            foreach (var song in songs.Skip(1))
            {
                var songView = new SongContentView(song);
                songView.Tapped += Song_Tapped;
                var separator = new BoxView
                {
                    HeightRequest = 1,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.Gray
                };
                SongContainer.Children.Add(separator);
                SongContainer.Children.Add(songView);
            }
        }
    }
}