using System;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;
using Image = DIPS.Mobile.UI.Components.Images.Image;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.CheckBoxes
{
    public partial class CheckBox : Grid
    {
        private readonly Image m_selectedImage;

        public CheckBox()
        {
            Padding = new Thickness(0, 10); //TODO: Use DesignSystem
            
            //Touch
            var gestureRecognizer = new TapGestureRecognizer();
            gestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, new Binding(){Source = this, Path = nameof(Command)});
            GestureRecognizers.Add(gestureRecognizer);

            m_selectedImage = new Image() {VerticalOptions = LayoutOptions.Center};
            m_selectedImage.SetAppThemeColor(Image.ColorProperty,(Device.PlatformServices.RuntimePlatform == Device.Android)? ColorName.color_primary_90:ColorName.color_system_black);

            m_selectedImage.Margin = new Thickness(5); //TODO: Use DesignSystem
            var itemLabel = new Label()
            {
                VerticalTextAlignment = TextAlignment.Center
            };
            itemLabel.SetBinding(Xamarin.Forms.Label.TextProperty, new Binding(){Source=this, Path = nameof(Text)});

            if (Device.PlatformServices.RuntimePlatform == Device.Android)
            {
                ColumnDefinitions = new ColumnDefinitionCollection()
                {
                    new() {Width = GridLength.Star}, new() {Width = GridLength.Auto}
                };
                Children.Add(itemLabel, 0, 0);
                Children.Add(m_selectedImage, 1, 0);
            }
            else
            {
                ColumnDefinitions = new ColumnDefinitionCollection()
                {
                    new() {Width = 30}, new() {Width = GridLength.Star}
                };
                Children.Add(m_selectedImage, 0, 0);
                Children.Add(itemLabel, 1, 0);
            }
            
            SetSelectedState();
        }

        private void GestureRecognizerOnTapped(object sender, EventArgs e)
        {
            
        }

        private static void IsSelectedChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is not CheckBox checkBox) return;
            checkBox.SetSelectedState();
        }

        private void SetSelectedState()
        {
            if (Device.PlatformServices.RuntimePlatform == Device.Android)
            {
                m_selectedImage.AndroidProperties.IconResourceName =
                    IsSelected ? "btn_checkbox_checked_mtrl" : "btn_checkbox_unchecked_mtrl";
                m_selectedImage.SetAppThemeColor(Image.ColorProperty, ColorName.color_primary_90);
            }
            else
            {
                m_selectedImage.iOSProperties.SystemIconName = "checkmark";
                m_selectedImage.SetAppThemeColor(Image.ColorProperty, ColorName.color_system_black);
                m_selectedImage.IsVisible = IsSelected;
            }
        }
    }
}