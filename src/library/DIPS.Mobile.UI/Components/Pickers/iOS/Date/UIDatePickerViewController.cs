using CoreGraphics;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Foundation;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pickers.iOS.Date
{
    internal class UIDatePickerViewController : UIViewController
    {
        private readonly DatePicker m_duiDatePicker;
        private UIDatePicker? m_datePicker;
        private UIButton? m_doneButton;
        private UIButton? m_closeButton;

        public UIDatePickerViewController(DatePicker duiDatePicker)
        {
            m_duiDatePicker = duiDatePicker;
        }

        public override void ViewDidLoad()
        {
            var stackView = new UIStackView {Axis = UILayoutConstraintAxis.Vertical};
            stackView.BackgroundColor = UIColor.SystemBackground;
            stackView.DirectionalLayoutMargins = new NSDirectionalEdgeInsets(15, 0, 15, 0); //TODO: Use DesignSystem
            stackView.LayoutMarginsRelativeArrangement = true;


            AddTopArea(stackView);
            AddDatePicker(stackView);
            AddDoneButton(stackView, m_datePicker);

            View = stackView;
            base.ViewDidLoad();
        }

        private void AddTopArea(UIStackView stackView)
        {
            var toolbarStackView = new UIStackView() {Axis = UILayoutConstraintAxis.Horizontal,};

            if (!string.IsNullOrEmpty(m_duiDatePicker.Description))
            {
                var descriptionLabel = new UILabel()
                {
                    Text = m_duiDatePicker.Description, TextAlignment = UITextAlignment.Center,
                };
                //The below is needed for it to not push the UIButton out of view when the text for UILabel is too long
                descriptionLabel.SetContentHuggingPriority(250, UILayoutConstraintAxis.Horizontal);
                descriptionLabel.SetContentHuggingPriority(251, UILayoutConstraintAxis.Vertical);
                descriptionLabel.SetContentCompressionResistancePriority(749, UILayoutConstraintAxis.Horizontal);
                descriptionLabel.SetContentCompressionResistancePriority(750, UILayoutConstraintAxis.Vertical);
                toolbarStackView.AddArrangedSubview(descriptionLabel);
            }

            m_closeButton = new UIButtonWithExtraTappableArea(-20, -20, new CGRect(0, 0, 50, 50));
            m_closeButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Right;
            m_closeButton.SetImage(UIImage.GetSystemImage("xmark.circle.fill"), UIControlState.Normal);
            m_closeButton.ImageView.TintColor = Colors.GetColor(ColorName.color_neutral_50).ToPlatform();
            m_closeButton.TouchUpInside += Close;
            toolbarStackView.AddArrangedSubview(m_closeButton);

            stackView.AddArrangedSubview(toolbarStackView);
        }

        private void AddDatePicker(UIStackView stackView)
        {
            m_datePicker = new UIDatePicker {PreferredDatePickerStyle = UIDatePickerStyle.Inline};
            m_datePicker.Mode = UIDatePickerMode.Date;
            m_datePicker.TimeZone = NSTimeZone.LocalTimeZone;
            m_datePicker.BackgroundColor = UIColor.SystemBackground;
            m_datePicker.TintColor = Colors.GetColor(ColorName.color_primary_90).ToPlatform();
            if (m_duiDatePicker.SelectedDate != default)
            {
                m_datePicker.SetDate((NSDate)m_duiDatePicker.SelectedDate.ToLocalTime(), true);
            }

            stackView.AddArrangedSubview(m_datePicker);
        }

        private void AddDoneButton(UIStackView stackView, UIDatePicker datePicker)
        {
            m_doneButton =
                new UIButton(new CGRect(0, 0, 100, 50));
            m_doneButton.BackgroundColor = Colors.GetColor(ColorName.color_primary_d_90).ToPlatform();
            m_doneButton.Layer.CornerRadius = 8;
            m_doneButton.SetTitle(DUILocalizedStrings.Done, UIControlState.Normal);
            m_doneButton.TitleEdgeInsets = new UIEdgeInsets(5, 10, 5, 10);
            m_doneButton.TouchUpInside += Done;

            stackView.AddArrangedSubview(m_doneButton);
        }

        private void Done(object sender, EventArgs e)
        {
            m_duiDatePicker.SelectedDate = ((DateTime)m_datePicker?.Date).ToLocalTime();
            Close(sender, e);
        }

        private void Close(object sender, EventArgs eventArgs)
        {
            DismissViewController(true, null);
        }

        public override void ViewWillDisappear(bool animated)
        {
            // var date = m_datePicker.Date;
            base.ViewWillDisappear(animated);
            m_duiDatePicker.IsOpen = false;

            if (m_doneButton != null && m_closeButton != null)
            {
                m_doneButton.TouchUpInside -= Done;
                m_closeButton.TouchUpInside -= Close;
            }
        }
    }
}