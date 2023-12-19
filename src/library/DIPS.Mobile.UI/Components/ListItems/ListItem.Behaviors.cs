using DIPS.Mobile.UI.Components.ListItems.Behaviors;

namespace DIPS.Mobile.UI.Components.ListItems;

public partial class ListItem
{
    public static readonly BindableProperty AutoDividerProperty = BindableProperty.Create(
        nameof(AutoDivider),
        typeof(bool),
        typeof(ListItem),
        propertyChanged: OnAutoDividerChanged);

    /// <summary>
    /// Automatically sets divider based on where it is placed in a <see cref="VerticalStackLayout"/> or <see cref="CollectionView"/>
    /// </summary>
    /// <remarks>Must be a child of <see cref="VerticalStackLayout"/> or <see cref="CollectionView"/></remarks>
    public bool AutoDivider
    {
        get => (bool)GetValue(AutoDividerProperty);
        set => SetValue(AutoDividerProperty, value);
    }
    
    private static void OnAutoDividerChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var attachBehavior = (bool)newValue;
        if (bindable is not ListItem listItem)
            return;
        
        if (attachBehavior)
        {
            listItem.Behaviors.Add(new AutoDividerBehavior());
        }
        else
        {
            var toRemove = listItem.Behaviors.FirstOrDefault(b => b is AutoDividerBehavior);
            if (toRemove != null)
            {
                listItem.Behaviors.Remove(toRemove);
            }
        }
    }
}