using System.ComponentModel;
using Microsoft.Maui.Controls.Platform;

namespace DIPS.Mobile.UI.Effects.Touch;

public partial class TouchPlatformEffect : PlatformEffect
{
    protected override void OnAttached()
    {
        if (Element is not VisualElement element)
            return;
        
        element.PropertyChanged += ElementOnPropertyChanged;
        
        if (element.IsEnabled)
            Init();
    }
    
    private partial void Init();
    
    private void ElementOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != VisualElement.IsEnabledProperty.PropertyName)
            return;

        if(sender is not VisualElement element)
            return;

        if (element.IsEnabled)
            Init();
        else
            Dispose();
    }

    protected override void OnDetached()
    {
        Dispose();
        
        if (Element is not VisualElement element)
            return;
        
        element.PropertyChanged -= ElementOnPropertyChanged;
    }
    
    private partial void Dispose();
}