using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.SyntaxHighlighting.Json;

public partial class JsonViewer
{
    
    public static readonly BindableProperty JsonProperty = BindableProperty.Create(
        nameof(Json),
        typeof(string),
        typeof(JsonViewer), propertyChanged: (bindable, _, _) => ((JsonViewer)bindable).OnJsonChanged());

    public static readonly BindableProperty KeyColorProperty = BindableProperty.Create(
        nameof(KeyColor),
        typeof(Color),
        typeof(JsonViewer), defaultValue: Colors.GetColor(ColorName.color_primary_90));
    
    /// <summary>
    /// The JSON to display in the viewer.
    /// </summary>
    public string Json
    {
        get => (string)GetValue(JsonProperty);
        set => SetValue(JsonProperty, value);
    }
    
    /// <summary>
    /// The color of the Key of a JSON.
    /// </summary>
    /// <remarks> { "key":"value" }</remarks>
    public Color KeyColor
    {
        get => (Color)GetValue(KeyColorProperty);
        set => SetValue(KeyColorProperty, value);
    }
}