using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Extensions.iOS;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.iOS;

public class DUIDatePicker : UIDatePicker
{
    private UIView? m_dateOrTimeView;
    private UIView? m_timeView;

    private UIView? m_dateOrTimeViewButton;
    
    private Chip? m_dateOrTimePlaceholderChip;
    private Chip m_dateOrTimeChip;

#nullable disable
    public View VirtualView { get; set; }
    #nullable enable
    
    public void Initialize()
    {
        UpdateInLineLayerAttributes(); //Update attributes when its first drawn
        
        ValueChanged += OnValueChanged;
        EditingDidBegin += OnOpen;

        FetchSubviews();
        
        CreateChip();
        
        if (VirtualView is DatePicker { SelectedDate: null })
        {
            CreatePlaceholders();
        }
    }

    private void CreateChip()
    {
        m_dateOrTimeChip = new Chip();
        var nativeView = m_dateOrTimeChip.ToPlatform(DUI.GetCurrentMauiContext!);
        
        AddSubview(nativeView);
        NSLayoutConstraint.ActivateConstraints([
            nativeView.LeadingAnchor.ConstraintEqualTo(m_dateOrTimeView.LeadingAnchor),
            nativeView.TrailingAnchor.ConstraintEqualTo(m_dateOrTimeView.TrailingAnchor),
        ]);
    }

    private void FetchSubviews()
    {
        m_dateOrTimeView = Subviews.FirstOrDefault()?.Subviews.FirstOrDefault();
        m_dateOrTimeViewButton = m_dateOrTimeView?.Subviews.FirstOrDefault();
        if (Mode == UIDatePickerMode.DateAndTime)
            m_timeView = Subviews.FirstOrDefault()?.Subviews.LastOrDefault();
    }

    private void OnOpen(object? sender, EventArgs e)
    {
        RemovePlaceholders();
    }

    private void CreatePlaceholders()
    {
        switch (Mode)
        {
            case UIDatePickerMode.Date:
            case UIDatePickerMode.Time:
                {
                    if(m_dateOrTimePlaceholderChip is null && m_dateOrTimeView is not null)
                    {
                        m_dateOrTimePlaceholderChip = new Chip { Title = "Dato", Style = Styles.GetChipStyle(ChipStyle.EmptyInput)};
                        AddPlaceholder();
                    }
                    
                    break;
                }
            case UIDatePickerMode.DateAndTime:
                {
                    m_dateOrTimeView = Subviews.FirstOrDefault()?.Subviews.FirstOrDefault();
                    m_timeView = Subviews.FirstOrDefault()?.Subviews.LastOrDefault();
                    
                    if(m_dateOrTimeView is not null && m_timeView is not null)
                    {
                        //Set the width of the date picker to the width of the date and time picker plus their spacing
                        VirtualView.WidthRequest = m_dateOrTimeView.Frame.Width + m_timeView.Frame.Width + (m_timeView.Frame.X - m_dateOrTimeView.Frame.Width);
                    }
                }
                break;
            case UIDatePickerMode.CountDownTimer:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void AddPlaceholder()
    {
        if (m_dateOrTimePlaceholderChip is not null && m_dateOrTimeView is not null)
        {
            var nativeView = m_dateOrTimePlaceholderChip.ToPlatform(DUI.GetCurrentMauiContext!);
            AddSubview(nativeView);
            
            NSLayoutConstraint.ActivateConstraints([
                nativeView.LeadingAnchor.ConstraintEqualTo(m_dateOrTimeView.LeadingAnchor),
                nativeView.BottomAnchor.ConstraintEqualTo(m_dateOrTimeView.BottomAnchor),
                nativeView.TrailingAnchor.ConstraintEqualTo(m_dateOrTimeView.TrailingAnchor),
                nativeView.HeightAnchor.ConstraintEqualTo(m_dateOrTimeView.HeightAnchor)
            ]);
        }
        
    }

    private void RemovePlaceholders()
    {
        m_dateOrTimePlaceholderChip?.ToPlatform(DUI.GetCurrentMauiContext!).RemoveFromSuperview();
    }
    
    private void UpdateInLineLayerAttributes()
    {
        if (PreferredDatePickerStyle == UIDatePickerStyle.Inline) return; //Changing these colors when the style is inline will change the entire in line date picker. Which messes up the colors.
        
        //Tested on iOS 15,16,17
        
        //In line date picker
        var inlineDateViewLayer = this.Subviews.FirstOrDefault()?.Subviews.FirstOrDefault()?.Subviews
            .FirstOrDefault();
        SetDefaultLayerAttributes(inlineDateViewLayer);
        
        //In line time picker
        var inLineTimeView = this.Subviews.FirstOrDefault()?.Subviews.LastOrDefault();
        SetDefaultLayerAttributes(inLineTimeView);
        
    }

    private static void SetDefaultLayerAttributes(UIView? view)
    {
        if (view == null)
        {
            return;
        }

        var defaultColor = Colors.GetColor(ColorName.color_secondary_30);
        var defaultCornerRadius = Sizes.GetSize(SizeName.size_2);

        view.BackgroundColor = defaultColor.ToPlatform();
        view.Layer.CornerRadius = defaultCornerRadius;
    }

    public void DisposeLayer()
    {
        this.ValueChanged -= OnValueChanged;
    }

    private void OnValueChanged(object? sender, EventArgs e)
    {
        UpdateInLineLayerAttributes();
        UpdateChipTitle();
    }

    private void UpdateChipTitle()
    {
        var dateLabel = m_dateOrTimeView?.FindChildView<UILabel>();
        if (dateLabel is not null)
        {
            m_dateOrTimeChip.Title = dateLabel.Text ?? string.Empty;
        }
    }
}