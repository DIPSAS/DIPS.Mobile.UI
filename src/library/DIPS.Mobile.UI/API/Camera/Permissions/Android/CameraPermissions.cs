namespace DIPS.Mobile.UI.API.Camera.Permissions;
internal static partial class CameraPermissions
{
    internal static partial async Task<bool> CanUseCamera()
    {
        var status = await Microsoft.Maui.ApplicationModel.Permissions.CheckStatusAsync<Microsoft.Maui.ApplicationModel.Permissions.Camera>();
        if (status != PermissionStatus.Granted)
        {
            if (await Microsoft.Maui.ApplicationModel.Permissions.RequestAsync<Microsoft.Maui.ApplicationModel.Permissions.Camera>() != PermissionStatus.Granted)
            {
                return false;
            }
        }

        return true;
    }
}