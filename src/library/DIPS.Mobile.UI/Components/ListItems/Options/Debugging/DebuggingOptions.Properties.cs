namespace DIPS.Mobile.UI.Components.ListItems.Options.Debugging;

public partial class DebuggingOptions
{
    public static readonly BindableProperty ShouldColorEverythingProperty = BindableProperty.Create(
        nameof(ShouldColorEverything),
        typeof(bool),
        typeof(Options.Debugging.DebuggingOptions),
        defaultValue:true);

    public bool ShouldColorEverything
    {
        get => (bool)GetValue(ShouldColorEverythingProperty);
        set => SetValue(ShouldColorEverythingProperty, value);
    }
}