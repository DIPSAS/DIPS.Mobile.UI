using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Sizes.Sizes;
using Label = DIPS.Mobile.UI.Components.Labels.Label;
using Image = DIPS.Mobile.UI.Components.Images.Image;

namespace DIPS.Mobile.UI.Components.Pickers.Base
{
    /// <summary>
    /// Base class for Pickers
    /// </summary>
    public partial class Picker : Frame
    {
        protected readonly Label PickedItemLabel;

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
            PickedItemLabel = new Label {HorizontalTextAlignment = TextAlignment.End, VerticalTextAlignment = TextAlignment.Center};
            PickedItemLabel.SetAppThemeColor(Microsoft.Maui.Controls.Label.TextColorProperty, ColorName.color_primary_90);

            var image = new Image();
            image.iOSProperties.SystemIconName = "chevron.up.chevron.down";
            image.AndroidProperties.IconResourceName = "mtrl_dropdown_arrow";
            image.SetAppThemeColor(Image.ColorProperty, DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_primary_90),
                DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_primary_90));

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
            
            pickerContent.Children.Add(PickedItemLabel);
            Grid.SetColumn(PickedItemLabel, 1);

            pickerContent.Children.Add(image);
            Grid.SetColumn(image, 2);

            Content = pickerContent;
        }

        public void SetPickedItemText(string selectedText)
        {
            PickedItemLabel.Text = selectedText;
        }

    }
}