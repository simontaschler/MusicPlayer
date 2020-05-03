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
    public partial class AlbumDetailPage : ContentPage
    {
        private bool Loaded = false;
        private Dictionary<Label, Artist> ArtistLabels;

        public AlbumDetailPage(Album album)
        {
            InitializeComponent();
            BindingContext = new AlbumDetailViewModel(album);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Loaded)
                return;
            InsertArtists();
            InsertSongs();
            Loaded = true;
        }

        private void ArtistTapped(object sender, EventArgs e) => 
            Navigation.PushAsync(new ArtistDetailPage(ArtistLabels[(Label)sender]));

        private void Song_Tapped(object sender, SongEventArgs e) => 
            ((AlbumDetailViewModel)BindingContext).PlayCommand.Execute(e.Song);

        private async void InsertSongs() 
        {
            SongContainer.Children.Clear();
            var songs = await ((AlbumDetailViewModel)BindingContext).LoadSongs();

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

        private async void InsertArtists() 
        {
            ArtistLabels = new Dictionary<Label, Artist>();
            var artists = await ((AlbumDetailViewModel)BindingContext).LoadArtists();
            var last = artists.Last();
            var nextColumn = 0;
            foreach (var artist in artists) 
            {
                ArtistContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                var labelText = artist.Equals(last) ? artist.Name : artist.Name + ",";
                var label = new Label
                {
                    Text = labelText,
                    FontSize = 14
                };

                var tgr = new TapGestureRecognizer();
                tgr.Tapped += (s, e) => ArtistTapped(s, e);
                label.GestureRecognizers.Add(tgr);

                Grid.SetColumn(label, nextColumn);
                ArtistContainer.Children.Add(label);
                nextColumn++;

                ArtistLabels[label] = artist;
            }
        }
    }
}