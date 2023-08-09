using System.Drawing;
using System.Runtime.Versioning;
using CoreGraphics;
using DIPS.Mobile.UI.Effects.Touch.iOS;
using DIPS.Mobile.UI.Resources.Colors;
using Foundation;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using Color = Microsoft.Maui.Graphics.Color;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Chips;

public partial class ChipHandler : ViewHandler<Chip, UIButton>
{
    private Button m_button;

    protected override UIButton CreatePlatformView()
    {
        m_button = new Button();
        return (UIButton)m_button.ToPlatform(MauiContext!);
    }

    protected override void ConnectHandler(UIButton platformView)
    {
        base.ConnectHandler(platformView);
        m_button.TextColor = Colors.GetColor(ColorName.color_system_black);

        // Here we style the button as close as possible to native compact datepicker in iOS
        // We do not use the design system here so this does not diverge at a later point
        m_button.Background = Color.FromArgb(BackgroundColorAsHex);
        m_button.FontSize = 17;
        m_button.CornerRadius = 6;
        m_button.Padding = new Thickness(12, 6, 12, 6);
        platformView.AddGestureRecognizer(new ChipGestureRecgonizer(this));
    }

    private static partial void MapTitle(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.SetTitle(chip.Title, UIControlState.Normal);
    }

    private static partial void MapHasCloseButton(ChipHandler handler, Chip chip)
    {
        var uiButton = handler.PlatformView;
        if (handler.VirtualView.HasCloseButton)
        {
            var image = UIImage.GetSystemImage("xmark.circle.fill",
                UIImageSymbolConfiguration.Create(Colors.GetColor(ColorName.color_neutral_60).ToPlatform()));
            if (image == null) return;
            uiButton.SetImage(image, UIControlState.Normal);

            ShiftImageToTheRight(handler, uiButton);
        }
        else
        {
            uiButton.SetImage(null, UIControlState.Normal);
        }
    }

    private static void ShiftImageToTheRight(ChipHandler handler, UIButton uiButton)
    {
        if (!OperatingSystem.IsIOSVersionAtLeast(14, 1))
        {
            return;
        }

        var imageWidth = uiButton.ImageView.IntrinsicContentSize.Width;
        uiButton.TitleEdgeInsets = new UIEdgeInsets(0, -imageWidth, 0, imageWidth);

        var titleWidth = uiButton.TitleLabel.IntrinsicContentSize.Width;
        var titleImageSpacing = Sizes.GetSize(SizeName.size_2);
        var spacing = titleWidth + titleImageSpacing;
        uiButton.ImageEdgeInsets = new UIEdgeInsets(0, spacing, 0, -spacing);

        var oldContentEdgeInsets = uiButton.ContentEdgeInsets;
        uiButton.ContentEdgeInsets = new UIEdgeInsets(oldContentEdgeInsets.Top, oldContentEdgeInsets.Left,
            oldContentEdgeInsets.Bottom, oldContentEdgeInsets.Right + titleImageSpacing);
    }

    [SupportedOSPlatform("ios14.0")] // MacCatalyst is a superset of iOS, therefore it's also supported.
    public static void SupportedOnWindowsIos14AndMacCatalyst14() { }

    public class ChipGestureRecgonizer : UITapGestureRecognizer
    {
        private readonly ChipHandler m_chipHandler;

        public ChipGestureRecgonizer(ChipHandler chipHandler)
        {
            m_chipHandler = chipHandler;
        }


        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            if (!m_chipHandler.VirtualView.HasCloseButton)
            {
                m_chipHandler.OnChipTapped();
                return;
            }

            var firstObject = touches.First();
            if (firstObject is not UITouch uiTouch) return;
            var uiButton = m_chipHandler.PlatformView;
            var touchLocationInView = uiTouch.LocationInView(uiButton);
            var didTouchInsideImage =
                touchLocationInView.X > uiButton.TitleLabel.Frame.X + uiButton.TitleLabel.Frame.Width;

            if (didTouchInsideImage)
            {
                m_chipHandler.OnCloseTapped();
            }
            else
            {
                m_chipHandler.OnChipTapped();
            }
        }
    }
}