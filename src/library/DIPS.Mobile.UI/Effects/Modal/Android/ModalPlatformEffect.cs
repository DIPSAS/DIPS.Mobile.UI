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

        var grid = new Grid(){RowDefinitions = new RowDefinitionCollection(){new(GridLength.Auto), new(GridLength.Star)}};

        var toolbar = new DUIToolbar 
        { 
            PageConnectedTo = contentPage, 
            BackgroundColor = Colors.GetColor(ColorName.color_primary_90),
        };
        grid.Add(toolbar, 0, 0);
        grid.Add(new ContentView { Content = contentPage.Content, Padding = contentPage.Padding }, 0, 1);
        contentPage.Padding = 0;
        contentPage.Content = grid;
    }

    protected override void OnDetached()
    {
        
    }
}