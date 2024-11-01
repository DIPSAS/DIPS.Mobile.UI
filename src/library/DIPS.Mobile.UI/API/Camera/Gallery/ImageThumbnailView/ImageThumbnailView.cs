using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Microsoft.Maui.Controls.Shapes;
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
        
        Clear();

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
        
        var closeButton = new Border
        {
            StrokeShape = new Ellipse(),
            BackgroundColor = Colors.GetColor(ColorName.color_neutral_60),
            StrokeThickness = 1,
            Stroke = Colors.GetColor(ColorName.color_system_white),
            HeightRequest = Sizes.GetSize(SizeName.size_5),
            WidthRequest = Sizes.GetSize(SizeName.size_5),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.End,
#if __IOS__
            Margin = new Thickness(0, Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_2), 0),
#endif
            Content = new Image
            {
                TintColor = Colors.GetColor(ColorName.color_system_white),
                Source = Icons.GetIcon(IconName.close_line)
            },
            Padding = Sizes.GetSize(SizeName.size_1)
        };

        Touch.SetCommand(closeButton, new Command(async () =>
        {
            var dialogResult = await DialogService.ShowDestructiveConfirmationMessage(
                DUILocalizedStrings.RemoveImageTitle,
                DUILocalizedStrings.RemoveImageDescription, DUILocalizedStrings.Cancel, DUILocalizedStrings.Remove);

            if (dialogResult == DialogAction.Closed)
                return;

            m_onRemoveImage.Invoke(imageThumbnailViewModel.Index);
        }));
        
        Add(image);
        Add(closeButton);
    }
}