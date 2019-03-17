using System;
using Xamarin.Forms;

namespace XamarinVLCSample
{
    /// <summary>
    /// PlaybackSlider
    /// </summary>
    public class PlaybackSlider : Slider
    {
        /// <summary>
        /// ElapsedColorProperty
        /// </summary>
        public static readonly BindableProperty ElapsedColorProperty = BindableProperty.Create(nameof(ElapsedColor),
            typeof(Color), typeof(PlaybackSlider), Color.Default);

        /// <summary>
        /// ElapsedColor
        /// </summary>
        /// <value>The color of the elapsed.</value>
        public Color ElapsedColor
        {
            get { return (Color)GetValue(ElapsedColorProperty); }
            set { SetValue(ElapsedColorProperty, value); }
        }

        /// <summary>
        /// RemainingColorProperty
        /// </summary>
        public static readonly BindableProperty RemainingColorProperty = BindableProperty.Create(nameof(RemainingColor),
            typeof(Color), typeof(PlaybackSlider), Color.Default);

        /// <summary>
        /// RemainingColor
        /// </summary>
        /// <value>The color of the remaining.</value>
        public Color RemainingColor
        {
            get { return (Color)GetValue(RemainingColorProperty); }
            set { SetValue(RemainingColorProperty, value); }
        }

        /// <summary>
        /// TouchUpEventHandler
        /// </summary>
        public event EventHandler TouchUp;

        /// <summary>
        /// TouchUpInsideEvent
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        public void TouchUpEvent(object sender, EventArgs e)
        {
            EventHandler eventHandler = this.TouchUp;
            eventHandler?.Invoke(sender, e);
        }

        /// <summary>
        /// TouchDownEventHandler
        /// </summary>
        public event EventHandler TouchDown;

        /// <summary>
        /// TouchDownEvent
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        public void TouchDownEvent(object sender, EventArgs e)
        {
            EventHandler eventHandler = this.TouchDown;
            eventHandler?.Invoke(sender, e);
        }
    }
}
