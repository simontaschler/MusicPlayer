using Autofac;
using MusicPlayer.Helpers;
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
    public partial class AlbumContentView : ContentView
    {
        private readonly Album Album;

        public AlbumContentView(Album album)
        {
            InitializeComponent();
            Album = album ?? throw new ArgumentException();
            Title.Text = Album.Title;
            Cover.Source = Album.CoverImage ?? DependencyHelper.Container.ResolveNamed<ImageSource>("defaultCover");
        }

        public event EventHandler<AlbumEventArgs> Tapped;

        protected virtual void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var args = new AlbumEventArgs { Album = Album };
            Tapped?.Invoke(this, args);
        }
    }

    public class AlbumEventArgs : EventArgs 
    { 
        public Album Album { get; set; }
    }
}