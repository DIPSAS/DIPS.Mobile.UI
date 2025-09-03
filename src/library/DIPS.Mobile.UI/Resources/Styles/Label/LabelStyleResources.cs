using DIPS.Mobile.UI.Resources.Styles.Shared;

namespace DIPS.Mobile.UI.Resources.Styles.Label;

internal class LabelStyleResources
{
    private static readonly Dictionary<TextStyle, Style> s_sharedStyles = SharedStyleResources.GetStylesForType(typeof(Components.Labels.Label));
    
    public static Dictionary<LabelStyle, Style> Styles { get; } = new()
    {
        [LabelStyle.None] = s_sharedStyles[TextStyle.None],
        [LabelStyle.Body400] = s_sharedStyles[TextStyle.Body400],
        [LabelStyle.Body300] = s_sharedStyles[TextStyle.Body300],
        [LabelStyle.Body200] = s_sharedStyles[TextStyle.Body200],
        [LabelStyle.Body100] = s_sharedStyles[TextStyle.Body100],
        [LabelStyle.UI400] = s_sharedStyles[TextStyle.UI400],
        [LabelStyle.UI300] = s_sharedStyles[TextStyle.UI300],
        [LabelStyle.UI200] = s_sharedStyles[TextStyle.UI200],
        [LabelStyle.UI100] = s_sharedStyles[TextStyle.UI100],
        [LabelStyle.Header1000] = s_sharedStyles[TextStyle.Header1000],
        [LabelStyle.Header900] = s_sharedStyles[TextStyle.Header900],
        [LabelStyle.Header800] = s_sharedStyles[TextStyle.Header800],
        [LabelStyle.Header700] = s_sharedStyles[TextStyle.Header700],
        [LabelStyle.Header600] = s_sharedStyles[TextStyle.Header600],
        [LabelStyle.Header500] = s_sharedStyles[TextStyle.Header500],
        [LabelStyle.SectionHeader] = s_sharedStyles[TextStyle.SectionHeader]
    };
}