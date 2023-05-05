
using DIPS.Mobile.UI.Resources.Colors;

namespace DIPS.Mobile.UI.Components.Searching.Android
{
    /// <summary>
    /// Only available for Android
    /// </summary>
    internal partial class IndeterminateProgressBar
    {
        public static readonly BindableProperty IsRunningProperty = BindableProperty.Create(
            nameof(IsRunning),
            typeof(bool),
            typeof(Android.IndeterminateProgressBar));

        public bool IsRunning
        {
            get => (bool)GetValue(IsRunningProperty);
            set => SetValue(IsRunningProperty, value);
        }

        public static readonly BindableProperty TrackColorProperty = BindableProperty.Create(
            nameof(TrackColor),
            typeof(Color),
            typeof(IndeterminateProgressBar), defaultValueCreator: bindable => DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_neutral_30));

        public Color TrackColor
        {
            get => (Color)GetValue(TrackColorProperty);
            set => SetValue(TrackColorProperty, value);
        }

        public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(
            nameof(IndicatorColor),
            typeof(Color),
            typeof(IndeterminateProgressBar), defaultValueCreator: bindable => DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_primary_90));

        public Color IndicatorColor
        {
            get => (Color)GetValue(IndicatorColorProperty);
            set => SetValue(IndicatorColorProperty, value);
        }
    }
}