namespace DIPS.Mobile.UI.Components.ListItems.Options.Debugging;

public partial class DebuggingOptions
{
    public static readonly BindableProperty ShouldColorEverythingProperty = BindableProperty.Create(
        nameof(ShouldColorEverything),
        typeof(bool),
        typeof(DebuggingOptions),
        defaultValue:true,
        defaultBindingMode: BindingMode.OneTime);

    public bool ShouldColorEverything
    {
        get => (bool)GetValue(ShouldColorEverythingProperty);
        set => SetValue(ShouldColorEverythingProperty, value);
    }
}