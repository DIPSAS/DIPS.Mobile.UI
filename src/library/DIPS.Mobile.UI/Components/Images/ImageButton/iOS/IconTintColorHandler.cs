using System.ComponentModel;
using Microsoft.Maui.Platform;
using UIKit;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using IImage = Microsoft.Maui.IImage;

namespace DIPS.Mobile.UI.Components.Images;

// Some inspiration from Maui.CommunityToolkit: https://github.com/CommunityToolkit/Maui/blob/main/src/CommunityToolkit.Maui/Behaviors/PlatformBehaviors/IconTintColor/IconTintColorBehavior.macios.cs
// Attempts to set the Tint Color of an image on iOS, on the following controls: ImageButton, Image, Button
internal class IconTintColorHandler : IDisposable
{
    private readonly View m_view;

    public IconTintColorHandler(IView view)
    {
        if (view is not View v)
            throw new ArgumentException($"{view.GetType().Name} is not a View");
        
        v.PropertyChanged += ViewOnPropertyChanged;
        m_view = v;
    }

    private static void ViewOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is not View view)
            return;
        
        if ((e.PropertyName != Microsoft.Maui.Controls.ImageButton.IsLoadingProperty.PropertyName
             && e.PropertyName != Microsoft.Maui.Controls.Image.SourceProperty.PropertyName
             && e.PropertyName != Microsoft.Maui.Controls.ImageButton.SourceProperty.PropertyName
            && e.PropertyName != Microsoft.Maui.Controls.Button.ImageSourceProperty.PropertyName)
            || view is not IImageElement element)
        {
            return;
        }
        
        if (!element.IsLoading)
        {
            TrySetTintColor(view);
        }
    }
    
    private static void TrySetTintColor(View view)
    {
        if(view.Handler?.PlatformView is not UIView uiView)
            return;

        switch (uiView)
        {
            case UIImageView imageView:
                
                if (view is not Image.Image image)
                {
                    break;
                }
                
                SetUIImageViewTintColor(imageView, image.TintColor);
                break;
            case UIButton button:

                switch (view)
                {
                    case Button buttonView:
                        SetUIButtonTintColor(button, buttonView.ImageTintColor);
                        break;
                    case ImageButton.ImageButton imageButton:
                        SetUIButtonTintColor(button, imageButton.TintColor);
                        break;
                }

                break;
        }
    }
    
    private static void SetUIButtonTintColor(UIButton button, Color color)
    {
        if (button.ImageView.Image is null || button.CurrentImage is null)
        {
            return;
        }

        var templatedImage = button.CurrentImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);

        button.SetImage(null, UIControlState.Normal);
        var platformColor = color.ToPlatform();
        button.TintColor = platformColor;
        button.ImageView.TintColor = platformColor;
        button.SetImage(templatedImage, UIControlState.Normal);
    }

    private static void SetUIImageViewTintColor(UIImageView imageView, Color? color)
    {
        if (imageView.Image is null)
        {
            return;
        }

        if (color is null)
        {
            // Reset to original image colors (no tint)
            imageView.Image = imageView.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
        }
        else
        {
            imageView.Image = imageView.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            imageView.TintColor = color.ToPlatform();
        }
    }

    public void Dispose()
    {
        m_view.PropertyChanged -= ViewOnPropertyChanged;
    }
}