namespace DIPS.Mobile.UI.Extensions.Markup;

[ContentProperty(nameof(Format))]
[AcceptEmptyServiceProvider]
public class StringFormatExtension : IMarkupExtension
{
    /// <summary>
    /// A composite format string.
    /// </summary>
    public string Format { get; set; }
    /// <summary>
    /// The object to format.
    /// </summary>
    public string Argument { get; set; }
    
    public object ProvideValue(IServiceProvider serviceProvider)
    {
        return string.Format(Format, Argument);
    }
}