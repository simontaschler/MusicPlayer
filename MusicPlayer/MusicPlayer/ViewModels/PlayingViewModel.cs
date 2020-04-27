using MusicPlayer.Services;
using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MusicPlayer.ViewModels
{
    public class PlayingViewModel : BaseViewModel
    {
        private Command _playCommand;
        public Command PlayCommand => _playCommand ?? (_playCommand = new Command(Play));

        private Command _pauseCommand;
        public Command PauseCommand => _pauseCommand ?? (_pauseCommand = new Command(Pause));

        private Command _stopCommand;
        public Command StopCommand => _stopCommand ?? (_stopCommand = new Command(Stop));

        public PlayingViewModel() 
        {
            Player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();           
        }

        public async void Test() 
        {
            //Test
            var api = DependencyService.Resolve<IMusicPlayerAPI>();
            var songs = await api.GetAlbumSongs(1);
            Player.Load(songs.FirstOrDefault()?.Audio);
        }

        private void Play() 
        {
            Player.Play();
        }

        private void Pause() 
        {
            Player.Pause();
        }

        private void Stop() 
        {
            Player.Stop();
        }

        private ISimpleAudioPlayer _player;
        public ISimpleAudioPlayer Player
        {
            get => _player;
            set => SetProperty(ref _player, value);
        }
    }
}
