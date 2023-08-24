namespace DIPS.Mobile.UI.Components.Labels
{
    public partial class Label : Microsoft.Maui.Controls.Label
    {
        public Label()
        {
            this.SetAppThemeColor(TextColorProperty, ColorName.color_neutral_90);
            MaxLines = int.MaxValue;
        }
    }
}