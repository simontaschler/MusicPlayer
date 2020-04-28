using MusicPlayer.Models;
using MusicPlayer.Services;
using MusicPlayer.Views;
using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MusicPlayer.ViewModels
{
    public class PlayingViewModel : BaseViewModel
    {
        private List<Song> Playlist;
        private int CurrentIndex;

        private readonly ImageSource PlayImage;
        private readonly ImageSource PauseImage;

        private Command _playCommand;
        public Command PlayCommand => _playCommand ?? (_playCommand = new Command(Play));

        private Command _previousCommand;
        public Command PreviousCommand => _previousCommand ?? (_previousCommand = new Command(Previous));

        private Command _nextCommand;
        public Command NextCommand => _nextCommand ?? (_nextCommand = new Command(Next));
        
        private Command _dragCompletedCommand;
        public Command DragCompletedCommand => _dragCompletedCommand ?? (_dragCompletedCommand = new Command(DragCompleted));

        public PlayingViewModel() 
        {
            Player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            PlayImage = ImageSource.FromResource("MusicPlayer.Resources.playButton.png", typeof(PlayingViewModel));
            PauseImage = ImageSource.FromResource("MusicPlayer.Resources.pauseButton.png", typeof(PlayingViewModel));
            ButtonImage = PlayImage;
            DurationValue = .1;
            PositionValue = 0;
        }

        public async void Test() 
        {
            //Test
            var api = DependencyService.Resolve<IMusicPlayerAPI>();
            var songs = await api.GetAlbumSongs(1);
            var song = songs.FirstOrDefault();
            await LoadSong(song);
        }

        public async Task LoadPlaylist(int startIndex = 0) 
        {
            var playlist = DependencyService.Resolve<List<Song>>();
            var playingSong = playlist.ElementAtOrDefault(startIndex) ?? Playlist.FirstOrDefault();
            if (playingSong == null)
                return;
            Playlist = playlist;
            CurrentIndex = startIndex;
            await LoadSong(playingSong);
        }

        private async Task LoadSong(Song song) 
        {
            Player.Load(song.Audio);
            SongTitle = song.Title;
            Artists = string.Join(", ", song.ArtistNames ?? await song.GetArtistNames());
            DurationValue = Player.Duration;
            DurationValue = Player.Duration;
            DurationText = TimeSpan.FromSeconds(DurationValue).ToString(@"m\:ss");
            PositionValue = Player.CurrentPosition;
            PositionText = TimeSpan.FromSeconds(PositionValue).ToString(@"m\:ss");
            Cover = song.Cover ?? await song.GetCover();
        }

        private void Play() 
        {
            if (Player.IsPlaying)
            {
                Player.Pause();
                ButtonImage = PlayImage;
            }
            else 
            {
                Player.Play();
                Device.StartTimer(TimeSpan.FromSeconds(0.5), UpdatePosition);
                ButtonImage = PauseImage;
            }
        }

        private async void Previous() 
        {
            if (Player.CurrentPosition > 1) 
            {
                Player.Seek(0);
                return;
            }

            if (CurrentIndex == 0)
                CurrentIndex = Playlist.Count() - 1;
            else
                CurrentIndex--;

            await LoadSong(Playlist.ElementAt(CurrentIndex));
            Play();
        }

        private async void Next() 
        {
            if (CurrentIndex == Playlist.Count() - 1)
                CurrentIndex = 0;
            else
                CurrentIndex++;

            await LoadSong(Playlist.ElementAt(CurrentIndex));
            Play();
        }

        private void DragCompleted() 
        {
            if (PositionValue != Player.CurrentPosition && PositionValue != Player.Duration)
                Player.Seek(PositionValue);
        }

        private bool UpdatePosition() 
        {
            PositionValue = Player.CurrentPosition;
            PositionText = TimeSpan.FromSeconds(PositionValue).ToString(@"m\:ss");
            return Player.IsPlaying;
        }

        private ISimpleAudioPlayer _player;
        public ISimpleAudioPlayer Player
        {
            get => _player;
            set => SetProperty(ref _player, value);
        }

        private ImageSource _cover;
        public ImageSource Cover 
        {
            get => _cover;
            set => SetProperty(ref _cover, value);
        }

        private ImageSource _buttonImage;
        public ImageSource ButtonImage
        {
            get => _buttonImage;
            set => SetProperty(ref _buttonImage, value);
        }

        private double _positionValue;
        public double PositionValue
        {
            get => _positionValue;
            set => SetProperty(ref _positionValue, value);
        }

        private double _durationValue;
        public double DurationValue
        {
            get => _durationValue;
            set => SetProperty(ref _durationValue, value);
        }

        private string _positionText;
        public string PositionText
        {
            get => _positionText;
            set => SetProperty(ref _positionText, value);
        }

        private string _durationText;
        public string DurationText
        {
            get => _durationText;
            set => SetProperty(ref _durationText, value);
        }

        private string _songTitle;
        public string SongTitle
        {
            get => _songTitle;
            set => SetProperty(ref _songTitle, value);
        }

        private string _artists;
        public string Artists
        {
            get => _artists;
            set => SetProperty(ref _artists, value);
        }
    }
}
