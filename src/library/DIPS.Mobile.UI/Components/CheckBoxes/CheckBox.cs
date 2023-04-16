using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Sizes.Sizes;
using Image = DIPS.Mobile.UI.Components.Images.Image;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.CheckBoxes
{
    public partial class CheckBox : Grid
    {
        private readonly Image m_selectedImage;

        public CheckBox()
        {
            Padding = new Thickness(UI.Resources.Sizes.Sizes.GetSize(SizeName.size_0), UI.Resources.Sizes.Sizes.GetSize(SizeName.size_2)); //TODO: Use DesignSystem

            //Touch
            GestureRecognizers.Add(new TapGestureRecognizer() {Command = new Command(SetSelectedState)});

            //Image
            m_selectedImage = new Image() {VerticalOptions = LayoutOptions.Center};
            m_selectedImage.SetAppThemeColor(Image.ColorProperty,
                (DeviceInfo.Platform == DevicePlatform.Android)
                    ? ColorName.color_primary_90
                    : ColorName.color_system_black);
            m_selectedImage.Margin = new Thickness(UI.Resources.Sizes.Sizes.GetSize(SizeName.size_1));

            //Title label
            var itemLabel = new Label() {VerticalTextAlignment = TextAlignment.Center};
            itemLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, new Binding() {Source = this, Path = nameof(Text)});
            
            ColumnDefinitions = new ColumnDefinitionCollection()
            {
                new() {Width = 30}, new() {Width = GridLength.Star}
            };
            Children.Add(m_selectedImage);
            Children.Add(itemLabel);
            Grid.SetColumn(itemLabel, 1);

            IsSelectedChanged(this, IsSelected, IsSelected);
        }

        private static void IsSelectedChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is not CheckBox checkBox) return;
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                checkBox.m_selectedImage.AndroidProperties.IconResourceName =
                    checkBox.IsSelected ? "btn_checkbox_checked_mtrl" : "btn_checkbox_unchecked_mtrl";
                checkBox.m_selectedImage.SetAppThemeColor(Image.ColorProperty, ColorName.color_primary_90);
            }
            else
            {
                checkBox.m_selectedImage.iOSProperties.SystemIconName = (checkBox.IsSelected)
                    ? "checkmark.circle.fill"
                    : "checkmark.circle";
                checkBox.m_selectedImage.SetAppThemeColor(Image.ColorProperty, ColorName.color_primary_90);
            }

            checkBox.Command?.Execute(checkBox.CommandParameter);
        }

        private void SetSelectedState()
        {
            IsSelected = !IsSelected;
        }
    }
}