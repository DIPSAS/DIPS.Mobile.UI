using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.API.Tip;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;
using Microsoft.Maui.Platform;
using UIKit;
using UIModalPresentationStyle = UIKit.UIModalPresentationStyle;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;

public partial class DatePickerService
{
    internal static InlineDatePickerPopoverViewController? PresentedViewController { get; set; }
    
    public static partial void Open(DatePicker datePicker, View? sourceView)
    {
        if (IsOpen())
        {
            Close();
        }
        
        var inlineDatePicker = new InlineDatePicker
        {
            MaximumDate = datePicker.MaximumDate,
            MinimumDate = datePicker.MinimumDate,
            SelectedDate = datePicker.SelectedDate,
            IgnoreLocalTime = datePicker.IgnoreLocalTime
        };

        inlineDatePicker.SelectedDateCommand = new Command(() =>
        {
            Close();
            datePicker.SelectedDate = inlineDatePicker.SelectedDate;
            datePicker.SelectedDateCommand?.Execute(null);
        });
       
        PresentedViewController = new InlineDatePickerPopoverViewController();
        PresentedViewController.Setup(inlineDatePicker, sourceView);
        
        var currentViewController = Platform.GetCurrentUIViewController();
        _ = currentViewController?.PresentViewControllerAsync(PresentedViewController, true);
    }

    internal class InlineDatePickerPopoverViewController : UIViewController
    {
#nullable disable
        private DatePicker m_datePicker;
#nullable enable

        public void Setup(InlineDatePicker inlineDatePicker, View? sourceView)
        {
            m_datePicker = inlineDatePicker;
            
            var nativeSourceView = sourceView?.ToPlatform(DUI.GetCurrentMauiContext!);
            
            ModalPresentationStyle = UIModalPresentationStyle.Popover;
            if(PopoverPresentationController is null)
                return;
            
            PopoverPresentationController.PermittedArrowDirections = UIPopoverArrowDirection.Down;
            if (nativeSourceView is not null)
            {
                PopoverPresentationController.SourceView = nativeSourceView;
                PopoverPresentationController.SourceRect = nativeSourceView.Bounds;
            }
            PopoverPresentationController.Delegate = new TipUIPopoverPresentationControllerDelegate();
            
            if (OperatingSystem.IsIOSVersionAtLeast(16, 0) && nativeSourceView is not null)
            {
                PopoverPresentationController.SourceItem = nativeSourceView;
            }
        }
        
        public override void ViewDidLoad()
        {
            var inlineDatePicker = m_datePicker.ToPlatform(DUI.GetCurrentMauiContext!);
            
            View = inlineDatePicker;
            
            base.ViewDidLoad();
        }
        
        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            
            Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            
            DatePickerService.PresentedViewController = null;
            m_datePicker = null;
        }

    }
    
    internal class InlineDatePickerPopoverDelegate : UIPopoverPresentationControllerDelegate
    {
        public override UIModalPresentationStyle GetAdaptivePresentationStyle(UIPresentationController controller,
            UITraitCollection traitCollection)
        {
            return UIModalPresentationStyle.None;
        }
    }

    internal static partial bool IsOpen() => PresentedViewController is not null;

    public static partial void Close() => _ = PresentedViewController?.DismissViewControllerAsync(true);
}