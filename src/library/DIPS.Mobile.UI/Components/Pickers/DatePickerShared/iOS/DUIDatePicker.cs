using CoreGraphics;
using DIPS.Mobile.UI.Extensions.iOS;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;

public class DUIDatePicker : UIDatePicker
{
    private UIView? m_dateOrTimeView;
    private UIView? m_timeView;

    private bool m_firstDraw = true;

    public override void Draw(CGRect rect)
    {
        base.Draw(rect);

        if (m_firstDraw)
        {
            FetchSubviews();
        }

        m_firstDraw = false;

        SetStyle(); 
    }

    private void FetchSubviews()
    {
        m_dateOrTimeView = Subviews.FirstOrDefault()?.Subviews.FirstOrDefault();
        if (Mode == UIDatePickerMode.DateAndTime)
            m_timeView = Subviews.FirstOrDefault()?.Subviews.LastOrDefault();
    }

    // Tested on iOS 15,16,17
    private void SetStyle()
    {
        if (PreferredDatePickerStyle == UIDatePickerStyle.Inline)
            return; //Changing these colors when the style is inline will change the entire in line date picker. Which messes up the colors.

        if (m_dateOrTimeView is null)
            return;

        if (OperatingSystem.IsIOSVersionAtLeast(17))
        {
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
        else
        {
            SetDefaultLayerAttributes(Mode == UIDatePickerMode.Time
                ? m_dateOrTimeView
                : m_dateOrTimeView.Subviews.FirstOrDefault());
            SetDefaultLayerAttributes(m_timeView);
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
    }

}