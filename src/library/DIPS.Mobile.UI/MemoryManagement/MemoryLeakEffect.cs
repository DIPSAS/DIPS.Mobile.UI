using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Controls.Platform;

namespace DIPS.Mobile.UI.MemoryManagement;


/// <summary>
/// An effect that will cause a view to have memory leaks. 
/// </summary>
/// <remarks>DO NOT USE THIS EFFECT AS IT WILL CAUSE MEMORY LEAK. ONLY USE IT IF YOU NEED TO TEST A PAGE WITH MEMORY LEAKS!</remarks>
[Obsolete("DO NOT USE THIS EFFECT AS IT WILL CAUSE MEMORY LEAK. ONLY USE IT IF YOU NEED TO TEST A PAGE WITH MEMORY LEAKS!")]
public class MemoryLeakEffect : PlatformEffect
{
    protected override void OnAttached()
    {
        if (DUI.IsDebug)
        {
            DeviceDisplay.MainDisplayInfoChanged += DeviceDisplayOnMainDisplayInfoChanged;
        }
    }

    private void DeviceDisplayOnMainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
    {
        
    }

    protected override void OnDetached()
    {
    }
}