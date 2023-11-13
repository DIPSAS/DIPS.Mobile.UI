namespace DIPS.Mobile.UI.Components.TextFields.Entry;

public partial class Entry
{
   
    /// <summary>
    /// Determines whether the <see cref="Entry"/> should have borders
    /// </summary>
    /// <remarks>
    /// Android: A line below the Entry
    /// iOS: A border around the Entry
    /// </remarks>
    public bool HasBorder
    {
        get => (bool)GetValue(HasBorderProperty);
        set => SetValue(HasBorderProperty, value);
    }

    /// <summary>
    /// Determines whether all the text in the <see cref="Entry"/> should be selected when user taps the <see cref="Entry"/>
    /// </summary>
    public bool ShouldSelectAllTextOnFocused
    {
        get => (bool)GetValue(ShouldSelectTextOnTappedProperty);
        set => SetValue(ShouldSelectTextOnTappedProperty, value);
    }
    
    public static readonly BindableProperty HasBorderProperty = BindableProperty.Create(
        nameof(HasBorder),
        typeof(bool),
        typeof(Entry));
    
    public static readonly BindableProperty ShouldSelectTextOnTappedProperty = BindableProperty.Create(
        nameof(ShouldSelectAllTextOnFocused),
        typeof(bool),
        typeof(Entry));
}