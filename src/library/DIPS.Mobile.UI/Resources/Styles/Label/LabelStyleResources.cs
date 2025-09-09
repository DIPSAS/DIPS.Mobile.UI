using DIPS.Mobile.UI.Resources.Styles.Shared;

namespace DIPS.Mobile.UI.Resources.Styles.Label;

internal class LabelStyleResources
{
    public static readonly Dictionary<LabelStyle, Style> Styles;
    
    static LabelStyleResources()
    {
        var sharedStyles = SharedStyleResources.GetStylesForType(typeof(Components.Labels.Label));
        Styles = new Dictionary<LabelStyle, Style>
        {
            [LabelStyle.None] = sharedStyles[TextStyle.None],
            [LabelStyle.Body400] = sharedStyles[TextStyle.Body400],
            [LabelStyle.Body300] = sharedStyles[TextStyle.Body300],
            [LabelStyle.Body200] = sharedStyles[TextStyle.Body200],
            [LabelStyle.Body100] = sharedStyles[TextStyle.Body100],
            [LabelStyle.UI400] = sharedStyles[TextStyle.UI400],
            [LabelStyle.UI300] = sharedStyles[TextStyle.UI300],
            [LabelStyle.UI200] = sharedStyles[TextStyle.UI200],
            [LabelStyle.UI100] = sharedStyles[TextStyle.UI100],
            [LabelStyle.Header1000] = sharedStyles[TextStyle.Header1000],
            [LabelStyle.Header900] = sharedStyles[TextStyle.Header900],
            [LabelStyle.Header800] = sharedStyles[TextStyle.Header800],
            [LabelStyle.Header700] = sharedStyles[TextStyle.Header700],
            [LabelStyle.Header600] = sharedStyles[TextStyle.Header600],
            [LabelStyle.Header500] = sharedStyles[TextStyle.Header500],
            [LabelStyle.SectionHeader] = sharedStyles[TextStyle.SectionHeader]
        };
    }
}