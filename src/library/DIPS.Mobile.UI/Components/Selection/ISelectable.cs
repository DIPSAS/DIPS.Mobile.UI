using System.Windows.Input;
using ImageButton = DIPS.Mobile.UI.Components.Images.ImageButton.ImageButton;

namespace DIPS.Mobile.UI.Components.Selection;

internal interface  ISelectable
{
    internal static Color s_tintColor = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_neutral_90);
    /// <summary>
    /// Determines if the item is selected.
    /// </summary>
    public bool IsSelected { get; set; }
    /// <summary>
    /// Command that will get executed when a item is selected.
    /// </summary>
    public ICommand? SelectedCommand { get; set; }
    /// <summary>
    /// Command parameter for <see cref="SelectedCommand"/>
    /// </summary>
    public object? SelectedCommandParameter { get; set; }

    public event EventHandler<SelectionChangedEventArgs>? SelectionChanged;
}

public class SelectionChangedEventArgs : EventArgs
{
    public bool OldValue { get; }
    public bool NewValue { get; }

    public SelectionChangedEventArgs(bool oldValue, bool newValue)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }
}