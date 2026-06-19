namespace DIPS.Mobile.UI.Resources.Styles.SystemMessage;

internal static class SystemMessageStyleResources
{
    internal static Dictionary<SystemMessageStyle, SystemMessageStyleColors> Styles => new()
    {
        [SystemMessageStyle.Information] = SystemMessageTypeStyle.Information,
        [SystemMessageStyle.Error] = SystemMessageTypeStyle.Error,
        [SystemMessageStyle.Warning] = SystemMessageTypeStyle.Warning,
        [SystemMessageStyle.Success] = SystemMessageTypeStyle.Success
    };
}