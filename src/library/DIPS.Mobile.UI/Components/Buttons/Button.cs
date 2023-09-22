using DIPS.Mobile.UI.Resources.Styles.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Buttons
{
    public partial class Button : Microsoft.Maui.Controls.Button
    {
        private Style? m_buttonStyleBasedOn;
        private Color? m_lastImageTintColor;

        public Button()
        {
            Style = ButtonTypeStyle.PrimaryLarge;
            HorizontalOptions = LayoutOptions.Center;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            
            if (propertyName == IsEnabledProperty.PropertyName)
            {
                OnIsEnabledChanged();
            }
        }

        private void OnIsEnabledChanged()
        {
            if (IsEnabled)
            {
                if (m_buttonStyleBasedOn is not null)
                {
                    Style.BasedOn = m_buttonStyleBasedOn;
                    ImageTintColor = m_lastImageTintColor!;
                }
            }
            else
            {
                if (m_buttonStyleBasedOn is null)
                {
                    m_buttonStyleBasedOn = Style.BasedOn;
                    m_lastImageTintColor = ImageTintColor;
                }
                
                Style.BasedOn = ButtonTypeStyle.Disabled;
                ImageTintColor = Colors.GetColor(ColorName.color_system_black);
            }
        }
        
    }
}