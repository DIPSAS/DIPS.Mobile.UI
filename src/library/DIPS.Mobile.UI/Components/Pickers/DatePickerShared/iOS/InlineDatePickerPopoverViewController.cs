using CoreGraphics;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Components.Pickers.NullableDatePickerShared;
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

    private bool m_hasAddedAdditionalButtons;
    private Grid? m_grid;
    private INullableDatePicker? m_nullableDatePicker;

    public void Setup(IDatePicker inlineDatePicker, View? sourceView, INullableDatePicker? nullableDatePicker)
    {
        m_datePickerReference = new WeakReference<IDatePicker>(inlineDatePicker);
        m_uiDatePickerReference =
            new WeakReference<UIDatePicker>(inlineDatePicker.ToPlatform(DUI.GetCurrentMauiContext!) as UIDatePicker ??
                                            new UIDatePicker());
        m_nullableDatePicker = nullableDatePicker;

        var nativeSourceView = sourceView?.ToPlatform(DUI.GetCurrentMauiContext!);

        ModalPresentationStyle = UIModalPresentationStyle.Popover;
        if (PopoverPresentationController is null)
            return;

        if (DeviceInfo.Idiom != DeviceIdiom.Phone)
        {
            PopoverPresentationController.PermittedArrowDirections = UIPopoverArrowDirection.Any;
        }
        else
        {
            PopoverPresentationController.PermittedArrowDirections =
                UIPopoverArrowDirection.Down | UIPopoverArrowDirection.Up;
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

    private UIDatePicker? DatePicker => m_uiDatePickerReference.TryGetTarget(out var target) ? target : null;
    private IDatePicker? InlineDatePicker => m_datePickerReference.TryGetTarget(out var target) ? target : null;

    private UIView ConstructView()
    {
        m_grid = new Grid
        {
            RowDefinitions = [new RowDefinition(GridLength.Star), new RowDefinition(1), new RowDefinition(40)],
            ColumnDefinitions = [new ColumnDefinition(GridLength.Star), new ColumnDefinition(GridLength.Star)],
            RowSpacing = Sizes.GetSize(SizeName.size_1),
            Padding = Sizes.GetSize(SizeName.size_1)
        };

        m_grid?.Add(InlineDatePicker);
        m_grid?.SetColumnSpan(InlineDatePicker, 2);

        var divider = new Divider();
        m_grid?.Add(divider, 0, 1);
        m_grid?.SetColumnSpan((IView)divider, 2);

        var displayTodayButton = InlineDatePicker is DatePicker.DatePicker { DisplayTodayButton: true }
            or DateAndTimePicker.DateAndTimePicker { DisplayTodayButton: true };

        if (displayTodayButton)
        {
            m_grid?.Add(
                new Button
                {
                    Text = DUILocalizedStrings.Today,
                    Command = new Command(SetDateToCurrent),
                    Style = Styles.GetLabelStyle(LabelStyle.UI300),
                    TextColor = Colors.GetColor(ColorName.color_primary_90),
                    HorizontalOptions = LayoutOptions.End
                }, 1, 2);

            m_hasAddedAdditionalButtons = true;
        }

        if (m_nullableDatePicker is not null)
        {
            m_grid?.Add(
                new Button
                {
                    Text = DUILocalizedStrings.Clear,
                    Command = new Command(SetDateToNull),
                    Style = Styles.GetLabelStyle(LabelStyle.UI300),
                    TextColor = Colors.GetColor(ColorName.color_primary_90),
                    HorizontalOptions = LayoutOptions.Start
                }, 0, 2);

            m_hasAddedAdditionalButtons = true;
        }

        return m_grid!.ToPlatform(DUI.GetCurrentMauiContext!);
    }

    public override void ViewWillDisappear(bool animated)
    {
        base.ViewWillDisappear(animated);

        // We can't make this a weak reference since the view is sometimes being GC'ed while the popover is visible causing the buttons to not work
        m_grid = null;

        // iOS complains that the UICalendarView's height is smaller than it can render its content in when the popover is
        // animating its close animation, so we null out the view to prevent this
        View = new UIView();
    }

    public override async void ViewIsAppearing(bool animated)
    {
        base.ViewIsAppearing(animated);

        // iOS complains that the UICalendarView's height is smaller than it can render its content in when the popover is
        // animating its opening animation, so we wait a bit until the popover is larger to prevent this
        await Task.Delay(50);

        if (DeviceInfo.Idiom != DeviceIdiom.Phone)
            return;

        View = DatePicker?.Mode is UIDatePickerMode.Time ? DatePicker : ConstructView();

        PreferredContentSize = InlineDatePicker is TimePicker.TimePicker ? new CGSize(200, 150) : new CGSize(320, 400);

        // If the popover is pointing down or right, we need to increase the bottom padding of the grid to fit the additional buttons
        // (For some odd reason)
        if (PopoverPresentationController?.ArrowDirection is UIPopoverArrowDirection.Down &&
            m_hasAddedAdditionalButtons && m_grid is not null)
        {
            m_grid.Padding = new Thickness(m_grid.Padding.Left, m_grid.Padding.Top,
                m_grid.Padding.Right, m_grid.Padding.Bottom + Sizes.GetSize(SizeName.size_2));
        }
    }

    private void SetDateToCurrent()
    {
        switch (InlineDatePicker)
        {
            case DatePicker.DatePicker datePicker:
                datePicker.SelectedDate = DateTime.Now;
                datePicker.SelectedDateCommand?.Execute(null);
                break;
            case DateAndTimePicker.DateAndTimePicker dateAndTimePicker:
                dateAndTimePicker.SelectedDateTime = DateTime.Now;
                dateAndTimePicker.SelectedDateTimeCommand?.Execute(null);
                break;
        }
    }

    private void SetDateToNull()
    {
        switch (m_nullableDatePicker)
        {
            case NullableDatePicker.NullableDatePicker nullableDatePicker:
                nullableDatePicker.SelectedDate = null;
                nullableDatePicker.SelectedDateCommand?.Execute(null);
                break;
            case NullableDateAndTimePicker.NullableDateAndTimePicker nullableDateAndTimePicker:
                nullableDateAndTimePicker.SelectedDateTime = null;
                nullableDateAndTimePicker.SelectedDateTimeCommand?.Execute(null);
                break;
            case NullableTimePicker.NullableTimePicker nullableTimePicker:
                nullableTimePicker.SelectedTime = null;
                nullableTimePicker.SelectedTimeCommand?.Execute(null);
                break;
        }

        DismissViewControllerAsync(true);
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