using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace DIPS.Mobile.UI.Components.Pages;

public partial class ContentPage
{
    public static readonly BindableProperty ShouldHideFloatingNavigationMenuProperty = BindableProperty.Create(
        nameof(ShouldHideFloatingNavigationMenuButton),
        typeof(bool),
        typeof(ContentPage),
        propertyChanged: (bindable, _, _) => ((ContentPage)bindable).HideOrShowFloatingNavigationMenu());

    /// <summary>
    /// Determines whether the FloatingNavigationMenuButton should be hidden or not
    /// </summary>
    /// <remarks>Modal pages will always cover the button</remarks>
    public bool ShouldHideFloatingNavigationMenuButton
    {
        get => (bool)GetValue(ShouldHideFloatingNavigationMenuProperty);
        set => SetValue(ShouldHideFloatingNavigationMenuProperty, value);
    }

    /// <summary>
    /// If the ContentPage has run its <see cref="OnAppearing"/>
    /// </summary>
    public bool HasAppeared { get; private set; }

    /// <summary>
    /// Determines if the content page should force garbage collection and log it to the console when navigated to.
    /// </summary>
    /// <remarks>This will only run when <see cref="DUI.IsDebug"/>.</remarks>
    public bool ShouldGarbageCollectAndLogWhenNavigatedTo { get; set; }
    

    public static readonly BindableProperty ShouldLogWhenGarbageCollectedProperty = BindableProperty.Create(
        nameof(ShouldLogWhenGarbageCollected),
        typeof(bool),
        typeof(ContentPage));

    /// <summary>
    /// Will log to Console when the finalizer has run. This happens when the object was garbage collected.
    /// </summary>
    /// <remarks>This will only run when <see cref="DUI.IsDebug"/>.</remarks>
    public bool ShouldLogWhenGarbageCollected
    {
        get => (bool)GetValue(ShouldLogWhenGarbageCollectedProperty);
        set => SetValue(ShouldLogWhenGarbageCollectedProperty, value);
    }
    
    public static readonly BindableProperty ShouldLogLoadingTimeProperty = BindableProperty.Create(
        nameof(ShouldLogLoadingTime),
        typeof(bool),
        typeof(ContentPage));

    /// <summary>
    /// Will log to the Console when the OnLoaded event occured. This can be useful to profile changes to the page that affects the time it takes for people to see the page.
    /// </summary>
    /// <remarks>This will only run when <see cref="DUI.IsDebug"/>.</remarks>
    public bool ShouldLogLoadingTime
    {
        get => (bool)GetValue(ShouldLogLoadingTimeProperty);
        set => SetValue(ShouldLogLoadingTimeProperty, value);
    }

    public static readonly BindableProperty LargeTitleDisplayProperty = BindableProperty.Create(
        nameof(LargeTitleDisplay),
        typeof(LargeTitleDisplayMode),
        typeof(ContentPage),
        defaultValue: LargeTitleDisplayMode.Never);

    public LargeTitleDisplayMode LargeTitleDisplay
    {
        get => (LargeTitleDisplayMode)GetValue(LargeTitleDisplayProperty);
        set => SetValue(LargeTitleDisplayProperty, value);
    }

    public static readonly BindableProperty StatusBarColorProperty = BindableProperty.Create(
        nameof(StatusBarColor),
        typeof(Color),
        typeof(ContentPage),
        defaultValueCreator: _ => DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(Shell.Shell.BackgroundColorName));

    public Color StatusBarColor
    {
        get => (Color)GetValue(StatusBarColorProperty);
        set => SetValue(StatusBarColorProperty, value);
    }
    
    public static readonly BindableProperty StatusBarStyleProperty = BindableProperty.Create(
        nameof(StatusBarStyle),
        typeof(StatusBarStyle),
        typeof(ContentPage),
        defaultValue: StatusBarStyle.Auto);

    /// <summary>
    /// Controls whether status bar icons (clock, battery, etc.) should be light or dark.
    /// Use Light for dark backgrounds, Dark for light backgrounds, or Auto to calculate automatically.
    /// <remarks>
    /// The status bar style will be automatically set to Light when the status bar background color is dark, and Dark when the background color is light, regardless of this property value. This is because Android does not support setting status bar icon color directly, and relies on the background color luminosity to determine icon color for visibility. 
    /// 
    /// </summary>
    public StatusBarStyle StatusBarStyle
    {
        get => (StatusBarStyle)GetValue(StatusBarStyleProperty);
        set => SetValue(StatusBarStyleProperty, value);
    }
}