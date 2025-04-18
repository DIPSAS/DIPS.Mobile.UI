using DIPS.Mobile.UI.Components.Images;
using DIPS.Mobile.UI.Components.Images.NativeIcon;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.Sizes;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = Microsoft.Maui.Graphics.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.CheckBoxes
{
    [Obsolete("This component is not using the correct styling. Please use Checkmark / RadioButton")]
    public partial class CheckBox : Grid
    {
        private readonly NativeIcon m_selectedNativeIcon;

        public CheckBox()
        {
            Padding = new Thickness(Sizes.GetSize(SizeName.size_0), Sizes.GetSize(SizeName.content_margin_small));
            //Touch
            Touch.SetCommand(this, new Command(SetSelectedState));

            //Image
            m_selectedNativeIcon = new NativeIcon() {VerticalOptions = LayoutOptions.Center};
            m_selectedNativeIcon.HeightRequest = Sizes.GetSize(SizeName.size_6);
            m_selectedNativeIcon.WidthRequest = Sizes.GetSize(SizeName.size_6);
            m_selectedNativeIcon.SetAppThemeColor(NativeIcon.ColorProperty,
                ColorName.color_icon_default);
            m_selectedNativeIcon.Margin = new Thickness(Sizes.GetSize(SizeName.content_margin_xsmall));

            //Title label
            var itemLabel = new Label() {VerticalTextAlignment = TextAlignment.Center, Style = Styles.GetLabelStyle(LabelStyle.Body200)};
            itemLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (CheckBox checkbox) => checkbox.Text, source: this);
            
            ColumnDefinitions = [new ColumnDefinition { Width = GridLength.Auto }, new ColumnDefinition { Width = GridLength.Star }];
            Children.Add(m_selectedNativeIcon);
            Children.Add(itemLabel);
            Grid.SetColumn(itemLabel, 1);

            IsSelectedChanged(this, IsSelected, IsSelected);
        }

        private static void IsSelectedChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is not CheckBox checkBox) return;
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                checkBox.m_selectedNativeIcon.AndroidIconResourceName =
                    checkBox.IsSelected ? "btn_checkbox_checked_mtrl" : "btn_checkbox_unchecked_mtrl";
                checkBox.m_selectedNativeIcon.SetAppThemeColor(NativeIcon.ColorProperty, ColorName.color_icon_default);
            }
            else
            {
                checkBox.m_selectedNativeIcon.iOSSystemIconName = (checkBox.IsSelected)
                    ? "checkmark.circle.fill"
                    : "checkmark.circle";
                checkBox.m_selectedNativeIcon.SetAppThemeColor(NativeIcon.ColorProperty, ColorName.color_icon_default);
            }

            checkBox.Command?.Execute(checkBox.CommandParameter);
        }

        private void SetSelectedState()
        {
            IsSelected = !IsSelected;
        }
    }
}