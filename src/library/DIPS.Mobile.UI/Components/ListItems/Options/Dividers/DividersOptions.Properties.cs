namespace DIPS.Mobile.UI.Components.ListItems.Options.Dividers;

public partial class DividersOptions
{
    public static readonly BindableProperty TopDividerMarginProperty = BindableProperty.Create(
        nameof(TopDividerMargin),
        typeof(Thickness),
        typeof(DividersOptions),
        defaultValue: new Thickness(Sizes.GetSize(SizeName.content_margin_small), 0, 0, 0),
        defaultBindingMode: BindingMode.OneTime);

    public static readonly BindableProperty BottomDividerMarginProperty = BindableProperty.Create(
        nameof(BottomDividerMargin),
        typeof(Thickness),
        typeof(DividersOptions),
        defaultValue: new Thickness(Sizes.GetSize(SizeName.content_margin_small), 0, 0, 0),
        defaultBindingMode: BindingMode.OneTime);

    /// <summary>
    /// Horizontal margin only supported
    /// </summary>
    public Thickness BottomDividerMargin
    {
        get => (Thickness)GetValue(BottomDividerMarginProperty);
        set => SetValue(BottomDividerMarginProperty, value);
    }
    
    /// <summary>
    /// Horizontal margin only supported
    /// </summary>
    public Thickness TopDividerMargin
    {
        get => (Thickness)GetValue(TopDividerMarginProperty);
        set => SetValue(TopDividerMarginProperty, value);
    }
}