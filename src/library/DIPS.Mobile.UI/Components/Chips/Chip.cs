using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.Styles.Chip;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Chips;

public partial class Chip : View
{
    private Style? m_buttonToggleStyle;

    public Chip()
    {
        Style = InputStyle.Current;
    }

    public void SendTapped()
    {
        Command?.Execute(CommandParameter);
        Tapped?.Invoke(this, EventArgs.Empty);
    }

    public void SendCloseTapped()
    {
        CloseCommand?.Execute(CloseCommandParameter);
        CloseTapped?.Invoke(this, EventArgs.Empty);
    }
    
    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
            
        if (propertyName == IsToggledProperty.PropertyName)
        {
            OnIsToggledChanged();
        }
    }
    
    private void OnIsToggledChanged()
    {
        if (!IsToggled)
        {
            if (m_buttonToggleStyle is not null)
            {
                Style = m_buttonToggleStyle;
            }
        }
        else
        {
            if (m_buttonToggleStyle is null)
            {
                m_buttonToggleStyle = Style;
            }
                
            Style = ToggleStyle.ToggledOn;
        }
    }
}