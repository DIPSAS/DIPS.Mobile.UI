using DIPS.Mobile.UI.Resources.Styles.Shared;

namespace DIPS.Mobile.UI.Resources.Styles.Span;

internal class SpanStyleResources
{
    private static readonly Dictionary<TextStyle, Style> s_sharedStyles = SharedStyleResources.GetStylesForType(typeof(Microsoft.Maui.Controls.Span));
    
    public static Dictionary<SpanStyle, Style> Styles { get; } = new()
    {
        [SpanStyle.None] = s_sharedStyles[TextStyle.None],
        [SpanStyle.Body400] = s_sharedStyles[TextStyle.Body400],
        [SpanStyle.Body300] = s_sharedStyles[TextStyle.Body300],
        [SpanStyle.Body200] = s_sharedStyles[TextStyle.Body200],
        [SpanStyle.Body100] = s_sharedStyles[TextStyle.Body100],
        [SpanStyle.UI400] = s_sharedStyles[TextStyle.UI400],
        [SpanStyle.UI300] = s_sharedStyles[TextStyle.UI300],
        [SpanStyle.UI200] = s_sharedStyles[TextStyle.UI200],
        [SpanStyle.UI100] = s_sharedStyles[TextStyle.UI100],
        [SpanStyle.Header1000] = s_sharedStyles[TextStyle.Header1000],
        [SpanStyle.Header900] = s_sharedStyles[TextStyle.Header900],
        [SpanStyle.Header800] = s_sharedStyles[TextStyle.Header800],
        [SpanStyle.Header700] = s_sharedStyles[TextStyle.Header700],
        [SpanStyle.Header600] = s_sharedStyles[TextStyle.Header600],
        [SpanStyle.Header500] = s_sharedStyles[TextStyle.Header500],
        [SpanStyle.SectionHeader] = s_sharedStyles[TextStyle.SectionHeader]
    };
}
