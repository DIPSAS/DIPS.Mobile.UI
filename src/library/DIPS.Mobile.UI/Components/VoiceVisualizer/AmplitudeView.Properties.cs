using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.VoiceVisualizer;

public partial class AmplitudeView
{
    /// <summary>
    /// The controller to configure the <see cref="AmplitudeView"/>
    /// </summary>
    /// <remarks>This must be set</remarks>
    public AmplitudeViewController? Controller
    {
        get => (AmplitudeViewController?)GetValue(ControllerProperty);
        set => SetValue(ControllerProperty, value);
    }

    /// <summary>
    /// The sample rate to get the amplitude from the controller
    /// </summary>
    public int SampleRate
    {
        get => (int)GetValue(SampleRateProperty);
        set => SetValue(SampleRateProperty, value);
    }

    /// <summary>
    /// Frames per second (FPS) to update the amplitude view
    /// </summary>
    public int FramesPerSecond
    {
        get => (int)GetValue(FramesPerSecondProperty);
        set => SetValue(FramesPerSecondProperty, value);
    }

    /// <summary>
    /// Whether the timer should be displayed or not
    /// </summary>
    public bool HasTimer
    {
        get => (bool)GetValue(HasTimerProperty);
        set => SetValue(HasTimerProperty, value);
    }

    /// <summary>
    /// Sets the color of the amplitudes
    /// </summary>
    public Color AmplitudeColor
    {
        get => (Color)GetValue(AmplitudeColorProperty);
        set => SetValue(AmplitudeColorProperty, value);
    }

    /// <summary>
    /// Sets the color of the placeholder amplitudes
    /// </summary>
    public Color PlaceholderAmplitudeColor
    {
        get => (Color)GetValue(PlaceholderAmplitudeColorProperty);
        set => SetValue(PlaceholderAmplitudeColorProperty, value);
    }

    /// <summary>
    /// Sets the color of the fade boxes
    /// </summary>
    /// <remarks>Should be same color as the container <see cref="AmplitudeView"/> is in</remarks>
    public Color FadeColor
    {
        get => (Color)GetValue(FadeColorProperty);
        set => SetValue(FadeColorProperty, value);
    }
    
    public static readonly BindableProperty FadeColorProperty = BindableProperty.Create(
        nameof(FadeColor),
        typeof(Color),
        typeof(AmplitudeView),
        defaultBindingMode: BindingMode.OneTime,
        defaultValue: Colors.GetColor(ColorName.color_surface_default));

    public static readonly BindableProperty PlaceholderAmplitudeColorProperty = BindableProperty.Create(
        nameof(PlaceholderAmplitudeColor),
        typeof(Color),
        typeof(AmplitudeView),
        defaultValue: Colors.GetColor(ColorName.color_text_placeholder),
        defaultBindingMode: BindingMode.OneTime);

    public static readonly BindableProperty AmplitudeColorProperty = BindableProperty.Create(
        nameof(AmplitudeColor),
        typeof(Color),
        typeof(AmplitudeView),
        defaultValue: Colors.GetColor(ColorName.color_text_default),
        defaultBindingMode: BindingMode.OneTime);

    public static readonly BindableProperty HasTimerProperty = BindableProperty.Create(
        nameof(HasTimer),
        typeof(bool),
        typeof(AmplitudeView),
        defaultValue: true,
        defaultBindingMode: BindingMode.OneTime);

    public static readonly BindableProperty SampleRateProperty = BindableProperty.Create(
        nameof(SampleRate),
        typeof(int),
        typeof(AmplitudeView),
        defaultBindingMode: BindingMode.OneTime,
        defaultValue: 15);

    public static readonly BindableProperty ControllerProperty = BindableProperty.Create(
        nameof(Controller),
        typeof(AmplitudeViewController),
        typeof(AmplitudeView),
        defaultBindingMode: BindingMode.OneTime);

    public static readonly BindableProperty FramesPerSecondProperty = BindableProperty.Create(
        nameof(FramesPerSecond),
        typeof(int),
        typeof(AmplitudeView),
        defaultValueCreator: _ => (int)DeviceDisplay.MainDisplayInfo.RefreshRate,
        defaultBindingMode: BindingMode.OneTime,
        coerceValue: (_, value) =>
        {
            if (value is not int intValue)
            {
                return DeviceDisplay.MainDisplayInfo.RefreshRate;
            }

            var newValue = intValue;
            if (newValue < 1)
            {
                newValue = 1;
            }
            else if (newValue > DeviceDisplay.MainDisplayInfo.RefreshRate)
            {
                newValue = (int)DeviceDisplay.MainDisplayInfo.RefreshRate;
            }

            return newValue;
        });
}