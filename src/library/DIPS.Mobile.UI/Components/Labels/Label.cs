using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;

namespace DIPS.Mobile.UI.Components.Labels
{
    public partial class Label : Microsoft.Maui.Controls.Label
    {
        public Label()
        {
            this.SetAppThemeColor(TextColorProperty, ColorName.color_neutral_90);
            MaxLines = int.MaxValue;
            Style = Styles.GetLabelStyle(LabelStyle.Body300);
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
