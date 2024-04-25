using CoreGraphics;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.Pickers.Platforms;
using DIPS.Mobile.UI.Extensions.iOS;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;
using Microsoft.Maui.Platform;
using ObjCRuntime;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.iOS;

public class DUIDatePicker : UIDatePicker
{
    private UIView? m_dateOrTimeView;
    private UIView? m_timeView;

    private Chip? m_dateOrTimePlaceholderChip;
    private Chip? m_timePlaceholderChip;
    
    private bool m_firstDraw = true;

#nullable disable
    public IDateTimePicker DateTimePicker { get; set; }
#nullable enable

    public override void Draw(CGRect rect)
    {
        base.Draw(rect);

        if (!m_firstDraw)
            return;

        m_firstDraw = false;

        FetchSubviews();

        if (DateTimePicker is View view)
        {
            view.WidthRequest = Mode switch
            {
                UIDatePickerMode.Date => 121,
                UIDatePickerMode.Time => 70,
                _ => view.WidthRequest
            };
        }

        if (DateTimePicker.IsNullable)
        {
            CreatePlaceholders();
        }

        if (DateTimePicker.IsDateTimeOrTimeSpanDefault)
        {
            AddPlaceholders();
        }

        SetStyle(); 
    }

    public void UpdatePlaceholders()
    {
        if (DateTimePicker.IsDateTimeOrTimeSpanDefault)
        {
            AddPlaceholders();
        }
        else
        {
            RemovePlaceholders();
        }
    }

    private void FetchSubviews()
    {
        m_dateOrTimeView = Subviews.FirstOrDefault()?.Subviews.FirstOrDefault();
        if (Mode == UIDatePickerMode.DateAndTime)
            m_timeView = Subviews.FirstOrDefault()?.Subviews.LastOrDefault();
    }

    private void CreatePlaceholders()
    {
        m_dateOrTimePlaceholderChip = new Chip
        {
            Title = Mode == UIDatePickerMode.Time ? DUILocalizedStrings.Time : DUILocalizedStrings.Date,
            Style = Styles.GetChipStyle(ChipStyle.EmptyInput)
        };

        if (Mode != UIDatePickerMode.DateAndTime)
            return;

        m_timePlaceholderChip = new Chip
        {
            Title = DUILocalizedStrings.Time, 
            Style = Styles.GetChipStyle(ChipStyle.EmptyInput)
        };
    }

    private void AddPlaceholders()
    {
        UIView nativeView;
        if (m_dateOrTimePlaceholderChip is not null && m_dateOrTimeView is not null)
        {
            nativeView = m_dateOrTimePlaceholderChip.ToPlatform(DUI.GetCurrentMauiContext!);
            AddSubview(nativeView);

            NSLayoutConstraint.ActivateConstraints([
                nativeView.LeadingAnchor.ConstraintEqualTo(m_dateOrTimeView.LeadingAnchor),
                nativeView.BottomAnchor.ConstraintEqualTo(m_dateOrTimeView.BottomAnchor),
                nativeView.TrailingAnchor.ConstraintEqualTo(m_dateOrTimeView.TrailingAnchor),
                nativeView.HeightAnchor.ConstraintEqualTo(m_dateOrTimeView.HeightAnchor)
            ]);
        }

        if (m_timePlaceholderChip is null || m_timeView is null)
            return;

        nativeView = m_timePlaceholderChip.ToPlatform(DUI.GetCurrentMauiContext!);
        AddSubview(nativeView);

        NSLayoutConstraint.ActivateConstraints([
            nativeView.LeadingAnchor.ConstraintEqualTo(m_timeView.LeadingAnchor),
            nativeView.BottomAnchor.ConstraintEqualTo(m_timeView.BottomAnchor),
            nativeView.TrailingAnchor.ConstraintEqualTo(m_timeView.TrailingAnchor),
            nativeView.HeightAnchor.ConstraintEqualTo(m_timeView.HeightAnchor)
        ]);
    }

    private void RemovePlaceholders()
    {
        m_dateOrTimePlaceholderChip?.ToPlatform(DUI.GetCurrentMauiContext!).RemoveFromSuperview();
        m_timePlaceholderChip?.ToPlatform(DUI.GetCurrentMauiContext!).RemoveFromSuperview();
    }

    private void SetStyle()
    {
        if (PreferredDatePickerStyle == UIDatePickerStyle.Inline)
            return; //Changing these colors when the style is inline will change the entire in line date picker. Which messes up the colors.

        //Tested on iOS 15,16,17
        if (m_dateOrTimeView is null)
            return;

        //In line date picker
        SetDefaultLayerAttributes(m_dateOrTimeView.FindChildView<UIButton>());

        var systemBackgroundViewForDateOrTimeView = FindSystemBackgroundView(m_dateOrTimeView);
        if (systemBackgroundViewForDateOrTimeView is not null)
        {
            // Removes the slightly black overlay
            systemBackgroundViewForDateOrTimeView.Alpha = 0;
        }

        if (m_timeView is null)
            return;

        //In line time picker
        SetDefaultLayerAttributes(m_timeView.FindChildView<UIButton>());

        var systemBackgroundViewForTimeView = FindSystemBackgroundView(m_timeView);
        if (systemBackgroundViewForTimeView is not null)
        {
            // Removes the slightly black overlay
            systemBackgroundViewForTimeView.Alpha = 0;
        }
    }

    private static UIView? FindSystemBackgroundView(UIView root)
    {
        return root.Subviews.FirstOrDefault()?.Subviews.FirstOrDefault()?.Subviews.FirstOrDefault();
    }

    private static void SetDefaultLayerAttributes(UIView? view)
    {
        if (view is null)
            return;

        var defaultColor = Colors.GetColor(ColorName.color_secondary_30);
        var defaultCornerRadius = Sizes.GetSize(SizeName.size_2);

        view.BackgroundColor = defaultColor.ToPlatform();
        view.Layer.CornerRadius = defaultCornerRadius;
    }

    public void DisposeLayer()
    {
        m_dateOrTimeView = null;
        m_timeView = null;
        m_dateOrTimePlaceholderChip = null;
        m_timePlaceholderChip = null;
    }

}