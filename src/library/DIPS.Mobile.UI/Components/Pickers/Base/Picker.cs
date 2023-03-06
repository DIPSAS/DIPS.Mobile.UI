using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Label = DIPS.Mobile.UI.Components.Labels.Label;
using Image = DIPS.Mobile.UI.Components.Images.Image;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;

namespace DIPS.Mobile.UI.Components.Pickers.Base
{
    public abstract partial class Picker : ContentView
    {
        private readonly Label m_pickedItemLabel;

        public Picker()
        {
            HorizontalOptions = LayoutOptions.Fill;
            VerticalOptions = LayoutOptions.Center;
            BackgroundColor = Color.Transparent;

            //Header
            var headerLabel = new Label();
            headerLabel.SetBinding(Label.TextProperty,
                new Binding() {Source = this, Path = nameof(Title)});
            
            //Outer button to make sure its accessible
            var button = new Button(){Command = new Command(Open)};
            button.On<Android>().SetElevation(0);
            button.CornerRadius = 10;
            button.SetAppThemeColor(BackgroundColorProperty, ColorName.color_system_white);

            //Selected item
            m_pickedItemLabel = new Label {HorizontalTextAlignment = TextAlignment.End};
            m_pickedItemLabel.SetAppThemeColor(Xamarin.Forms.Label.TextColorProperty, ColorName.color_primary_90);

            var image = new Image();
            image.iOSProperties.SystemIconName = "chevron.up.chevron.down";
            image.AndroidProperties.IconResourceName = "mtrl_dropdown_arrow";
            image.SetAppThemeColor(Image.ColorProperty, Colors.GetColor(ColorName.color_primary_90),
                Colors.GetColor(ColorName.color_primary_90));

            //Arrange the grid
            var pickerContent = new Grid()
            {
                InputTransparent = true,
                Padding = new Thickness(15, 10), //TODO: Replace with design system margins,
                ColumnSpacing = 10, //TODO: Replace with design system margins,
                VerticalOptions = LayoutOptions.Center,
                ColumnDefinitions = new ColumnDefinitionCollection()
                {
                    new() {Width = GridLength.Auto},
                    new() {Width = GridLength.Star},
                    new() {Width = GridLength.Auto}
                }
            };

            pickerContent.Children.Add(headerLabel, 0, 0);
            pickerContent.Children.Add(m_pickedItemLabel, 1, 0);
            pickerContent.Children.Add(image, 2, 0);
            pickerContent.On<Android>().SetElevation(0);

            var outerGrid = new Grid()
            {
                Children = { button, pickerContent }
            };
            Content = outerGrid;
        }

        public void SetPickedItemText(string selectedText)
        {
            m_pickedItemLabel.Text = selectedText;
        }
    }
}