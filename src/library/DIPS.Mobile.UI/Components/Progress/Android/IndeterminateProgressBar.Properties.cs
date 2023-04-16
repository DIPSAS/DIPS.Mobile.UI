
namespace DIPS.Mobile.UI.Components.Progress.Android
{
    /// <summary>
    /// Only available for Android
    /// </summary>
    public partial class IndeterminateProgressBar
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
    }
}