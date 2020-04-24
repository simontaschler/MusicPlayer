using MusicPlayer.Services;
using MusicPlayer.ViewModels;
using MusicPlayer.Views.ContentViews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPlayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlbumsPage : ContentPage
    {
        private bool Loaded = false;

        public AlbumsPage()
        {
            InitializeComponent();
            BindingContext = new AlbumsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Loaded)
                return;
            InsertAlbums();
            Loaded = true;
        }

        private void AlbumTapped(object sender, AlbumEventArgs e)
        {
            Navigation.PushAsync(new AlbumDetailPage(e.Album));
        }

        private async void InsertAlbums() 
        {
            Container.Children.Clear();
            var albums = await ((AlbumsViewModel)BindingContext).LoadAlbums();

            var firstAlbum = albums.FirstOrDefault();
            firstAlbum.Tapped += AlbumTapped;
            Container.Children.Add(firstAlbum);

            foreach (var album in albums.Skip(1))
            {
                album.Tapped += AlbumTapped;
                var separator = new BoxView
                {
                    HeightRequest = 1,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.Gray
                };
                Container.Children.Add(separator);
                Container.Children.Add(album);
            }
        }
    }
}