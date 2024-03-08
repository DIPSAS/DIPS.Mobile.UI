using CoreGraphics;
using CoreImage;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Effects.Touch;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = Microsoft.Maui.Graphics.Colors;
using ContentView = Microsoft.Maui.Controls.ContentView;

namespace DIPS.Mobile.UI.API.Tip;

internal class TipUIViewController : UIViewController
{
    private CGSize m_maxSize = new(300, 300);
    private readonly string m_message;
    private readonly UIView m_anchorView;
    private Label? m_label;
    private Grid? m_grid;

    public TipUIViewController(string message, UIView anchorView)
    {
        m_message = message;
        m_anchorView = anchorView;
    }

    
    public override void ViewWillAppear(bool animated)
    {
        base.ViewWillAppear(animated);
        if (PopoverPresentationController == null) return;
        
        //Set padding depending on arrow direction
        if (m_grid == null) return;
        var padding = new Thickness(Sizes.GetSize(SizeName.size_3));
        var arrowSize = Sizes.GetSize(SizeName.size_3);
        
        switch (PopoverPresentationController.ArrowDirection)
        {
            case UIPopoverArrowDirection.Up:
                padding.Top += arrowSize;
                padding.Bottom = 0;
                break;
            case UIPopoverArrowDirection.Down:
                padding.Bottom = arrowSize;
                break;
            case UIPopoverArrowDirection.Left: //Can be tested by rotating the phone
                padding.Left += arrowSize;
                break;
            case UIPopoverArrowDirection.Right: //Can be tested by rotating the phone
                padding.Right += arrowSize;
                break;
            case UIPopoverArrowDirection.Any:
            case UIPopoverArrowDirection.Unknown:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        m_grid.Padding = padding;

        if (View == null) return;

        //Set the size of the popover to fit the grid
        PreferredContentSize = View.SizeThatFits(m_maxSize);
    }

    public override void ViewDidLoad()
    {
        m_grid = new Grid()
        {
            BackgroundColor = Colors.Transparent,
            ColumnDefinitions = [new ColumnDefinition(GridLength.Star), new ColumnDefinition(GridLength.Auto)],
            RowDefinitions = new RowDefinitionCollection(new RowDefinition(GridLength.Star))
        };

        var closeImage = new Image()
        {
            VerticalOptions = LayoutOptions.Start,
            Source = Icons.GetIcon(IconName.close_fill),
            WidthRequest = Sizes.GetSize(SizeName.size_4),
            HeightRequest = Sizes.GetSize(SizeName.size_4)
        };
        var wrappingContentView = new ContentView() {Content = closeImage, Padding = 3};
        
        Touch.SetCommand(wrappingContentView,  new Command(() =>
        {
            _ = Close();
        }));
        
        m_label = new Label() {Text = m_message};
        m_grid.Add(m_label, 0);
        m_grid.Add(wrappingContentView, 1);

        
        if (DUI.GetCurrentMauiContext != null)
        {
            View = m_grid.ToPlatform(DUI.GetCurrentMauiContext);
        }
        base.ViewDidLoad();
         
    }

    internal Task? Close()
    {
        return PopoverPresentationController?.PresentingViewController.DismissViewControllerAsync(true);
    }

    public void SetupPopover(UIPopoverArrowDirection permittedArrowDirection = UIPopoverArrowDirection.Any)
    {
        ModalPresentationStyle = UIModalPresentationStyle.Popover;

        if (PopoverPresentationController is null)
        {
            return;
        }

        PopoverPresentationController.PermittedArrowDirections = permittedArrowDirection;
        PopoverPresentationController.SourceRect = m_anchorView.Bounds;
        PopoverPresentationController.SourceView = m_anchorView;
        PopoverPresentationController.Delegate =
            new TipUIPopoverPresentationControllerDelegate();
    }
}