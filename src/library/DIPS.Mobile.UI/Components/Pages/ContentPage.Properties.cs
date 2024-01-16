using System.Windows.Input;
using DIPS.Mobile.UI.Components.Saving.SaveView;

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
    /// <remarks>This will only run when debugging.</remarks>
    public bool ShouldGarbageCollectAndLogWhenNavigatedTo { get; set; }

}   
