using CoreGraphics;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.API.Tip;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Microsoft.Maui.Platform;
using UIKit;
using UIModalPresentationStyle = UIKit.UIModalPresentationStyle;

namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;

internal class InlineDatePickerPopoverViewController : UIViewController
    {
#nullable disable
        private IDatePicker m_datePicker;
        private Action m_onDispose;
#nullable enable
        
        public void Setup(IDatePicker inlineDatePicker, View? sourceView, Action onDispose, View? passThroughView = null)
        {
            m_datePicker = inlineDatePicker;
            m_onDispose = onDispose;
            
            var nativeSourceView = sourceView?.ToPlatform(DUI.GetCurrentMauiContext!);
            
            ModalPresentationStyle = UIModalPresentationStyle.Popover;
            if(PopoverPresentationController is null)
                return;
            
            PopoverPresentationController.PermittedArrowDirections = UIPopoverArrowDirection.Down | UIPopoverArrowDirection.Up;
            if (nativeSourceView is not null)
            {
                PopoverPresentationController.SourceView = nativeSourceView;
                PopoverPresentationController.SourceRect = nativeSourceView.Bounds;
            }
            PopoverPresentationController.Delegate = new InlineDatePickerPopoverDelegate();
            
            if (OperatingSystem.IsIOSVersionAtLeast(16, 0) && nativeSourceView is not null)
            {
                PopoverPresentationController.SourceItem = nativeSourceView;
            }

            if (passThroughView is not null)
            {
                PopoverPresentationController.PassthroughViews = [passThroughView.ToPlatform(DUI.GetCurrentMauiContext!)];
            }
        }
        
        public override void ViewDidLoad()
        {
            View = m_datePicker.ToPlatform(DUI.GetCurrentMauiContext!);
            
            base.ViewDidLoad();
        }
        
        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            
            Dispose();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            
            PreferredContentSize = View!.SizeThatFits(new CGSize(int.MaxValue, int.MaxValue));
            
            if (m_datePicker is TimePicker.TimePicker)
            {
                PreferredContentSize = new CGSize(200, PreferredContentSize.Height);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            
            m_onDispose.Invoke();
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