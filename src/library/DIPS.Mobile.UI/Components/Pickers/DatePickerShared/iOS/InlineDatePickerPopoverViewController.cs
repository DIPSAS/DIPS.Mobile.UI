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

        private bool m_hasAddedAdditionalButtons = false;
        private WeakReference<Grid>? m_gridReference;

        public void Setup(IDatePicker inlineDatePicker, View? sourceView)
        {
            m_datePickerReference = new WeakReference<IDatePicker>(inlineDatePicker);
            m_uiDatePickerReference = new WeakReference<UIDatePicker>(inlineDatePicker.ToPlatform(DUI.GetCurrentMauiContext!) as UIDatePicker ?? new UIDatePicker());
            
            var nativeSourceView = sourceView?.ToPlatform(DUI.GetCurrentMauiContext!);
            
            ModalPresentationStyle = UIModalPresentationStyle.Popover;
            if(PopoverPresentationController is null)
                return;

            if (DeviceInfo.Idiom != DeviceIdiom.Phone)
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

            PopoverPresentationController.CanOverlapSourceViewRect = true;
            PopoverPresentationController.Delegate = new InlineDatePickerPopoverDelegate();
            
            if (OperatingSystem.IsIOSVersionAtLeast(16, 0) && nativeSourceView is not null)
            {
                PopoverPresentationController.SourceItem = nativeSourceView;
            }
        }
        
        private Grid? Grid => m_gridReference is not null ? m_gridReference.TryGetTarget(out var target) ? target : null : null;
        private UIDatePicker? DatePicker => m_uiDatePickerReference.TryGetTarget(out var target) ? target : null;
        private IDatePicker? InlineDatePicker => m_datePickerReference.TryGetTarget(out var target) ? target : null;

        private UIView ConstructView()
        {
            m_gridReference = new WeakReference<Grid>(new Grid
            {
                RowDefinitions = [new RowDefinition(GridLength.Star), new RowDefinition(1), new RowDefinition(40)],
                ColumnDefinitions = [new ColumnDefinition(GridLength.Star), new ColumnDefinition(GridLength.Star)],
                RowSpacing = Sizes.GetSize(SizeName.size_1),
                Padding = Sizes.GetSize(SizeName.size_1)
            });

            Grid?.Add(InlineDatePicker);
            Grid?.SetColumnSpan(InlineDatePicker, 2);

            if (InlineDatePicker is not DatePicker.DatePicker datePicker)
                return Grid!.ToPlatform(DUI.GetCurrentMauiContext!);

            var divider = new Divider();
            Grid?.Add(divider, 0, 1);
            Grid?.SetColumnSpan((IView)divider, 2);
            
            Grid?.Add(new Button
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

            m_hasAddedAdditionalButtons = true;

            return Grid!.ToPlatform(DUI.GetCurrentMauiContext!);
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

            // iOS complains that the UICalendarView's height is smaller than it can render its content in when the popover is
            // animating its opening animation, so we wait a bit until the popover is larger to prevent this
            await Task.Delay(10);
            
            if(DeviceInfo.Idiom != DeviceIdiom.Phone)
                return;

            View = DatePicker?.Mode is UIDatePickerMode.Time ? DatePicker : ConstructView();

            PreferredContentSize = InlineDatePicker is TimePicker.TimePicker ? new CGSize(200, 150) : new CGSize(320, 400);
            
            // If the popover is pointing down or right, we need to increase the bottom padding of the grid to fit the additional buttons
            // (For some odd reason)
            if (PopoverPresentationController?.ArrowDirection is UIPopoverArrowDirection.Down && m_hasAddedAdditionalButtons && Grid is not null)
            {
                Grid.Padding = new Thickness(Grid.Padding.Left, Grid.Padding.Top,
                    Grid.Padding.Right, Grid.Padding.Bottom + Sizes.GetSize(SizeName.size_2));
            }
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