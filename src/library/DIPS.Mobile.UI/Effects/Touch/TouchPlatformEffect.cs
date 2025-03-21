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

        var isEnabled = Touch.GetIsEnabled(Element) && element.IsEnabled;
        
        if (isEnabled)
            Init();
    }
    
    private partial void Init();
    
    private void ElementOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != VisualElement.IsEnabledProperty.PropertyName)
            return;

        if(sender is not VisualElement element)
            return;

        var isEnabled = element.IsEnabled && Touch.GetIsEnabled(element);
        
        if (isEnabled)
            Init();
        else
            Remove(false);
    }

    protected override void OnDetached()
    {
        Remove(true);
        
        if (Element is not VisualElement element)
            return;
        
        element.PropertyChanged -= ElementOnPropertyChanged;
    }
    
    private partial void Remove(bool isDetaching);
}