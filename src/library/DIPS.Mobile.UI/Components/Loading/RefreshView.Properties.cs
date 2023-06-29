namespace DIPS.Mobile.UI.Components.Loading;

public partial class RefreshView
{
    public static readonly BindableProperty MainContentProperty = BindableProperty.Create(
        nameof(MainContent),
        typeof(View),
        typeof(RefreshView));

    public View? MainContent
    {
        get => (View)GetValue(MainContentProperty);
        set => SetValue(MainContentProperty, value);
    }
}