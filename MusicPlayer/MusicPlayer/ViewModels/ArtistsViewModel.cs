using MusicPlayer.Services;
using MusicPlayer.Views.ContentViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MusicPlayer.ViewModels
{
    public class ArtistsViewModel : BaseViewModel
    {
        public async Task<List<ArtistContentView>> LoadArtists() 
        {
            var api = DependencyService.Resolve<IMusicPlayerAPI>();

            var artists = await api.GetAllArtists();
            var contentViews = new List<ArtistContentView>();
            foreach (var artist in artists)
                contentViews.Add(new ArtistContentView(artist));

            return contentViews;
        }
    }
}
