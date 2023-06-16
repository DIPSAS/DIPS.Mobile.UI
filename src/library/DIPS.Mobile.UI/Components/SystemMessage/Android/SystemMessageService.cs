namespace DIPS.Mobile.UI.Components.SystemMessage;

public static partial class SystemMessageService
{
    private static async partial void PlatformShow(SystemMessage systemMessage)
    {
        var fragment = new SystemMessageFragment(systemMessage);

        // Small delay so that FragmentManager is initialized
        await Task.Delay(10);
        var fragmentManager = Platform.CurrentActivity.FragmentManager;

        fragmentManager.BeginTransaction()
            .Add(Android.Resource.Id.Content, fragment)
            .Commit();
    }
}