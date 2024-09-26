using DIPS.Mobile.UI.Converters.ValueConverters;
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

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

#if __IOS__
           ResizeIcon((float)height); 
#endif
            // If the CornerRadius has not been set by consumer, set it so it replicates a RoundRectangle
            if(CornerRadius == -1)
                CornerRadius = (int)(Math.Min(Width, Height) / 2);
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
            //Had to add this due to a random app crash when button padding changes.
            if (Handler?.PlatformView == null) return;
            
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