using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Buttons
{
    public partial class Button : Microsoft.Maui.Controls.Button
    {
        private Style? m_buttonStyleBasedOn;
        private int m_tries;

        public Button()
        {
            Style = ButtonTypeStyle.PrimaryLarge;
            HorizontalOptions = LayoutOptions.Center;
        }

        /// <summary>
        /// This method is overriden to try replicating the CornerRadius of a RoundRectangle if the button is not an icon button (Or if the consumer has not set the CornerRadius themselves)
        /// <br /><br/>
        /// We can't specify a CornerRadius in the Style for text buttons, because the CornerRadius is relative to the Width/Height of a text button
        /// <br /><br/>
        /// If the user increases the font-size to for example 200% (Which increases the button's Width/Height), the CornerRadius value should also increase to match a RoundRectangle
        /// </summary>
        protected override async void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (CornerRadius == -1)
            {
                m_tries = 0;
                
                // The Width/Height can be 0 if the button is inside a Grid that has IsVisible changed, with a * Row/Column definition
                // Most likely a bug in MAUI, because the Width/Height is set on a later frame
                while (Width == 0 || Height == 0)
                {
                    // Safe guard to prevent infinite loop
                    if(m_tries > 50) 
                        return;
                    
                    await Task.Delay(1);
                    m_tries++;
                }
                
                CornerRadius = (int)(Math.Min(Width, Height) / 2);
            }
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