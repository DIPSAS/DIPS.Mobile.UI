using System.Windows.Input;
using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Components.ListItems.Options.Icon;
using DIPS.Mobile.UI.Components.ListItems.Options.InLineContent;
using DIPS.Mobile.UI.Components.ListItems.Options.Title;
using DIPS.Mobile.UI.Components.Selection;
using Colors = Microsoft.Maui.Graphics.Colors;
using SelectionChangedEventArgs = DIPS.Mobile.UI.Components.Selection.SelectionChangedEventArgs;

namespace DIPS.Mobile.UI.Components.ListItems.Extensions;

public partial class CheckmarkListItem : ListItem, ISelectable
{
    private readonly IconOptions m_iconOptions;

    public CheckmarkListItem()
    {
        Command = new Command(() =>
        {
            if (HasHaptics)
            {
                VibrationService.SelectionChanged();    
            }
            
            IsSelected = !IsSelected;
            
            SelectedCommand?.Execute(SelectedCommandParameter);
        });
        m_iconOptions = new IconOptions();
        IconOptions = m_iconOptions;
        
        //Forces the title to take full width
        TitleOptions = new TitleOptions() {Width = GridLength.Star}; 
        InLineContentOptions = new InLineContentOptions() {Width = GridLength.Auto};
        
#if __ANDROID__
        m_iconOptions.Color = ISelectable.s_tintColor;
#elif __IOS__
        Icon = Icons.GetIcon(IconName.check_line);
#endif
        OnIsSelectedChanged();
    }

    private void OnIsSelectedChanged()
    {
#if __ANDROID__
        Icon = Icons.GetIcon(IsSelected ? IconName.checkbox_checked_fill : IconName.checkbox_unchecked_line);
#elif __IOS__
        m_iconOptions.Color = IsSelected ? ISelectable.s_tintColor : Colors.Transparent;
#endif
        
        SelectionChanged?.Invoke(this, new SelectionChangedEventArgs(!IsSelected, IsSelected));
    }
    
}