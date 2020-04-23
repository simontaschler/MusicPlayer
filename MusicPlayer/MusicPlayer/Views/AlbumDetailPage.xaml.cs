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
    public partial class AlbumDetailPage : ContentPage
    {
        private Dictionary<Label, Artist> ArtistLabels;

        public AlbumDetailPage(Album album)
        {
            InitializeComponent();
            BindingContext = new AlbumDetailViewModel(album);
            InsertArtists();
            InsertSongs();
        }

        private void ArtistTapped(object sender, EventArgs e) 
        {
            //Navigate to ArtistDetailPage
        }

        private void Song_Tapped(object sender, ContentViews.SongEventArgs e)
        {
            //start playing song
        }

        private async void InsertSongs() 
        {
            SongContainer.Children.Clear();
            var songs = await ((AlbumDetailViewModel)BindingContext).LoadSongs();

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

        private async void InsertArtists() 
        {
            ArtistLabels = new Dictionary<Label, Artist>();
            var artists = await ((AlbumDetailViewModel)BindingContext).LoadArtists();
            var nextColumn = 0;
            foreach (var artist in artists) 
            {
                ArtistContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                var labelText = nextColumn != 0 ? ", " + artist.Name : artist.Name;
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