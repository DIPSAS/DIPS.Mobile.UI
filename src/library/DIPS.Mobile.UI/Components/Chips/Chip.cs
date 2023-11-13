using DIPS.Mobile.UI.Resources.Styles.Chip;

namespace DIPS.Mobile.UI.Components.Chips;

public partial class Chip : View
{
    private static Style? m_buttonToggleStyle;

    public Chip()
    {
        Style = m_buttonToggleStyle = InputStyle.Current;
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
            m_buttonToggleStyle = Style;

            Style = ToggleStyle.ToggledOn;
        }
    }
}