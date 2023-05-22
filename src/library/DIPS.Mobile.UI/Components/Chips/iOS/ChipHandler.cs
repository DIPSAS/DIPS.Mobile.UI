using DIPS.Mobile.UI.Resources.Colors;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Chips;

public partial class ChipHandler : ViewHandler<Chip, UIButton>
{
    private Button Button { get; set; }

    protected override UIButton CreatePlatformView()
    {
        Button = new Button();
        return (UIButton)Button.ToPlatform(MauiContext!);
    }

    protected override void ConnectHandler(UIButton platformView)
    {
        base.ConnectHandler(platformView);

        Button.TextColor = Colors.GetColor(ColorName.color_system_black);
        
        // Here we style the button as close as possible to native compact datepicker in iOS
        // We do not use the design system here so this does not diverge at a later point
        Button.BackgroundColor = new Color(118, 118, 128, 30);
        Button.FontSize = 17;
        Button.CornerRadius = 6;
        Button.Padding = new Thickness(12, 6, 12, 6);
    }

    private static partial void MapTitle(ChipHandler handler, Chip chip)
    {
        handler.Button.Text = chip.Title;
    }
    

}