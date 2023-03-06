using System.ComponentModel;
using System.Threading.Tasks;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using DatePickerRenderer = DIPS.Mobile.UI.iOS.Components.Pickers.Date.DatePickerRenderer;
using DUIDatePicker = DIPS.Mobile.UI.Components.Pickers.DatePicker;

[assembly: ExportRenderer(typeof(DUIDatePicker), typeof(DatePickerRenderer))]

namespace DIPS.Mobile.UI.iOS.Components.Pickers.Date
{
    public class DatePickerRenderer : ViewRenderer
    {
        private DUIDatePicker? m_duiDatePicker;
        private UIDatePicker? m_uiDatePicker;
        internal const string DatePickerRestorationIdentifier = nameof(UIDatePickerViewController);

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (e.NewElement is DUIDatePicker datePicker)
                {
                    m_duiDatePicker = datePicker;
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case nameof(DUIDatePicker.IsOpen):
                    ToggleShowing();
                    break;
            }
        }

        private void ToggleShowing()
        {
            if (m_duiDatePicker is not {IsOpen: true}) return;

            var currentViewController = DUI.CurrentViewController;
            if (currentViewController?.View == null) return;
            
            var viewController = new UIDatePickerViewController(m_duiDatePicker);
            viewController.RestorationIdentifier = DatePickerRestorationIdentifier;
            var screenWidth = UIScreen.MainScreen.Bounds.Width;
            var screenHeight = UIScreen.MainScreen.Bounds.Height;
            var sizeOfPopover = new CGSize(screenWidth - 30, screenHeight / 2);
            viewController.PreferredContentSize = sizeOfPopover;
            viewController.ModalPresentationStyle = UIModalPresentationStyle.Popover;
            var popoverPresentationController = viewController.PopoverPresentationController;
            if (popoverPresentationController != null)
            {
                // set up the popover presentation controller
                popoverPresentationController.PermittedArrowDirections =
                    0; //This allows the popover to display over / under the native view bounds depending on where the native view is placed in the page
                popoverPresentationController.Delegate = new UIPopoverPresentationControllerDelegate();
                
                popoverPresentationController.SourceView = currentViewController.View; //This is to make the popover relative to the entire page and not a particular view in the page for it to be placed correctly on the screen;
                popoverPresentationController.SourceRect = new CGRect(screenWidth - sizeOfPopover.Width,
                    screenHeight-sizeOfPopover.Height, 0, 0);
            }

            
            currentViewController.PresentViewController(viewController, true, null);
        }
        
        internal static async Task Close()
        {
            var potentialBottomSheetUiViewController = DUI.CurrentViewController;
            if (potentialBottomSheetUiViewController != null)
            {
                await potentialBottomSheetUiViewController.DismissViewControllerAsync(false);
                await Task.Delay(100); //Small delay before its actually removed.    
            }
        }

        internal static bool IsOpen()
        {
            return DUI.CurrentViewController?.RestorationIdentifier == DatePickerRestorationIdentifier;
        }
    }
}