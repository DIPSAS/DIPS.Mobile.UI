using DIPS.Mobile.UI.Components.Images;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Sizes.Sizes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Pickers.Base
{
    /// <summary>
    /// Base class for Pickers
    /// </summary>
    public partial class Picker : Frame
    {
        protected readonly Label m_pickedItemLabel;

        public Picker()
        {
            HorizontalOptions = LayoutOptions.Fill;
            VerticalOptions = LayoutOptions.Center;
            HasShadow = false;

            Padding = UI.Resources.Sizes.Sizes.GetSize(SizeName.size_0);
            CornerRadius = UI.Resources.Sizes.Sizes.GetSize(SizeName.size_2);
            
            this.SetAppThemeColor(BackgroundProperty, ColorName.color_system_white);

            //Header
            var headerLabel = new Label {VerticalTextAlignment = TextAlignment.Center};
            headerLabel.SetBinding(Label.TextProperty,
                new Binding() {Source = this, Path = nameof(Title)});

            //Selected item
            m_pickedItemLabel = new Label {HorizontalTextAlignment = TextAlignment.End, VerticalTextAlignment = TextAlignment.Center};
            m_pickedItemLabel.SetAppThemeColor(Microsoft.Maui.Controls.Label.TextColorProperty, ColorName.color_primary_90);

            var image = new NativeIcon();
            image.iOSSystemIconName = "chevron.up.chevron.down";
            image.AndroidIconResourceName = "mtrl_dropdown_arrow";
            image.SetAppThemeColor(NativeIcon.ColorProperty, Colors.GetColor(ColorName.color_primary_90),
                Colors.GetColor(ColorName.color_primary_90));

            //Arrange the grid
            var pickerContent = new Grid()
            {
                Padding = new Thickness(UI.Resources.Sizes.Sizes.GetSize(SizeName.size_4), UI.Resources.Sizes.Sizes.GetSize(SizeName.size_2)),
                ColumnSpacing = UI.Resources.Sizes.Sizes.GetSize(SizeName.size_2),
                VerticalOptions = LayoutOptions.Center,
                ColumnDefinitions = new ColumnDefinitionCollection()
                {
                    new() {Width = GridLength.Auto},
                    new() {Width = GridLength.Star},
                    new() {Width = GridLength.Auto}
                }
            };

            pickerContent.Children.Add(headerLabel);
            
            pickerContent.Children.Add(m_pickedItemLabel);
            Grid.SetColumn(m_pickedItemLabel, 1);

            pickerContent.Children.Add(image);
            Grid.SetColumn(image, 2);

            Content = pickerContent;
        }

        public void SetPickedItemText(string selectedText)
        {
            m_pickedItemLabel.Text = selectedText;
        }

    }
}