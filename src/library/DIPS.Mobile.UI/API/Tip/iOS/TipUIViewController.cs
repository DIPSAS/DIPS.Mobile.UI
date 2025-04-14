using CoreGraphics;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using UIKit;
using ImageButton = DIPS.Mobile.UI.Components.Images.ImageButton.ImageButton;
using UIModalPresentationStyle = UIKit.UIModalPresentationStyle;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.API.Tip;

internal class TipUIViewController : UIViewController
{
    // After manual testing, we found out that having the popover's Height < 50px, the arrow is not drawn correctly.
    private const int MinHeight = 50;
    private const int MaxHeightWidth = 300;

    private Grid m_grid;
    private UIView m_gridPlatform;
    
    private readonly string m_message;

    public TipUIViewController(string message)
    {
        m_message = message;
    }

    public bool IsDismissed { get; private set; }

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        m_grid = new Grid
        {
            ColumnSpacing = Sizes.GetSize(SizeName.content_margin_large),
            Padding = new Thickness(Sizes.GetSize(SizeName.content_margin_small)),
            ColumnDefinitions = new ColumnDefinitionCollection(new ColumnDefinition(GridLength.Star), new ColumnDefinition(GridLength.Auto)),
            Children =
            {
                new Components.Labels.Label 
                { 
                    Text = m_message, 
                    VerticalTextAlignment = TextAlignment.Center 
                }
            }
        };
        
        var closeButton = new ImageButton
        {
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.End,
            Source = Icons.GetIcon(IconName.close_fill),
            WidthRequest = Sizes.GetSize(SizeName.size_4),
            HeightRequest = Sizes.GetSize(SizeName.size_4),
            TintColor = Resources.Colors.Colors.GetColor(ColorName.color_icon_default),
            AdditionalHitBoxSize = Sizes.GetSize(SizeName.size_2),
            Command = new Command(async () =>
            {
                var closeTask = Close();
                if (closeTask is not null)
                    await closeTask;
                
                // DidDismiss not called in delegate when closing from code
                base.Dispose();
            })
        };
        
        m_grid.Add(closeButton, 1);
        
        m_gridPlatform = m_grid.ToPlatform(DUI.GetCurrentMauiContext!);
        
        View?.AddSubview(m_gridPlatform);

        SetPopoverSizeBasedOnGridContent();
    }

    private void SetPopoverSizeBasedOnGridContent()
    {
        var measurement = m_grid.Measure(MaxHeightWidth, MaxHeightWidth);

        var clampedWidth = Math.Min(measurement.Width, MaxHeightWidth);
        var clampedHeight = Math.Max(measurement.Height, MinHeight);

        PreferredContentSize = new CGSize(clampedWidth, clampedHeight);
    }

    /// <summary>
    /// We need to translate the grid when the arrow is pointing up or left, after manual testing we found out that the arrow is approx 13px.
    /// Because, the grid uses the arrow's container as a reference point, we need to move the grid to the left or up.
    /// </summary>
    public override void ViewDidLayoutSubviews()
    {
        base.ViewDidLayoutSubviews();
        
        var xOffset = 0;
        var yOffset = 0;

        if (PopoverPresentationController?.ArrowDirection != null)
        {
            switch (PopoverPresentationController.ArrowDirection)
            {
                case UIPopoverArrowDirection.Left:
                    xOffset = 13;
                    break;
                case UIPopoverArrowDirection.Up:
                    yOffset = 13;
                    break;
            }
        }

        m_gridPlatform.Frame = new CGRect(xOffset, yOffset, PreferredContentSize.Width, PreferredContentSize.Height);
    }

    internal Task? Close()
    {
        return IsDismissed ? Task.CompletedTask : PopoverPresentationController?.PresentingViewController.DismissViewControllerAsync(true);
    }

    public void SetupPopover(UIView? anchorView = null, UIBarButtonItem? anchorUiBarButton = null, UIPopoverArrowDirection permittedArrowDirection = UIPopoverArrowDirection.Any)
    {
        ModalPresentationStyle = UIModalPresentationStyle.Popover;

        if (PopoverPresentationController is null)
        {
            return;
        }

        PopoverPresentationController.PermittedArrowDirections = permittedArrowDirection;
        if (anchorView != null)
        {
            PopoverPresentationController.SourceRect = anchorView.Bounds;
            PopoverPresentationController.SourceView = anchorView;
        }

        if (anchorUiBarButton != null)
        {
            if (OperatingSystem.IsIOSVersionAtLeast(16))
            {
                PopoverPresentationController.SourceItem = anchorUiBarButton;
#pragma warning disable CA1422
                PopoverPresentationController.BarButtonItem = anchorUiBarButton;
#pragma warning restore CA1422
            }
        }

        PopoverPresentationController.Delegate = new TipUIPopoverPresentationControllerDelegate()
        {
            TipUiViewController = this
        };
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        IsDismissed = true;
        m_gridPlatform = null!;
    }
}