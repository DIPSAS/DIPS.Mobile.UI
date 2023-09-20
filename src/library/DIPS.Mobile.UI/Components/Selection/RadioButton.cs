using DIPS.Mobile.UI.API.Vibration;

namespace DIPS.Mobile.UI.Components.Selection;

public partial class RadioButton : DIPS.Mobile.UI.Components.Images.ImageButton.ImageButton, ISelectable
{
    public RadioButton()
    {
        Command = new Command(() =>
        {
            if (HasHaptics)
            {
                VibrationService.SelectionChanged();    
            }
            
            IsSelected = !IsSelected;
        });
        TintColor = ISelectable.s_tintColor;
        OnIsSelectedChanged();
    }

    private void OnIsSelectedChanged()
    {
        Source = Icons.GetIcon(IsSelected ? IconName.radio_checked_line : IconName.radio_unchecked_line);
        
        SelectedCommand?.Execute(SelectedCommandParameter);
        SelectionChanged?.Invoke(this, new SelectionChangedEventArgs(!IsSelected, IsSelected));
    }
}