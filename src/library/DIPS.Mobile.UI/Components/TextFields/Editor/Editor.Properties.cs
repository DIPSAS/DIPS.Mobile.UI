namespace DIPS.Mobile.UI.Components.TextFields.Editor;

public partial class Editor
{
    /// <summary>
    /// Determines whether the <see cref="Editor"/> should have borders
    /// </summary>
    /// <remarks>
    /// Android: A line below the Entry
    /// iOS: No border
    /// </remarks>
    public bool HasBorder
    {
        get => (bool)GetValue(HasBorderProperty);
        set => SetValue(HasBorderProperty, value);
    }

    /// <summary>
    /// Determines whether all the text in the <see cref="Editor"/> should be selected when user taps the <see cref="Editor"/>
    /// </summary>
    public bool ShouldSelectAllTextOnFocused
    {
        get => (bool)GetValue(ShouldSelectTextOnTappedProperty);
        set => SetValue(ShouldSelectTextOnTappedProperty, value);
    }
    
    public bool ShouldUseDefaultPadding
    {
        get => (bool)GetValue(ShouldUseDefaultPaddingProperty);
        set => SetValue(ShouldUseDefaultPaddingProperty, value);
    }

    public static readonly BindableProperty ShouldUseDefaultPaddingProperty = BindableProperty.Create(
        nameof(ShouldUseDefaultPadding),
        typeof(bool),
        typeof(Editor),
        true);
    
    public static readonly BindableProperty HasBorderProperty = BindableProperty.Create(
        nameof(HasBorder),
        typeof(bool),
        typeof(Editor));
    
    public static readonly BindableProperty ShouldSelectTextOnTappedProperty = BindableProperty.Create(
        nameof(ShouldSelectAllTextOnFocused),
        typeof(bool),
        typeof(Editor));

}