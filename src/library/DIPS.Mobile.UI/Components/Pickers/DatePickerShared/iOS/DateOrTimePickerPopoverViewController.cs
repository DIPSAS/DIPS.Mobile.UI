using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Platform;
using UIKit;
using CGSize = CoreGraphics.CGSize;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using UIModalPresentationStyle = UIKit.UIModalPresentationStyle;

namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;

internal class DateOrTimePickerPopoverViewController : UIViewController, IDatePickerPopoverViewController
{
#nullable disable
    private IDatePicker m_inlineDatePicker;
    private IDatePicker m_datePicker;
#nullable enable

    private Grid? m_grid;
    private Color? m_previousTitleColor;

    public void Setup(IDatePicker inlineDatePicker, IDatePicker datePicker, View? sourceView)
    {
        m_inlineDatePicker = inlineDatePicker;
        m_datePicker = datePicker;
        
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

        /*PopoverPresentationController.CanOverlapSourceViewRect = true;*/
        PopoverPresentationController.Delegate = new InlineDatePickerPopoverDelegate();

        if (OperatingSystem.IsIOSVersionAtLeast(16) && nativeSourceView is not null)
        {
            PopoverPresentationController.SourceItem = nativeSourceView;
        }

        inlineDatePicker.SelectedDateTimeChanged += OnDateChanged;
    }

    private void OnDateChanged(DateTime? dateTime)
    {
        if (m_datePicker is DatePicker.DatePicker { ShouldCloseOnDateSelected: true })
        {
            _ = DismissViewControllerAsync(true);
        }
        
        m_datePicker?.SetSelectedDateTime(dateTime);
    }

    private UIView ConstructView()
    {
        if (!m_datePicker.ShouldDisplayTodayButton)
        {
            return m_inlineDatePicker.ToPlatform(DUI.GetCurrentMauiContext!);
        }
        
        m_grid = new Grid
        {
            RowDefinitions = [new RowDefinition(GridLength.Star), new RowDefinition(1), new RowDefinition(40)],
            ColumnDefinitions = [new ColumnDefinition(GridLength.Star), new ColumnDefinition(GridLength.Star)],
            RowSpacing = Sizes.GetSize(SizeName.content_margin_xsmall),
            Padding = Sizes.GetSize(SizeName.content_margin_xsmall)
        };

        m_grid.Add(m_inlineDatePicker);
        m_grid.SetColumnSpan(m_inlineDatePicker, 2);

        var divider = new Divider();
        m_grid.Add(divider, 0, 1);
        m_grid.SetColumnSpan(divider, 2);

        m_grid.Add(
            new Button
            {
                Text = DUILocalizedStrings.Today,
                Command = new Command(() => m_inlineDatePicker?.SetSelectedDateTime(DateTime.Now)),
                Style = Styles.GetLabelStyle(LabelStyle.UI300),
                TextColor = Colors.GetColor(ColorName.color_text_action),
                HorizontalOptions = LayoutOptions.End
            }, 1, 2);

        return m_grid.ToPlatform(DUI.GetCurrentMauiContext!);
    }

    public override void ViewIsAppearing(bool animated)
    {
        base.ViewIsAppearing(animated);

        if (m_datePicker is Chip chip)
        {
            m_previousTitleColor = chip.TitleColor;
            chip.TitleColor = Colors.GetColor(ColorName.color_text_action);
        }
        
        View = ConstructView();

        PreferredContentSize = m_datePicker.Mode is DatePickerMode.Time ? new CGSize(200, 150) : new CGSize(320, 400);
        
        // If the popover is pointing down or right, we need to increase the bottom padding of the grid to fit the additional buttons
        // (For some odd reason)
        if (PopoverPresentationController?.ArrowDirection is UIPopoverArrowDirection.Down &&
            m_datePicker.ShouldDisplayTodayButton && m_grid is not null)
        {
            m_grid.Padding = new Thickness(m_grid.Padding.Left, m_grid.Padding.Top,
                m_grid.Padding.Right, m_grid.Padding.Bottom + Sizes.GetSize(SizeName.content_margin_small));
        }
    }
    
    public override void ViewWillDisappear(bool animated)
    {
        base.ViewWillDisappear(animated);

        // We can't make this a weak reference since the view is sometimes being GC'ed while the popover is visible causing the buttons to not work
        m_grid = null;

        m_inlineDatePicker.SelectedDateTimeChanged -= OnDateChanged;
        m_inlineDatePicker = null;
        
        if (m_datePicker is Chip chip)
        {
            chip.TitleColor = m_previousTitleColor;
        }
        
        m_datePicker = null;
        
        // iOS complains that the UICalendarView's height is smaller than it can render its content in when the popover is
        // animating its close animation, so we null out the view to prevent this
        View = new UIView();
    }
}