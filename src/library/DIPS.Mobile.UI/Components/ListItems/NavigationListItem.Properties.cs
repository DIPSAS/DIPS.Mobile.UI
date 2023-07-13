using System.Windows.Input;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.ListItems;

public partial class NavigationListItem
{
    
    public static readonly BindableProperty CustomContentItemProperty = BindableProperty.Create(
        nameof(CustomContentItem),
        typeof(IView),
        typeof(NavigationListItem), propertyChanged: ((bindable, value, newValue) =>
        {
            if (bindable is not NavigationListItem navigationListItem) return;
            navigationListItem.OnCustomContentItemPropertyChanged();
        }));

    /// <summary>
    /// A custom content that is placed on the left side of the navigation icon
    /// </summary>
    public IView? CustomContentItem
    {
        get => (IView)GetValue(CustomContentItemProperty);
        set => SetValue(CustomContentItemProperty, value);
    }
}