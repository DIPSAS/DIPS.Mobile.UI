using DIPS.Mobile.UI.Resources.Styles.Shared;

namespace DIPS.Mobile.UI.Resources.Styles.Span;

internal class SpanStyleResources
{
    public static Dictionary<SpanStyle, Style> Styles
    {
        get
        {
            var sharedStyles = SharedStyleResources.GetStylesForType(typeof(Microsoft.Maui.Controls.Span));
            return new Dictionary<SpanStyle, Style>
            {
                [SpanStyle.None] = sharedStyles[TextStyle.None],
                [SpanStyle.Body400] = sharedStyles[TextStyle.Body400],
                [SpanStyle.Body300] = sharedStyles[TextStyle.Body300],
                [SpanStyle.Body200] = sharedStyles[TextStyle.Body200],
                [SpanStyle.Body100] = sharedStyles[TextStyle.Body100],
                [SpanStyle.UI400] = sharedStyles[TextStyle.UI400],
                [SpanStyle.UI300] = sharedStyles[TextStyle.UI300],
                [SpanStyle.UI200] = sharedStyles[TextStyle.UI200],
                [SpanStyle.UI100] = sharedStyles[TextStyle.UI100],
                [SpanStyle.Header1000] = sharedStyles[TextStyle.Header1000],
                [SpanStyle.Header900] = sharedStyles[TextStyle.Header900],
                [SpanStyle.Header800] = sharedStyles[TextStyle.Header800],
                [SpanStyle.Header700] = sharedStyles[TextStyle.Header700],
                [SpanStyle.Header600] = sharedStyles[TextStyle.Header600],
                [SpanStyle.Header500] = sharedStyles[TextStyle.Header500],
                [SpanStyle.SectionHeader] = sharedStyles[TextStyle.SectionHeader]
            };
        }
    }
}
