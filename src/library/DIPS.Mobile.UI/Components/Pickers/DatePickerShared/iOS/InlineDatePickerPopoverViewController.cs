using CoreGraphics;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Intents;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using CGSize = CoreGraphics.CGSize;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using UIModalPresentationStyle = UIKit.UIModalPresentationStyle;
using VisualElement = Microsoft.Maui.Controls.VisualElement;

namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;

internal class InlineDatePickerPopoverViewController : UIViewController
    {
#nullable disable
        private WeakReference<IDatePicker> m_datePickerReference;
        private WeakReference<UIDatePicker> m_uiDatePickerReference;
#nullable enable
        
        public void Setup(IDatePicker inlineDatePicker, View? sourceView)
        {
            m_datePickerReference = new WeakReference<IDatePicker>(inlineDatePicker);
            m_uiDatePickerReference = new WeakReference<UIDatePicker>(inlineDatePicker.ToPlatform(DUI.GetCurrentMauiContext!) as UIDatePicker ?? new UIDatePicker());
            
            var nativeSourceView = sourceView?.ToPlatform(DUI.GetCurrentMauiContext!);
            
            ModalPresentationStyle = UIModalPresentationStyle.Popover;
            if(PopoverPresentationController is null)
                return;

            if (DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Landscape || DeviceInfo.Idiom != DeviceIdiom.Phone)
            {
                PopoverPresentationController.PermittedArrowDirections = UIPopoverArrowDirection.Any;
            }
            else
            {
                PopoverPresentationController.PermittedArrowDirections = UIPopoverArrowDirection.Down | UIPopoverArrowDirection.Up;
            }
            
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
        }

        private UIView ConstructView(UIDatePicker uiDatePicker)
        {
            var grid = new Grid
            {
                RowDefinitions = [new RowDefinition(GridLength.Auto), new RowDefinition(GridLength.Auto), new RowDefinition(GridLength.Auto)],
                ColumnDefinitions = [new ColumnDefinition(GridLength.Star), new ColumnDefinition(GridLength.Star)],
                RowSpacing = Sizes.GetSize(SizeName.size_1)
            };

            grid.WidthRequest = 320;
            grid.HeightRequest = 400;
            
            if (!m_datePickerReference.TryGetTarget(out var target))
                return uiDatePicker;

            if (target is not VisualElement visualElement)
                return uiDatePicker;
            
            /*visualElement.WidthRequest = 320;*/
            visualElement.HeightRequest = 366;
            
            grid.Add(visualElement);
            grid.SetColumnSpan(visualElement, 2);

            if (target is not DatePicker.DatePicker datePicker)
                return grid.ToPlatform(DUI.GetCurrentMauiContext!);

            var divider = new Divider();
            grid.Add(divider, 0, 1);
            grid.SetColumnSpan(divider, 2);
            
            grid.Add(new Button
            {
                Text = DUILocalizedStrings.Today,
                Command = new Command(() =>
                {
                    datePicker.SelectedDate = DateTime.Now;
                    datePicker.SelectedDateCommand?.Execute(null);
                }),
                Style = Styles.GetLabelStyle(LabelStyle.UI300),
                TextColor = Colors.GetColor(ColorName.color_primary_90),
                HorizontalOptions = LayoutOptions.End
            }, 1, 2);

            return grid.ToPlatform(DUI.GetCurrentMauiContext!);
        }
        
        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            // iOS complains that the UICalendarView's height is smaller than it can render its content in when the popover is
            // animating its close animation, so we null out the view to prevent this
            View = new UIView();
        }

        public override async void ViewIsAppearing(bool animated)
        {
            base.ViewIsAppearing(animated);
            
            // For some reason, iOS complains that the UICalendarView's height is smaller than it can render its content in
            // Therefore, we delay the construction of the view by one frame
            await Task.Delay(1);
            if (m_uiDatePickerReference.TryGetTarget(out var uiDatePicker))
            {
                View = ConstructView(uiDatePicker);

                /*PreferredContentSize = View.SizeThatFits(new CGSize(int.MaxValue, int.MaxValue));*/
                PreferredContentSize = new CGSize(320, 400);
                /*if (PopoverPresentationController?.ArrowDirection is UIPopoverArrowDirection.Down)
                {
                    PreferredContentSize = new CGSize(PreferredContentSize.Width, PreferredContentSize.Height + 40);
                }*/

            }

            if (!m_datePickerReference.TryGetTarget(out var target))
                return;

            if (target is TimePicker.TimePicker)
            {
                PreferredContentSize = new CGSize(200, PreferredContentSize.Height);
            }
            
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            
            
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