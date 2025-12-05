namespace DIPS.Mobile.UI.Components.TiffViewer;

public partial class TiffViewer
{
    public static readonly BindableProperty SourceProperty = BindableProperty.Create(
        nameof(Source),
        typeof(byte[]),
        typeof(TiffViewer),
        propertyChanged: (bindable, _, _) => _ = ((TiffViewer)bindable).LoadTiffPages());

    public byte[]? Source
    {
        get => (byte[]?)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }
}
