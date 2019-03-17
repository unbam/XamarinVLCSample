using System;
using System.Drawing;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinVLCSample;
using XamarinVLCSample.iOS;

[assembly: ExportRenderer(typeof(PlaybackSlider), typeof(PlaybackSliderRenderer))]

namespace XamarinVLCSample.iOS
{
    public class PlaybackSliderRenderer : SliderRenderer
    {
        private PlaybackSlider _slider;

        public PlaybackSliderRenderer()
        {
        }

        /// <summary>
        /// Ons the element changed.
        /// </summary>
        /// <param name="e">E.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
            {
                return;
            }

            _slider = (PlaybackSlider)Element;

            Control.ThumbTintColor = _slider.ThumbColor.ToUIColor();
            Control.MinimumTrackTintColor = _slider.ElapsedColor.ToUIColor();
            Control.MaximumTrackTintColor = _slider.RemainingColor.ToUIColor();
            Control.SetThumbImage(CreateThumb(), UIControlState.Normal);

            Control.TouchDown += OnPlaybackSliderTouchDown;
            Control.TouchUpInside += OnPlaybackSliderTouchUp;
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
        private void OnPlaybackSliderTouchDown(object sender, EventArgs e)
        {
            _slider.TouchDownEvent(sender, e);
        }

        /// <summary>
        /// Ons the playback slider touch up.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnPlaybackSliderTouchUp(object sender, EventArgs e)
        {
            _slider.TouchUpEvent(((UISlider)sender).Value, e);
        }

        /// <summary>
        /// Creates the thumb.
        /// </summary>
        /// <returns>The thumb.</returns>
        private UIImage CreateThumb()
        {
            float width = 15;
            float height = 15;

            UIGraphics.BeginImageContext(new SizeF(width, height));
            var context = UIGraphics.GetCurrentContext();

            context.SetFillColor(UIColor.FromRGB(170, 170, 170).CGColor);
            context.FillEllipseInRect(new RectangleF(0, 0, width, height));

            return UIGraphics.GetImageFromCurrentImageContext();
        }
    }
}
