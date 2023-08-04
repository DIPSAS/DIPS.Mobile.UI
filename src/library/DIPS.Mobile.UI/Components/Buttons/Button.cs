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

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            // TrySetBackgroundColorAlpha();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName.Equals(nameof(BackgroundColor)))
            {
                // TrySetBackgroundColorAlpha();
            }
        }

        private void TrySetBackgroundColorAlpha()
        {
            if (BackgroundColorAlpha != null) //Set background alpha if it specifically set
            {
                var oldBackgroundColor = BackgroundColor;
                BackgroundColor = new Color(oldBackgroundColor.Red, oldBackgroundColor.Green,
                    oldBackgroundColor.Blue,
                    (float)BackgroundColorAlpha);
            }
        }
    }
}