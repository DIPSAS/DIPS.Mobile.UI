using DIPS.Mobile.UI.Components.Toolbars.Android;
using Microsoft.Maui.Controls.Platform;
using AView = Android.Views.View;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;


namespace DIPS.Mobile.UI.Effects.Modal.Android;

public class ModalPlatformEffect : PlatformEffect
{
    protected override void OnAttached()
    {
        if (Element is not ContentPage contentPage)
            return;

        var verticalStackLayout = new VerticalStackLayout();

        var toolbar = new DUIToolbar 
        { 
            PageConnectedTo = contentPage, 
            BackgroundColor = Colors.GetColor(ColorName.color_primary_90),
        };
        verticalStackLayout.Add(toolbar);
        verticalStackLayout.Add(contentPage.Content);
        contentPage.Content = verticalStackLayout;
    }

    protected override void OnDetached()
    {
        
    }
}