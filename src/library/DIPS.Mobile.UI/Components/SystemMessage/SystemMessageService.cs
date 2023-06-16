namespace DIPS.Mobile.UI.Components.SystemMessage;

public static partial class SystemMessageService
{
    public static void Show(string text)
    {
        PlatformShow(new SystemMessage());
    }

    private static partial void PlatformShow(SystemMessage systemMessage);
}