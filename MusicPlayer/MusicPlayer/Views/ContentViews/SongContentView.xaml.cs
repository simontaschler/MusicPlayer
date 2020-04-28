using Autofac;
using MusicPlayer.Helpers;
using MusicPlayer.Models;
using MusicPlayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPlayer.Views.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SongContentView : ContentView
    {
        private readonly Song Song;

        public SongContentView(Song song)
        {
            InitializeComponent();
            Song = song;
            Title.Text = song.Title;
            Artists.Text = string.Join(", ", song.ArtistNames);
            Cover.Source = song.Cover ?? DependencyHelper.Container.ResolveNamed<ImageSource>("defaultCover"); 
        }

        public event EventHandler<SongEventArgs> Tapped;

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var args = new SongEventArgs { Song = Song };
            Tapped?.Invoke(this, args);
        }
    }

    public class SongEventArgs : EventArgs 
    { 
        public Song Song { get; set; }
    }
}