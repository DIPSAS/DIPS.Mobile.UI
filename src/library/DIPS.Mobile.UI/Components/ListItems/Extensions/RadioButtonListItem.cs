using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Components.ListItems.Options.Icon;
using DIPS.Mobile.UI.Components.ListItems.Options.InLineContent;
using DIPS.Mobile.UI.Components.ListItems.Options.Title;
using DIPS.Mobile.UI.Components.Selection;
using DIPS.Mobile.UI.Effects.Accessibility;
using SelectionChangedEventArgs = DIPS.Mobile.UI.Components.Selection.SelectionChangedEventArgs;

namespace DIPS.Mobile.UI.Components.ListItems.Extensions;

public partial class RadioButtonListItem : ListItem, ISelectable
{
    private readonly IconOptions m_iconOptions;

    public RadioButtonListItem()
    {
        m_iconOptions = new IconOptions();
        IconOptions = m_iconOptions;
        //Forces the title to take full width
        TitleOptions = new TitleOptions() {Width = GridLength.Star}; 
        InLineContentOptions = new InLineContentOptions() {Width = GridLength.Auto};
        UI.Effects.Accessibility.Accessibility.SetMode(this, Mode.GroupChildren);
        
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
        
        // Update accessibility trait based on selection state
        var trait = IsSelected ? Trait.Selected : Trait.NotSelected;
        UI.Effects.Accessibility.Accessibility.SetTrait(this, trait);
        
        SelectedCommand?.Execute(SelectedCommandParameter);
        SelectionChanged?.Invoke(this, new SelectionChangedEventArgs(!IsSelected, IsSelected));
    }
}