using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.Chip;
using DIPS.Mobile.UI.Resources.Styles.Label;

namespace DIPS.Mobile.UI.Resources.Styles
{
    public class StylesExtension : IMarkupExtension<Style>
    {
        /// <summary>
        /// The <see cref="ChipStyle"/> to look for.
        /// </summary>
        public ChipStyle Chip { get; set; }
        
        /// <summary>
        /// The <see cref="ButtonStyle"/> to look for.
        /// </summary>
        public ButtonStyle Button { get; set; }
        
        /// <summary>
        /// The <see cref="LabelStyle"/> to look for.
        /// </summary>
        public LabelStyle Label { get; set; }

        public Style ProvideValue(IServiceProvider serviceProvider)
        {
            if (Chip != ChipStyle.None)
            {
                return Styles.GetChipStyle(Chip);
            }

            if(Button != ButtonStyle.None)
            {
                return Styles.GetButtonStyle(Button);
            }

            if (Label != LabelStyle.None)
            {
                return Styles.GetLabelStyle(Label);
            }

            return new Style(typeof(View));
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<Style>).ProvideValue(serviceProvider);
        }
    }
}