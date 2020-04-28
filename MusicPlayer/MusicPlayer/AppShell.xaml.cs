using MusicPlayer.Views;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MusicPlayer
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell(PlayingPage player)
        {
            InitializeComponent();

            var artistsTab = GetArtistsTab();
            var albumTab = GetAlbumsTab();
            var playerTab = GetPlayerTab(player);

            Tabbar.Items.Add(artistsTab);
            Tabbar.Items.Add(albumTab);
            Tabbar.Items.Add(playerTab);
        }

        private Tab GetArtistsTab()
        {
            var tab = new Tab();
            var shellContent = new ShellContent
            {
                ContentTemplate = new DataTemplate(typeof(ArtistsPage))
            };
            tab.Items.Add(shellContent);
            tab.Title = "Artists";
            tab.Icon = ImageSource.FromResource("MusicPlayer.Resources.artistIcon.png", typeof(AppShell));
            return tab;
        }

        private Tab GetAlbumsTab() 
        {
            var tab = new Tab();
            var shellContent = new ShellContent
            {
                ContentTemplate = new DataTemplate(typeof(AlbumsPage))
            };
            tab.Items.Add(shellContent);
            tab.Title = "Albums";
            tab.Icon = ImageSource.FromResource("MusicPlayer.Resources.albumIcon.png", typeof(AppShell));
            return tab;
        }

        private Tab GetPlayerTab(PlayingPage player) 
        {
            var tab = new Tab();
            var shellContent = new ShellContent
            {
                Content = player,
                Route = "playingpage"
            };
            tab.Items.Add(shellContent);
            tab.Route = "player";
            tab.Title = "Playing";
            tab.Icon = ImageSource.FromResource("MusicPlayer.Resources.playerIcon.png", typeof(AppShell));
            return tab;
        }
    }
}
