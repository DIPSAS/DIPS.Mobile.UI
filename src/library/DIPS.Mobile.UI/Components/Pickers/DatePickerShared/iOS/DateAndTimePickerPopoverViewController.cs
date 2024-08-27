using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Platform;
using UIKit;
using CGSize = CoreGraphics.CGSize;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using UIModalPresentationStyle = UIKit.UIModalPresentationStyle;

namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;

internal class DateAndTimePickerPopoverViewController : UIViewController
{
#nullable disable
    private IDatePicker m_inlineDatePicker;
    private DateAndTimePicker.DateAndTimePicker m_dateAndTimePicker;
    
#nullable enable

    private bool m_hasAddedAdditionalButtons;
    private Grid? m_grid;

    public void Setup(DateAndTimePicker.DateAndTimePicker dateAndTimePicker, View? chipTapped, bool tappedDatePicker)
    {
        m_dateAndTimePicker = dateAndTimePicker;

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

        PopoverPresentationController.CanOverlapSourceViewRect = true;
        PopoverPresentationController.Delegate = new InlineDatePickerPopoverDelegate();

        SetPopoverSourceView(chipTapped);
        
        m_dateAndTimePicker.SetSelectedDateTimeOnPopoverOpen();
        
        SetDatePicker(tappedDatePicker);
    }

    private void SetPopoverSourceView(View? view)
    {
        var nativeSourceView = view?.ToPlatform(DUI.GetCurrentMauiContext!);
        
        if(nativeSourceView is null || PopoverPresentationController is null)
            return;
        
        PopoverPresentationController.SourceView = nativeSourceView;
        PopoverPresentationController.SourceRect = nativeSourceView.Bounds;
        
        if (OperatingSystem.IsIOSVersionAtLeast(16))
        {
            PopoverPresentationController.SourceItem = nativeSourceView;
        }
    }
    
    public void OnTappedChip(bool tappedDatePicker, View chipTapped)
    {
        switch (m_inlineDatePicker.Mode)
        {
            case DatePickerMode.Date when tappedDatePicker:
                DismissViewControllerAsync(true);
                return;
            case DatePickerMode.Time when !tappedDatePicker:
                DismissViewControllerAsync(true);
                return;
        }

        /*SetPopoverSourceView(chipTapped);*/
        SetDatePicker(tappedDatePicker);
        UpdateView();
    }

    private void SetDatePicker(bool tappedDatePicker)
    {
        if (tappedDatePicker)
        {
            DatePickerEnabled();
        }
        else
        {
            TimePickerEnabled();
        }
        
        m_inlineDatePicker!.SelectedDateTimeChanged -= OnDateChanged;
        m_inlineDatePicker.SelectedDateTimeChanged += OnDateChanged;
    }

    private void DatePickerEnabled()
    {
        m_inlineDatePicker = new InlineDatePicker
        {
            MaximumDate = m_dateAndTimePicker.MaximumDate,
            MinimumDate = m_dateAndTimePicker.MinimumDate,
            SelectedDate = m_dateAndTimePicker.SelectedDateTime,
            IgnoreLocalTime = m_dateAndTimePicker.IgnoreLocalTime
        };
    }

    private void TimePickerEnabled()
    {
        m_inlineDatePicker = new InlineTimePicker { SelectedTime = m_dateAndTimePicker.SelectedDateTime.TimeOfDay };
    }

    private void OnDateChanged(DateTime? dateTime)
    {
        m_dateAndTimePicker?.SetSelectedDateTime(dateTime);
    }

    private UIView ConstructView()
    {
        m_grid = new Grid
        {
            RowDefinitions = [new RowDefinition(GridLength.Star), new RowDefinition(1), new RowDefinition(40)],
            ColumnDefinitions = [new ColumnDefinition(GridLength.Star), new ColumnDefinition(GridLength.Star)],
            RowSpacing = Sizes.GetSize(SizeName.size_1),
            Padding = Sizes.GetSize(SizeName.size_1)
        };

        m_grid.Add(m_inlineDatePicker);
        m_grid.SetColumnSpan(m_inlineDatePicker, 2);

        var divider = new Divider();
        m_grid.Add(divider, 0, 1);
        m_grid.SetColumnSpan(divider, 2);

        if (m_dateAndTimePicker!.DisplayTodayButton)
        {
            m_grid.Add(
                new Button
                {
                    Text = DUILocalizedStrings.Today,
                    Command = new Command(() => m_inlineDatePicker?.SetSelectedDateTime(DateTime.Now)),
                    Style = Styles.GetLabelStyle(LabelStyle.UI300),
                    TextColor = Colors.GetColor(ColorName.color_primary_90),
                    HorizontalOptions = LayoutOptions.End
                }, 1, 2);

            m_hasAddedAdditionalButtons = true;
        }

        if (m_dateAndTimePicker!.IsNullable())
        {
            m_grid.Add(
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

        return m_grid.ToPlatform(DUI.GetCurrentMauiContext!);
    }

    public override void ViewIsAppearing(bool animated)
    {
        base.ViewIsAppearing(animated);

        UpdateView();
    }

    private void UpdateView()
    {
        View = ConstructView();

        PreferredContentSize = m_inlineDatePicker.Mode is DatePickerMode.Time ? new CGSize(200, 150) : new CGSize(320, 400);

        if (m_dateAndTimePicker.Mode is DatePickerMode.Time && m_hasAddedAdditionalButtons)
        {
            PreferredContentSize = new CGSize(200, 225);
        }
        
        // If the popover is pointing down or right, we need to increase the bottom padding of the grid to fit the additional buttons
        // (For some odd reason)
        if (PopoverPresentationController?.ArrowDirection is UIPopoverArrowDirection.Down &&
            m_hasAddedAdditionalButtons && m_grid is not null)
        {
            m_grid.Padding = new Thickness(m_grid.Padding.Left, m_grid.Padding.Top,
                m_grid.Padding.Right, m_grid.Padding.Bottom + Sizes.GetSize(SizeName.size_2));
        }
    }

    private void SetDateToNull()
    {
        m_inlineDatePicker?.SetSelectedDateTime(null);
        DismissViewControllerAsync(true);
    }
    
    public override void ViewWillDisappear(bool animated)
    {
        base.ViewWillDisappear(animated);

        // We can't make this a weak reference since the view is sometimes being GC'ed while the popover is visible causing the buttons to not work
        m_grid = null;

        m_inlineDatePicker.SelectedDateTimeChanged -= OnDateChanged;
        m_inlineDatePicker = null;
        m_dateAndTimePicker = null;
        
        
        // iOS complains that the UICalendarView's height is smaller than it can render its content in when the popover is
        // animating its close animation, so we null out the view to prevent this
        View = new UIView();
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