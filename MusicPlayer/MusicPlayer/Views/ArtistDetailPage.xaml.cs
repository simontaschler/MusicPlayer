using MusicPlayer.Models;
using MusicPlayer.ViewModels;
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

        private void AlbumTapped(object sender, ContentViews.AlbumEventArgs e)
        {
            Navigation.PushAsync(new AlbumDetailPage(e.Album));
        }

        private void Song_Tapped(object sender, ContentViews.SongEventArgs e)
        {
            //start playing song
        }

        private async void InsertAlbums()
        {
            AlbumContainer.Children.Clear();
            var albums = await ((ArtistDetailViewModel)BindingContext).LoadAlbums();

            if (albums.Count == 0)
                return;

            AlbumsHeader.IsVisible = true;
            var firstAlbum = albums.FirstOrDefault();
            firstAlbum.Tapped += AlbumTapped;
            AlbumContainer.Children.Add(firstAlbum);

            foreach (var album in albums.Skip(1))
            {
                album.Tapped += AlbumTapped;
                var separator = new BoxView
                {
                    HeightRequest = 1,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.Gray
                };
                AlbumContainer.Children.Add(separator);
                AlbumContainer.Children.Add(album);
            }
        }

        private async void InsertSongs()
        {
            SongContainer.Children.Clear();
            var songs = await ((ArtistDetailViewModel)BindingContext).LoadSongs();

            var firstSong = songs.FirstOrDefault();
            firstSong.Tapped += Song_Tapped;
            SongContainer.Children.Add(firstSong);

            foreach (var song in songs.Skip(1))
            {
                song.Tapped += Song_Tapped;
                var separator = new BoxView
                {
                    HeightRequest = 1,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.Gray
                };
                SongContainer.Children.Add(separator);
                SongContainer.Children.Add(song);
            }
        }
    }
}