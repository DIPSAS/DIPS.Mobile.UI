using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Components.ListItems.Options.Icon;
using DIPS.Mobile.UI.Components.Selection;
using SelectionChangedEventArgs = DIPS.Mobile.UI.Components.Selection.SelectionChangedEventArgs;

namespace DIPS.Mobile.UI.Components.ListItems.Extensions;

public partial class RadioButtonListItem : ListItem, ISelectable
{
    private readonly IconOptions m_iconOptions;

    public RadioButtonListItem()
    {
        m_iconOptions = new IconOptions();
        IconOptions = m_iconOptions;
        
        Command = new Command(() =>
        {
            if (HasHaptics)
            {
                VibrationService.SelectionChanged();    
            }
            
            IsSelected = !IsSelected;
        });
        m_iconOptions.Color = ISelectable.s_tintColor;
        OnIsSelectedChanged();
    }

    private void OnIsSelectedChanged()
    {
        Icon = Icons.GetIcon(IsSelected ? IconName.radio_checked_line : IconName.radio_unchecked_line);
        
        SelectedCommand?.Execute(SelectedCommandParameter);
        SelectionChanged?.Invoke(this, new SelectionChangedEventArgs(!IsSelected, IsSelected));
    }
}