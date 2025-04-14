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
    private CGSize m_maxSize = new(300, 300);
    private CGSize m_minSize = new(0, 30);
    
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
            Children = { new Components.Labels.Label { Text = m_message } }
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
                
                base.Dispose();
            })
        };
        
        m_grid.Add(closeButton, 1);
        
        m_gridPlatform = m_grid.ToPlatform(DUI.GetCurrentMauiContext!);
        
        View?.AddSubview(m_gridPlatform);

        SetPopoverSize();
    }

    private void SetPopoverSize()
    {
        var measurement = m_grid.Measure(m_maxSize.Width, m_maxSize.Height);

        var clampedWidth = Math.Max(m_minSize.Width, Math.Min(measurement.Width, m_maxSize.Width));
        var clampedHeight = Math.Max(m_minSize.Height, Math.Min(measurement.Height, m_maxSize.Height));

        PreferredContentSize = new CGSize(clampedWidth, clampedHeight);
    }

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
                    xOffset = 7;
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