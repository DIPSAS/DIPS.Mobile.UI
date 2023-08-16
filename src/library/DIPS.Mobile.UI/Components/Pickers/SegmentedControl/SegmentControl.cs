using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Pickers.SegmentedControl;

public class SegmentControl : BindableObject
{
    /// <summary>
    /// The command to execute when people tap the segment.
    /// </summary>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    
    /// <summary>
    /// The parameter to pass to <see cref="Command"/>. 
    /// </summary>
    public object CommandParameter
    {
        get => (object)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    /// <summary>
    /// The title of the segment that people will see in the <see cref="SegmentedControl"/>.
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// The icon to display alongside the <see cref="Title"/> for people to see in the <see cref="SegmentedControl"/>.
    /// </summary>
    public ImageSource? Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(SegmentControl));

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
        nameof(CommandParameter),
        typeof(object),
        typeof(SegmentControl));

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(SegmentControl));

    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon),
        typeof(ImageSource),
        typeof(SegmentControl));
}