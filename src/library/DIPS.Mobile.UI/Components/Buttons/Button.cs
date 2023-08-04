namespace DIPS.Mobile.UI.Components.Buttons
{
    public partial class Button : Microsoft.Maui.Controls.Button
    {
        public Button()
        {
            this.SetAppThemeColor(BackgroundColorProperty, ColorName.color_primary_90);
            this.SetAppThemeColor(TextColorProperty, ColorName.color_system_white);
            Padding = new Thickness(Sizes.GetSize(SizeName.size_3), Sizes.GetSize(SizeName.size_2));
            CornerRadius = Sizes.GetSize(SizeName.size_2);
        }
    }
}