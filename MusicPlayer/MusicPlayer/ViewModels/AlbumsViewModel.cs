using MusicPlayer.Services;
using MusicPlayer.Views.ContentViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MusicPlayer.ViewModels
{
    public class AlbumsViewModel : BaseViewModel
    {
        public async Task<List<AlbumContentView>> LoadAlbums()
        {
            var api = DependencyService.Resolve<IMusicPlayerAPI>();

            var albums = await api.GetAllAlbums();
            var contentViews = new List<AlbumContentView>();
            foreach (var album in albums)
                contentViews.Add(new AlbumContentView(album));

            return contentViews;
        }
    }
}