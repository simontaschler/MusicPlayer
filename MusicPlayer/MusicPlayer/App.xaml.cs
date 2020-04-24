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

namespace MusicPlayer
{
    public partial class App : Application
    {
        public static readonly string HOST = "http://10.10.50.17/MusicPlayer";

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

            DependencyHelper.Builder.RegisterInstance(api);

            DependencyHelper.BuildContainer();
            DependencyResolver.ResolveUsing(type => DependencyHelper.Container.IsRegistered(type) ? DependencyHelper.Container.Resolve(type) : null);
            MainPage = new AppShell();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
