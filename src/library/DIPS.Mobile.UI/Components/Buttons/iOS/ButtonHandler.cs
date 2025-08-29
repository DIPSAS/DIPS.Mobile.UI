using System.ComponentModel;
using CoreGraphics;
using DIPS.Mobile.UI.Platforms.iOS;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Buttons;

public partial class ButtonHandler : Microsoft.Maui.Handlers.ButtonHandler
{
    private bool m_disposed;

    protected override UIButton CreatePlatformView()
    {
        var button = new UIButtonWithExtraTappableArea();
        return button;
    }

    protected override void ConnectHandler(UIButton platformView)
    {
        base.ConnectHandler(platformView);
        
        (VirtualView as View).PropertyChanged += OnPropertyChanged;
    }

    /// <summary>
    /// TODO: Remove when this PR is merged: https://github.com/dotnet/maui/pull/25107
    /// <remarks>When Button's IsVisible is toggled the Image is reset by .NET MAUI so we need to set the tint color again, but it happens after this method is called, hence the Task.Delay</remarks>
    /// </summary>
    private async void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == View.IsVisibleProperty.PropertyName)
        {
            await Task.Delay(1);
            if(!m_disposed)
                MapImageTintColor(this, VirtualView as Button);
        }
    }

    private partial void AppendPropertyMapper()
    {
    }

    private static partial void MapImageToRightSide(ButtonHandler handler, Button button)
    {
        if(button.ImagePlacement == ImagePlacement.Left)
            return;
        
        handler.PlatformView.Transform = CGAffineTransform.MakeScale(-1, 1);
        handler.PlatformView.TitleLabel.Transform = CGAffineTransform.MakeScale(-1, 1);

        var imageViewTransform = handler.PlatformView.ImageView.Transform;
        handler.PlatformView.ImageView.Transform = CGAffineTransform.MakeScale(-1 * imageViewTransform.A, 1 * imageViewTransform.D);
    }
    
    private static async partial void OverrideMapImageSource(ButtonHandler handler, Button button)
    {
        await MapImageSourceAsync(handler, button);
        MapImageTintColor(handler, button);
    }

    private static partial void MapImageTintColor(ButtonHandler handler, Button button)
    {
        if(handler.PlatformView.ImageView.Image is null)
            return;
        
        handler.PlatformView.ImageView.TintColor = button.ImageTintColor.ToPlatform();
        handler.PlatformView.SetImage(handler.PlatformView.ImageView.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), UIControlState.Normal);
    }

    private static partial void MapAdditionalHitBoxSize(ButtonHandler handler, Button button)
    {
        if (handler.PlatformView is UIButtonWithExtraTappableArea uiButton)
            uiButton.AdditionalHitBoxSize = button.AdditionalHitBoxSize;
    }

    protected override void DisconnectHandler(UIButton platformView)
    {
        base.DisconnectHandler(platformView);

        m_disposed = true;
        
        (VirtualView as View).PropertyChanged -= OnPropertyChanged;
    }
}