using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;

namespace DIPS.Mobile.UI.Components.Labels
{
    public partial class Label : Microsoft.Maui.Controls.Label
    {
        public static Style DefaultLabelStyle = Styles.GetLabelStyle(LabelStyle.Body300);
        
        public Label()
        {
            this.SetAppThemeColor(TextColorProperty, ColorName.color_text_default);
            MaxLines = int.MaxValue;
            Style = DefaultLabelStyle;
        }
        
        private void OnTextChanged()
        {
            var previousValue = ((Microsoft.Maui.Controls.Label)this).Text;
            ((Microsoft.Maui.Controls.Label)this).Text = this.Text;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (previousValue == null) //This happens due to the way we truncate text by using formatted text in our implementation. It nulls out Text and we have to reset it + invalidate for it to re-truncate.
            {
                this.InvalidateMeasure();
            }
        }
    }
}
