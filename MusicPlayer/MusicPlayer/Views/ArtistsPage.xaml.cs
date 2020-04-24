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
    public partial class ArtistsPage : ContentPage
    {
        private bool Loaded = false;

        public ArtistsPage()
        {
            InitializeComponent();
            BindingContext = new ArtistsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Loaded)
                return;
            InsertArtists();
            Loaded = true;
        }

        private void ArtistTapped(object sender, ContentViews.ArtistEventArgs e)
        {
            Navigation.PushAsync(new ArtistDetailPage(e.Artist));
        }

        private async void InsertArtists() 
        {
            Container.Children.Clear();
            var artists = await ((ArtistsViewModel)BindingContext).LoadArtists();

            var firstArtist = artists.FirstOrDefault();
            firstArtist.Tapped += ArtistTapped;
            Container.Children.Add(firstArtist);

            foreach (var artist in artists.Skip(1))
            {
                artist.Tapped += ArtistTapped;
                var separator = new BoxView
                {
                    HeightRequest = 1,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.Gray
                };
                Container.Children.Add(separator);
                Container.Children.Add(artist);
            }
        }
    }
}