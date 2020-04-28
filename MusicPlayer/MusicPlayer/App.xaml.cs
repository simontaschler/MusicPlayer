using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MusicPlayer.Services;
using MusicPlayer.Views;
using Refit;
using Newtonsoft.Json;
using MusicPlayer.Helpers;
using Autofac;
using Xamarin.Forms.Internals;
using System.Net.Http;
using System.Collections.Generic;
using MusicPlayer.Models;
using MusicPlayer.ViewModels;

namespace MusicPlayer
{
    public partial class App : Application
    {
        public static readonly string HOST = "http://10.10.50.1/MusicPlayer";

        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            var refitSettings = new RefitSettings();
            var jsonSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            refitSettings.ContentSerializer = new NewtonsoftJsonContentSerializer(jsonSettings);

            var api = RestService.For<IMusicPlayerAPI>(HOST, refitSettings);
            var defaultCover = ImageSource.FromResource("MusicPlayer.Resources.defaultCover.png", typeof(App));
            var defaultArtist = ImageSource.FromResource("MusicPlayer.Resources.defaultArtist.png", typeof(App));
            var player = new PlayingPage();
            var loadPlaylistCommand = ((PlayingViewModel)player.BindingContext).LoadPlaylistCommand;

            DependencyHelper.Builder.RegisterInstance(api);
            DependencyHelper.Builder.RegisterInstance(new List<Song>());
            DependencyHelper.Builder.RegisterInstance(defaultCover).Named<ImageSource>("defaultCover");
            DependencyHelper.Builder.RegisterInstance(defaultArtist).Named<ImageSource>("defaultArtist");
            DependencyHelper.Builder.RegisterInstance(loadPlaylistCommand).Named<Command>("loadPlaylist");
            DependencyHelper.Builder.RegisterInstance(new AppShell(player));
            

            DependencyHelper.BuildContainer();
            DependencyResolver.ResolveUsing(type => DependencyHelper.Container.IsRegistered(type) ? DependencyHelper.Container.Resolve(type) : null);
            MainPage = DependencyService.Resolve<AppShell>();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
