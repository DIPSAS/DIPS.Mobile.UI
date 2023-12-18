namespace DIPS.Mobile.UI.Components.Lists;

public partial class ListItemContainer
{
    public static readonly BindableProperty DividerMarginProperty = BindableProperty.Create(
        nameof(DividerMargin),
        typeof(Thickness),
        typeof(Lists.ListItemContainer),
        defaultValue: new Thickness(Sizes.GetSize(SizeName.size_2), 0, 0, 0));

    /// <summary>
    /// Set the margin of the divider between ListItems
    /// </summary>
    public Thickness DividerMargin
    {
        get => (Thickness)GetValue(DividerMarginProperty);
        set => SetValue(DividerMarginProperty, value);
    }
}