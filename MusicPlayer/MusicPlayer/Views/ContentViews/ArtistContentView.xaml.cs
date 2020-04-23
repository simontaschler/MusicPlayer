using MusicPlayer.Models;
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
    public partial class ArtistContentView : ContentView
    {
        private readonly Artist Artist;

        public ArtistContentView(Artist artist)
        {
            InitializeComponent();
            Artist = artist ?? throw new ArgumentException();
            Name.Text = Artist.Name;
            ArtistImage.Source = Artist.ArtistImage ?? ImageSource.FromResource("MusicPlayer.Resources.defaultArtist.png", typeof(ArtistContentView));
        }

        public event EventHandler<ArtistEventArgs> Tapped;

        protected virtual void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var args = new ArtistEventArgs { Artist = Artist };
            Tapped?.Invoke(this, args);
        }
    }

    public class ArtistEventArgs : EventArgs 
    { 
        public Artist Artist { get; set; }
    }
}