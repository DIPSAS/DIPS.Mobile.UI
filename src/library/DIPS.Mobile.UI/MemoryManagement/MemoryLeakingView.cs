using DIPS.Mobile.UI.API.Library;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.MemoryManagement;

/// <summary>
/// A view that will cause a view to have memory leaks. 
/// </summary>
/// <remarks>DO NOT USE THIS VIEW AS IT WILL CAUSE MEMORY LEAK. ONLY USE IT IF YOU NEED TO TEST A PAGE WITH MEMORY LEAKS!</remarks>
[Obsolete("DO NOT USE THIS VIEW AS IT WILL CAUSE MEMORY LEAK. ONLY USE IT IF YOU NEED TO TEST A PAGE WITH MEMORY LEAKS!")]
public class MemoryLeakingView : ContentView
{
    public MemoryLeakingView()
    {
        Content = new Label() {Text = nameof(MemoryLeakingView)};
    }

    protected override void OnBindingContextChanged()
    {
        if (DUI.IsDebug)
        {
            DeviceDisplay.MainDisplayInfoChanged += DeviceDisplayOnMainDisplayInfoChanged;
        }
    }

    private void DeviceDisplayOnMainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
    {
        
    }
}