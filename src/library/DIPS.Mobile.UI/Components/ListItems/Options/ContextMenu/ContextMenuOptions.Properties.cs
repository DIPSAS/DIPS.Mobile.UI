using DIPS.Mobile.UI.Components.ContextMenus;

namespace DIPS.Mobile.UI.Components.ListItems.Options.ContextMenu;

public partial class ContextMenuOptions
{
   
    public static readonly BindableProperty ModeProperty = BindableProperty.Create(
        nameof(Mode),
        typeof(ContextMenuEffect.ContextMenuMode),
        typeof(ContextMenuOptions),
        defaultValue: ContextMenuEffect.ContextMenuMode.LongPressed);

    /// <summary>
    /// The mode of the Context Menu
    /// <remarks>Default is 'LongPressed'</remarks>
    /// </summary>
    public ContextMenuEffect.ContextMenuMode Mode
    {
        get => (ContextMenuEffect.ContextMenuMode)GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }
    
}