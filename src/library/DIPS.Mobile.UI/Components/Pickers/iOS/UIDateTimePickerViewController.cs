using CoreGraphics;
using DIPS.Mobile.UI.Components.Pickers.DateTimePickers;
using DIPS.Mobile.UI.Components.Pickers.iOS;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Sizes.Sizes;
using Foundation;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using DatePicker = DIPS.Mobile.UI.Components.Pickers.DateTimePickers.DatePicker;
using TimePicker = DIPS.Mobile.UI.Components.Pickers.DateTimePickers.TimePicker;

namespace DIPS.Mobile.UI.Components.Pickers.iOS;

public abstract class UIDateTimePickerViewController : UIViewController
{
    private readonly IDateTimePicker m_dateTimePicker;
    private UIButton? m_doneButton;
    private UILabel? m_description;
    
    private UIStackView m_content;
    private UIButton? m_closeButton;
    private UIStackView? m_toolbarStackLayout;
    private UIDatePicker? m_UIDateTimePicker;
        

    public UIDateTimePickerViewController(IDateTimePicker dateTimePicker)
    {
        m_dateTimePicker = dateTimePicker;
    }
    
    public override void ViewDidLoad()
    {
        m_content = new UIStackView { Axis = UILayoutConstraintAxis.Vertical, Distribution = UIStackViewDistribution.EqualSpacing};
        m_content.BackgroundColor = UIColor.SystemBackground;
        m_content.DirectionalLayoutMargins = new NSDirectionalEdgeInsets(Resources.Sizes.Sizes.GetSize(SizeName.size_4), 0, Resources.Sizes.Sizes.GetSize(SizeName.size_4), 0); //TODO: Use DesignSystem
        m_content.LayoutMarginsRelativeArrangement = true;

        CreateCloseButton();

        AddTopArea();
        AddDateTimePicker();
        AddDoneButton();
        
        View = m_content;
        base.ViewDidLoad();
    }

    private void CreateCloseButton()
    {
        m_closeButton = new UIButtonWithExtraTappableArea(-20, -20, new CGRect(0, 0, 50, 50));
        m_closeButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Right;
        m_closeButton.SetImage(UIImage.GetSystemImage("xmark.circle.fill"), UIControlState.Normal);
        m_closeButton.ImageView.TintColor = Colors.GetColor(ColorName.color_neutral_50).ToPlatform();
        m_closeButton.TouchUpInside += Close;
    }

    
    private void AddTopArea()
    {
        m_toolbarStackLayout = new UIStackView();
        
        if (!string.IsNullOrEmpty(m_dateTimePicker.Description))
        {
            var descriptionLabel = new UILabel
            {
                Text = m_dateTimePicker.Description, TextAlignment = UITextAlignment.Center,
            };
            //The below is needed for it to not push the UIButton out of view when the text for UILabel is too long
            descriptionLabel.SetContentHuggingPriority(250, UILayoutConstraintAxis.Horizontal);
            descriptionLabel.SetContentHuggingPriority(251, UILayoutConstraintAxis.Vertical);
            descriptionLabel.SetContentCompressionResistancePriority(749, UILayoutConstraintAxis.Horizontal);
            descriptionLabel.SetContentCompressionResistancePriority(750, UILayoutConstraintAxis.Vertical);
            m_toolbarStackLayout.InsertArrangedSubview(descriptionLabel, 0);
        }
        else
        {
            // Add empty view so that close button does not cover the whole top area
            m_toolbarStackLayout.AddArrangedSubview(new UIView());
        }

        m_toolbarStackLayout.AddArrangedSubview(m_closeButton!);

        m_content.AddArrangedSubview(m_toolbarStackLayout);
    }

    private void AddDateTimePicker()
    {
        m_UIDateTimePicker = new UIDatePicker {PreferredDatePickerStyle = UIDatePickerStyle.Inline};
        m_UIDateTimePicker.TimeZone = NSTimeZone.LocalTimeZone;
        m_UIDateTimePicker.BackgroundColor = UIColor.SystemBackground;
        m_UIDateTimePicker.TintColor = Colors.GetColor(ColorName.color_primary_90).ToPlatform();

        switch (m_dateTimePicker)
        {
            case DatePicker:
                m_UIDateTimePicker.Mode = UIDatePickerMode.Date;
                break;
            case DateAndTimePicker:
                m_UIDateTimePicker.Mode = UIDatePickerMode.DateAndTime;
                break;
            case TimePicker:
                m_UIDateTimePicker.Mode = UIDatePickerMode.Time;
                m_UIDateTimePicker.PreferredDatePickerStyle = UIDatePickerStyle.Wheels;
                break;
                    
        }

        SetDateTimePickerMode(m_UIDateTimePicker);
        SetDateTime(m_UIDateTimePicker);

        m_content.AddArrangedSubview(m_UIDateTimePicker);
    }

    protected abstract void SetDateTimePickerMode(UIDatePicker uiDatePicker);
    protected abstract void SetDateTime(UIDatePicker uiDatePicker);

    private void AddDoneButton()
    {
        m_doneButton =
            new UIButton(new CGRect(0, 0, 100, 50));
        m_doneButton.BackgroundColor = Colors.GetColor(ColorName.color_primary_d_90).ToPlatform();
        m_doneButton.Layer.CornerRadius = 8;
        m_doneButton.SetTitle(DUILocalizedStrings.Done, UIControlState.Normal);
        m_doneButton.TitleEdgeInsets = new UIEdgeInsets(5, 10, 5, 10);
        m_doneButton.TouchUpInside += Done;

        m_content.AddArrangedSubview(m_doneButton);
    }

    private void Done(object? sender, EventArgs e)
    {
        OnFinished(m_UIDateTimePicker!);
        Close(sender, e);
    }

    protected abstract void OnFinished(UIDatePicker uiDatePicker);

    private void Close(object? sender, EventArgs eventArgs)
    {
        DismissViewController(true, null);
    }

    public override void ViewWillDisappear(bool animated)
    {
        base.ViewWillDisappear(animated);

        if (m_doneButton != null && m_closeButton != null)
        {
            m_doneButton.TouchUpInside -= Done;
            m_closeButton.TouchUpInside -= Close;
        }
    }
}