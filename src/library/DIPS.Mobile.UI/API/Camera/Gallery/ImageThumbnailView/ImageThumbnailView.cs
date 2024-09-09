using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.API.Camera.Gallery.ImageThumbnailView;

internal class ImageThumbnailView : Grid
{
    private readonly Action<int> m_onRemoveImage;
    private readonly Action<int> m_onTappedImage;

    public ImageThumbnailView(Action<int> onRemoveImage, Action<int> onTappedImage)
    {
        m_onRemoveImage = onRemoveImage;
        m_onTappedImage = onTappedImage;

        WidthRequest = Sizes.GetSize(SizeName.size_18); 
        HeightRequest = Sizes.GetSize(SizeName.size_18);
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        
        if (BindingContext is not ImageThumbnailViewModel imageThumbnailViewModel)
            return;

        Margin = new Thickness(Sizes.GetSize(SizeName.size_2), 0);
        
        var image = new Image
        {
            Aspect = Aspect.AspectFill,
            VerticalOptions = LayoutOptions.Center,
            WidthRequest = Sizes.GetSize(SizeName.size_15),
            HeightRequest = Sizes.GetSize(SizeName.size_15)
        };
        image.SetBinding(Microsoft.Maui.Controls.Image.SourceProperty, new Binding(nameof(ImageThumbnailViewModel.Image), source: BindingContext, converter: new ByteArrayToImageSourceConverter()));
        Touch.SetCommand(image, new Command(() => m_onTappedImage.Invoke(imageThumbnailViewModel.Index)));
        
        UI.Effects.Layout.Layout.SetCornerRadius(image, Sizes.GetSize(SizeName.size_2));
        
        var closeButton = new Button
        {
            ImageSource = Icons.GetIcon(IconName.close_line),
            ImageTintColor = Colors.GetColor(ColorName.color_system_white),
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonSmall),
            BackgroundColor = Colors.GetColor(ColorName.color_neutral_60),
            BorderWidth = 1,
            BorderColor = Colors.GetColor(ColorName.color_system_white),
            HeightRequest = Sizes.GetSize(SizeName.size_6),
            WidthRequest = Sizes.GetSize(SizeName.size_6),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.End,
            Command = new Command(async () =>
            {
                var dialogResult = await DialogService.ShowDestructiveConfirmationMessage(DUILocalizedStrings.RemoveImageTitle,
                    DUILocalizedStrings.RemoveImageDescription, DUILocalizedStrings.Cancel, DUILocalizedStrings.Remove);
                
                if(dialogResult == DialogAction.Closed)
                    return;
                
                m_onRemoveImage.Invoke(imageThumbnailViewModel.Index);
            })
        };
        
        Add(image);
        Add(closeButton);
    }
}