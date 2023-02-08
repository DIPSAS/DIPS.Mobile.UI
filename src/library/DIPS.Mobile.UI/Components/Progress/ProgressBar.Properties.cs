using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Progress
{
    public partial class ProgressBar
    {
        public static readonly BindableProperty ModeProperty = BindableProperty.Create(
            nameof(Mode),
            typeof(ProgressBarMode),
            typeof(ProgressBar));

        public ProgressBarMode Mode
        {
            get => (ProgressBarMode)GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }
    }

    /// <summary>
    /// The mode that the progressbar will run in.
    /// </summary>
    public enum ProgressBarMode
    {
        /// <summary>
        /// Use determinate mode for the progress bar when you want to show that a specific quantity of progress has occurred. 
        /// </summary>
        Determinate,

        /// <summary>
        /// Use indeterminate mode for the progress bar when you do not know how long an operation will take
        /// </summary>
        Indeterminate,
    }
}