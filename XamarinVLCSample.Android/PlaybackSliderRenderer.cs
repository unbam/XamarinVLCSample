using System;
using Android.Content.Res;
using Android.Graphics;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinVLCSample;
using XamarinVLCSample.Droid;

[assembly: ExportRenderer(typeof(PlaybackSlider), typeof(PlaybackSliderRenderer))]

namespace XamarinVLCSample.Droid
{
    public class PlaybackSliderRenderer : SliderRenderer
    {
        private PlaybackSlider _slider;

        public PlaybackSliderRenderer(Android.Content.Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
            {
                return;
            }

            _slider = (PlaybackSlider)Element;
            Control.Thumb.SetColorFilter(Xamarin.Forms.Color.FromHex("#AAAAAA").ToAndroid(), PorterDuff.Mode.SrcIn);

            Control.ProgressTintList = ColorStateList.ValueOf(_slider.ElapsedColor.ToAndroid());
            Control.ProgressTintMode = PorterDuff.Mode.SrcIn;

            Control.ProgressBackgroundTintList = ColorStateList.ValueOf(_slider.RemainingColor.ToAndroid());
            Control.ProgressBackgroundTintMode = PorterDuff.Mode.SrcIn;

            Control.StartTrackingTouch += OnPlaybackSliderTouchDown;
            Control.StopTrackingTouch += OnPlaybackSliderTouchUp;
        }

        /// <summary>
        /// Dispose the specified disposing.
        /// </summary>
        /// <param name="disposing">If set to <c>true</c> disposing.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        /// <summary>
        /// Ons the playback slider touch down.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnPlaybackSliderTouchDown(object sender, SeekBar.StartTrackingTouchEventArgs e)
        {
            _slider.TouchDownEvent(sender, e);
        }

        /// <summary>
        /// Ons the playback slider touch up.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnPlaybackSliderTouchUp(object sender, SeekBar.StopTrackingTouchEventArgs e)
        {
            float progress = (float)(((SeekBar)sender).Progress) / 1000;

            // PlaybackSlider側にイベント通知
            _slider.TouchUpEvent(progress, e);
        }
    }
}
