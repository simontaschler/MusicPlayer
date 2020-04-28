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
    public partial class PlayingPage : ContentPage
    {
        public PlayingPage()
        {
            InitializeComponent();
            BindingContext = new PlayingViewModel();
            PreviousButton.Source = ImageSource.FromResource("MusicPlayer.Resources.previousButton.png", typeof(PlayingPage));
            NextButton.Source = ImageSource.FromResource("MusicPlayer.Resources.nextButton.png", typeof(PlayingPage));
        }
    }
}