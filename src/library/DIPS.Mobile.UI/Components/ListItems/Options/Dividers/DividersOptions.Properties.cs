namespace DIPS.Mobile.UI.Components.ListItems.Options.Dividers;

public partial class DividersOptions
{
    public static readonly BindableProperty TopDividerMarginProperty = BindableProperty.Create(
        nameof(TopDividerMargin),
        typeof(Thickness),
        typeof(DividersOptions),
        defaultValue: new Thickness(Sizes.GetSize(SizeName.size_2), 0, 0, 0),
        defaultBindingMode: BindingMode.OneTime);

    public static readonly BindableProperty BottomDividerMarginProperty = BindableProperty.Create(
        nameof(BottomDividerMargin),
        typeof(Thickness),
        typeof(DividersOptions),
        defaultValue: new Thickness(Sizes.GetSize(SizeName.size_2), 0, 0, 0),
        defaultBindingMode: BindingMode.OneTime);

    public Thickness BottomDividerMargin
    {
        get => (Thickness)GetValue(BottomDividerMarginProperty);
        set => SetValue(BottomDividerMarginProperty, value);
    }
    
    public Thickness TopDividerMargin
    {
        get => (Thickness)GetValue(TopDividerMarginProperty);
        set => SetValue(TopDividerMarginProperty, value);
    }
}