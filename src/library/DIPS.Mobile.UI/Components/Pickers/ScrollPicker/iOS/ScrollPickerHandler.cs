using CoreGraphics;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.API.Tip;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;
using Foundation;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

public partial class ScrollPickerHandler : ViewHandler<ScrollPicker, UIButton>
{
    private Chip m_chip;
    private ScrollPickerViewController? m_scrollPickerViewController;

    protected override UIButton CreatePlatformView()
    {
        m_chip = new Chip { Style = Styles.GetChipStyle(ChipStyle.EmptyInput) };
        m_chip.Command = new Command(() => OnTapped(m_chip));

        return (UIButton)m_chip.ToPlatform(DUI.GetCurrentMauiContext!);
    }

    protected override void ConnectHandler(UIButton platformView)
    {
        base.ConnectHandler(platformView);

        m_chip.Title = VirtualView.PlaceholderText;
    }

    private void OnTapped(Chip chip)
    {
        if (PlatformView.Window.RootViewController is not {} rootViewController)
            return;
        
        m_scrollPickerViewController = new ScrollPickerViewController();
        m_scrollPickerViewController.Setup(chip, VirtualView);
        
        _ = rootViewController.PresentViewControllerAsync(m_scrollPickerViewController, true);
    }

    private static partial void MapSelectedIndex(ScrollPickerHandler handler, ScrollPicker scrollPicker)
    {
        if(scrollPicker.SelectedIndex == -1)
            return;
        
        handler.m_chip.Title = scrollPicker.ItemsSource.Cast<object>().ElementAt(scrollPicker.SelectedIndex).ToString()!;
        handler.m_chip.Style = Styles.GetChipStyle(ChipStyle.Input);
        handler.m_scrollPickerViewController?.OnChipTitleChanged();
    }
    
    protected override void DisconnectHandler(UIButton platformView)
    {
        base.DisconnectHandler(platformView);
        
        m_chip.Command = null;
        m_scrollPickerViewController = null;
    }

}

internal class ScrollPickerViewModel : UIPickerViewModel
{
#nullable disable
    public ScrollPicker ScrollPicker { get; set; }
#nullable enable
    
    public override nint GetComponentCount(UIPickerView pickerView)
    {
        return 1;
    }

    public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
    {
        return ScrollPicker.ItemsSource.Cast<object>().Count();
    }

    public override string GetTitle(UIPickerView pickerView, IntPtr row, IntPtr component)
    {
        return ScrollPicker.ItemsSource.Cast<object>().ElementAt((int)row).ToString()!;
    }

    public override void Selected(UIPickerView pickerView, IntPtr row, IntPtr component)
    {
        var index = (int)row;
        ScrollPicker.SelectedIndex = index;
    }
}

internal class ScrollPickerViewController : UIViewController
{
#nullable disable
    private ScrollPicker m_scrollPicker;
    private UIPickerView m_uiPicker;
    private UIButton m_uiButton;
#nullable enable

    public void Setup(Chip chip, ScrollPicker scrollPicker)
    {
        m_scrollPicker = scrollPicker;
        
        m_uiPicker = new UIPickerView();
        var vm = new ScrollPickerViewModel { ScrollPicker = m_scrollPicker };
        m_uiPicker.Model = vm;
        if (m_scrollPicker.SelectedIndex != -1)
        {
            m_uiPicker.Select(m_scrollPicker.SelectedIndex, 0, false);
            vm.Selected(m_uiPicker, m_scrollPicker.SelectedIndex, 0);
        }
        
        ModalPresentationStyle = UIModalPresentationStyle.Popover;
        
        if(PopoverPresentationController is null)
            return;

        m_uiButton = chip.ToPlatform(DUI.GetCurrentMauiContext!) as UIButton;
        
        PopoverPresentationController.PermittedArrowDirections = UIPopoverArrowDirection.Any;
        PopoverPresentationController.SourceView = m_uiButton!;
        PopoverPresentationController.SourceRect = m_uiButton!.Bounds;
        PopoverPresentationController.Delegate = new TipUIPopoverPresentationControllerDelegate();
        
        if (OperatingSystem.IsIOSVersionAtLeast(16, 0))
        {
            PopoverPresentationController.SourceItem = m_uiButton;
        }
    }

    public async void OnChipTitleChanged()
    {
        if (PopoverPresentationController is null)
            return;

        await Task.Delay(1);
        PopoverPresentationController.SourceView = m_uiButton!;
        PopoverPresentationController.SourceRect = m_uiButton!.Bounds;
        PopoverPresentationController.ContainerView.SetNeedsLayout();
        PopoverPresentationController.ContainerView.LayoutIfNeeded();
    }

    public override void ViewWillAppear(bool animated)
    {
        base.ViewWillAppear(animated);
        
        var padding = new Thickness(Sizes.GetSize(SizeName.size_6));
        if (View is null) 
            return;

        var width = CalculatePopoverWidth(m_uiPicker.Bounds);
        var fittingSize = View.SystemLayoutSizeFittingSize(UIView.UILayoutFittingCompressedSize);
        
        PreferredContentSize = new CGSize(width + padding.HorizontalThickness, fittingSize.Height);
    }
    
    public override void ViewDidLoad()
    {
        View = m_uiPicker;

        base.ViewDidLoad();
    }   

    private float CalculatePopoverWidth(CGRect bounds)
    {
        var textsFromItemsSource = m_scrollPicker.ItemsSource.Cast<object>().Select(obj => obj.ToString()).ToList();
        var longestTextInItemsSource = textsFromItemsSource.OrderByDescending(s => s?.Length).First();

        if (longestTextInItemsSource is null)
            return 0;
        
        var nssString = new NSString(longestTextInItemsSource);

        var labelSize = nssString.GetBoundingRect(new CGSize(bounds.Width, nfloat.PositiveInfinity),
            NSStringDrawingOptions.UsesLineFragmentOrigin, new UIStringAttributes { Font = UIFont.PreferredTitle2 },null);

        return (float)labelSize.Width;
    }
    
    public override void ViewWillDisappear(bool animated)
    {
        base.ViewWillDisappear(animated);

        m_uiPicker = null!;
        m_scrollPicker = null!;
        if(PopoverPresentationController is not null)
            PopoverPresentationController.Delegate = null!;
    }
    
}