using DIPS.Mobile.UI.Resources.Styles.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Buttons
{
    public partial class Button : Microsoft.Maui.Controls.Button
    {
        private Style? m_buttonStyleBasedOn;

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
                }
            }
            else
            {
                if (m_buttonStyleBasedOn is null)
                {
                    m_buttonStyleBasedOn = Style.BasedOn;
                }
                
                Style.BasedOn = ButtonTypeStyle.Disabled;
            }
        }
        
    }
}