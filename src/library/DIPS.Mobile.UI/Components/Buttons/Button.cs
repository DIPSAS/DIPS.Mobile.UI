using DIPS.Mobile.UI.Resources.Styles.Button;

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

        /// <summary>
        /// This method is overriden to try replicating the CornerRadius of a RoundRectangle if the button is not an icon button (Or if the consumer has not set the CornerRadius themselves)
        /// <br /><br/>
        /// We can't specify a CornerRadius in the Style for text buttons, because the CornerRadius is relative to the Width/Height of a text button
        /// <br /><br/>
        /// If the user increases the font-size to for example 200% (Which increases the button's Width/Height), the CornerRadius value should also increase to match a RoundRectangle
        /// </summary>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (CornerRadius != -1)
                return;

            if (!(Height == 0 || Width == 0))
            {
                CornerRadius = (int)(Math.Min(Width, Height) / 2);
#if __ANDROID__
                // We need to update the foreground ripple if the button's corner radius changes, if not, the ripple will go out of bounds
                if (Handler is ButtonHandler buttonHandler)
                {
                    buttonHandler.UpdateForegroundRipple();
                }
#endif
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

        protected override void OnHandlerChanging(HandlerChangingEventArgs args)
        {
            base.OnHandlerChanging(args);

            if (args.NewHandler is null)
                return;
            
            // If the button is not enabled as the default, we need to update the style to the disabled style
            if(!IsEnabled)
                OnIsEnabledChanged();
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