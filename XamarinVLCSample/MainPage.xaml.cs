using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibVLCSharp.Shared;
using Xamarin.Forms;

namespace XamarinVLCSample
{
    public partial class MainPage : ContentPage
    {
        private long _length;
        private double _elapsedTotalSeconds;
        private Media _media;
        private ImageSource _playImage;
        private ImageSource _pauseImage;

        public MainPage()
        {
            InitializeComponent();

            _elapsedTotalSeconds = 0;
            _playImage = ImageSource.FromResource("XamarinVLCSample.Resources.play.png");
            _pauseImage = ImageSource.FromResource("XamarinVLCSample.Resources.pause.png");
        }

        /// <summary>
        /// Ons the appearing.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // VLC Initialize
            Core.Initialize();

            var libVLC = new LibVLC();
            _media = new Media(libVLC, "http://www.quirksmode.org/html5/videos/big_buck_bunny.webm", FromType.FromLocation);

            VideoView.MediaPlayer = new MediaPlayer(libVLC) { Media = _media };

            VideoView.MediaPlayer.TimeChanged += OnMediaPlayerTimeChanged;
            VideoView.MediaPlayer.LengthChanged += OnMediaPlayerLengthChanged;
            VideoView.MediaPlayer.EndReached += OnMediaPlayerEndReached;
            PlaybackSlider.TouchUp += OnPlaybackSliderTouchUp;

            PlayImageButton.Source = _playImage;
            Tool.IsVisible = false;
            ElapsedTime.Text = "00:00";
            RemainingTime.Text = "00:00";
        }

        /// <summary>
        /// Ons the disappearing.
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Tool.IsVisible = false;
            PlaybackSlider.Value = 0;
            PlaybackSlider.TouchUp -= OnPlaybackSliderTouchUp;

            if (VideoView.MediaPlayer.State == VLCState.Playing)
            {
                VideoView.MediaPlayer.Stop();
                PlayImageButton.Source = _playImage;
            }

            VideoView.MediaPlayer.TimeChanged -= OnMediaPlayerTimeChanged;
            VideoView.MediaPlayer.LengthChanged -= OnMediaPlayerLengthChanged;
            VideoView.MediaPlayer.EndReached -= OnMediaPlayerEndReached;

            // Android Only
            if(Device.RuntimePlatform == Device.Android)
            {
                VideoView.MediaPlayer.Dispose();
                VideoView.MediaPlayer = null;
            }
        }

        /// <summary>
        /// Ons the sleep.
        /// </summary>
        public void OnSleep()
        {
            OnDisappearing();
        }

        /// <summary>
        /// Ons the resume.
        /// </summary>
        public void OnResume()
        {
            OnAppearing();
        }

        /// <summary>
        /// Ons the media player length changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnMediaPlayerLengthChanged(object sender, MediaPlayerLengthChangedEventArgs e)
        {
            _length = e.Length;
        }

        /// <summary>
        /// Ons the media player time changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnMediaPlayerTimeChanged(object sender, MediaPlayerTimeChangedEventArgs e)
        {
            var elapsedTimeSpan = TimeSpan.FromMilliseconds((double)e.Time);                // ElapsedTime
            var remainingTimeSpan = TimeSpan.FromMilliseconds((double)(_length - e.Time));  // RemainingTime
            var elapsedTotalSeconds = Math.Floor(elapsedTimeSpan.TotalSeconds);             // ElapsedTime(TotalSeconds)

            // Draw
            Device.BeginInvokeOnMainThread(() => {
                PlaybackSlider.Value = VideoView.MediaPlayer.Position;
            });

            if (Math.Abs(_elapsedTotalSeconds - elapsedTotalSeconds) < 1)
            {
                return;
            }

            // Backup TotalSeconds
            _elapsedTotalSeconds = elapsedTotalSeconds;

            // Draw(1Seconds)
            Device.BeginInvokeOnMainThread(() => {
                ElapsedTime.Text = string.Format("{0:D2}:{1:D02}", elapsedTimeSpan.Minutes, elapsedTimeSpan.Seconds);
                RemainingTime.Text = string.Format("{0:D2}:{1:D02}", remainingTimeSpan.Minutes, remainingTimeSpan.Seconds);
            });
        }

        /// <summary>
        /// Ons the media player end reached.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnMediaPlayerEndReached(object sender, EventArgs e)
        {
            _elapsedTotalSeconds = 0;

            // Draw
            Device.BeginInvokeOnMainThread(() => {
                PlaybackSlider.Value = 0;
                PlayImageButton.Source = _playImage;
                Tool.IsVisible = false;
                ElapsedTime.Text = "00:00";
                RemainingTime.Text = "00:00";
            });
        }

        /// <summary>
        /// Ons the play image button clicked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnPlayImageButtonClicked(object sender, EventArgs e)
        {
            if (VideoView.MediaPlayer.State == VLCState.Playing)
            {
                // Pause
                VideoView.MediaPlayer.Pause();
                PlayImageButton.Source = _playImage;
            }
            else
            {
                if (VideoView.MediaPlayer.State == VLCState.Ended)
                {
                    VideoView.MediaPlayer.Media = _media;
                }

                // Play
                VideoView.MediaPlayer.Play();
                PlayImageButton.Source = _pauseImage;
                Tool.IsVisible = true;
            }
        }

        /// <summary>
        /// Ons the playback slider touch up.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnPlaybackSliderTouchUp(object sender, EventArgs e)
        {
            // Draw
            Device.BeginInvokeOnMainThread(() => {
                VideoView.MediaPlayer.Position = (float)sender;
            });
        }
    }
}
